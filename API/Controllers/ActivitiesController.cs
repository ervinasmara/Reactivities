// ActivitiesController.cs

using Application.Activities; // Memanggil class List & Details
using Domain; // Untuk memanggil class Activity
using MediatR; // Memanggil IMediator
using Microsoft.AspNetCore.Mvc; // Untuk memanggil [HTTPGet dan lain lain]
using Microsoft.EntityFrameworkCore; // Memanggil fungsi "ToListAsync"
using Persistence; // Untuk memanggil DataContext

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController // Artinya setelah penambahan BaseApiController bisa dikatakan ini sudah memiliki atribut API
    {
        [HttpGet] //api/activities
        /* Dan ini akan mengembalikan suatu jenis hasil tindakan (ActionResult)
        Dan kita dapat menentukan  hal yang akan dimasukkan kembali ke dalam BodyResponse dan yang akan kembali 
        adalah "List Activity" 
        Dan kemudian kita akan mengatakan "GetActivities" sebagai nama metode ini */
        public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
        {
            return await Mediator.Send(new List.Query(), ct);
        }

        [HttpGet("{id}")] //api/activities/:id
        /* Sebuah ActionResult yang akan mengembalikan suatu jenis "Activity"
         Dan yang ini hanya akan disebut "GetActivity" 
        Dan kita akan meneruskan GUID ID, yang akan kita dapatkan dari parameter rute di sini */
        public async Task<ActionResult<Activity>> GetActivity(Guid id, CancellationToken ct)
        {
            return await Mediator.Send(new Details.Query{Id = id}, ct);
        }

        [HttpPost]
        /* "IActionResult" = kita tidak benar-benar mengembalikan apa pun dari ini 
         Kita tidak mengembalikan objek aktifitas 
        Jadi yang akan kita lakukan adalah kita hanya akan return (kembali)
        Dalam hal ini, kita akan  mengembalikan hasil tindakan (ActionResult) */
        public async Task<IActionResult> CreateActivity(Activity activity, CancellationToken ct)
        {
            await Mediator.Send(new Create.Command { Activity = activity }, ct); 
            // Penginisialan objek untuk mengatakan Activity sama dengan activity
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity, CancellationToken ct)
        {
            activity.Id = id;

            await Mediator.Send(new Edit.Command { Activity = activity }, ct);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken ct)
        {
            await Mediator.Send(new Delete.Command { Id = id }, ct);
            return Ok();
        }
    }
}
