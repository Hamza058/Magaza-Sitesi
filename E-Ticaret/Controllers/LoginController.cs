using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        UserManager um = new UserManager(new EFUserDal());
        AdminManager am = new AdminManager(new EFAdminDal());

        // GET: LoginController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            var users = um.TGetList();
            var value = users.FirstOrDefault(x => x.UserMail == user.UserMail && x.UserPassword == user.UserPassword);
            HttpContext.Session.SetString("UserMail", user.UserMail);
            if (value != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserMail)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Default");
            }
            ViewBag.Message = "Hatalı Kullancı Adı veya Şifre";
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(User user)
        //{
        //    var users = um.TGetList();
        //    var value=users.FirstOrDefault(x => x.UserMail == user.UserMail && x.UserPassword == user.UserPassword);

        //    if (value != null)
        //    {
        //        HttpContext.Session.SetString("UserMail", user.UserMail);
        //        return RedirectToAction("Index", "Default");
        //    }
        //    ViewBag.Message = "Hatalı Kullancı Adı veya Şifre";
        //    return View();
        //}

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
                    user.UserName.Trim();
                    user.UserSurname.Trim();
                    user.UserMail.Trim();
                    user.UserPassword.Trim();
                    user.UserPhone.Trim();
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
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminLogin(Admin admin)
        {
            var admins = am.TGetList();
            var value = admins.FirstOrDefault(x => x.AdminName == admin.AdminName && x.AdminPassword == admin.AdminPassword);
            if (value != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,ClaimTypes.Role,admin.AdminName,admin.AdminRole.ToString())
                };
                var useridentity = new ClaimsIdentity(claims,"a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Category");
            }
            ViewBag.Message = "Hatalı Kullancı Adı veya Şifre";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserMail");
            return RedirectToAction("Index", "Default");
        }
    }
}
