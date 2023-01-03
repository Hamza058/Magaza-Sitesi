using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductSizeColor
    {
        [Key]
        public int ProductSizeColorId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
