// ApplicationServiceExtensions.cs

using Persistence;
using Application.Core;
using FluentValidation;
using Application.Activities;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    // Ketika kita membuat metode Ekstensi, maka kita perlu memastikan class kita adalah static
    public static class ApplicationServiceExtensions
    {
        // Dan kemudian kita membuat metode  ekstensi itu sendiri, dan itu akan disebut public
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        /* Sekarang parameter pertama dari metode ekstensi ini adalah hal yang akan kita perluas daftar*/
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("KoneksiKePostgreSQL"));
            });

            // Menambahkan CORS Policy
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"); // localhost:3000 adalah URL milik client (frontend)
                });
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            /* Kita ingin perilaku ini terjadi secara otomatis karena kita tidak akan
             menulis banyak kode untuk melakukannya sendiri secara manual */
            services.AddFluentValidationAutoValidation();
            /* Kemudian kita akan menentukan hanya salah satu penanganan kami yang berisi beberapa validasi,
             logika, atau turunan dari validator abstrak
            Jadi kita akan menentukan Create sebagai nama kelas didalam services yang kita cari */
            services.AddValidatorsFromAssemblyContaining<Create>();

            return services;
        }
    }
}
