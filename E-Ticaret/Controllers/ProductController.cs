using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
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
        BrandManager bm = new BrandManager(new EFBrandDal());
        ProductCategoryManager pcm = new ProductCategoryManager(new EFProductCategoryDal());
        ImageManager im = new ImageManager(new EFImageDal());

        private readonly IFileProvider _fileProvider;

        public ProductController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var products = pm.GetBrands().Where(x => x.ProductName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
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
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = pm.GetBrands().SingleOrDefault(x => x.ProductId == id);
            ViewBag.Brand = bm.TGetList();
            ViewBag.Category = pcm.TGetList();
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product, List<IFormFile> files)
        {
            if (files != null)
            {
                Image image;
                var root = _fileProvider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "img");
                for (int i = 0; i < files.Count; i++)
                {
                    var randomImageName = Guid.NewGuid() + Path.GetExtension(files[i].FileName);
                    var path = Path.Combine(images.PhysicalPath, randomImageName);
                    using var stream = new FileStream(path, FileMode.Create);
                    image = new Image()
                    {
                        ImageUrl = randomImageName,
                        ProductId = product.ProductId
                    };
                    im.TAdd(image);
                }
                product.ProductStatus = true;
                pm.TUpdate(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetInfoProduct(int id)
        {
            Dictionary<string, string> product = new Dictionary<string, string>();
            product.Add("Name", pm.TGetByID(id).ProductName);
            product.Add("Brand", pm.GetBrands().FirstOrDefault(x => x.ProductId == id).Brand.BrandName);
            product.Add("Size", pscm.TGetByID(id).Size);
            product.Add("Color", pscm.TGetByID(id).Color);

            return Json(product);
        }
        [HttpGet]
        public IActionResult GetImageProduct(int id)
        {
            var images = im.TGetList().Where(x => x.ProductId == id).ToList();
            return Json(images);
        }
    }
}
