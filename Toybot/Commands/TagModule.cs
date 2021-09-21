using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Toybot.Services;

namespace Toybot.Commands
{
    [ModuleLifespan(ModuleLifespan.Transient)]
    public class TagModule: BaseCommandModule
    {
        private readonly ITagService _tag;
        public TagModule(ITagService tag)
        {
            _tag = tag;
        }
        
        [Command("tag")]
        public async Task GetTagByNameAsync(CommandContext ctx, string name)
        {

            var tag = await _tag.GetTagByNameAsync(ctx.Guild.Id, name);
            await ctx.RespondAsync(tag.Content);

        }
    }
}