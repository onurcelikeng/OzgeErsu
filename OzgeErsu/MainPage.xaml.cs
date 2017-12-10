using System;
using OzgeErsu.Views;
using OzgeErsu.Models;
using OzgeErsu.Helpers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.Foundation;
using Windows.System.Profile;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using System.Collections.Generic;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls.Primitives;

namespace OzgeErsu
{
    public sealed partial class MainPage : Page
    {
        private FadeHelper fade = new FadeHelper();
        private string[] imageArray = new string[3];
        private static Information info;
        private bool isCredit = false;
        private static string page;
        private bool isLateradio;
        private string share;


        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.InitializeUI();
        }


        private async void InitializeUI()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
            }

            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop")
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(800, 1200));
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested += DataShareControl;

            imageArray[0] = "ms-appx:///Assets/Backgrounds/radio.png";
            isLateradio = true;
            lateRadioPlayer.CurrentStateChanged += LateRadioPlayer_CurrentStateChanged;

            if (page == null)
            {
                introMedia.Play();
                flipView.SelectedItem = Main;
                GetInformationRequest();
            }

            else if (page == "Lateradio")
            {
                introMedia.IsMuted = true;
                flipView.SelectedItem = Lateradio;
            }

            else if (page == "Video")
            {
                introMedia.IsMuted = true;
                flipView.SelectedItem = Video;
            }

            else
            {
                introMedia.IsMuted = true;
            }
        }

        private void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flipView.SelectedItem == Lateradio)
            {
                page = "Lateradio";
                biographyMedia.Stop();
                creditMedia.Stop();
            }

            else if (flipView.SelectedItem == Latelive)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                GetLateliveRequst();
            }

            else if (flipView.SelectedItem == Laterna)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                GetLaternaRequest();
            }

            else if (flipView.SelectedItem == Advertisement)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                advertisementImage.Source = new BitmapImage(new Uri(info.advertisement));
            }

            else if (flipView.SelectedItem == Latelive)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                GetLateliveRequst();
            }

            else if (flipView.SelectedItem == Travel)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                travelFrame.Navigate(typeof(TravelView));
            }

            else if (flipView.SelectedItem == Content)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                contentFrame.Navigate(typeof(ContentView));
            }

            else if (flipView.SelectedItem == Video)
            {
                page = "Video";
                biographyMedia.Stop();
                creditMedia.Stop();
                GetVideosRequest();
            }

            else if (flipView.SelectedItem == Instagram)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                instagramFrame.Navigate(typeof(InstagramView));
            }

            else if (flipView.SelectedItem == Biography)
            {
                if ((lateRadioPlayer.CurrentState == MediaElementState.Playing) || (laternaPlayer.CurrentState == MediaElementState.Playing) || (latelivePlayer.CurrentState == MediaElementState.Playing))
                {
                    biographyMedia.Stop();
                    creditMedia.Stop();
                }

                else
                {
                    isCredit = false;
                    myTimer.Tick += (sndr, f) =>
                    {
                        if (isCredit) return;

                        if (fade.Out(creditMedia))
                        {
                            if (biographyMedia.CurrentState != MediaElementState.Playing)
                            {
                                biographyMedia.Volume = 0;
                                biographyMedia.Play();
                            }
                            if (fade.In(biographyMedia))
                            {
                                myTimer.Stop();
                                myTimer.Tick -= (a, b) => { };
                            }
                        }
                    };
                    myTimer.Start();
                }

                GetBiographyrequest();
            }

            else if (flipView.SelectedItem == Credits)
            {
                if ((lateRadioPlayer.CurrentState == MediaElementState.Playing) || (laternaPlayer.CurrentState == MediaElementState.Playing) || (latelivePlayer.CurrentState == MediaElementState.Playing))
                {
                    biographyMedia.Stop();
                    creditMedia.Stop();
                }

                else
                {
                    isCredit = true;
                    myTimer.Tick += (sndr, f) =>
                    {
                        if (!isCredit) return;
                        if (fade.Out(biographyMedia))
                        {
                            if (creditMedia.CurrentState != MediaElementState.Playing)
                            {
                                creditMedia.Volume = 0;
                                creditMedia.Play();
                            }

                            if (fade.In(creditMedia))
                            {
                                myTimer.Stop();
                                isCredit = false;
                                myTimer.Tick -= (a, b) => { };
                            }
                        }
                    };
                    myTimer.Start();
                }

                creditsImage.Source = new BitmapImage(new Uri(info.credits));
                timer.Tick += Timer_Tick;
                timer.Start();
                IsTimemrStarted = true;
            }

            else if (flipView.SelectedItem == SocialMedia)
            {
                var tm = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(10)
                };
                tm.Tick += (sndr, f) =>
                {
                    if (fade.Out(biographyMedia) & fade.Out(creditMedia))
                    {
                        biographyMedia.Stop();
                        creditMedia.Stop();
                        tm.Stop();
                    }
                };
                tm.Start();
            }

            else if (flipView.SelectedItem == Contact)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
            }

            else if (flipView.SelectedItem == Notification)
            {
                biographyMedia.Stop();
                creditMedia.Stop();
                GetNotificationRequest();
            }
        }

        private void DataShareControl(DataTransferManager sender, DataRequestedEventArgs e)
        {
            if (share == "index")
            {
                e.Request.Data.Properties.Title = "ÖZGE ERSU | Yurt dışı gezi uzmanı, turizmci, profesyonel tursit rehberi, radyo belgesel yapımcısı, yayıncı, sunucu, gezi ve müzik yazarı Özge Ersu'nun ücretsiz içerik uygulaması tüm platformalarda #ozgeersu";
                e.Request.Data.SetWebLink(new Uri("http://ozge.ersu.net/guncel/ozge-ersu-icerik-uygulama/"));
            }

            else if (share == "lateradio" && !string.IsNullOrEmpty(lateradioTitle.Text))
            {
                e.Request.Data.Properties.Title = "#Lateradio by" + lateradioTitle.Text + "@ozgeersu dinliyorum";
                e.Request.Data.SetWebLink(new Uri("http://radyo.ersu.net"));
            }

            else if (share == "laterna")
            {
                e.Request.Data.Properties.Title = "Özge Ersu tarafından hazırlanan ve sunulan Laterna programını dinliyorum | #Laterna";
                e.Request.Data.SetWebLink(new Uri("http://radyo.ersu.net/laterna/muzikal-belgesel/"));
            }

            else if (share == "latelive")
            {
                e.Request.Data.Properties.Title = "Özge Ersu tarafından hazırlanıp canlı yayınlanan Latelive programını dinliyorum | #Latelive";
                e.Request.Data.SetWebLink(new Uri("http://radyo.ersu.net/lateradio/canli-yayin-kayitlari-latelive"));
            }

            else if (share == "biography")
            {
                e.Request.Data.Properties.Title = "Turizm Uzmanı, Profesyonel Turist Rehberi, Radyo Belgesel Yapımcısı, Sunucu, Gezi ve Müzik Yazarı Özge Ersu Hakkında";
                e.Request.Data.SetWebLink(new Uri("http://ozge.ersu.net/ozge-ersu/kimdir/"));
            }
        }

        private void CheckPlaying(int type)
        {
            if (type == 0)//lateradio
            {
                laternaPlayer.Pause();
                laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));

                latelivePlayer.Pause();
                latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            else if (type == 1)//laterna
            {
                lateRadioPlayer.Pause();
                lateradioCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/radio.png"));
                lateradioPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));

                latelivePlayer.Pause();
                latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            else if (type == 2)//latelive
            {
                lateRadioPlayer.Pause();
                lateradioCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/radio.png"));
                lateradioPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));

                laternaPlayer.Pause();
                laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            else
            {
                lateRadioPlayer.Pause();
                lateradioCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Backgrounds/radio.png"));
                lateradioPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));

                laternaPlayer.Pause();
                laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));

                latelivePlayer.Pause();
                latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            biographyMedia.Stop();
            creditMedia.Stop();
        }

        #region Button Events

        private static int index = 0;

        private void index_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var gridName = (sender as Grid).Name;

            if (gridName == "lateradioButton") flipView.SelectedItem = Lateradio;
            else if (gridName == "laternaButton") flipView.SelectedItem = Laterna;
            else if (gridName == "lateliveButton") flipView.SelectedItem = Latelive;
            else if (gridName == "travelButton") flipView.SelectedItem = Travel;
            else if (gridName == "contentButton") flipView.SelectedItem = Content;
            else if (gridName == "videoButton") flipView.SelectedItem = Video;
            else if (gridName == "instagramButton") flipView.SelectedItem = Instagram;
            else if (gridName == "biographyButton") flipView.SelectedItem = Biography;
            else if (gridName == "creditButton") flipView.SelectedItem = Credits;
            else if (gridName == "socialMediaButton") flipView.SelectedItem = SocialMedia;
            else if (gridName == "communicationButton") flipView.SelectedItem = Contact;
            else if (gridName == "notificationButton") flipView.SelectedItem = Notification;
        }

        private void share_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var imageName = (sender as Windows.UI.Xaml.Controls.Image).Name;

            if (imageName == "indexShare") share = "index";
            else if (imageName == "lateradioShare") share = "lateradio";
            else if (imageName == "laternaShare") share = "laterna";
            else if (imageName == "lateliveShare") share = "latelive";
            else if (imageName == "biographyShare") share = "biography";

            DataTransferManager.ShowShareUI();
        }

        private async void advertisementImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(info.ad_url));
        }

        private void streamingButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            page = "Lateradio";
            Frame.Navigate(typeof(StreamingView));
        }

        private async void socialMedia_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var gridName = (sender as Windows.UI.Xaml.Controls.Image).Name;

            if (gridName == "facebook") await Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/ozge.ersu"));
            else if (gridName == "instagram") await Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/ozgeersu/"));
            else if (gridName == "twitter") await Launcher.LaunchUriAsync(new Uri("https://twitter.com/ozgeersu"));
            else if (gridName == "linkedin") await Launcher.LaunchUriAsync(new Uri("https://www.linkedin.com/in/ozgeersu"));
            else if (gridName == "periscope") await Launcher.LaunchUriAsync(new Uri("https://www.periscope.tv/ozgeersu"));
        }

        private void contactButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GetContactRequest();
        }

        private void Notification_Tapped(object sender, TappedRoutedEventArgs e)
        {
            flipView.SelectedItem = Lateradio;
        }

        #endregion

        #region Selection Changed

        private DispatcherTimer myTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        private DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(10)
        };
        private bool IsTimemrStarted;

        private void Timer_Tick(object sender, object e)
        {

            if (creditsScroll.VerticalOffset == creditsScroll.ScrollableHeight)
            {
                creditsScroll.ChangeView(null, 1, null);
                return;
            }
            creditsScroll.ChangeView(null, creditsScroll.VerticalOffset + 1, null);
        }

        private bool IsLoadLaterna = false;
        private bool IsLoadLatelive = false;
        private bool IsCreateRowLatelive = false;

        private void LatelivePlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (IsLoadLatelive & latelivePlayer.CurrentState == MediaElementState.Paused)
            {
                CheckPlaying(2);
                latelivePlayer.Play();
                IsLoadLatelive = false;
            }
        }

        private void videoListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            page = "Video";

            Models.Video item = videoListView.SelectedItem as Models.Video;
            if (item == null) return;
            Frame.Navigate(typeof(VideoDetailsView), item.link);
        }

        #endregion

        #region DataClient Request

        private async void GetInformationRequest()
        {
            try
            {
                InformationModel model = await App.Client.GetInformations();

                if (model.status == "true")
                {
                    info = new Information()
                    {
                        advertisement = model.filebase + model.feeds[0].advertisement,
                        ad_url = model.feeds[0].ad_url,
                        applicationGenericShare = model.feeds[0].applicationGenericShare,
                        credits = model.filebase + model.feeds[0].credits,
                        id = model.feeds[0].id,
                        laternaMainShare = model.feeds[0].laternaMainShare,
                        priority = model.feeds[0].priority,
                        toursMainShare = model.feeds[0].toursMainShare
                    };
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetLateradioRequest()
        {
            try
            {
                LateradioModel model = await App.Client.GetLateradio();

                if (model.status == "true")
                {
                    var lateradio = new Lateradio()
                    {
                        id = model.feeds[0].id,
                        notification = model.filebase + model.feeds[0].notification,
                        programflow = model.filebase + model.feeds[0].programflow,
                        nowplaying = model.filebase + model.feeds[0].nowplaying,
                        porturl = model.feeds[0].porturl
                    };

                    imageArray[1] = lateradio.nowplaying;
                    lateradioCover.Source = new BitmapImage(new Uri(lateradio.nowplaying));
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetLateradioArtistRequest(string artist)
        {
            var model = await App.Client.GetLateradioArtist(artist);
        }

        private async void GetLaternaRequest()
        {
            try
            {
                List<Laterna> laternaList = new List<Laterna>();
                LaternaModel model = await App.Client.GetLaternas();

                if (model.status == "true")
                {
                    for (int i = 0; i < model.feeds.Count; i++)
                    {
                        var laterna = new Laterna()
                        {
                            id = model.feeds[i].id,
                            priority = model.feeds[i].priority,
                            audio = model.feeds[i].audio,
                            splash = model.filebase + model.feeds[i].splash,
                            program = model.feeds[i].program,
                            active = model.feeds[i].active
                        };

                        laternaList.Add(laterna);
                    }

                    laternaListView.ItemsSource = laternaList;
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetLateliveRequst()
        {
            try
            {
                List<Latelive> lateliveList = new List<Latelive>();
                LateliveModel model = await App.Client.GetLatelives();

                if (model.status == "true")
                {
                    for (int i = 0; i < model.feeds.Count; i++)
                    {
                        var latelive = new Latelive()
                        {
                            id = model.feeds[i].id,
                            priority = model.feeds[i].priority,
                            audio = model.feeds[i].audio,
                            splash = model.filebase + model.feeds[i].splash,
                            program = model.feeds[i].program,
                            active = model.feeds[i].active
                        };

                        lateliveList.Add(latelive);
                    }

                    lateliveListView.ItemsSource = lateliveList;
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetVideosRequest()
        {
            try
            {
                List<Video> videoList = new List<Video>();
                VideoModel model = await App.Client.GetVideos();

                if (model.status == "true")
                {
                    for (int i = 0; i < model.feeds.Count; i++)
                    {
                        var video = new Models.Video()
                        {
                            id = model.feeds[i].id,
                            priority = model.feeds[i].priority,
                            video = model.feeds[i].video,
                            splash = model.filebase + model.feeds[i].splash,
                            link = model.feeds[i].link,
                            active = model.feeds[i].active
                        };

                        videoList.Add(video);
                    }

                    videoListView.ItemsSource = videoList;
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetBiographyrequest()
        {
            try
            {
                BiographyModel model = await App.Client.GetBiography();

                if (model.status == "true")
                {
                    biographyWebView.NavigateToString(model.feeds[0].post_content);
                }
            }

            catch (Exception)
            {

            }
        }

        private async void GetContactRequest()
        {
            try
            {
                if (!string.IsNullOrEmpty(nameSurname.Text) && !string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(gsm.Text) && !string.IsNullOrEmpty(message.Text))
                {
                    contactText.Text = "GÖNDERİLİYOR";

                    var model = new ContactModel()
                    {
                        email = email.Text,
                        gsm = gsm.Text,
                        ileti = message.Text,
                        isimSoyisim = nameSurname.Text
                    };

                    await App.Client.GetContact(model);

                    var newMessage = new MessageDialog("Ayırdığınız zaman için teşekkür ederim. Özge Ersu", "İletiniz gönderilmiştir");
                    IUICommand result = await newMessage.ShowAsync();

                    nameSurname.Text = "";
                    email.Text = "";
                    gsm.Text = "";
                    message.Text = "";

                    contactText.Text = "GÖNDER";
                }

                else
                {
                    var newMessage = new MessageDialog("Eksik bilgi girdiniz.", "Lütfen");
                    IUICommand result = await newMessage.ShowAsync();
                }
            }

            catch (Exception)
            {
                contactText.Text = "GÖNDER";
            }
        }

        private async void GetNotificationRequest()
        {
            try
            {
                NotificationModel model = await App.Client.GetNotifications();

                if (model.status == "true")
                {
                    notificationImage.Source = new BitmapImage(new Uri(model.filebase + model.feeds[0].image, UriKind.Absolute));
                }
            }

            catch (Exception)
            {

            }
        }

        #endregion

        #region Lateradio Section

        private void lateradioCover_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (index % 3 == 0 && imageArray[0] != null)
            {
                lateradioCover.Source = new BitmapImage(new Uri(imageArray[0]));
            }

            else if (index % 3 == 1 && imageArray[1] != null)
            {
                lateradioCover.Source = new BitmapImage(new Uri(imageArray[1]));
            }

            else if (index % 3 == 2 && imageArray[2] != null)
            {
                lateradioCover.Source = new BitmapImage(new Uri(imageArray[2]));
            }

            index++;
        }

        private void playerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lateRadioPlayer.CurrentState == MediaElementState.Playing)
            {
                lateRadioPlayer.Pause();
                lateradioTitle.Text = "";
                imageArray[1] = null;
                imageArray[2] = null;
                lateradioPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
                lateradioCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Sample/radio.png"));
            }

            else if (lateRadioPlayer.CurrentState == MediaElementState.Paused)
            {
                lateRadioPlayer.Play();
                lateradioPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pause.png"));
            }

            CheckPlaying(0);
        }

        private void LateRadioPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (lateRadioPlayer.CurrentState == MediaElementState.Playing)
            {
                if (isLateradio == true)
                {
                    isLateradio = false;
                    lateRadioPlayer.Volume = 0;

                    lateradioMedia.Play();
                    lateradioMedia.CurrentStateChanged += LateradioMedia_CurrentStateChanged;
                }

                CheckPlaying(0);
            }
        }

        private async void LateradioMedia_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (lateradioMedia.CurrentState == MediaElementState.Paused)
            {
                lateRadioPlayer.Volume = 1;
                string artistName = await App.Client.GetLateradioTitle();

                if (artistName != null)
                {
                    lateradioTitle.Text = artistName;

                    ArtistModel artist = await App.Client.GetLateradioArtist(artistName.Split('-')[0]);
                    if (artist.artist != null)
                    {
                        imageArray[2] = artist.artist.image[4].text;
                    }
                }

                GetLateradioRequest();
            }
        }

        private void volumaSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (lateRadioPlayer.Volume > volumaSlider.Value / 100)
            {
                lateRadioPlayer.Volume -= 0.1;
            }

            else if (lateRadioPlayer.Volume < volumaSlider.Value / 100)
            {
                lateRadioPlayer.Volume += 0.1;
            }
        }

        #endregion

        #region Laterna Section

        private DispatcherTimer laternaPosition = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };

        private void LaternaPosition_Tick(object sender, object e)
        {
            TimeSpan time = laternaPlayer.Position;
            laternaCurrentPosition.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
            laternaSlider.Value = time.TotalSeconds;
        }

        private void laternaListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Laterna item = laternaListView.SelectedItem as Laterna;

            if (item == null) return;

            IsLoadLaterna = true;

            laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pause.png"));
            laternaPlayerPanel.Visibility = Visibility.Visible;
            laternaPlayerTitle.Text = item.program;
            laternaPlayer.Source = new Uri(item.audio);
            laternaPlayer.CurrentStateChanged += LaternaPlayer_CurrentStateChanged;
        }

        private void laternaPlayerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (laternaPlayer.CurrentState == MediaElementState.Playing)
            {
                laternaPlayer.Pause();
                laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            else //if (laternaPlayer.CurrentState == MediaElementState.Paused)
            {
                laternaPlayer.Play();
                laternaPlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pause.png"));
                CheckPlaying(1);
            }           
        }

        private void LaternaPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (IsLoadLaterna & laternaPlayer.CurrentState == MediaElementState.Paused)
            {
                CheckPlaying(1);
                laternaPlayer.Play();
                IsLoadLaterna = false;
            }

            TimeSpan t = laternaPlayer.NaturalDuration.TimeSpan;
            laternaTotalDuration.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            laternaSlider.Maximum = laternaPlayer.NaturalDuration.TimeSpan.TotalSeconds;

            laternaPosition.Start();
            laternaPosition.Tick += LaternaPosition_Tick;
        }

        #endregion

        #region Latelive Seciton

        private DispatcherTimer latelivePosition = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(1000)
        };

        private void LatelivePosition_Tick(object sender, object e)
        {
            TimeSpan time = latelivePlayer.Position;
            lateliveCurrentPosition.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
            lateliveSlider.Value = time.TotalSeconds;
        }

        private void lateliveListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Latelive item = (Latelive)lateliveListView.SelectedItem;

            if (item == null) return;

            IsLoadLatelive = true;

            latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pause.png"));
            latelivePlayerPanel.Visibility = Visibility.Visible;
            latelivePlayerTitle.Text = item.program;
            latelivePlayer.Source = new Uri(item.audio);
            latelivePlayer.CurrentStateChanged += LatelivePlayer_CurrentStateChanged1;
        }

        private void latelivelayerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (latelivePlayer.CurrentState == MediaElementState.Playing)
            {
                latelivePlayer.Pause();
                latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/play.png"));
            }

            else //if (latelivePlayer.CurrentState == MediaElementState.Paused)
            {
                latelivePlayer.Play();
                latelivePlayerButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pause.png"));
                CheckPlaying(2);
            }
        }

        private void LatelivePlayer_CurrentStateChanged1(object sender, RoutedEventArgs e)
        {
            if (IsLoadLatelive & latelivePlayer.CurrentState == MediaElementState.Paused)
            {
                CheckPlaying(2);
                latelivePlayer.Play();
                IsLoadLatelive = false;
            }

            TimeSpan t = latelivePlayer.NaturalDuration.TimeSpan;
            lateliveTotalDuration.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            lateliveSlider.Maximum = latelivePlayer.NaturalDuration.TimeSpan.TotalSeconds;

            latelivePosition.Start();
            latelivePosition.Tick += LatelivePosition_Tick;
        }

        #endregion
    }
}