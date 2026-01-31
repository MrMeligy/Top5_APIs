using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class Team
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid captinId { get; set; }
        public string? picUrl { get; set; }
        public int matchCount { get; set; }
        public int goals { get; set; }
        public int goalsAgainest { get; set; }
        public int wins { get; set; }
        public int loses { get; set; }
        public int Rank { get; set; }
        public int points { get; set; } = 0;
        [JsonIgnore]
        public Player captin { get; set; }
        [JsonIgnore]
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
 