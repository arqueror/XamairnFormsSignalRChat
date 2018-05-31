using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RaccoonMsgr.Models;
using RaccoonMsgr.Views;

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

        public MasterPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
            {
                new MasterPageMenuItem { Id = 0, Title = "Connected" , TargetType = typeof(MainPage)},
                new MasterPageMenuItem { Id = 1, Title = "Messages",TargetType = typeof(MainPage) },
                new MasterPageMenuItem { Id = 2, Title = "Page 3" ,TargetType = typeof(MainPage)},
                new MasterPageMenuItem { Id = 3, Title = "Page 4" ,TargetType = typeof(MainPage)},
                new MasterPageMenuItem { Id = 4, Title = "Page 5" ,TargetType = typeof(MainPage)},
            });
        }
    }
}
