using OzgeErsu.Models;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace OzgeErsu.Views
{
    public sealed partial class JoinTravelView : Page
    {

        public JoinTravelView()
        {
            this.InitializeComponent();
            DataContext = TravelDetailsView.travel.gezi;
        }


        private async void GetJoinTravelRequest()
        {
            try
            {
                if (!string.IsNullOrEmpty(nameSurname.Text) && !string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(gsm.Text))
                {
                    contactText.Text = "GÖNDERİLİYOR";

                    var model = new TravelReqModel()
                    {
                        isimSoyisim = nameSurname.Text,
                        email = email.Text,
                        gsm = gsm.Text,
                        turname = TravelDetailsView.travel.gezi
                    };

                    await App.Client.GetJoinTravel(model);

                    var newMessage = new MessageDialog("Sizinle en kısa zamanda bağlantıya geçeceğim. Özge Ersu", "Teşekkür ederim");
                    IUICommand result = await newMessage.ShowAsync();

                    nameSurname.Text = "";
                    email.Text = "";
                    gsm.Text = "";

                    contactText.Text = "GÖNDER";
                    Frame.GoBack();
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void travelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GetJoinTravelRequest();
        }
    }
}