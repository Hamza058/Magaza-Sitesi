using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using E_Ticaret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_Ticaret.Views.Shared.ViewComponents
{
    [AllowAnonymous]
    public class SizeAndColorViewComponent : ViewComponent
    {
        ProductSizeColorManager pscm = new ProductSizeColorManager(new EFProductSizeColorDal());

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Colors = pscm.TGetByID(id).Color.Split('-');
            ViewBag.Size = pscm.TGetByID(id).Size.Split('-');
            ViewBag.id = id;

            return View();
        }
    }
}
