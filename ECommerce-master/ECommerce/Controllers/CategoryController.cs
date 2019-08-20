using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        [Route("/kategori/{id}")]
        public IActionResult Index(int id)
        {
            Category category = new Category();

            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                category = eCommerceContext.Categories.SingleOrDefault(a => a.Id == id);
            }

            ViewData["Title"] = category.Name;

            return View(category);
        }
    }
}