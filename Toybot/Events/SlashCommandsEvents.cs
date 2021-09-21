using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Nefarius.DSharpPlus.SlashCommands.Extensions.Hosting.Attributes;
using Nefarius.DSharpPlus.SlashCommands.Extensions.Hosting.Events;
using Toybot.CheckAttributes;

namespace Toybot.Events
{
    [DiscordSlashCommandsEventsSubscriber]
    public class SlashCommandsEvents: IDiscordSlashCommandsEventsSubscriber
    {
        public async Task SlashCommandsOnContextMenuErrored(SlashCommandsExtension sender, ContextMenuErrorEventArgs args)
        {
            if (args.Exception is ContextMenuExecutionChecksFailedException chex)
            {
                foreach (var check in chex.FailedChecks)
                    if (check is RequireRoleTypeContextMenuAttribute requireRoleTypeAttribute)
                        await args.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
                            new DiscordInteractionResponseBuilder()
                                .WithContent($"You must have the role {requireRoleTypeAttribute.Role.Mention} to use this context menu action.")
                                .AsEphemeral(true));
            }
        }

        public Task SlashCommandsOnContextMenuExecuted(SlashCommandsExtension sender, ContextMenuExecutedEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public async Task SlashCommandsOnSlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs args)
        {
            if (args.Exception is SlashExecutionChecksFailedException slex)
            {
                foreach (var check in slex.FailedChecks)
                    if (check is RequireRoleTypeAttribute att)
                        await args.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
                            new DiscordInteractionResponseBuilder()
                                .WithContent($"You must have the role {att.Role.Mention} to use this command.")
                                .AsEphemeral(true));
            }
        }

        public Task SlashCommandsOnSlashCommandExecuted(SlashCommandsExtension sender, SlashCommandExecutedEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}