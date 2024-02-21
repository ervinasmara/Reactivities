// Program.cs

using Microsoft.EntityFrameworkCore; // Menggunakan "Npgsql" (membuat koneksi ke database PostgreSQL)
using Persistence; // Memanggil DataContext.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dan kita akan memberitahukan tentang class apa yang akan kita gunakan untuk metode kita (DataContext)
// Kemudian didalam kurung, kita menambahkan "opt", dan didalam "opt" ini  kita akan menentukan apa yang kita inginkan
// Yaitu kita ingin membuat koneksi dengan database PostgreSQL
builder.Services.AddDbContext<DataContext>(opt=>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("KoneksiKePostgreSQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Dan kemudian di dalam sini kita akan mencoba dan membuat database kita
// Dan jika perintah itu gagal, maka kita akan mendapatkan pengecualian
try
{
    var context = services.GetRequiredService<DataContext>();
    // Migrate = Ini akan menerapkan semua perintah yang tertunda migrasi untuk context ke database
    // Hal ini akan membuat database jika belum ada
    await context.Database.MigrateAsync();
    /* Karena kita sudah menggunakan kode "async" di baris ke-49 kita juga harus menggunakannya untuk perintah migrasi,
    menambahkan "await" dan menambahkan "Async" di "Migrate" agar tidak error */
    await Seed.SeedData(context); // Karena di Seed.cs kita menggunakan "async" maka kita harus menambahkan "await" di kode baris Seed ini
}
// Di catch ini kita tidak akan berbuat banyak, tapi kita akan mendapatkan akses ke logger
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    // Kemudian kita bisa mengambil errornya
    logger.LogError(ex, "Terjadi kesalahan selama migrasi");
}

app.Run();
