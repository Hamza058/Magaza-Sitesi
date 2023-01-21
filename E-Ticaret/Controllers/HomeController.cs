using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using E_Ticaret.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());

        public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var admins = am.TGetList().Where(x => x.AdminName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(admins);
        }
        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            am.TAdd(admin);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAdmin(int id)
        {
            var admin = am.TGetByID(id);
            if (admin.AdminStatus == true)
                admin.AdminStatus = false;
            else
                admin.AdminStatus = true;
            am.TDelete(admin);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Error(int code)
        {
            ViewBag.code = code;
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Items()
        {
			var category = cm.TGetList();
			return Json(category);
        }
    }
}
