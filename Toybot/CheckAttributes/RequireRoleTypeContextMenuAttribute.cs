using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.CheckAttributes
{
    public class RequireRoleTypeContextMenuAttribute: ContextMenuCheckBaseAttribute
    {
        public string RoleType;
        public DiscordRole Role;

        public RequireRoleTypeContextMenuAttribute(string roleType)
        {
            RoleType = roleType;
        }
        public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
        {
            var factory = ctx.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = factory.CreateScope())
            {
                
                var roleConfigService = scope.ServiceProvider.GetRequiredService<IRoleConfigService>();
                
                var roleConfig = await roleConfigService.GetRoleConfigByTypeAsync(ctx.Guild.Id, RoleType);

                if (roleConfig is null)
                    return false;

                Role = ctx.Guild.GetRole(roleConfig.RoleId);
                if (ctx.Member.Roles.Contains(Role))
                    return true;
            
                return false;
            }
            
        }
        
    }
}