using OzgeErsu.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace OzgeErsu.Views
{
    public sealed partial class InstagramView : Page
    {
        private InstagramDataModel instagramData;


        public InstagramView()
        {
            this.InitializeComponent();
            this.GetInstagramRequest();
        }


        private async void GetInstagramRequest()
        {
            progress.IsIndeterminate = true;

            try
            {
                List<Instagram> instagramList = new List<Models.Instagram>();
                instagramData = await App.Client.InstagramData();

                if (instagramData.status == "ok")
                {
                    instagramListView.ItemsSource = instagramData.items;
                }
            }

            catch (Exception)
            {

            }

            progress.IsIndeterminate = false;
        }

        private async void instagramListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            int index = args.ItemIndex;
            if (index == instagramListView.Items.Count - 1 && instagramData.more_available)
            {
                Item item = (Item)args.Item;
                var data = await App.Client.InstagramData(item.id);
                if (data != null && data.items.Count > 0)
                    foreach (Item i in data.items)
                    {
                        instagramData.items.Add(i);
                    }
            }
        }
    }
}