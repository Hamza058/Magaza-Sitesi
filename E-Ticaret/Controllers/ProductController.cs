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
    public class ProductController : Controller
    {
        ProductManager pm = new ProductManager(new EFProductDal());
        ProductSizeColorManager pscm = new ProductSizeColorManager(new EFProductSizeColorDal());
        ProductClothesInfoManager pcim = new ProductClothesInfoManager(new EFProductClothesInfoDal());

        public IActionResult Index(string f = "", int p = 1)
        {
            if (f == null)
                f = "";
            var products = pm.GetBrands().Where(x => x.ProductName.ToLower().Contains(f.ToLower())).ToPagedList(p, 5);
            return View(products);
        }

        public IActionResult AddProduct(Product product, ProductSizeColor sizeColor, ProductClothesInfo clothesInfo)
        {
            product.ProductStatus = true;
            pm.TAdd(product);

            int id = pm.TGetList().FirstOrDefault(x => x.ProductName == product.ProductName).ProductId;

            sizeColor.ProductId = id;
            clothesInfo.ProductId = id;
            pscm.TAdd(sizeColor);
            pcim.TAdd(clothesInfo);

            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(int id)
        {
            var product = pm.TGetByID(id);
            if (product.ProductStatus == true)
                product.ProductStatus = false;
            else
                product.ProductStatus = true;
            pm.TDelete(product);
            return RedirectToAction("Index");
        }
    }
}
