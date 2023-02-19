using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=dbeticaret;");//Eğer mysql kullanılacaksa admin role string yap.
		}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<ProductSizeColor> ProductSizeColors { get; set; }
        public DbSet<ProductClothesInfo> ProductClothesInfos { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
    }
}
