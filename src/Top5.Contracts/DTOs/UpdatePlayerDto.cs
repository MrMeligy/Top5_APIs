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
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? password { get; set; }
        public Positions position { get; set; }
        public int level { get; set; }
        public string? picUrl { get; set; }
    }
}
