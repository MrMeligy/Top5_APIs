using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public int PitchId { get; set; }
        public Guid PlayerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsMatchOnApp { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public Pitch Pitch { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
    }
}
