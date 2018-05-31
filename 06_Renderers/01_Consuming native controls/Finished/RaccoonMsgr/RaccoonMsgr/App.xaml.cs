using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Controls;
using RaccoonMsgr.Views;
using RaccoonMsgr.Views.MasterPage;
using Xamarin.Forms;

namespace RaccoonMsgr
{
    public partial class App : Application
    {
        public static event EventHandler OnApplicationResumed;
        public App()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("username"))
            {
                ChatService.Instance.Connect();
                MainPage = new NavigationPage(new RaccoonMsgr.Views.MasterPage.MasterPage());

            }
            else
                MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            ChatServices.ChatService.Instance.Disconnect();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            ChatServices.ChatService._instanceHolder = new Lazy<ChatService>(() => new ChatService());
            if (OnApplicationResumed != null)
                OnApplicationResumed(this, null);
            ChatServices.ChatService.Instance.Connect();
        }
    }
}
