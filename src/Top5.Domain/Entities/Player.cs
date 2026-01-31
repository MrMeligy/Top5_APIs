using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Domain.Models
{
    public class Player
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? picUrl { get; set; }
        public string phone { get; set; }
        public Positions position { get; set; }
        public int level { get; set; }
        public DateOnly dob { get; set; }
        
    }
}
