using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaccoonMsgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageMenuItem;
            Page page =new MainPage();                                      //Default home page
            if (item == null)
                return;
            if (item.TargetType == typeof(MainPage))
            {
                page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
               
            }

            this.IsPresented = false;                                       //Close left menu
            Detail = new NavigationPage(page);
            masterPage.ListView.SelectedItem = null;
        }
    }
}