using System;
using System.Collections.Generic;
using System.Text;

namespace Top5.Contracts.DTOs
{
    public class PaginationDto
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
