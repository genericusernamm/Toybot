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
        public DbSet<RoleConfig> RoleConfigs { get; set; }
        public DbSet<GuildConfig> GuildConfigs { get; set; }
        public DbSet<RolePersist> RolePersists { get; set; }
        public DbSet<Modlog> Modlogs { get; set; }
    }
}