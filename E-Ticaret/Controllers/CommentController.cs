using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EFCommentDal());

        [AllowAnonymous]
        public IActionResult Index(int id)
        {
            var comments = cm.GetWithUsers().Where(x => x.ProductId == id).ToList();
            return View(comments);
        }

        [AllowAnonymous]
        public IActionResult ProductInfo()
        {
            return View();
        }
    }
}
