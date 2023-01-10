﻿using DataAccessLayer.Absract;
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
    public class EFProductDal : GenericRepository<Product>, IProductDal
    {
        public List<Product> GetListWithBrand()
        {
            using (var c = new Context())
            {
                return c.Products.Include(x => x.Brand)
                    .Include(x => x.ProductCategory).ToList();
            }
        }
    }
}
