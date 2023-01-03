using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ProductClothesInfo
    {
        [Key]
        public int ProductClothesInfoId { get; set; }
        public string Gender { get; set; }
        public string? Pattern { get; set; }
        public string? CollarStyle { get; set; }
        public string? Material { get; set; }
        public string? AddInfo { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
