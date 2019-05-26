using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApiExample.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
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