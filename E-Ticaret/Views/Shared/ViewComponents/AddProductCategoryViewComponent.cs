using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Views.Shared.ViewComponents
{
    public class AddProductCategoryViewComponent : ViewComponent
    {
        CategoryManager cm = new CategoryManager(new EFCategoryDal());

        public IViewComponentResult Invoke()
        {
            ViewBag.category = cm.TGetList();

            return View();
        }
    }
}
