using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu.Views
{
    public sealed partial class VideoDetailsView : Page
    {

        public VideoDetailsView()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            this.InitializeComponent();
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string videoUrl = await VimeoHelpers.GetVideoUrl(e.Parameter.ToString());

                videoPlayer.Source = new Uri(videoUrl);

                base.OnNavigatedTo(e);
            }

            catch (Exception)
            {

            }
        }

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bool ignored = false;
            this.BackRequested(ref ignored);
        }

        private void BackRequested(ref bool handled)
        {
            if (this.Frame == null)
                return;

            if (this.Frame.CanGoBack && !handled)
            {
                handled = true;
                this.Frame.GoBack();
            }
        }

        #endregion
    }
}