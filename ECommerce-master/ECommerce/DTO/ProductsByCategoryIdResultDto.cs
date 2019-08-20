using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;

namespace ECommerce.DTO
{
    public class ProductsByCategoryIdResultDto
    {
        public int CategoryId { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<Product> Products { get; internal set; }
    }
}
