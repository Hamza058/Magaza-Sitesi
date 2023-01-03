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
    public class ProductSizeColorManager : IProductSizeColorService
    {
        IProductSizeColorDal _productSizeColor;

        public ProductSizeColorManager(IProductSizeColorDal productSizeColor)
        {
            _productSizeColor = productSizeColor;
        }

        public void TAdd(ProductSizeColor t)
        {
            _productSizeColor.Insert(t);
        }

        public void TDelete(ProductSizeColor t)
        {
            _productSizeColor.Delete(t);
        }

        public ProductSizeColor TGetByID(int id)
        {
            return _productSizeColor.Get(x => x.ProductId == id);
        }

        public List<ProductSizeColor> TGetList()
        {
            return _productSizeColor.GetList();
        }

        public void TUpdate(ProductSizeColor t)
        {
            _productSizeColor.Update(t);
        }
    }
}
