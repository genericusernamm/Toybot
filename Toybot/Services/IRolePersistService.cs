using System.Threading.Tasks;
using Toybot.Models;

namespace Toybot.Services
{
    public interface IRolePersistService
    {
        Task<RolePersist[]> GetRolePersistByMemberAsync(ulong guildId, ulong memberId);
        Task<RolePersist[]> GetRolePersistByGuildAsync(ulong guildId);
        Task<RolePersist> AddRolePersistAsync(RolePersist rolePersist);
        Task<RolePersist> DeleteRolePersistAsync(RolePersist rolePersist);
    }
}