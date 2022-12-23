using DataAccessLayer.Absract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFImageDal : GenericRepository<Image>, IImageDal
    {
        public List<Image> GetListWithProduct()
        {
            using(var c=new Context())
            {
                return c.Images.Include(x => x.Product)
                    .Include(x=>x.Product.Brand)
                    .Include(x=>x.Product.ProductCategory).ToList();

            }
        }
    }
}
