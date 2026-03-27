using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class CreateMatchPlayerDto
    {
        public Guid matchId { get; set; }
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        public int goals { get; set; }
        public int assists { get; set; }
        public int saves { get; set; }
    }
}
