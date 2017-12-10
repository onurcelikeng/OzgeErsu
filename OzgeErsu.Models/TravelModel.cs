using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{
    public class TravelFeed
    {
        public string id { get; set; }
        public string priority { get; set; }
        public string gezi { get; set; }
        public string image { get; set; }
        public string pdf { get; set; }
        public string shareLink { get; set; }
        public string active { get; set; }
    }

    public class TravelModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<TravelFeed> feeds { get; set; }
    }
}
