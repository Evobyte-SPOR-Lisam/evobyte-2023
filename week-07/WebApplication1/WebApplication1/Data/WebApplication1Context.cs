using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class WebApplication1Context : IdentityDbContext
    {
        public DbSet<Models.Profile> Profile { get; set; } = default!;

        public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
            : base(options)
        { }
    }
}
