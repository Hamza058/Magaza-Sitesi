using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer.Concrete;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace E_Ticaret.Controllers
{
    public class DefaultController : Controller
    {
        ProductManager pm = new ProductManager(new EFProductDal());
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        BrandManager bm = new BrandManager(new EFBrandDal());
        ImageManager im = new ImageManager(new EFImageDal());
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = im.GetProducts().Where(x => x.ImageUrl.Contains('1')).ToList();
            ViewBag.category = pcm.TGetList();
            ViewBag.brand = bm.TGetList();

            return View(values);
        }
        [AllowAnonymous]
        public IActionResult Single(int id)
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

            var value = im.GetByIDProduct(id);
            ViewBag.imgs = im.TGetList().Where(x => x.ProductId == id).ToList();

            ViewBag.products = im.GetProducts().Where(x => x.ImageUrl.Contains('1') && x.Product.ProductCategory.ProductCategoryName==value.Product.ProductCategory.ProductCategoryName).ToList();

            return View(value);
        }
    }
}
