using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Controllers
{
    public class AjaxController : Controller
    {
        //MVP design pattern incele projede bu uygulanıyor
        //performans iyileştirme         
        private static readonly AjaxMethod AjaxMethod = new AjaxMethod();

        [Route("/api")]
        public JsonResult Handle()
        {
            DTO.AjaxResponseDto ajaxResponse = new DTO.AjaxResponseDto();
            string json = HttpContext.Request.Form["JSON"].ToString();
            DTO.AjaxRequestDto ajaxRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.AjaxRequestDto>(json);
            if (ajaxRequest.Method == "SaveProduct")
            {
                AjaxMethod.SaveProduct(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "ProductsByCategoryId")
            {
                ajaxResponse.Dynamic = AjaxMethod.ProductsByCategoryId(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "RemoveProduct")
            {
                ajaxResponse.Dynamic = AjaxMethod.RemoveProduct(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "SaveContact")
            {
                AjaxMethod.SaveContact(ajaxRequest.Json);
            }
            return new JsonResult(ajaxResponse);
        }
    }

    public class AjaxMethod
    {
        public void SaveProduct(string json)
        {
            DTO.ProductSaveDto productSave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductSaveDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Products.Add(new Models.Product()
                {
                    Name = productSave.ProductName,
                    Description = productSave.ProductDescription,
                    StateId = (int)Enums.State.Active,
                    CategoryId = productSave.CategoryId,
                    CreateDate = DateTime.UtcNow,
                });
                eCommerceContext.SaveChanges();
            }
        }
        public DTO.ProductsByCategoryIdResultDto ProductsByCategoryId(string json)
        {
            DTO.ProductsByCategoryIdResultDto result;
            DTO.ProductsByCategoryIdDto productsByCategoryId = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductsByCategoryIdDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                List<Models.Product> products = eCommerceContext.Products.Where(x => x.CategoryId == productsByCategoryId.CategoryId).ToList();
                result = new DTO.ProductsByCategoryIdResultDto
                {
                    Products = products.Skip((productsByCategoryId.CurrentPage - 1) * productsByCategoryId.PageSize).Take(productsByCategoryId.PageSize).ToList(),
                    PageSize = productsByCategoryId.PageSize,
                    CategoryId = productsByCategoryId.CategoryId,
                    CurrentPage = productsByCategoryId.CurrentPage,
                    PageCount = (int)Math.Ceiling(products.Count / (double)productsByCategoryId.PageSize),
                };
            }

            return result;
        }
        public bool RemoveProduct(string json)
        {
            bool result = false;
            DTO.ProductRemoveDto productRemove = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductRemoveDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                Models.Product product = eCommerceContext.Products.SingleOrDefault(x => x.Id == productRemove.ProductId);
                if (product != null)
                {
                    eCommerceContext.Remove(product);
                    eCommerceContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }
        public void SaveContact(string json)
        {
            DTO.ContactSaveDto contactSave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ContactSaveDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Contacts.Add(new Models.Contact
                {
                    EMail = contactSave.EMail,
                    Message = contactSave.Message,
                    Name = contactSave.Name
                });
                eCommerceContext.SaveChanges();
            }
        }

    }
}