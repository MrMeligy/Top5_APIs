using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Enums;

namespace Top5.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ReservationStatusEnum ReservationStatus { get; set; }
    }
}
