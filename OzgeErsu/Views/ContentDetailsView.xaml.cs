using System;
using OzgeErsu.Models;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu.Views
{
    public sealed partial class ContentDetailsView : Page
    {
        private ContentDetailModel model;
        public static string id;


        public ContentDetailsView()
        {
            this.InitializeComponent();
            this.Loaded += ContentDetailsView_Loaded;
        }


        private async void GetContentDetailsRequest()
        {
            try
            {
                model = await App.Client.GetContentDetail(id);

                if (model.status == "true")
                {
                    webView.NavigateToString(model.feeds[0].post_content);
                }
            }

            catch (Exception)
            {

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested += DataShareControl;
        }

        private void DataShareControl(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "Özge Ersu tarafından hazırlanan " + model.feeds[0].post_title + " konulu içerik";
            e.Request.Data.SetWebLink(new Uri(model.feeds[0].guid));
        }

        private void ContentDetailsView_Loaded(object sender, RoutedEventArgs e)
        {
            GetContentDetailsRequest();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void shareButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}