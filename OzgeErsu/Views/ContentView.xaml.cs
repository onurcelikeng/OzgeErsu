using OzgeErsu.Models;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu.Views
{
    public sealed partial class ContentView : Page
    {
        private List<Content> contentList = new List<Content>();
        private int page = 0;


        public ContentView()
        {
            this.InitializeComponent();
            this.GetContentsRequest(20, page); //buraya bakılacak
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested += DataShareControl;
        }

        private void DataShareControl(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = "Özge Ersu tarafından hazırlanan içerik |";
            e.Request.Data.SetWebLink(new Uri("http://ozge.ersu.net/yazilar"));
        }

        private async void GetContentsRequest(int limit, int page)
        {
            try
            {
                ContentModel model = await App.Client.GetContents(limit, page);

                if (model.status == "true")
                {
                    for (int i = 0; i < model.feeds.Count; i++)
                    {
                        var content = new Content()
                        {
                            ID = model.feeds[i].ID,
                            guid = model.feeds[i].guid,
                            image = model.feeds[i].image,
                            post_title = model.feeds[i].post_title
                        };

                        contentList.Add(content);
                    }

                    contentListView.ItemsSource = contentList;
                }
            }

            catch (Exception)
            {

            }
        }

        private void contentShare_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void contentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentDetailsView.id = (contentListView.SelectedItem as Content).ID;
            Frame.Navigate(typeof(ContentDetailsView));
        }

        private void contentListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            int index = args.ItemIndex;
            if (index == contentListView.Items.Count - 1)
            {
                page++;
                Content item = (Content)args.Item;
                GetContentsRequest(20, page);
            }
        }
    }
}