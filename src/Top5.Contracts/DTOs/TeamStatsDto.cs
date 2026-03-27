using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class TeamStatsDto
    {
        public Guid? teamId { get; set; }
        public Guid? matchId { get; set; }
        public int goals { get; set; } = 0;
        public int assists { get; set; } = 0;
    }
}
