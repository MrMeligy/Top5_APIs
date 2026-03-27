using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class MatchPlayers
    {
        public Guid id { get; set; }
        public Guid matchId { get; set; }
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public double rate { get; set; }
        [JsonIgnore]
        public Match match { get; set; }
        [JsonIgnore]
        public Team team{ get; set; }
        [JsonIgnore]
        public Player player{ get; set; }
    }
}
