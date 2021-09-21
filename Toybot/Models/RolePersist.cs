namespace Toybot.Models
{
    public class RolePersist
    {
        public int Id { get; set; }
        
        public ulong MemberId { get; set; }
        public ulong RoleId { get; set; }
        public ulong GuildId { get; set; }
    }
}