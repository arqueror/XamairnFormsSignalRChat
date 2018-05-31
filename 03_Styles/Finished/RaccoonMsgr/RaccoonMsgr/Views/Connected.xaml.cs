using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Messier16.Forms.Controls;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using RaccoonMsgr.Models;
using System.Linq;
using Xamanimation;
using RaccoonMsgr.Controls;
using RaccoonMsgr.ViewModels;

namespace RaccoonMsgr.Views
{

    public partial class Connected : PlatformTabbedPage
    {
        public ObservableCollection<Contact> Contacts = new ObservableCollection<Contact>();
        BaseViewModel vm;
        public Connected()
        {
            InitializeComponent();
            vm = new BaseViewModel(Navigation, "Contacts");
            Contacts = new ObservableCollection<Contact>() {
                new Contact(){UnreadMessages=3,Name="Usuario 1",BackgroundImageColor=Color.Green},
                new Contact(){Name="Usuario 2"},
                new Contact(){Name="Usuario 3",BackgroundImageColor=Color.Green},
                new Contact(){UnreadMessages=10,Name="Usuario 4"},
            };
            contactsList.ItemsSource = Contacts;
            conversationsList.ItemsSource = Contacts;

        }

        public void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as SearchBar).Text;
            if (string.IsNullOrEmpty(filter))
            {
                contactsList.ItemsSource = Contacts;
                return;
            }
            var filteredContacts = new ObservableCollection<Contact>(Contacts.Where(c => c.Name.ToLower().Contains(filter.ToLower())));
            contactsList.ItemsSource = filteredContacts;
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var contact = e.Item as Contact;

            vm.NavigateToDetail(new Messages(), contact.Name);
        }
    }
}
