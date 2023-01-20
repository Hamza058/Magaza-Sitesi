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
using X.PagedList;

namespace E_Ticaret.Controllers
{
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EFCommentDal());
        UserManager um = new UserManager(new EFUserDal());

        [HttpPost]
        public IActionResult AddComment(Comment comment, int id)
        {
            var user = um.TGetList().Find(x => x.UserMail == HttpContext.Session.GetString("UserMail")).UserId;
            comment.UserId = user;
            comment.CreatedDate = DateTime.Now;
            comment.ProductId = id;
            cm.TAdd(comment);
            return RedirectToAction("Single", "Default", new { id = id });
        }

        public IActionResult AdminComment(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var comments = cm.GetWithUsers().Where(x => x.Content.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);

            return View(comments);
        }

        public IActionResult DeleteComment(int id)
        {
            var comment = cm.TGetByID(id);
            if (comment.CommentStatus == true)
                comment.CommentStatus = false;
            else
                comment.CommentStatus = true;
            cm.TDelete(comment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AdminCommentGet(int id)
        {
            var comment = cm.TGetByID(id).Content;
            return Json(comment);
        }
    }
}
