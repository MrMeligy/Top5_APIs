using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Top5.Domain.Entities;
using Top5.Domain.Models;

namespace Top5.Contracts.DTOs
{
    public class MatchPlayerDto
    {
        public Guid id { get; set; }
        public Guid matchId { get; set; }
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
        public double rate { get; set; }
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public DateTime kickOff { get; set; }
        public string pitch { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public string playerName { get; set; }
        public string teamName { get; set; }
        public string? picUrl { get; set; }
        public DateOnly dob { get; set; }
    }
}
