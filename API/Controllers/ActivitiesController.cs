// ActivitiesController.cs

using Domain; // Untuk memanggil class Activity
using Microsoft.AspNetCore.Mvc; // Untuk memanggil [HTTPGet dan lain lain]
using Microsoft.EntityFrameworkCore; // Memanggil fungsi "ToListAsync"
using Persistence; // Untuk memanggil DataContext

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController // Artinya setelah penambahan BaseApiController bisa dikatakan ini sudah memiliki atribut API
    {
        private readonly DataContext _context;

        public ActivitiesController(DataContext context) // Parameternya yaitu DataContext dan akan disebut context
        {
            // this.context = context; 
            /* Sebenarnya dari sononya sudah dibuat menggunakan "this", tapi para Developer tidak menyukai itu 
            maka dari itulah mereka menggunakan garis bawah "_" */
            _context = context;
        }

        [HttpGet] //api/activities
        /* Dan ini akan mengembalikan suatu jenis hasil tindakan (ActionResult)
        Dan kita dapat menentukan  hal yang akan dimasukkan kembali ke dalam BodyResponse dan yang akan kembali 
        adalah "List Activity" 
        Dan kemudian kita akan mengatakan "GetActivities" sebagai nama metode ini */
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            // Dari sini kita dapat mengatakan Activities return, await, dan context, lalu kita dapat menggunakan ToListAsync
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] //api/activities/:id
        /* Sebuah ActionResult yang akan mengembalikan suatu jenis "Activity"
         Dan yang ini hanya akan disebut "GetActivity" 
        Dan kita akan meneruskan GUID ID, yang akan kita dapatkan dari parameter rute di sini */
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            /* Dan kita akan menggunakan metode asinkron yang ditentukan dari kerangka entitas
            dan ditentukan bahwa kita menginginkan "Activity"  dengan ID yang diminta */
            return await _context.Activities.FindAsync(id);
        }


    }
}
