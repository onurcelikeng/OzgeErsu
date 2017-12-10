﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgeErsu.Models
{
    public class Location
    {
        public string name { get; set; }
    }

    public class LowResolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class StandardResolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Images
    {
        public LowResolution low_resolution { get; set; }
        public Thumbnail thumbnail { get; set; }
        public StandardResolution standard_resolution { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
        public List<object> data { get; set; }
    }

    public class From
    {
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string id { get; set; }
        public string full_name { get; set; }
    }

    public class Caption
    {
        public string created_time { get; set; }
        public string text { get; set; }
        public From from { get; set; }
        public string id { get; set; }
    }

    public class Datum
    {
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string id { get; set; }
        public string full_name { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
        public List<Datum> data { get; set; }
    }

    public class User
    {
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string id { get; set; }
        public string full_name { get; set; }
    }

    public class Item
    {
        public bool can_delete_comments { get; set; }
        public string code { get; set; }
        public Location location { get; set; }
        public Images images { get; set; }
        public bool can_view_comments { get; set; }
        public Comments comments { get; set; }
        public object alt_media_url { get; set; }
        public Caption caption { get; set; }
        public string link { get; set; }
        public Likes likes { get; set; }
        public string created_time { get; set; }
        public bool user_has_liked { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public User user { get; set; }
    }

    public class InstagramDataModel
    {
        public string status { get; set; }
        public ObservableCollection<Item> items { get; set; }
        public bool more_available { get; set; }
    }
}
