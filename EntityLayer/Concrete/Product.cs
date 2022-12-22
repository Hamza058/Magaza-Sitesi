using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string ProductImage { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int ProductCategoryId { get; set; }
        public bool ProductStatus { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
