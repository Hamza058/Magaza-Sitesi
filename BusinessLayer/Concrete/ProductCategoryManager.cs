using BusinessLayer.Abstract;
using DataAccessLayer.Absract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductCategoryManager : IProductCategoryService
    {
        IProductCategoryDal _productCategoryDal;

        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public void TAdd(ProductCategory t)
        {
            _productCategoryDal.Insert(t);
        }

        public void TDelete(ProductCategory t)
        {
            _productCategoryDal.Delete(t);
        }

        public ProductCategory TGetByID(int id)
        {
            return _productCategoryDal.Get(x => x.ProductCategoryId == id);
        }

        public List<ProductCategory> TGetList()
        {
            return _productCategoryDal.GetList();
        }

        public void TUpdate(ProductCategory t)
        {
            _productCategoryDal.Update(t);
        }
    }
}
