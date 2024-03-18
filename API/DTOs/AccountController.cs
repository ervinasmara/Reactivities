// AccountController.cs

using API.Services; // Memanggil class "TokenService"
using Domain; // Memanggil class "AppUser"
using Microsoft.AspNetCore.Authorization; // Menggunakan fungsi "AllowAnonymous"
using Microsoft.AspNetCore.Identity; // Menggunakan fungsi "UserManager"
using Microsoft.AspNetCore.Mvc; // Menggunakan fungsi "AnyAsync"
using Microsoft.EntityFrameworkCore; // Menggunakan fungsi "ControllerBase"
using System.Security.Claims; // Menggunakan fungsi "FindFirstValue"

namespace API.DTOs
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email); // Mengambil pengguna dari database

            if(user == null) Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password); // Memeriksa kata sandi yang disinkronkan

            if(result)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Pemeriksaan username supaya berbeda dengan yang lain
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Username sudah dipakai");
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email sudah dipakai");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email)); // Dan ini akan mendapatkan klaim email dari token yang kita berikan ke server API

            return CreateUserObject(user);
        }
        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
        }
    }
}