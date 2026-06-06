using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public int PitchId { get; set; }
        public Guid PlayerId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsMatchOnApp { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string playerName { get; set; }
        public string playerPicUrl { get; set; }
        public string pitchName { get; set; }
        public string pitchPicUrl { get; set;
    }
}
