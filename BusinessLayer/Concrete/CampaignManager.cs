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
    public class CampaignManager : ICampaignService
    {
        ICampaignDal _campaignDal;

        public CampaignManager(ICampaignDal campaignDal)
        {
            _campaignDal = campaignDal;
        }

        public void TAdd(Campaign t)
        {
            _campaignDal.Insert(t);
        }

        public void TDelete(Campaign t)
        {
            _campaignDal.DeleteSql(t);
        }

        public Campaign TGetByID(int id)
        {
            return _campaignDal.Get(x => x.CampaignId == id);
        }

        public List<Campaign> TGetList()
        {
            return _campaignDal.GetList();
        }

        public void TUpdate(Campaign t)
        {
            _campaignDal.Update(t);
        }
    }
}
