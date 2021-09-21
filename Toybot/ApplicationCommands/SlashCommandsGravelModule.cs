using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Toybot.CheckAttributes;
using Toybot.Services;

namespace Toybot.ApplicationCommands
{
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    public class SlashCommandsGravelModule: ApplicationCommandModule
    {
        private readonly IRoleConfigService _roleConfig;
        public SlashCommandsGravelModule(IRoleConfigService roleConfig)
        {
            _roleConfig = roleConfig;
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
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Added {ctx.TargetMember.Mention} to Gravel."));
            }
        }
    }
}