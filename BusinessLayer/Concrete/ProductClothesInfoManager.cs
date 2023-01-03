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
    public class ProductClothesInfoManager : IProductClothesInfoService
    {
        IProductClothesInfoDal _productClothesInfo;

        public ProductClothesInfoManager(IProductClothesInfoDal productClothesInfo)
        {
            _productClothesInfo = productClothesInfo;
        }

        public void TAdd(ProductClothesInfo t)
        {
            _productClothesInfo.Insert(t);
        }

        public void TDelete(ProductClothesInfo t)
        {
            _productClothesInfo.DeleteSql(t);
        }

        public ProductClothesInfo TGetByID(int id)
        {
            return _productClothesInfo.Get(x => x.ProductId == id);
        }

        public List<ProductClothesInfo> TGetList()
        {
            return _productClothesInfo.GetList();
        }

        public void TUpdate(ProductClothesInfo t)
        {
            _productClothesInfo.Update(t);
        }
    }
}
