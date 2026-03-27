using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Entities;
using Top5.Domain.Enums;
using Top5.Domain.Models;

namespace Top5.Contracts.DTOs
{
    public class TeamPlayerDto
    {
        public Guid id { get; set; }
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        public string playerName { get; set; }
        public string teamName { get; set; }
        public string playerPicUrl { get; set; }
        public string teamPicUrl { get; set; }
        public DateTime JoinedOn { get; set; }
        public bool IsLeft { get; set; }
        public DateTime? LeftTime { get; set; }
        public Positions position { get; set; }
        public string phone { get; set; }

    }
}
