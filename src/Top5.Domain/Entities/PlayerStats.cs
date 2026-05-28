using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Top5.Domain.Models;

namespace Top5.Domain.Entities
{
    public class PlayerStats
    {
        [Key]
        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }
        public int goals { get; set; } = 0;
        public int assists { get; set; } = 0;
        public int saves { get; set; }  = 0;
        public int matchCount { get; set; } = 0;
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public Player player { get; set; }
    }
}
