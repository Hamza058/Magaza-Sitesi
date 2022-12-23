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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void TAdd(Brand t)
        {
            _brandDal.Insert(t);
        }

        public void TDelete(Brand t)
        {
            _brandDal.Delete(t);
        }

        public Brand TGetByID(int id)
        {
            return _brandDal.Get(x => x.BrandId == id);
        }

        public List<Brand> TGetList()
        {
            return _brandDal.GetList();
        }

        public void TUpdate(Brand t)
        {
            _brandDal.Update(t);
        }
    }
}
