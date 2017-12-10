using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{
    public class LateradioFeed
    {
        public string id { get; set; }
        public string nowplaying { get; set; }
        public string notification { get; set; }
        public string programflow { get; set; }
        public string porturl { get; set; }
    }

    public class LateradioModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<LateradioFeed> feeds { get; set; }
    }
}
