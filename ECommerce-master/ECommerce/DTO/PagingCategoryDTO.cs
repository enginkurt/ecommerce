using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DTO
{
    public class PagingCategoryDTO
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentCategory { get; set; }
        public int CurrentPage { get; set; }
    }
}
