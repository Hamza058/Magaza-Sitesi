using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace E_Ticaret.Controllers
{
	[AllowAnonymous]
	public class CampaignController : Controller
	{
		CampaignManager cm = new CampaignManager(new EFCampaignDal());
		ProductManager pm = new ProductManager(new EFProductDal());
		ImageManager im = new ImageManager(new EFImageDal());
		public IActionResult Index()
		{
			var value = cm.TGetList();
			foreach (var item in value)
			{
				if (item.LastDay < DateTime.Now)
				{
					cm.TDelete(item);
				}
			}
			ViewBag.Product = pm.TGetList();
			return View();
		}

		[HttpGet]
		public IActionResult Campaigns()
		{
			return Json(cm.TGetList());
		}
		[HttpPost]
		public IActionResult AddCampaign(string product, int newprice, DateTime lastday)
		{
			var value = pm.GetBrands().First(x => x.ProductName == product);
			if (cm.TGetList().Count < 5 && newprice < value.Price && DateTime.Now < lastday)
			{
				Campaign c = new Campaign()
				{
					ProductName = product,
					Brand = value.Brand.BrandName,
					ProductCategory = value.ProductCategory.ProductCategoryName,
					OldPrice = value.Price,
					NewPrice = newprice,
					LastDay = lastday,
					ProductImage = im.TGetList().First(x => x.ProductId == value.ProductId).ImageUrl
				};
				cm.TAdd(c);
				return Json(new { IsSuccess = "true" });
			}
			else
			{
				TempData["message"] = "Girilen Bilgiler Hatalı veya Kampanya Kapasitesi Dolu";
				return Json(new { IsSuccess = "false" });
			}
		}
		[HttpPost]
		public IActionResult DeleteCampaign(int id)
		{
			cm.TDelete(cm.TGetByID(id));
			return Json(new { IsSuccess = "true" });
		}
	}
}
