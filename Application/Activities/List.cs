// List.cs

using Persistence; // Memanggil DataContext
using MediatR; // Menggunakan fungsi IRequest
using Domain; // Memanggil Activity untuk List
using Application.Core; // Menggunakan fungsi Result
using Microsoft.EntityFrameworkCore; // Menggunakan fungsi ToListAsync

namespace Application.Activities
{
    public class List
    {
        /* Daripada hanya mengembalikan daftar aktivitas, kita akan mengembalikan objek hasil */
        public class Query : IRequest<Result<List<Activity>>>
        {
        /* Kita tidak memerlukan parameter tambahan untuk meneruskan ke query*/
        }

        public class Handler : IRequestHandler<Query, Result<List<Activity>>>
        // Setelah membuat class "Handler" kita akan membuat interface
        {
            private readonly DataContext _context;

            /* Jika kita ingin mengembalikan List(daftar) kegiatan, maka kita perlu mendapatkannya didatabase kita
            Jadi kita akan Injection(menyuntikkan) DataContext kita kesini, tetapi itu tidak akan menjadi "public" */
            public Handler(DataContext context) // Memanggil DataContext dan menyebutnya sebagai context
            {
                _context = context;
            }

            /* Sekarang ini menciptakan bagi kita metode Handler yang mengembalikan Task(tugas) List(daftar) Activities didatabase
            Dan karena kita mengembalikan Task(tugas), berarti kita perlu menjadikan metodi ini asinkron */
            public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
            {
                // Kita dapat menentukan Success dan kita dapat membungkus panggilan ke database dengan tanda kurung
                return Result<List<Activity>>.Success(await _context.Activities.ToListAsync(cancellationToken));
            }
        }
    }
}
