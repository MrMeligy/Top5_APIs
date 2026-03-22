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
        public bool IsHomeCaptinUpdated { get; set; } = false;
        public bool IsAwayCaptinUpdated { get; set; } = false;
        public DateTime kickOff { get; set; }
        public DateTime endTime { get; set; }
        public MatchStatues statues { get; set; } = MatchStatues.Pending;
        public string matchFormat { get; set; } = "5 vs 5";
        public bool isComptitve { get; set; }
        [JsonIgnore]
        public Team homeTeam { get; set; }
        [JsonIgnore]
        public Team awayTeam { get; set; }

    }
}
