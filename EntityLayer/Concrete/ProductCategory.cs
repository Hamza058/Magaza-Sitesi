using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int CategoryId { get; set; }

        public ICollection<Product> Products { get; set; }

        public Category Category { get; set; }
    }
}
