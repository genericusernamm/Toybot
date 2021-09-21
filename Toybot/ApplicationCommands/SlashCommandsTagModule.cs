using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using Toybot.CheckAttributes;
using Toybot.Models;
using Toybot.Services;

namespace Toybot.ApplicationCommands
{
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    [SlashCommandGroup("tag", "A group related to tags.")]
    public class SlashCommandsTagModule: ApplicationCommandModule
    {
        private readonly ITagService _tag;

        public SlashCommandsTagModule(ITagService tag)
        {
            _tag = tag;
        }

        [RequireRole("Regular")]
        [SlashCommand("get", "Gets a tag by name.")]
        public async Task GetTagByNameAsync(InteractionContext ctx, 
            [Option("name", "The name of the tag")] string name)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            
            var tag = await _tag.GetTagByNameAsync(ctx.Guild.Id, name);

            if (tag is not null)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(tag.Content));
            }
            else
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                        .WithContent($"A tag with the name `{name}` does not exist.")
                        .AsEphemeral(true));
            }
        }

        [RequireRole("Regular")]
        [SlashCommand("list", "Gets all tags for this guild.")]
        public async Task GetTagsAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            
            var tags = await _tag.GetTagsByGuildAsync(ctx.Guild.Id);

            if (tags is not null && tags.Length != 0)
            {
                var em = new DiscordEmbedBuilder()
                    .WithTitle($"{ctx.Guild.Name}'s Tags [{tags.Length}]")
                    .WithDescription(String.Join(", ", tags.Select(x => x.Name)))
                    .WithFooter("Use /tag `name` to view a tag.");
                
                await ctx.EditResponseAsync(
                    new DiscordWebhookBuilder()
                        .AddEmbed(em));
            }
            else
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"There are no tags registered for this guild.")
                    .AsEphemeral(true));
            }
        }
        
        [RequireRole("Helper")]
        [SlashCommand("set", "Adds or edits a tag.")]
        public async Task TagSetAsync(InteractionContext ctx, 
            [Option("name", "The name of the tag")] string name, 
            [Option("content", "The content of the tag")] string content)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .AsEphemeral(true));
            
            var tag = await _tag.GetTagByNameAsync(ctx.Guild.Id, name);

            if (tag is not null)
            {
                var interactivity = ctx.Client.GetInteractivity();

                var confirmButton = new DiscordButtonComponent(ButtonStyle.Success, "ok", "OK");
                var cancelButton = new DiscordButtonComponent(ButtonStyle.Danger, "cancel", "Cancel");

                var confirmationMessage = await ctx.FollowUpAsync(
                    new DiscordFollowupMessageBuilder()
                    .WithContent($"A tag with the name `{name}` already exists. Would you like to overwrite it?")
                    .AddComponents(confirmButton, cancelButton));

                var result = await interactivity.WaitForButtonAsync(confirmationMessage);
                if (result.Result.Interaction.Data.CustomId == "ok")
                {
                    tag.Content = content;
                    await _tag.UpdateTagAsync(tag);
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder()
                        .WithContent($"Updated tag `{name}`."));
                }
                else
                {
                    await ctx.EditResponseAsync(new DiscordWebhookBuilder()
                        .WithContent("Cancelled tag update."));
                }
                    
                
            }
            else
            {
                var newTag = new Tag()
                {
                    GuildId = ctx.Guild.Id,
                    AuthorId = ctx.User.Id,
                    Name = name,
                    Content = content
                };
                await _tag.CreateTagAsync(newTag);
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Created tag `{name}`."));
                
            }
            
        }
        
        [RequireRole("Mod")] 
        [SlashCommand("delete", "Deletes a tag")]
        public async Task RemoveTagAsync(InteractionContext ctx,
            [Option("name", "The name of the tag to remove")]
            string name)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource, 
                new DiscordInteractionResponseBuilder()
                    .AsEphemeral(true));

            var tag = await _tag.GetTagByNameAsync(ctx.Guild.Id, name);

            if (tag is not null)
            {
                await _tag.DeleteTagAsync(tag);
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"Deleted tag `{name}`."));
            }
            else
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                    .WithContent($"A tag named `{name}` does not exist for this guild"));
            }
        }
        
        
    }
}