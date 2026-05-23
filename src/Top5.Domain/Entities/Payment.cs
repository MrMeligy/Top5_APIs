using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Enums;

namespace Top5.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public double PaymentAmount { get; set; }
        public double RemainingAmount { get; set; }
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
