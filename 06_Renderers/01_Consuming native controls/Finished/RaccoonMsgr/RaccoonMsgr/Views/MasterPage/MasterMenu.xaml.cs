using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RaccoonMsgr.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaccoonMsgr.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenu : ContentPage
    {
        public ListView ListView;

        public MasterMenu()
        {
            InitializeComponent();
            BindingContext = new MasterPageMasterViewModel();
            ListView = MenuItems;
        }
    }
}