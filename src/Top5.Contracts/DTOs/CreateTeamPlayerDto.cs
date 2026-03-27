using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class CreateTeamPlayerDto
    {
        public Guid playerId { get; set; }
        public Guid teamId { get; set; }
        public DateTime JoinedOn { get; set; } = DateTime.Now;
        public bool IsLeft { get; set; } = false;
        public DateTime? LeftTime { get; set; }
    }
}
