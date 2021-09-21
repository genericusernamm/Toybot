using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Nefarius.DSharpPlus.CommandsNext.Extensions.Hosting.Attributes;
using Nefarius.DSharpPlus.CommandsNext.Extensions.Hosting.Events;

namespace Toybot.Commands
{
    public class PingModule: BaseCommandModule
    {
        [Command("ping")]
        public async Task PingAsync(CommandContext ctx) 
            => await ctx.RespondAsync($"Pong! {ctx.Client.Ping} ms");
            
    }
}