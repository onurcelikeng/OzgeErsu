using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgeErsu.Models
{
    public class InformationFeed
    {
        public string id { get; set; }
        public string priority { get; set; }
        public string toursMainShare { get; set; }
        public string laternaMainShare { get; set; }
        public string applicationGenericShare { get; set; }
        public string advertisement { get; set; }
        public string ad_url { get; set; }
        public string credits { get; set; }
    }

    public class InformationModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<InformationFeed> feeds { get; set; }
    }
}
