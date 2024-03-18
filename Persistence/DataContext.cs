// DataContext.cs

using Domain; // Memanggil class "Activity"
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Memanggil fungsi "IdentityDbContext"
using Microsoft.EntityFrameworkCore; // Memberikan "DbContext"

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            // Biarkan kosong
        }

        public DbSet<Activity> Activities { get; set; }
        // Dan kita membuat "Activity" sebagia nama entity, kemudian "Activities" sebagai nama tabel di database
    }
}
