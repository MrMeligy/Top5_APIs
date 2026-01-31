using Top5.Domain.Enums;
namespace Top5.Contracts.DTOs
{
    public class MatchDto
    {
        public Guid id { get; set; }
        public Guid homeTeamId { get; set; }
        public string homeTeamName { get; set; }
        public string homePic { get; set; }
        public Guid awayTeamId { get; set; }
        public string awayTeamName { get; set; }
        public string awayPic { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public string matchFormat { get; set; }
        public bool isComptitve { get; set; }
        public DateTime kickOff { get; set; }
        public MatchStatues statues { get; set; }
    }
}
