using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using E_Ticaret.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Views.Shared.ViewComponents
{
    public class AddCommentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
