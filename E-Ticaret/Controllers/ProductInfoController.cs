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
    [AllowAnonymous]
    public class ProductInfoController : Controller
    {
        ProductClothesInfoManager pcim = new ProductClothesInfoManager(new EFProductClothesInfoDal());
        [Route("[controller]/[action]/{id}")]
        public IActionResult ClothesInfo(int id)
        {
            var info = pcim.TGetByID(id);
            return View(info);
        }
    }
}
