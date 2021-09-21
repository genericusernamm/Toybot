using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toybot.Data;
using Toybot.Models;

namespace Toybot.Services
{
    public class RoleConfigService: IRoleConfigService
    {

        private readonly ApplicationDbContext _context;
        public RoleConfigService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoleConfig> GetRoleConfigByTypeAsync(ulong guildId, string type)
        {
            return await _context.RoleConfigs
                .SingleOrDefaultAsync(x => x.GuildId == guildId && x.RoleType == type);
        }

        public async Task<RoleConfig[]> GetRoleConfigByGuildAsync(ulong guildId)
        {
            return await _context.RoleConfigs
                .Where(x => x.GuildId == guildId)
                .ToArrayAsync();
        }

        public async Task<RoleConfig> AddRoleConfigAsync(RoleConfig roleConfig)
        {
            await _context.AddAsync(roleConfig);
            await _context.SaveChangesAsync();

            return roleConfig;
        }

        public async Task<RoleConfig> UpdateRoleConfigAsync(RoleConfig roleConfig)
        {
            _context.Update(roleConfig);
            await _context.SaveChangesAsync();
            return roleConfig;
        }

        public async Task<RoleConfig> DeleteRoleConfigAsync(RoleConfig roleConfig)
        {
            _context.Remove(roleConfig);
            await _context.SaveChangesAsync();
            return roleConfig;
        }
    }
}