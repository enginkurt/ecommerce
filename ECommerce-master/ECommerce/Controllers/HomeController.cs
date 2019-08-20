using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "HOŞGELDİNİZ!";
            return View();
        }

        public IActionResult Help()
        {
            ViewData["Title"] = "Yardım Masası!";
            return View();
        }
    }
}
