using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        //initialize field from the parameter context which gets the schema of the user id name password hash and safe.
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }
        //http uses API enpoint to register the user information via HTTPpost 
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key

            };
            //saving to the DB user in the data base 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // http post return the user with name , passwordhash and salt.
            return user;
        }
    }
}