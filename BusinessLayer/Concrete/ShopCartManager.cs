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
    public class ShopCartManager : IShopCartService
    {
        IShopCartDal _shopCartDal;

        public ShopCartManager(IShopCartDal shopCartDal)
        {
            _shopCartDal = shopCartDal;
        }

        public void TAdd(ShopCart t)
        {
            _shopCartDal.Insert(t);
        }

        public void TDelete(ShopCart t)
        {
            _shopCartDal.DeleteSql(t);
        }

        public ShopCart TGetByID(int id)
        {
            return _shopCartDal.Get(x => x.ShopCartId == id);
        }

        public List<ShopCart> TGetList()
        {
            return _shopCartDal.GetList();
        }

        public void TUpdate(ShopCart t)
        {
            _shopCartDal.Update(t);
        }
    }
}
