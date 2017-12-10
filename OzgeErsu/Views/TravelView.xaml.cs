using OzgeErsu.Models;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu.Views
{
    public sealed partial class TravelView : Page
    {

        public TravelView()
        {
            this.InitializeComponent();
            this.GetTravelsRequest();
        }


        private async void GetTravelsRequest()
        {
            try
            {
                List<Travel> travelList = new List<Travel>();
                TravelModel model = await App.Client.GetTravels();

                if (model.status == "true")
                {
                    for (int i = 0; i < model.feeds.Count; i++)
                    {
                        var travel = new Travel()
                        {
                            id = model.feeds[i].id,
                            active = model.feeds[i].active,
                            gezi = model.feeds[i].gezi,
                            image = model.filebase + model.feeds[i].image,
                            pdf = model.feeds[i].pdf,
                            priority = model.feeds[i].priority,
                            shareLink = model.feeds[i].shareLink
                        };

                        travelList.Add(travel);
                    }

                    travelListView.ItemsSource = travelList;
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
            e.Request.Data.Properties.Title = "Özge Ersu Gezileri TM Signature Collction Grand Deluxe ";
            e.Request.Data.SetWebLink(new Uri("http://ozge.ersu.net/gezi-programlari/2016-ozge-ersu-gezileri/"));
        }

        private void travelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TravelDetailsView.travel = travelListView.SelectedItem as Travel;
            Frame.Navigate(typeof(TravelDetailsView));
        }

        private void travelShare_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}