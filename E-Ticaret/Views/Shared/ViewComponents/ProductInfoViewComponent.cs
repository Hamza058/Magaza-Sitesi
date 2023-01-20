using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Views.Shared.ViewComponents
{
	public class ProductInfoViewComponent:ViewComponent
	{
		ProductClothesInfoManager pcim = new ProductClothesInfoManager(new EFProductClothesInfoDal());

		public IViewComponentResult Invoke(int id)
		{
			var info = pcim.TGetByID(id);
			return View(info);
		}
	}
}
