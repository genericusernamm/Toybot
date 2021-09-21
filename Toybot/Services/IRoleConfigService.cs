using System.Threading.Tasks;
using Toybot.Models;

namespace Toybot.Services
{
    public interface IRoleConfigService
    {
        Task<RoleConfig> GetRoleConfigByTypeAsync(ulong guildId, string type);
        Task<RoleConfig[]> GetRoleConfigByGuildAsync(ulong guildId);
        Task<RoleConfig> AddRoleConfigAsync(RoleConfig roleConfig);
        Task<RoleConfig> UpdateRoleConfigAsync(RoleConfig roleConfig);  
        Task<RoleConfig> DeleteRoleConfigAsync(RoleConfig roleConfig);
    }
}