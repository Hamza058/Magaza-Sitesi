﻿using BusinessLayer.Concrete;
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
            var shop = sm.TGetList().Where(x => x.UserId == user && x.ShopCartStatus == false).ToList();

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
            return RedirectToAction("Single","Default",new { id = id });
        }
        public IActionResult DeleteShop(int id)
        {
            var value=sm.TGetByID(id);
            sm.TDelete(value);
            return RedirectToAction("Index","ShopCart");
        }
        [AllowAnonymous]
        public IActionResult PendingOrder()
        {
            var value = sm.TGetList().Where(x => x.ShopCartStatus == true && x.ShopCartConfirm==false).ToList();
            List<User> users = new List<User>();
            if (value != null)
            {
                foreach (var item in value)
                {
                    users.Add(um.TGetByID(item.UserId));
                }
                users = users.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                return View(users);
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult ConfirmedOrder()
        {
            var value = sm.TGetList().Where(x => x.ShopCartConfirm == true).ToList();
            List<User> users = new List<User>();
            if (value != null)
            {
                foreach (var item in value)
                {
                    users.Add(um.TGetByID(item.UserId));
                }
                users = users.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                return View(users);
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Basket(int id)
        {
            var user = um.TGetByID(id).UserId;
            var shop = sm.TGetList().Where(x => x.UserId == user).ToList();
            shop.GroupBy(x => x.ShopCartConfirmDate).Where(g => g.Count() > 1).Select(y => y.Key);

            return Json(shop);
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            var shop = sm.TGetList().Where(x => x.UserId == id).ToList();
            foreach (var item in shop)
            {
                item.ShopCartConfirm = true;
                item.ShopCartConfirmDate = DateTime.Now;
                sm.TUpdate(item);
            }
            return Json(new { IsSuccess = "true" });
        }

        [HttpGet]
        public IActionResult ConfirmShop()
        {
            var user = um.TGetList().FirstOrDefault(x => x.UserMail == HttpContext.Session.GetString("UserMail")).UserId;
            ViewBag.adres = um.TGetByID(user).UserAdress;
            var shop = sm.TGetList().Where(x => x.UserId == user && x.ShopCartStatus == false).ToList();

            return View(shop);
        }
        [HttpPost]
        public IActionResult ConfirmShop(string adres)
        {
            var user = um.TGetList().FirstOrDefault(x=>x.UserMail== HttpContext.Session.GetString("UserMail"));
            user.UserAdress = adres;
            var shop = sm.TGetList().Where(x => x.UserId == user.UserId && x.ShopCartStatus == false).ToList();
            foreach (var item in shop)
            {
                item.ShopCartStatus = true;
            }
            return RedirectToAction("Index", "Default");
        }
    }
}
