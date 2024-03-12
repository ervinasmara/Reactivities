// Delete.cs

using MediatR; // Mengunakan Fungsi "IRequest"
using Application.Core; // Memanggil class "Result"
using Persistence; // Memanggil class "DataContext"

namespace Application.Activities
{
    public class Delete
    {
        /* Ini tidak akan mengembalikan apapun karena ini yang akan memperbarui database kita
         Jadi kita akan menyebutnya "Command" */
        public class Command : IRequest<Result<Unit>>
        {
            // Dan kita perlu menyediakan ini dengan parameter ID
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                /* Sekarang dalam hal ini, kita perlu memeriksa dengan jelas untuk melihat apakah aktivitas
                 ini nol karena temuan kita di FindAsync, metode akan mengembalikan null jika tidak menemukannya di database */
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null) return null;

                _context.Remove(activity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Gagal untuk menghapus Activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
