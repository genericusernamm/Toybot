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
        public Task SlashCommandsOnContextMenuErrored(SlashCommandsExtension sender, ContextMenuErrorEventArgs args)
        {
            throw new System.NotImplementedException();
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
                    if (check is RequireRoleAttribute att)
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