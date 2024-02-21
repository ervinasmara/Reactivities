// DataContext.cs

using Domain; // Memanggil class "Activity"
using Microsoft.EntityFrameworkCore; // Memberikan "DbContext"

namespace Persistence
{
    // DbContext ini mewakili sesi sesi dengan database dan dapat digunakan untuk query (bertanya) dan save instansi entity kita
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            // Biarkan kosong
        }

        public DbSet<Activity> Activities { get; set; }
        // Dan kita membuat "Activity" sebagia nama entity, kemudian "Activities" sebagai nama tabel di database
    }
}
