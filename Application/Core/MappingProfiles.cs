// MappingProfiles.cs

using AutoMapper; // Menggunakan Fungsi "Profile"
using Domain; // Memanggil class "Activity"

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            /* Di sinilah kita kita akan menambahkan profil
             Kemana kita akan pergi dan kemana kita ingin pergi */
            CreateMap<Activity, Activity>();
        }
    }
}
