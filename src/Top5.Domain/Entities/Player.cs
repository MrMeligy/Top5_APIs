using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Top5.Domain.Entities;
using Top5.Domain.Enums;

namespace Top5.Domain.Models
{
    public class Player
    {
        public Guid id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4,ErrorMessage = "Username must be at least 4 characters long")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", 
            ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string password { get; set; }
        public string? picUrl { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^01[0125]\\d{8}$", ErrorMessage ="Enter Valid Phone Number")]
        public string phone { get; set; }
        public Positions position { get; set; }
        public int level { get; set; }
        public DateOnly dob { get; set; }
        
    }
}
