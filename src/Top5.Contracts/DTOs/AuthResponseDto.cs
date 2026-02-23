using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class AuthResponseDto
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }

    }
}
