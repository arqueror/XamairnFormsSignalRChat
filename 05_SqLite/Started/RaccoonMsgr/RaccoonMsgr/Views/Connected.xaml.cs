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
        ConnectedViewModel vm;
        private Messages detailPage;
        public Connected()
        {
            InitializeComponent();
            vm = new ConnectedViewModel(Navigation, "Contacts");

            this.BindingContext = vm;

            vm.OnMessagePassingToDetail += (s, message) =>
            {
                if (detailPage != null && vm.IsDetailOpened)
                    detailPage.AddMessage(message);
            };
            //vm.StartListening();
            //contactsList.ItemsSource = vm.Contacts;
            //conversationsList.ItemsSource = vm.Contacts;

        }
        private void OnDetailPaseClosed()
        {
            vm.IsDetailOpened = false;
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
            detailPage = new Messages(contact);
            vm.IsDetailOpened = true;
            detailPage.PageClosedCallback = OnDetailPaseClosed;
            vm.NavigateToDetail(detailPage, contact.Name);
        }
    }
}
