using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace E_Ticaret.Controllers
{
	public class BrandController : Controller
	{
		BrandManager bm = new BrandManager(new EFBrandDal());

		public IActionResult Index(string f, int p = 1)
		{
			if (f == null)
				f = "";
			var values = bm.TGetList().Where(x => x.BrandName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);

			return View(values);
		}
        [HttpPost]
        public IActionResult AddBrand(Brand brand)
        {
            brand.BrandStatus = true;
            bm.TAdd(brand);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteBrand(int id)
        {
            var brand = bm.TGetByID(id);
            if (brand.BrandStatus == true)
                brand.BrandStatus = false;
            else
                brand.BrandStatus = true;
            bm.TDelete(brand);
            return RedirectToAction("Index");
        }
    }
}
