using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public DateTime LastDay { get; set; }
		public string ProductImage { get; set; }
	}
}
