using Newtonsoft.Json;
using OzgeErsu.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OzgeErsu
{

    public class VimeoHelpers
    {
        public static async Task<string> GetVideoUrl(string uri)
        {
            string id = uri.Replace("https://vimeo.com/", "");
            string url = String.Format("http://player.vimeo.com/video/{0}/config", id);
            using (HttpClient client = new HttpClient())
            {
                var req = await client.GetAsync(url);
                if (req != null && req.IsSuccessStatusCode)
                {
                    string json = await req.Content.ReadAsStringAsync();
                    var video = JsonConvert.DeserializeObject<VimeoData>(json);
                    if (video != null &&
                        video.request != null &&
                        video.request.files != null &&
                        video.request.files.progressive != null &&
                        video.request.files.progressive.Count() > 0)
                        return video.request.files.progressive[0].url;
                    return "";
                }
            }
            return "";
        }
    }

    public class Akfirestringerconnect
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class Level3SkyfireGce
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class FastlySkyfire
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class Cdns
    {
        public Akfirestringerconnect akfire_stringerconnect { get; set; }
        public Level3SkyfireGce level3_skyfire_gce { get; set; }
        public FastlySkyfire fastly_skyfire { get; set; }
    }

    public class Stream
    {
        public string profile { get; set; }
        public string quality { get; set; }
        public string id { get; set; }
        public string fps { get; set; }
    }

    public class Dash
    {
        public string origin { get; set; }
        public string url { get; set; }
        public string cdn { get; set; }
        public Cdns cdns { get; set; }
        public List<Stream> streams { get; set; }
        public bool separate_av { get; set; }
        public string default_cdn { get; set; }
    }

    public class Akfirestringerconnect2
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class Level3SkyfireGce2
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class FastlySkyfire2
    {
        public string url { get; set; }
        public string origin { get; set; }
    }

    public class Cdns2
    {
        public Akfirestringerconnect2 akfire_stringerconnect { get; set; }
        public Level3SkyfireGce2 level3_skyfire_gce { get; set; }
        public FastlySkyfire2 fastly_skyfire { get; set; }
    }

    public class Hls
    {
        public string origin { get; set; }
        public string url { get; set; }
        public string cdn { get; set; }
        public Cdns2 cdns { get; set; }
        public string default_cdn { get; set; }
        public bool separate_av { get; set; }
    }

    public class Progressive
    {
        public string profile { get; set; }
        public string width { get; set; }
        public string mime { get; set; }
        public string fps { get; set; }
        public string url { get; set; }
        public string cdn { get; set; }
        public string quality { get; set; }
        public string id { get; set; }
        public string origin { get; set; }
        public string height { get; set; }
    }

    public class Files
    {
        public Dash dash { get; set; }
        public Hls hls { get; set; }
        public List<Progressive> progressive { get; set; }
    }

    public class ThumbPreview
    {
        public string url { get; set; }
        public string frame_width { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string frame_height { get; set; }
        public string frames { get; set; }
        public string columns { get; set; }
    }

    public class Flags
    {
        public string flash_hls { get; set; }
        public string preload_video { get; set; }
        public string webp { get; set; }
        public string autohide_controls { get; set; }
        public string plays { get; set; }
        public string blurr { get; set; }
        public string cedexis { get; set; }
        public string dnt { get; set; }
        public string partials { get; set; }
        public string login { get; set; }
        public string increase_tap_area { get; set; }
    }

    public class Cookie
    {
        public string scaling { get; set; }
        public string volume { get; set; }
        public object quality { get; set; }
        public string hd { get; set; }
        public object captions { get; set; }
    }

    public class Build
    {
        public string player { get; set; }
        public string js { get; set; }
    }

    public class Urls
    {
        public string zeroclip_swf { get; set; }
        public string js { get; set; }
        public string proxy { get; set; }
        public string flideo { get; set; }
        public string moog { get; set; }
        public string comscore_js { get; set; }
        public string blurr { get; set; }
        public string chromeless_css { get; set; }
        public string cedexis { get; set; }
        public string vuid_js { get; set; }
        public string chromeless_js { get; set; }
        public string moog_js { get; set; }
        public string zeroclip_js { get; set; }
        public string css { get; set; }
    }

    public class Request
    {
        public Files files { get; set; }
        public string ga_account { get; set; }
        public ThumbPreview thumb_preview { get; set; }
        public object referrer { get; set; }
        public string cookie_domain { get; set; }
        public string timestamp { get; set; }
        public string fingerprstring { get; set; }
        public string expires { get; set; }
        public Flags flags { get; set; }
        public string currency { get; set; }
        public string comscore_id { get; set; }
        public string session { get; set; }
        public Cookie cookie { get; set; }
        public Build build { get; set; }
        public Urls urls { get; set; }
        public string signature { get; set; }
        public string cedexis_cache_ttl { get; set; }
        public string country { get; set; }
    }

    public class Rating
    {
        public string id { get; set; }
    }

    public class Owner
    {
        public string account_type { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string url { get; set; }
        public string img_2x { get; set; }
        public string id { get; set; }
    }

    public class Thumbs
    {

    }

    namespace Helpers
    {
        public class VideoVimeo
        {
            public Rating rating { get; set; }
            public string allow_hd { get; set; }
            public string height { get; set; }
            public Owner owner { get; set; }
            public Thumbs thumbs { get; set; }
            public string duration { get; set; }
            public string id { get; set; }
            public string hd { get; set; }
            public string embed_code { get; set; }
            public string default_to_hd { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public string privacy { get; set; }
            public string share_url { get; set; }
            public string width { get; set; }
            public string embed_permission { get; set; }
            public double fps { get; set; }
        }
    }
    public class Build2
    {
        public string player { get; set; }
        public string rpc { get; set; }
    }

    public class Settings
    {
        public string fullscreen { get; set; }
        public string byline { get; set; }
        public string like { get; set; }
        public string playbar { get; set; }
        public string title { get; set; }
        public string color { get; set; }
        public string branding { get; set; }
        public string share { get; set; }
        public string scaling { get; set; }
        public string logo { get; set; }
        public string collections { get; set; }
        public string info_on_pause { get; set; }
        public string watch_later { get; set; }
        public string portrait { get; set; }
        public string embed { get; set; }
        public string badge { get; set; }
        public string volume { get; set; }
    }

    public class Email
    {
        public string text { get; set; }
        public string confirmation { get; set; }
        public string time { get; set; }
    }

    public class Embed
    {
        public string autopause { get; set; }
        public string color { get; set; }
        public string on_site { get; set; }
        public string outro { get; set; }
        public string api { get; set; }
        public string player_id { get; set; }
        public object quality { get; set; }
        public Settings settings { get; set; }
        public string context { get; set; }
        public string time { get; set; }
        public Email email { get; set; }
        public string loop { get; set; }
        public string autoplay { get; set; }
    }

    public class User
    {
        public string liked { get; set; }
        public string account_type { get; set; }
        public string progress { get; set; }
        public string owner { get; set; }
        public string watch_later { get; set; }
        public string logged_in { get; set; }
        public string id { get; set; }
        public string mod { get; set; }
    }

    public class VimeoData
    {
        public string cdn_url { get; set; }
        public string view { get; set; }
        public Request request { get; set; }
        public string player_url { get; set; }
        public VideoVimeo video { get; set; }
        public Build2 build { get; set; }
        public Embed embed { get; set; }
        public string vimeo_url { get; set; }
        public User user { get; set; }
    }
}