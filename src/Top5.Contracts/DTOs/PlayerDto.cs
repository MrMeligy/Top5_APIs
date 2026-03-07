using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Contracts.DTOs
{
    public class PlayerDto
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string? picUrl { get; set; }
        public string phone { get; set; }
        public Positions position { get; set; }
        public int level { get; set; }
        public bool gender { get; set; }
        public int matchCount { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
        public double rate { get; set; }
        public DateOnly dob { get; set; }
        public Team? team { get; set; }
    }
}
