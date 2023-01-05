using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Views.Shared.ViewComponents
{
    public class GetUserInfoViewComponent : ViewComponent
    {
        UserManager um = new UserManager(new EFUserDal());
        ShopCartManager scm = new ShopCartManager(new EFShopCartDal());

        [HttpPost]
        public IViewComponentResult Invoke(int id)
        {
            var user = um.TGetByID(id);
            ViewBag.Shopcart = scm.TGetList().Where(x => x.UserId == id).ToList();
            return View("Default", user);
        }
    }
}
