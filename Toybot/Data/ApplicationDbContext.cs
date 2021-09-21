using Microsoft.EntityFrameworkCore;
using Toybot.Models;

namespace Toybot.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Tag> Tags { get; set; }
    }
}