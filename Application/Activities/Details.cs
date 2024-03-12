// Details.cs

using Domain; // Memanggil Activity
using Persistence; // Memanggil DataContext
using MediatR; // Menggunakan fungsi IRequest
using Application.Core; // Memanggil Result<Activity>

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<Activity>>
        {
            /* Sekarang yang ini akan mengambil parameter karena kita perlu menentukan apa ID
             dari AVTICITY tersebut yang ingin diambil */
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                return Result<Activity>.Success(activity);
            }
        }
    }
}
