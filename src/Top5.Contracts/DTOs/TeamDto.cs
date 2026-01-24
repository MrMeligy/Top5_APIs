namespace Top5.Contracts.DTOs
{
    public class TeamDto
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
        public string pitch { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public int cityRank { get; set; }
        public int countryRank { get; set; }
        public int points { get; set; }
        public string captinName { get; set; }
        public string captinPhone { get; set; }
    }
}
