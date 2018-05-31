using System;
using System.Collections.Generic;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Views;
using Xamarin.Forms;

namespace RaccoonMsgr.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

        }

        void LoginUser(object sender, System.EventArgs e)
        {
            if (!(string.IsNullOrEmpty(usernameEntry.Text)))
            {
                //Store username in Settings
                Application.Current.Properties["username"] = usernameEntry.Text;
                Application.Current.SavePropertiesAsync();
                ChatService.Instance.Connect();
                Application.Current.MainPage = new NavigationPage(new RaccoonMsgr.Views.MasterPage.MasterPage());

            }
        }
    }
}
