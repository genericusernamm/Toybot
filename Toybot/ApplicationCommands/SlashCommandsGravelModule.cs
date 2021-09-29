using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Toybot.HelperClasses;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.ApplicationCommands
{
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    public class SlashCommandsGravelModule: ApplicationCommandModule
    {
        private readonly IRoleConfigService _roleConfig;
        private readonly IRolePersistService _rolePersist;
        public SlashCommandsGravelModule(IRoleConfigService roleConfig, IRolePersistService rolePersist)
        {
            _roleConfig = roleConfig;
            _rolePersist = rolePersist;
        }

        [SlashCommand("gravel", "Gravels a user")]
        public async Task GravelCommandAsync(InteractionContext ctx, 
            [Option("user", "The user to gravel")]DiscordUser user,
            [Option("reason", "The reason to be displayed in modlogs.")]string reason,
            [Option("duration", "The duration to apply the role persist for.")]string duration 
                = default)
        {

            var member = user as DiscordMember;
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var gravelRole = await _roleConfig.GetRoleConfigByTypeAsync(ctx.Guild.Id, "Gravel");

            if (gravelRole is null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent("There is no Gravel role set up for this guild."));
                return;
            }

            await member.GrantRoleAsync(ctx.Guild.GetRole(gravelRole.RoleId));

            var memberPersists = await _rolePersist
                .GetRolePersistByMemberAsync(ctx.Guild.Id, user.Id);

            var existingPersist = memberPersists.SingleOrDefault(x
                => x.RoleId == gravelRole.RoleId);

            if (existingPersist is not null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent("That user is already gravelled."));
                return;
            }

            var newPersist = new RolePersist()
            {
                GuildId = ctx.Guild.Id,
                MemberId = user.Id,
                RoleId = gravelRole.RoleId,
            };

            try
            {
                var timeSpan = TimeSpan.ParseExact(duration, TimespanFormat.AllFormats, CultureInfo.InvariantCulture);
                newPersist.Expires = DateTime.UtcNow + timeSpan;
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Gravelled user {user.Mention} until {newPersist.Expires}."));
            }
            catch (FormatException exception)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Gravelled user {user.Mention} without a duration."));
            }

            await _rolePersist.AddRolePersistAsync(newPersist);

        }

        [SlashCommand("ungravel", "Removes a user from gravel.")]
        public async Task UngravelAsync(InteractionContext ctx, DiscordUser user)
        {
            
        }

    }
}
