using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Domain.Entities
{
    public class Pitch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Picture { get; set; }
        public double DayHourPrice { get; set; }
        public double NightHourPrice { get; set; }

    }
}
