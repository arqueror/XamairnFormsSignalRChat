using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RaccoonMsgr.Models;
using RaccoonMsgr.Views;
using Xamarin.Forms;

namespace RaccoonMsgr.ViewModels
{
    class MasterPageMasterViewModel : BaseViewModel
    {
        private ObservableCollection<MasterPageMenuItem> _menuItems { get; set; }

        public ObservableCollection<MasterPageMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                if (value != null)
                {
                    _menuItems = value;
                    RaisePropertyChanged();
                }
            }
        }
        public MasterPageMasterViewModel(INavigation navigation, string pageTitle) : base(navigation, pageTitle)
        {

        }
        public MasterPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
            {
                new MasterPageMenuItem { Id = 0, Title = "Home" , TargetType = typeof(MainPage),Icon="ic_home_white.png",IsBadgeVisible=false},
                new MasterPageMenuItem { Id = 1, Title = "Contacts" , TargetType = typeof(Connected),Icon="ic_people_white.png",BadgeColor=Color.Green}
            });
        }
    }
}
