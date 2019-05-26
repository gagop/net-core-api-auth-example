using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreApiExample.Infrastructure;
using NetCoreApiExample.Models;
using System.Threading.Tasks;

namespace NetCoreApiExample.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {

        private readonly CustomDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        
        [HttpGet("public-data")]
        public string PublicTest()
        {
            return "This data is public";
        }

        [Authorize]
        [HttpGet("secret-data")]
        public string SecretData()
        {
            return "This data is secret";
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("secret-data-for-admin")]
        public string SecretDataForAdmin()
        {
            return "This data is secret and only for admins";
        }
    }
}