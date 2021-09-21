

namespace Toybot.Models
{
    public class RoleConfig
    {
        public int Id { get; set; }
        public string RoleType { get; set; }
        public ulong RoleId { get; set; }
        public ulong GuildId { get; set; }
    }
}