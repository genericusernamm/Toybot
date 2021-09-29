using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Toybot.CheckAttributes;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.ApplicationCommands
{
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    public class ContextMenuGravelModule: ApplicationCommandModule
    {
        private readonly IRoleConfigService _roleConfig;
        private readonly IRolePersistService _rolePersist;
        public ContextMenuGravelModule(IRoleConfigService roleConfig, IRolePersistService rolePersist)
        {
            _roleConfig = roleConfig;
            _rolePersist = rolePersist;
        }

        [ContextMenu(ApplicationCommandType.UserContextMenu, "Gravel User")]
        [RequireRoleTypeContextMenu("Mod")]
        public async Task GravelUserMenu(ContextMenuContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            
            var gravelRole = await _roleConfig.GetRoleConfigByTypeAsync(ctx.Guild.Id, "Gravel");

            

            if (gravelRole is null)
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent("Please assign a Gravel role with /config role set Gravel"));
            else
            {
                
                await ctx.TargetMember.GrantRoleAsync(ctx.Guild.GetRole(gravelRole.RoleId), "Gravel");
                
                var rolePersistList = await _rolePersist
                    .GetRolePersistByMemberAsync(ctx.Guild.Id, ctx.TargetMember.Id);
                var existing = rolePersistList
                    .SingleOrDefault(x => x.RoleId == gravelRole.RoleId);

                if (existing is null)
                {
                    await _rolePersist.AddRolePersistAsync(new RolePersist()
                    {
                        GuildId = ctx.Guild.Id,
                        MemberId = ctx.TargetMember.Id,
                        RoleId = gravelRole.RoleId
                    });

                }
                
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Added {ctx.TargetMember.Mention} to Gravel."));
            }
        }
        
    }
}