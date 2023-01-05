using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        UserManager um = new UserManager(new EFUserDal());

        public IActionResult Index(string f = "", int p = 1)
        {
            if (f == null)
                f = "";
            var users = um.TGetList().Where(x => x.UserName.ToLower().Contains(f.ToLower())).ToPagedList(p, 5);
            return View(users);
        }

        public IActionResult DeleteUser(int id)
        {
            var user = um.TGetByID(id);
            if (user.UserStatus == true)
                user.UserStatus = false;
            else
                user.UserStatus = true;
            um.TDelete(user);
            return RedirectToAction("Index");
        }
    }
}
