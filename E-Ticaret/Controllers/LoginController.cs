using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        UserManager um = new UserManager(new EFUserDal());

        // GET: LoginController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            var users = um.TGetList();
            var value=users.FirstOrDefault(x => x.UserMail == user.UserMail && x.UserPassword == user.UserPassword);

            if (value != null)
            {
                HttpContext.Session.SetString("userMail", user.UserMail);
                return RedirectToAction("Index", "Default");
            }
            ViewBag.Message = "Hatalı Kullancı Adı veya Şifre";
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            try
            {
                MailAddress m = new MailAddress(user.UserMail);
                if (m.Host != null)
                {
                    user.UserStatus = true;
                    um.TAdd(user);
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.Message = "Geçerli bir mail adresi giriniz";
                return View();
            }
            catch (Exception)
            {
                ViewBag.Message = "Geçerli bir mail adresi giriniz";
                return View();
            }
        }
    }
}
