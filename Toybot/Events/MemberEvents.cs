using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Nefarius.DSharpPlus.Extensions.Hosting.Attributes;
using Nefarius.DSharpPlus.Extensions.Hosting.Events;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.Events
{
    [DiscordGuildMemberEventsSubscriber]
    public class MemberEvents: IDiscordGuildMemberEventsSubscriber
    {
        private readonly IRolePersistService _rolePersistService;
        
        public MemberEvents(IRolePersistService rolePersistService)
        {
            _rolePersistService = rolePersistService;
        }

        public async Task DiscordOnGuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            var persists =
               await _rolePersistService.GetRolePersistByMemberAsync(args.Guild.Id, args.Member.Id);

            foreach (RolePersist persist in persists)
            {
                await args.Member.GrantRoleAsync(args.Guild.GetRole(persist.RoleId));
            }
            
            
        }

        public Task DiscordOnGuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public Task DiscordOnGuildMemberUpdated(DiscordClient sender, GuildMemberUpdateEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public Task DiscordOnGuildMembersChunked(DiscordClient sender, GuildMembersChunkEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}