using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Absract
{
    public interface IImageDal: IGenericDal<Image>
    {
        List<Image> GetListWithProduct();
        Image GetWithProduct(Expression<Func<Image, bool>> filter);
    }
}
