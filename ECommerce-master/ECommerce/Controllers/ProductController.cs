using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        [Route("/urun/{id}")]
        public IActionResult Index(int id)
        {
            Models.Product product;

            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                product = eCommerceContext.Products.Include(c => c.Category).Include(x=>x.State).SingleOrDefault(x => x.Id == id);
            }
            return View(product);
        }

        //[Route("/guncelle/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewModels.ProductUpdateViewModel model;

            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                model = new ViewModels.ProductUpdateViewModel
                {
                    Product = eCommerceContext.Products.SingleOrDefault(x => x.Id == id),
                    Categories = eCommerceContext.Categories.ToList()
                };
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Models.Product product)
        {
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                //Models.Product updatedProduct = eCommerceContext.Products.SingleOrDefault(x => x.Id == product.Id);
                //updatedProduct.Name = product.Name;
                //updatedProduct.Description = product.Description;
                eCommerceContext.Update(product);
                eCommerceContext.SaveChanges();
            }
            TempData.Add("message", "Product was succesfully updated");
            return RedirectToAction("Index", new { id = product.Id });
        }

    }
}