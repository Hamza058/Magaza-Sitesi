using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryDal());

        public IActionResult Index()
        {
            var values = cm.TGetList();
            return View(values);
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            category.CategoryStatus = true;
            cm.TAdd(category);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCategory(int id)
        {
            var category = cm.TGetByID(id);
            if(category.CategoryStatus==true)
                category.CategoryStatus = false;
            else
                category.CategoryStatus = true;
            cm.TDelete(category);
            return RedirectToAction("Index");
        }
    }
}
