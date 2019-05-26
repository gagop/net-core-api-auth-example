using Microsoft.AspNetCore.Identity;
using NetCoreApiExample.Infrastructure;
using NetCoreApiExample.Models;
using System.Threading.Tasks;

namespace NetCoreApiExample.Repositories
{
    public class UserRepository
    {
        private CustomDbContext _context;
        private UserManager<AppUser> _userManager;

        public UserRepository(CustomDbContext context)
        {
            _context = context;
        }


        public async Task<AppUser> Create(string firstName, string lastName, string email, string userName, string password)
        {
            var appUser = new AppUser { Email = email, UserName = userName, IndexNumber = "s1234" };
            var identityResult = await _userManager.CreateAsync(appUser, password);

            _context.Users.Add(appUser);
            return appUser;
        }
    }
}
