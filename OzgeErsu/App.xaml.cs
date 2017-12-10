using OneSignalSDK_WP_WNS;
using OzgeErsu.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace OzgeErsu
{
    sealed partial class App : Application
    {
        public static DataClient Client = new DataClient();


        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }


        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            OneSignal.Init("c75f3c32-9afa-4a78-b425-edc6d4f9be68", e, notificationOpened);

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }

        private void notificationOpened(string message, IDictionary<string, string> additionalData, bool isActive)
        {
            System.Diagnostics.Debug.WriteLine("notificationOpened:message:" + message);
            System.Diagnostics.Debug.WriteLine("notificationOpened:additionalData:" + additionalData);
            System.Diagnostics.Debug.WriteLine("notificationOpened:isActive:" + isActive);
        }
    }
}