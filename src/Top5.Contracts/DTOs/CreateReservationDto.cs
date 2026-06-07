using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class CreateReservationDto
    {
        public int pitchId { get; set; }
        public bool IsMatchOnApp { get; set; } = false;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
