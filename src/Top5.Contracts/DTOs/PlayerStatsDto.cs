using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class PlayerStatsDto
    {
        public Guid playerId { get; set; }
        public Guid? teamId { get; set; }
        public string name { get; set; }
        public string? teamName { get; set; }
        public string? logo { get; set; }
        public string? picUrl { get; set; }
        public DateOnly dob { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }

    }
}
