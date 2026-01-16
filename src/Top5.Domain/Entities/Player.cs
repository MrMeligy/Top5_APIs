using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Domain.Models
{
    public class Player
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? picUrl { get; set; }
        public string phone { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string nationality { get; set; }
        public string position { get; set; }
        public int level { get; set; }
        public bool gender { get; set; }
        public DateOnly dob { get; set; }
        public int goals{ get; set; }
        public int assists{ get; set; }
        public int matches { get; set; }
        public int saves { get; set; }
        public Double rate { get; set; }
    }
}
