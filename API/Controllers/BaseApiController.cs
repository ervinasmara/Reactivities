// BaseApiController.cs

using MediatR; // Memanggil IMediator
using Microsoft.AspNetCore.Mvc; // Memanggil ApiController & ControllerBase

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        // Kita akan membuat bidang private(pribadi) untuk data
        private IMediator _mediator;

        /* Dan kita akan melakukan beberapa pekerjaan untuk berjaga-jaga di sini,
         karena kalau-kalau kita sudah memiliki "_mediator", karena kita berada di dalam "BaseApiController(Pengontrol API Dasar)"
        Dan jika kami memiliki "_mediator" tersedia untuk alasan apa pun, maka kita akan mencoba menggunakannya lagi
        Jika tidak, maka kita akan pergi dan mendapatkan layanan Mediator dan menggunakannya sebagai gantinya
        Jadi kita perlu membuat ini tersedia untuk setiap kelas turunan
        Jadi kita akan memiliki sarana yang dilindungi (protected), yang berarti dapat menggunakannya
        di dalam class ini atau kelas turunan apapun */

        protected IMediator Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();
            // Mengembalikan _mediator dan menggunakan ??= yaitu untuk siapapun yang berhak menggunakan ini
    }
}
