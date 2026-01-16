using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Domain.Entities
{
    public class Match
    {
        public Guid id { get; set; }
        public Guid homeTeamId { get; set; }
        public Guid awayTeamId { get; set; }
        public int homeScore { get; set; } = 0;
        public int awayScore { get; set; } = 0;
        public string city { get; set; }
        public string country { get; set; }
        public string pitch { get; set; }
        public DateTime kickOff { get; set; }
        public int statues { get; set; } = 0;
        public Team homeTeam { get; set; }
        public Team awayTeam { get; set; }

    }
}
