using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Views.Shared.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        BrandManager bm = new BrandManager(new EFBrandDal());
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        ImageManager im = new ImageManager(new EFImageDal());
        ProductSizeColorManager pscm = new ProductSizeColorManager(new EFProductSizeColorDal());

        public IViewComponentResult Invoke(int id, int type = 1)
        {
            if (type==1)
            {
                ViewBag.brands = bm.TGetList();
                ViewBag.category = pcm.TGetList();

                return View("Default");
            }
            else
            {
                ViewBag.Size = pscm.TGetByID(id).Size;
                ViewBag.Color = pscm.TGetByID(id).Color;
                ViewBag.imgs = im.TGetList().Where(x => x.ProductId == id).ToList();

                return View("Type2",im.GetByIDProduct(id));
            }

        }
    }
}
