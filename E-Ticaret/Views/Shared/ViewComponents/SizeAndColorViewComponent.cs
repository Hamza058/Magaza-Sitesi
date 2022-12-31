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
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Colors = new SelectList(new List<Color>()
            {
                new(){Data="Beyaz",Value="Beyaz"},
                new(){Data="Siyah",Value="Siyah"},
                new() { Data = "Kırmızı", Value = "Kırmızı" },
                new() { Data = "Mavi", Value = "Mavi" },
            }, "Value", "Data");
            ViewBag.Numbers = new SelectList(new List<Number>()
            {
                new(){Data="40",Value="40"},
                new(){Data="41",Value="41"},
                new(){Data="42",Value="42"},
                new(){Data="43",Value="43"},
                new(){Data="44",Value="44"},
                new(){Data="45",Value="45"},
            }, "Value", "Data");
            ViewBag.id = id;

            return View();
        }
    }
}
