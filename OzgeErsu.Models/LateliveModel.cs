using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{
    public class LateliveFeed
    {
        public string id { get; set; }
        public string priority { get; set; }
        public string program { get; set; }
        public string splash { get; set; }
        public string audio { get; set; }
        public string active { get; set; }
    }

    public class LateliveModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<LateliveFeed> feeds { get; set; }
    }
}
