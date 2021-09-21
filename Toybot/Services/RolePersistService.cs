using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toybot.Data;
using Toybot.Models;

namespace Toybot.Services
{
    public class RolePersistService: IRolePersistService
    {
        private readonly ApplicationDbContext _context;
        
        public RolePersistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RolePersist[]> GetRolePersistByMemberAsync(ulong guildId, ulong memberId)
        {
            return await _context.RolePersists
                .Where(x => x.GuildId == guildId && x.MemberId == memberId)
                .ToArrayAsync();
        }

        public async Task<RolePersist[]> GetRolePersistByGuildAsync(ulong guildId)
        {
            return await _context.RolePersists
                .Where(x => x.GuildId == guildId)
                .ToArrayAsync();
        }

        public async Task<RolePersist> AddRolePersistAsync(RolePersist rolePersist)
        {
            await _context.AddAsync(rolePersist);
            await _context.SaveChangesAsync();
            return rolePersist;
        }

        public async Task<RolePersist> DeleteRolePersistAsync(RolePersist rolePersist)
        {
            _context.Remove(rolePersist);
            await _context.SaveChangesAsync();
            return rolePersist;
        }
    }
}