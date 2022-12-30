using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    [Authorize]
    public class ShopCartController : Controller
    {
        ShopCartManager sm = new ShopCartManager(new EFShopCartDal());
        UserManager um = new UserManager(new EFUserDal());
        ProductManager pm = new ProductManager(new EFProductDal());

        public IActionResult Index()
        {
            var user = um.TGetList().Where(x => x.UserMail == HttpContext.Session.GetString("UserMail")).FirstOrDefault().UserId;
            var shop = sm.TGetList().Where(x => x.UserId == user).ToList();

            return View(shop);
        }

        public IActionResult AddShop(int id, string color, string size)
        {
            var product = pm.TGetByID(id);
            var user = um.TGetList().Where(x => x.UserMail == HttpContext.Session.GetString("UserMail")).FirstOrDefault().UserId;
            ShopCart shopCart = new ShopCart()
            {
                ShopCartProductName = product.Brand + " " + product.ProductName,
                ShopCartProductColor = color,
                ShopCartProductSize = size,
                ShopCartProductPrice = product.Price,
                UserId = user
            };
            sm.TAdd(shopCart);
            return RedirectToAction("Index","Default");
        }
        public IActionResult DeleteShop(int id)
        {
            var value=sm.TGetByID(id);
            sm.TDelete(value);
            return RedirectToAction("Index","ShopCart");
        }
    }
}
