using System;

namespace Toybot.Models
{
    public class RolePersist
    {
        public int Id { get; set; }
        
        //Info about the rolepersist (MemberId is who it is applied to).
        public ulong MemberId { get; set; }
        public ulong RoleId { get; set; }
        public ulong GuildId { get; set; }
        
        //The DateTime at which the rolepersist should be deleted.
        public DateTime Expires { get; set; }
    }
}