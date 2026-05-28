using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Domain.Entities
{
    public class PlayerStats
    {
        public Guid PlayerId { get; set; }
        public int goals { get; set; } = 0;
        public int assists { get; set; } = 0;
        public int saves { get; set; }  = 0;
        public int matchCount { get; set; } = 0;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
