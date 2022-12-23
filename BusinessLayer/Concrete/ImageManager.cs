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
    public class ImageManager : IImageService
    {
        IImageDal _imageDal;

        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        public List<Image> GetProducts()
        {
            return _imageDal.GetListWithProduct();
        }

        public void TAdd(Image t)
        {
            _imageDal.Insert(t);
        }

        public void TDelete(Image t)
        {
            _imageDal.Delete(t);
        }

        public Image TGetByID(int id)
        {
            return _imageDal.Get(x => x.ProductId == id);
        }

        public List<Image> TGetList()
        {
            return _imageDal.GetList();
        }

        public void TUpdate(Image t)
        {
            _imageDal.Update(t);
        }
    }
}
