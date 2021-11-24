using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController

    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> getSecret()
        {
            return "Secret Text";
        }


        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var userfound = _context.Users.Find(-1);
            if (userfound == null) return NotFound();
            return Ok(userfound);
        }


        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var userfind = _context.Users.Find(-1);
            var thingtoreturn = userfind.ToString();
            return thingtoreturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(" this was not a good request");
        }

    }
}