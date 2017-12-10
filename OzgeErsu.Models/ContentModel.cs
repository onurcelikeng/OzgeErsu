using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{
    public class ImageArray
    {
        public string guid { get; set; }
    }

    public class ContentFeed
    {
        public string image { get; set; }
        public string ID { get; set; }
        public string post_title { get; set; }
        public string guid { get; set; }
    }

    public class ContentModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public List<ImageArray> image_array { get; set; }
        public List<ContentFeed> feeds { get; set; }
    }
}
