using System.Linq;
using Newtonsoft.Json;
using OzgeErsu.Models;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OzgeErsu.Client
{
    public class DataClient
    {
        private HtmlDocument htmlDocument = new HtmlDocument();
        private HttpClient client = new HttpClient();


        public DataClient()
        {

        }


        public async Task<InformationModel> GetInformations()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_generic.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<InformationModel>(data);
            }

            return null;
        }

        public async Task<LateradioModel> GetLateradio()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_lateradio.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LateradioModel>(data);
            }

            return null;
        }

        public async Task<string> GetLateradioTitle()
        {
            string title = null;

            try
            {
                HttpResponseMessage req = await client.GetAsync("http://radyo.ersu.net/son.php");

                if (req != null && req.IsSuccessStatusCode)
                {
                    var data = await req.Content.ReadAsStringAsync();

                    htmlDocument.LoadHtml(data);
                    HtmlNode parent = htmlDocument.DocumentNode.Descendants("p").FirstOrDefault(o => o.GetAttributeValue("class", "") == "artist");
                    title = parent.InnerText;
                }
            }

            catch (System.Exception)
            {

            }

            return title;
        }

        public async Task<ArtistModel> GetLateradioArtist(string name)
        {
            try
            {
                var url = "http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + name.Trim() + "&api_key=462c553665a295f3df9a547136e9fb8f&format=json";
                HttpResponseMessage req = await client.GetAsync(url);

                if (req != null && req.IsSuccessStatusCode)
                {
                    var data = await req.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArtistModel>(data);
                }

                return null;
            }

            catch (System.Exception)
            {
                return null;
            }
        }

        public async Task<StreamingModel> GetStreaming()
        {
            HttpResponseMessage req = await client.GetAsync("http://ozge.ersu.net/mobileapp/get_content_detail.php?post=8407");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StreamingModel>(data);
            }

            return null;
        }

        public async Task<LaternaModel> GetLaternas()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_laterna.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LaternaModel>(data);
            }

            return null;
        }

        public async Task<LateliveModel> GetLatelives()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_latelive.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LateliveModel>(data);
            }

            return null;
        }

        public async Task<TravelModel> GetTravels()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_geziler.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TravelModel>(data);
            }

            return null;
        }

        public async Task<ContentModel> GetContents(int limit, int page)
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_content_main.php?limit=" + limit + "&page=" + page);

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContentModel>(data);
            }

            return null;
        }

        public async Task<ContentDetailModel> GetContentDetail(string id)
        {
            HttpResponseMessage req = await client.GetAsync("http://ozge.ersu.net/mobileapp/get_content_detail.php?post=" + id);

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContentDetailModel>(data);
            }

            return null;
        }

        public async Task<InstagramDataModel> InstagramData(string id = "")
        {
            HttpResponseMessage req = await client.GetAsync("https://www.instagram.com/ozgeersu/media" + (!string.IsNullOrEmpty(id) ? "?max_id=" + id : "/"));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<InstagramDataModel>(data);
            }

            return null;
        }

        public async Task<VideoModel> GetVideos()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_video.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VideoModel>(data);
            }

            return null;
        }

        public async Task<BiographyModel> GetBiography()
        {
            HttpResponseMessage req = await client.GetAsync("http://ozge.ersu.net/mobileapp/get_resume.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BiographyModel>(data);
            }

            return null;
        }

        public async Task GetContact(ContactModel model)
        {
            HttpResponseMessage req = await client.PostAsync("http://ersu.net/appwebservice/send_message.php", new FormUrlEncodedContent(
               new Dictionary<string, string>()
               {
                   { "isimSoyisim", model.isimSoyisim },
                   { "email", model.email},
                   { "gsm", model.gsm },
                   { "ilteti", model.ileti }

               }));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
            }
        }

        public async Task<NotificationModel> GetNotifications()
        {
            HttpResponseMessage req = await client.GetAsync("http://ersu.net/appwebservice/get_notification.php");

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NotificationModel>(data);
            }

            return null;
        }

        public async Task GetJoinTravel(TravelReqModel model)
        {
            HttpResponseMessage req = await client.PostAsync("http://ersu.net/appwebservice/rezervasyon.php", new FormUrlEncodedContent(
               new Dictionary<string, string>()
               {
                               { "isimSoyisim", model.isimSoyisim },
                               { "email", model.email},
                               { "gsm", model.gsm },
                               { "ilteti", model.turname }

               }));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
            }
        }
    }
}