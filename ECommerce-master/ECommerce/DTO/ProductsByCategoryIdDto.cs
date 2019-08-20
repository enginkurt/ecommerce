using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DTO
{
    public class ProductsByCategoryIdDto
    {
        public ProductsByCategoryIdDto()
        {
            PageCount = 1;
            PageSize = 10;
            CurrentPage = 1;
        }
        public int CategoryId { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
