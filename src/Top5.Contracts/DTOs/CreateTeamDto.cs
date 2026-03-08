using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class CreateTeamDto
    {
        [Required(ErrorMessage = "Team name is required")]
        [MinLength(4, ErrorMessage = "Team name must be at least 4 characters long")]
        public string name { get; set; }
        public string? picUrl { get; set; }
    }
}
