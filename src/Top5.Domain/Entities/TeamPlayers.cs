using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class TeamPlayers
    {
        public Guid id {  get; set; }
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        [JsonIgnore]
        public Player player { get; set; }
        [JsonIgnore]
        public Team team { get; set; }

    }
}
