// Program.cs

using Persistence; // Memanggil DataContext.cs?
using API.Middleware; // Memanggil class "ExceptionMiddleware"
using API.Extensions; // Menggunakan Fungsi "AddApplicationServices"
using Microsoft.EntityFrameworkCore; // Menggunakan "Npgsql" (membuat koneksi ke database PostgreSQL)
using Microsoft.AspNetCore.Identity; // Menggunakan fungsi "UserManager"
using Domain; // Memanggil class "AppUser"
using Microsoft.AspNetCore.Authorization; // Menggunakan fungsi "AuthorizationPolicyBuilder"
using Microsoft.AspNetCore.Mvc.Authorization; // Menggunakan fungsi "AuthorizeFilter"

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Pemesanan middleware kita menjadi penting dan jika kita meletakkannya diatas Authorization, itu akan berhasil
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Dan kemudian di dalam sini kita akan mencoba dan membuat database kita
// Dan jika perintah itu gagal, maka kita akan mendapatkan pengecualian
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    // Migrate = Ini akan menerapkan semua perintah yang tertunda migrasi untuk context ke database
    // Hal ini akan membuat database jika belum ada
    await context.Database.MigrateAsync();
    /* Karena kita sudah menggunakan kode "async" kita juga harus menggunakannya untuk perintah migrasi,
    menambahkan "await" dan menambahkan "Async" di "Migrate" agar tidak error */
    await Seed.SeedData(context, userManager); // Karena di Seed.cs kita menggunakan "async" maka kita harus menambahkan "await" di kode baris Seed ini
}
// Di catch ini kita tidak akan berbuat banyak, tapi kita akan mendapatkan akses ke logger
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    // Kemudian kita bisa mengambil errornya
    logger.LogError(ex, "Terjadi kesalahan selama migrasi");
}

app.Run();