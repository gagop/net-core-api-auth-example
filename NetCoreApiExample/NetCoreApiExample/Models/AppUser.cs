using Microsoft.AspNetCore.Identity;

namespace NetCoreApiExample.Models
{
    public class AppUser : IdentityUser
    {
        public string IndexNumber { get; set; }
    }
}
