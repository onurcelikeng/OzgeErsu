using System;
using System.Collections.Generic;

namespace OzgeErsu.Models
{
    public class NotificatioFeed
    {
        public string id { get; set; }
        public string image { get; set; }
        public string mesaj { get; set; }
        public string active { get; set; }
        public string oDate { get; set; }
    }

    public class NotificationModel
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string filebase { get; set; }
        public List<NotificatioFeed> feeds { get; set; }
    }
}
