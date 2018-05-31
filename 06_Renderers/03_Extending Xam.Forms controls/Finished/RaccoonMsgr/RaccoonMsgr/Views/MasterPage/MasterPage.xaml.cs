using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RaccoonMsgr.ViewModels;
using RaccoonMsgr.Controls;
using RaccoonMsgr.ChatServices;

namespace RaccoonMsgr.Views.MasterPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        private static VisualElement currView { get; set; }

        public MasterPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += ListView_ItemSelected;

            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            currView = this;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageMenuItem;

            Page page = new MainPage();                                      //Default home page
            if (item == null)
                return;

            page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;


            masterPage.ListView.SelectedItem = null;
            this.IsPresented = false;

            //Close left menu
            Detail = new NavigationPage(page);
            ChatService.Instance.RefreshUsersList();
        }
    }
}