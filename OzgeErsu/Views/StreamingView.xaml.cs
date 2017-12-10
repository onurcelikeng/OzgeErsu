using OzgeErsu.Models;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OzgeErsu.Views
{
    public sealed partial class StreamingView : Page
    {

        public StreamingView()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            this.InitializeComponent();
            this.Loaded += StreamingView_Loaded;
        }


        private async void GetStreamingRequest()
        {
            try
            {
                progress.IsIndeterminate = true;

                StreamingModel model = await App.Client.GetStreaming();

                if (model.status == "true")
                {
                    webView.NavigateToString(model.feeds[0].post_content);
                }

                progress.IsIndeterminate = false;
            }

            catch (Exception)
            {
                progress.IsIndeterminate = false;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void StreamingView_Loaded(object sender, RoutedEventArgs e)
        {
            GetStreamingRequest();
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
