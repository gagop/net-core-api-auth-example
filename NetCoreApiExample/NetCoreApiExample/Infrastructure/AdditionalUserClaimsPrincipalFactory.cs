using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetCoreApiExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApiExample.Infrastructure
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }


    }
}
