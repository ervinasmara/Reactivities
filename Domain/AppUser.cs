// AppUser.cs

using Microsoft.AspNetCore.Identity; // Menggunakan fungsi "IdentityUser"

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}