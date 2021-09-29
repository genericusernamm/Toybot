using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Toybot.ApplicationCommands
{
    public class SlashCommandsPingModule: ApplicationCommandModule
    {
        [SlashCommand("ping", "Play ping pong.")]
        public async Task PingAsync(InteractionContext ctx) =>
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent($":ping_pong: Pong! {ctx.Client.Ping} ms"));
    }
}