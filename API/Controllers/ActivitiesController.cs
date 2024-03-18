// ActivitiesController.cs

using MediatR; // Memanggil IMediator
using Domain; // Untuk memanggil class Activity
using Persistence; // Untuk memanggil DataContext
using Application.Activities; // Memanggil class List & Details
using Microsoft.EntityFrameworkCore; // Memanggil fungsi "ToListAsync"
using Microsoft.AspNetCore.Mvc; // Untuk memanggil [HTTPGet dan lain lain]
using Microsoft.AspNetCore.Authorization; // Menggunakan fungsi "Authorize"

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController // Artinya setelah penambahan BaseApiController bisa dikatakan ini sudah memiliki atribut API
    {
        [HttpGet] //api/activities
        /* Dan mari kita kembalikan hasil tindakan di sini sehingga kita tidak perlu
        menentukan informasi tipe apapun, lalu kita dapat mengembalikan HandleResult */
        public async Task<IActionResult> GetActivities(CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new List.Query(), ct));
        }

        [HttpGet("{id}")] //api/activities/:id
        /* Sebuah ActionResult yang akan mengembalikan suatu jenis "Activity"
         Dan yang ini hanya akan disebut "GetActivity"
        Dan kita akan meneruskan GUID ID, yang akan kita dapatkan dari parameter rute di sini */
        public async Task<ActionResult> GetActivity(Guid id, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }, ct));
        }

        [HttpPost]
        /* "IActionResult" = kita tidak benar-benar mengembalikan apa pun dari ini
         Kita tidak mengembalikan objek aktifitas
        Jadi yang akan kita lakukan adalah kita hanya akan return (kembali)
        Dalam hal ini, kita akan  mengembalikan hasil tindakan (ActionResult) */
        public async Task<IActionResult> CreateActivity(Activity activity, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }, ct));
            // Penginisialan objek untuk mengatakan Activity sama dengan activity
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity, CancellationToken ct)
        {
            activity.Id = id;

            return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }, ct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }, ct));
        }
    }
}
