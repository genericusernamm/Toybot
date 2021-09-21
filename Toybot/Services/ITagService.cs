using System.Threading.Tasks;
using Toybot.Models;

namespace Toybot.Services
{
    public interface ITagService
    {
        Task<Tag> GetTagByNameAsync(ulong guildId, string name);
        Task<Tag> CreateTagAsync(Tag tag);
        Task<Tag> UpdateTagAsync(Tag tag);
        Task<Tag[]> GetTagsByGuildAsync(ulong guildId);
        Task<Tag> DeleteTagAsync(Tag tag);
    }
}