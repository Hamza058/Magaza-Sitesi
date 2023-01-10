using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class ProductCategoryController : Controller
    {
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        
        public IActionResult Index(string f = "", int p = 1)
        {
            if (f == null)
                f = "";
            var productcategories = pcm.TGetList().Where(x => x.ProductCategoryName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(productcategories);
        }
        [HttpPost]
        public IActionResult AddProductCategory(ProductCategory productCategory)
        {
            productCategory.ProductCategoryStatus = true;
            pcm.TAdd(productCategory);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProductCategory(int id)
        {
            var productcategories = pcm.TGetByID(id);
            if (productcategories.ProductCategoryStatus == true)
                productcategories.ProductCategoryStatus = false;
            else
                productcategories.ProductCategoryStatus = true;
            pcm.TDelete(productcategories);
            return RedirectToAction("Index");
        }
    }
}
