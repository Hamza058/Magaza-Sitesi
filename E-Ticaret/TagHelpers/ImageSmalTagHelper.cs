using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret.TagHelpers
{
    public class ImageSmalTagHelper : TagHelper
    {
        public string ImageSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string fileName = ImageSrc.Split('.')[0];
            string fileExtension = Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{fileName}{fileExtension}");
            output.Attributes.SetAttribute("width", "100%");
        }
    }
}
