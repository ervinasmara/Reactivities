// List.cs

using Domain; // Memanggil Activity untuk List
using MediatR; // Menggunakan fungsi IRequest
using Microsoft.EntityFrameworkCore; // Menggunakan fungsi ToListAsync
using Persistence; // Memanggil DataContext

namespace Application.Activities
{
    public class List
    {
        // Jadi di dalam kelas (baris ke-10) kita akan membuat kelas lain untuk query kita
        /* Dan kita memberi tahu IRequest ini bahwa kita ingin meminta objek "List" yang akan dikembalikan dari query "Activity" */
        public class Query : IRequest<List<Activity>> // Dan ini akan berasal dari atau menggunakan IRequest dari Mediator
        /* Kami meneruskan class Query sebagai tipe pertama disini, kemudian sebagai tipe kedua yang akan menjadi List aktifitas*/
        {
        /* Kita tidak memerlukan parameter tambahan untuk meneruskan ke query*/
           
        }

        public class Handler : IRequestHandler<Query, List<Activity>>
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
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                /* Dan di dalam sini sangat sederhana, yang akan kita lakukan hanyalah melakukan persis seperti apa yang kita lakukan di API Controller */
                return await _context.Activities.ToListAsync();
            }
        }
    }
}
