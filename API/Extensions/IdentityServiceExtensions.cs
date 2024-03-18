// IdentityServiceExtensions.cs

using API.Services; // Memanggil class "TokenService"
using Domain; // Memanggil class "DataContext"
using Microsoft.AspNetCore.Authentication.JwtBearer; // Menggunakan fungsi "JwtBearerDefaults"
using Microsoft.IdentityModel.Tokens; // Menggunakan fungsi "TokenValidationParameters"
using Persistence; // Memanggil class "AppUser"
using System.Text; // Menggunakan fungsi "Encoding"

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services,
            IConfiguration config)
        {
            /* AddIdentityCore hanya menambah dan mengkonfigurasi sistem identitas untuk tipe pengguna yang ditentukan */
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true; // Artinya email harus berbeda dengan pengguna yang lain
            })
                .AddEntityFrameworkStores<DataContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); // sesuaikan TokenKey ini dengan appsettings.Development.js tadi
            /* Namun untuk saat ini, kita hanya akan memastikan bahwa kita menggunakan kunci yang sama dengan yang kita gunakan
             untuk menandatanganinya agar API kita dapat menggunakan kunci ini untuk mendeskripsikannya dan memastikan itu adalah
            token credentials (tanda tangan) yang valid */


            // Skema yang akan digunakan untuk mengautentikasi di sini adalah JwtBearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    // Parameter validasi token
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Ini penting karena ini akan memvalidasi bahwa token adalah token yang valid
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<TokenService>();

            return services;
        }
    }
}
