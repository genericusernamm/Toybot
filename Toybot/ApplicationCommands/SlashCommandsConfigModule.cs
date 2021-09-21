using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Nefarius.DSharpPlus.Extensions.Hosting.Attributes;
using Toybot.CheckAttributes;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.ApplicationCommands
{
    // We are setting the module lifespan to scoped. This means a new instance is created by the service container
    // every time a command is used, and is needed to inject scoped services into the module.
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    [SlashCommandGroup("config", "Change or view configuration for this guild.")]
    public class SlashCommandsConfigModule: ApplicationCommandModule
    {

        [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
        [SlashCommandGroup("roles", "Configure roles for this guild.")]
        public class RoleSubgroup : ApplicationCommandModule
        {
            private readonly IRoleConfigService _roleConfig;
        
            public RoleSubgroup(IRoleConfigService roleConfig)
            {
                _roleConfig = roleConfig;
            }
            
            public enum RoleChoice
            {
                [ChoiceName("Admin")]
                Admin,
                [ChoiceName("Mod")]
                Mod,
                [ChoiceName("Helper")]
                Helper,
                [ChoiceName("Regular")]
                Regular,
                [ChoiceName("Gravel")]
                Gravel,
                [ChoiceName("Muted")]
                Muted
            }

            [SlashCommand("view", "Shows the currently set configurable roles for this guild.")]
            public async Task ViewRolesAsync(InteractionContext ctx)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

                var roles = await _roleConfig.GetRoleConfigByGuildAsync(ctx.Guild.Id);

                var em = new DiscordEmbedBuilder()
                    .WithTitle("Roles for this guild")
                    .WithColor(DiscordColor.Blue);

                if (roles is null || roles.Length == 0)
                    em.WithDescription(
                        "There are no roles set up for this guild. Please use /config role set to configure this.");
                else
                {
                    foreach (RoleConfig roleConfig in roles)
                        em.AddField(roleConfig.RoleType, ctx.Guild.GetRole(roleConfig.RoleId).Mention);
                }


                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .AddEmbed(em));


            }
            
            [RequireRoleType("Admin")]
            [SlashCommand("set", "Set a configurable role.")]
            public async Task SetRoleConfigAsync(InteractionContext ctx,
                [Option("toSet", "Configurable role to set")]
                RoleChoice roleChoice,
                [Option("role", "The Discord role that the config wil be set to")]
                DiscordRole role)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AsEphemeral(true));
                
                var existingConfig = 
                    await _roleConfig.GetRoleConfigByTypeAsync(ctx.Guild.Id, roleChoice.GetName());

                if (existingConfig is not null)
                {
                    existingConfig.RoleId = role.Id;
                    await _roleConfig.UpdateRoleConfigAsync(existingConfig);
                    
                }
                else
                {
                    var newConfig = new RoleConfig()
                    {
                        GuildId = ctx.Guild.Id,
                        RoleType = roleChoice.GetName(),
                        RoleId = role.Id
                    };
                    await _roleConfig.AddRoleConfigAsync(newConfig);
                }
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Set {roleChoice.GetName()} role to {role.Mention}"));
                
            }
            
        }
    }
}