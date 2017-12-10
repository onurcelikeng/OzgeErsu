using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{

    public class VideoFeed
    {
        public string id { get; set; }
        public string priority { get; set; }
        public string video { get; set; }
        public string splash { get; set; }
        public string link { get; set; }
        public string active { get; set; }
    }

    public class VideoModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<VideoFeed> feeds { get; set; }
    }

}
