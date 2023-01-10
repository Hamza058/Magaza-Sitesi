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
    public class CommentViewComponent : ViewComponent
    {
        CommentManager cm = new CommentManager(new EFCommentDal());

        public IViewComponentResult Invoke(int id, int type)
        {
            ViewBag.id = id;

            if (type == 1)
                return View("Default");
            else
                return View("Type2",cm.TGetByID(id));
        }
    }
}
