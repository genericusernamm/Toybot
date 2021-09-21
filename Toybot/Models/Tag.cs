namespace Toybot.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        
        public string Name { get; set; }
        public string Content { get; set; }
        
        public ulong GuildId { get; set; }
        public ulong AuthorId { get; set; }
    }
}