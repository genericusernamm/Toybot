using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Nefarius.DSharpPlus.Extensions.Hosting.Attributes;
using Nefarius.DSharpPlus.Extensions.Hosting.Events;

namespace Toybot.Events
{
    [DiscordMessageEventsSubscriber]
    internal class MemberEvents: IDiscordMessageEventsSubscriber
    {
        public Task DiscordOnMessageCreated(DiscordClient sender, MessageCreateEventArgs args)
        {
            if (args.Message.MessageType == MessageType.GuildMemberJoin)
            {
                args.Message.CreateReactionAsync(DiscordEmoji.FromName(sender, ":tada:"));
            }
            return Task.CompletedTask;
        }

        public Task DiscordOnMessageAcknowledged(DiscordClient sender, MessageAcknowledgeEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task DiscordOnMessageUpdated(DiscordClient sender, MessageUpdateEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task DiscordOnMessageDeleted(DiscordClient sender, MessageDeleteEventArgs args)
        {
            return Task.CompletedTask;
        }

        public Task DiscordOnMessagesBulkDeleted(DiscordClient sender, MessageBulkDeleteEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}