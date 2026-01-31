using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Enums;

namespace Top5.Domain.Entities
{
    public class Match
    {
        public Guid id { get; set; }
        public Guid homeTeamId { get; set; }
        public Guid awayTeamId { get; set; }
        public int homeScore { get; set; } = 0;
        public int awayScore { get; set; } = 0;
        public DateTime kickOff { get; set; }
        public MatchStatues statues { get; set; } = MatchStatues.Approved;
        public string matchFormat { get; set; }
        public bool isComptitve { get; set; }
        [JsonIgnore]
        public Team homeTeam { get; set; }
        [JsonIgnore]
        public Team awayTeam { get; set; }

    }
}
