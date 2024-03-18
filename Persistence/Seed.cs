// Seed.cs

using Domain;
using Microsoft.AspNetCore.Identity; // Memanggil fungsi "UserManager"

namespace Persistence
{
    public class Seed
    /* Biasanya ketika kita menggunakan class, kita akan mengatakan
    dan itu akan memberi kita instance dari class */
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        /* Tapi kalau kita punya metode static, kita tinggal bilang Seed */
        {   /* Jadi kita menggunakan "!", lalu kita akan mengakses userManager kita
            dan kita mendapat akses ke pengguna kita */
            if (!userManager.Users.Any()) // untuk memeriksa apakah kita memiliki User? Jika tidak maka akan dibuatkan
            {
                var users = new List<AppUser>
                {
                    new AppUser{DisplayName = "Bob", UserName = "bob", Email = "bob@test.com"},
                    new AppUser{DisplayName = "Tom", UserName = "tom", Email = "tom@test.com"},
                    new AppUser{DisplayName = "Jane", UserName = "jane", Email = "jane@test.com"}
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (context.Activities.Any()) return;
            /* Kemudian di baris pertama ini akan memeriksa database kita untuk
             melihat "Apakah kita sudah memiliki data ditabel 'Activities'?" */
            /* Jika ya, kode ini tidak akan menanamkan lebih banyak data didalamnya,
             dan akan kembali kemudian menghentikan eksekusinya */
            /* Jika tidak ada data sama sekali di dalam tabel 'Activities', maka kode ini akan
             membuat daftar data yang baru untuk tabel 'Activities' kita.*/

            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new Activity
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Activity
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Activity
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Activity
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Activity
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Activity
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Activity
                {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Activity
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };

            /* Jika menyangkut query dan filter dan semua hal itu, maka kita punya metodenya untuk
             menambahkan rentang Activities, menambahkan rentang asinkron, dan itu akan memanggil Activities yang
            kita panggil di awal metode (baris ke-22), dan itu akan menambah rentangnya activites
            pada tahap ini (baris ke-123) ke dalam Memory */
            /* Ini tidak berpengaruh terhadap apapun pada database saat ini.
             Semua yang dilakukannya, ia menyimpannya ke dalam memori, kemudian dibaris berikutnya
            akan menyimpan perubahan tersebut (baris ke-124) ke dalam basis data */
            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}