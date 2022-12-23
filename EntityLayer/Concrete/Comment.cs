using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool CommentStatus { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
