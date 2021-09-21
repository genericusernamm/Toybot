using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toybot.Data;
using Toybot.Models;

namespace Toybot.Services
{
    public class TagService: ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Tag> GetTagByNameAsync(ulong guildId, string name)
        {
            return await _context.Tags.SingleOrDefaultAsync(x => x.GuildId == guildId && x.Name == name);
        }

        public async Task<Tag> CreateTagAsync(Tag tag)
        {
            await _context.AddAsync(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> UpdateTagAsync(Tag tag)
        {
            _context.Update(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> DeleteTagAsync(Tag tag)
        {
            _context.Remove(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag[]> GetTagsByGuildAsync(ulong guildId)
        {
            return await _context.Tags.Where(x => x.GuildId == guildId)
                .ToArrayAsync();
        }
    }
}