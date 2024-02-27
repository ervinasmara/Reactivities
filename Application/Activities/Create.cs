// Create.cs

using Domain; // Mengambil Activity
using MediatR; // Menggunakan fungsi IRequest
using Persistence; // Mengambil DataContext

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        /* Sekarang secara teknis, Command tidak mengembalikan apapun
         Dan ini merupakan perbedaan mendasar antara Command dan Query
        Query = mengembalikan data, sedangkan Command tidak */
        {
            public Activity Activity { get; set; } // Inilah yang ingin kita terima sebagai parameter dari API kita
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}