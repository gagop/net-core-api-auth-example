using Microsoft.EntityFrameworkCore;
using NetCoreApiExample.Models;

namespace NetCoreApiExample.Infrastructure
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
    }
}
