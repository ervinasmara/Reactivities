// Create.cs

using Domain; // Mengambil Activity
using Persistence; // Mengambil DataContext
using MediatR; // Menggunakan fungsi IRequest
using Application.Core; // Mengambil "Result"
using FluentValidation; // Menggunakan fungsi AbstractValidator

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>> // Jadi kita tidak mengembalikan hasil activiy apa pun dari ini
        {
            public Activity Activity { get; set; } // Inilah yang ingin kita terima sebagai parameter dari API kita
        }

        // Menambahkan validator didalam sini
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // Di sini kita akan membuat aturan untuk apa yang akan kita validasi
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
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
                _context.Activities.Add(request.Activity);

                /* Lalu yang bisa kita lakukan adalah memeriksa hasil penyimpanan perubahan pada database kita */
                /* Jadi jika tidak ada yang ditulis ke database, maka hasilnya akan False, tetapi jika jumlahnya
                 lebih besar dari 0 maka hasilnya akan True */
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Gagal Untuk Create Activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}