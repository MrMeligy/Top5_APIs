using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Enums;

namespace Top5.Contracts.DTOs
{
    public class PlayerStatesRankDto
    {
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPicture { get; set; }
        public Positions PlayerPosition { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
        public int matchCount { get; set; }
        public DateTime ModifiedOn { get; set; }
        
    }
}
