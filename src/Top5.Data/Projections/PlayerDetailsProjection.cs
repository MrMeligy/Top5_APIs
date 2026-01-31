using Top5.Domain.Enums;

namespace Top5.Data.Projections
{
    public class PlayerDetailsProjection
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string? picUrl { get; set; }
        public string nationality { get; set; }
        public Positions position { get; set; }
        public DateOnly dob { get; set; }
        public string phone { get; set; }
        public bool gender { get; set; }    
        public int level { get; set; }

        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
        public int matchCount { get; set; }
        public double rate { get; set; }
    }
}
