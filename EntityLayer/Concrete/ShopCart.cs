using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class ShopCart
    {
        [Key]
        public int ShopCartId { get; set; }
        public string ShopCartProductName { get; set; }
        public string ShopCartProductSize { get; set; }
        public string ShopCartProductColor { get; set; }
        public int ShopCartProductPrice { get; set; }
        public bool ShopCartStatus { get; set; }
        public bool ShopCartConfirm { get; set; }
        public DateTime? ShopCartConfirmDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
