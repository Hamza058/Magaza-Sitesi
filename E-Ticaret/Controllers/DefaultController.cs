using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Ticaret.Controllers
{
    public class DefaultController : Controller
    {
        ProductManager pm = new ProductManager(new EFProductDal());
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        public IActionResult Index()
        {
            var values = pm.TGetList();
            ViewBag.category = new List<string>();
            foreach (var item in pcm.TGetList())
            {
                ViewBag.category.Add(item.ProductCategoryName);
            }
            return View(values);
        }
        public IActionResult Single(int id = 1)
        {
            ViewBag.Colors = new SelectList(new List<Color>()
            {
                new(){Data="Beyaz",Value="Beyaz"},
                new(){Data="Siyah",Value="Siyah"},
                new(){Data="Kırmızı",Value="Kırmızı"},
                new(){Data="Mavi",Value="Mavi"},
            }, "Value", "Data");
            ViewBag.Numbers = new SelectList(new List<Number>()
            {
                new(){Data="40",Value="40"},
                new(){Data="41",Value="41"},
                new(){Data="42",Value="42"},
                new(){Data="43",Value="43"},
                new(){Data="44",Value="44"},
                new(){Data="45",Value="45"},
            }, "Value", "Data");
            var value = pm.TGetByID(id);
            return View(value);
        }
    }
}
