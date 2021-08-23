using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        //initialize field from the parameter context which gets the schema of the user id name password hash and safe.
        private readonly DataContext _context;
        private readonly ITokenService _tokenservice;

        public AccountController(DataContext context, ITokenService tokenservice)
        {
            _tokenservice = tokenservice;
            _context = context;
        }
        //http uses API enpoint to register the user information via HTTPpost 
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("User Name is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key

            };
            //saving to the DB user in the data base 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // http post return the user with name , passwordhash and salt.
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenservice.CreateToken(user)

            };



        }

        //Login Method  
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {
            var user = await _context.Users.
            SingleOrDefaultAsync(x => x.UserName == logindto.Username);

            if (user == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA512((user.PasswordSalt));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid Password");

            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };

        }

        private async Task<bool> UserExists(string Username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == Username.ToLower());


        }
    }
}