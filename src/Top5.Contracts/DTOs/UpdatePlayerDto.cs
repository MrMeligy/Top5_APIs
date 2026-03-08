using System.ComponentModel.DataAnnotations;
using Top5.Domain.Enums;

namespace Top5.Contracts.DTOs
{
    public class UpdatePlayerDto
    {
        [RegularExpression("^01[0125]\\d{8}$", ErrorMessage = "Enter Valid Phone Number")]
        public string phone { get; set; }
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        public string username { get; set; }
        public Positions position { get; set; }
        public int level { get; set; }
        public string? picUrl { get; set; }
    }
}
