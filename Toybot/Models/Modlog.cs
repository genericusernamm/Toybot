using System;

namespace Toybot.Models
{
    public class Modlog
    {
        public int Id { get; set; }
        
        //Guild specific case number.
        public int CaseNumber { get; set; }
        
        //Mod log type e.g. Role Persist, Ban, Kick
        public string Type { get; set; }
        
        //Creation time of the log.
        public DateTime DateTime { get; set; }
        
        public ulong ModeratorId { get; set; }
        public ulong MemberId { get; set; }
        public ulong GuildId { get; set; }
        
        //Optional fields
        public string Reason { get; set; }
        public TimeSpan Duration { get; set; }
        public ulong RoleId { get; set; } //For role persists
    }
}