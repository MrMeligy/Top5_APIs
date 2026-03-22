using System;
using System.Collections.Generic;
using System.Text;
using Top5.Domain.Enums;

namespace Top5.Contracts.DTOs
{
    public class CreateMatchDto
    {
        public Guid homeTeamId { get; set; }
        public Guid awayTeamId { get; set; }
        public string matchFormat { get; set; }
        public bool isComptitve { get; set; }
        public DateTime kickOff { get; set; }
        public DateTime endTime { get; set; }
    }
}
