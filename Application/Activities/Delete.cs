// Delete.cs

using MediatR; // Mengunakan Fungsi "IRequest"
using Persistence; // Memanggil class "DataContext"

namespace Application.Activities
{
    public class Delete
    {
        /* Ini tidak akan mengembalikan apapun karena ini yang akan memperbarui database kita
         Jadi kita akan menyebutnya "Command" */
        public class Command : IRequest
        {
            // Dan kita perlu menyediakan ini dengan parameter ID
            public Guid Id { get; set; }
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
                var activity = await _context.Activities.FindAsync(request.Id);
                // Kita tidak melakukan apa pun pada Delete ini yaitu penanganan kesalahan atau validasi
                /* Aktivities ini ada sekarang dengan perintah "FindAsync", jika tidak ada yang ditemukan, maka akan Null
                 Jadi jika dalam hal ini kita tidak menemukan aktivitas dan aktivitas adalah null, maka kita akan mendapatkan pengecualian */
                
                _context.Remove(activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
