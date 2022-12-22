using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Single(int id=1)
        {
            var value = pm.TGetByID(id);
            return View(value);
        }
    }
}
