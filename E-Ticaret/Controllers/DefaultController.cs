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
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        BrandManager bm = new BrandManager(new EFBrandDal());
        ImageManager im = new ImageManager(new EFImageDal());
        ProductSizeColorManager pscm = new ProductSizeColorManager(new EFProductSizeColorDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        CampaignManager cpm = new CampaignManager(new EFCampaignDal());

        //[Route("[controller]/[action]/{f?}")]
        public IActionResult Index(string f="")
        {
            var values = im.GetProducts().Where(x => x.ImageUrl.Contains('1') && (x.Product.Brand.BrandName.ToLower().Contains(f.ToLower()) || x.Product.ProductCategory.ProductCategoryName.ToLower().Contains(f.ToLower()) || x.Product.ProductName.ToLower().Contains(f.ToLower()) || x.Product.ProductCategory.Category.CategoryName.Contains(f))).ToList();
            ViewBag.category = pcm.TGetList();
            ViewBag.brand = bm.TGetList();
            ViewBag.cat = cm.TGetList();
            ViewBag.campaign = cpm.TGetList();

            return View(values);
        }
        [Route("[controller]/[action]/{id?}/{cid?}")]
        public IActionResult Single(int id,int cid)
        {
            int newprice = 0;
			if (cid != 0)
			{
                id = im.TGetList().First(x => x.ImageUrl == cpm.TGetByID(cid).ProductImage).ProductId;
                newprice = cpm.TGetByID(cid).NewPrice;
			}
            var value = im.GetByIDProduct(id);
            if (newprice > 0)
                value.Product.Price = newprice;

            ViewBag.Colors = pscm.TGetByID(id).Color.Split('-');
            ViewBag.Size = pscm.TGetByID(id).Size.Split('-');
            ViewBag.cat = cm.TGetList();

            
            ViewBag.imgs = im.TGetList().Where(x => x.ProductId == id).ToList();

            ViewBag.products = im.GetProducts().Where(x => x.ImageUrl.Contains('1') && x.Product.ProductCategory.ProductCategoryName == value.Product.ProductCategory.ProductCategoryName && x.ProductId != value.ProductId).ToList();

            return View(value);
        }

        [HttpGet]
        public IActionResult SizeColor(int id)
        {
            ViewBag.id = id;
            Dictionary<string, object> cs = new Dictionary<string, object>();
            var color = pscm.TGetByID(id).Color.Split('-');
            var size = pscm.TGetByID(id).Size.Split('-');
            cs.Add("color", color);
            cs.Add("size", size);

            return Json(cs);
        }
    }
}
