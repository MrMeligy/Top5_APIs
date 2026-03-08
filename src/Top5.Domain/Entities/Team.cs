using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class Team
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "Team name is required")]
        [MinLength(4, ErrorMessage = "Team name must be at least 4 characters long")]
        public string name { get; set; }
        public Guid captinId { get; set; }
        public string? picUrl { get; set; }
        public int matchCount { get; set; } = 0;
        public int goals { get; set; } = 0;
        public int goalsAgainest { get; set; } = 0;
        public int wins { get; set; } = 0;
        public int loses { get; set; } = 0;
        public int Rank { get; set; } = 0;
        public int points { get; set; } = 0;
        [JsonIgnore]
        public Player captin { get; set; }
        [JsonIgnore]
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
 