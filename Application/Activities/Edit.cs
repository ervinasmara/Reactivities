// Edit.cs

using Domain; // Memanggil class "Activity"
using Persistence; // Memanggil "DataContext"
using MediatR; // Menggunakan Fungsi "IRequest"
using AutoMapper; // Menggunakan Fungsi "IMapper"
using Application.Core; // Memanggil class "Result"
using FluentValidation; // Menggunakan fungsi "AbstractValidator"

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
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
            private readonly IMapper _mapper;
            private readonly DataContext _context;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                // Kita dapat menguji untuk melihat apakah activity tersebut nol (null/kosong)
                if (activity == null) return null;

                // Untuk Memperbarui / Mengedit / PUT
                _mapper.Map(request.Activity, activity);
                /* Dan kemudian AutoMapper akan mengambil semua properti yang ada di dalam
                Activity dan memperbarui properti activity */

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Gagal untuk edit Activity");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
