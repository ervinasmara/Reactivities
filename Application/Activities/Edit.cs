// Edit.cs

using AutoMapper; // Menggunakan Fungsi "IMapper"
using Domain; // Memanggil class "Activity"
using MediatR; // Menggunakan Fungsi "IRequest"
using Persistence; // Memanggil "DataContext"

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _context;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                /* Jadi apa yang perlu kita lakukan, kita harus pergi dan mendapatkan Activity yang ada di database
                 baris ke-16 dan kemudian memperbarui setiap bidang di dalam Activity itu dengan Activity yang sudah kita lewati */
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                // Untuk Memperbarui / Mengedit / PUT
                _mapper.Map(request.Activity, activity);
                /* Dan kemudian AutoMapper akan mengambil semua properti yang ada di dalam
                Activity dan memperbarui properti activity */


                await _context.SaveChangesAsync();
            }
        }
    }
}
