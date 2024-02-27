// Details.cs

using Domain; // Memanggil Activity
using MediatR; // Menggunakan fungsi IRequest
using Persistence; // Memanggil DataContext

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            /* Sekarang yang ini akan mengambil parameter karena kita perlu menentukan apa ID
             dari AVTICITY tersebut yang ingin diambil */
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities.FindAsync(request.Id);
            }
        }
    }
}
