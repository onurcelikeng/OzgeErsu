using OzgeErsu.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Pdf;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu.Views
{
    public sealed partial class TravelDetailsView : Page
    {
        public static Travel travel;


        public TravelDetailsView()
        {
            this.InitializeComponent();
            this.Loaded += TravelDetailsView_Loaded;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested += DataShareControl;
        }

        private void DataShareControl(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "Özge Ersu Gezileri TM Signature Collection Grand Deluxe " + travel.gezi;
            e.Request.Data.SetWebLink(new Uri(travel.shareLink));
        }

        public async void OpenRemote(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var stream = await client.GetStreamAsync(url);
                var memStream = new MemoryStream();
                await stream.CopyToAsync(memStream);
                memStream.Position = 0;
                PdfDocument doc = await PdfDocument.LoadFromStreamAsync(memStream.AsRandomAccessStream());

                Load(doc);
            }

            catch (Exception)
            {

            }
        }

        async void Load(PdfDocument pdfDoc)
        {
            try
            {
                PdfPages.Clear();

                for (uint i = 0; i < pdfDoc.PageCount; i++)
                {
                    BitmapImage image = new BitmapImage();

                    var page = pdfDoc.GetPage(i);

                    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                    {
                        await page.RenderToStreamAsync(stream);
                        await image.SetSourceAsync(stream);
                    }

                    PdfPages.Add(image);
                }
                pdfviewer.ItemsSource = PdfPages;
            }

            catch (Exception)
            {

            }
        }

        public ObservableCollection<BitmapImage> PdfPages
        {
            get;
            set;
        } = new ObservableCollection<BitmapImage>();

        private void TravelDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            OpenRemote(travel.pdf);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void travelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(JoinTravelView));
        }

        private void shareButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}