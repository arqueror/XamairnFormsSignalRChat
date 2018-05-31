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
using RaccoonMsgr.Renderers;

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

            if (Device.RuntimePlatform == Device.Android)
            {
                var floatingButtonFactory = DependencyService.Get<IFloatingActionButtonRenderer>();
                var floatingButton = floatingButtonFactory.CreateFloatingActionButton(() =>
                {
                    DisplayAlert("NativeButton", "Native ANdroid FoatingButton", "Close");
                });
                //groupsGrid.Children.Add(floatingButton);

            }
            //vm.StartListening();
            //contactsList.ItemsSource = vm.Contacts;
            //conversationsList.ItemsSource = vm.Contacts;

        }
        private void OnDetailPaseClosed()
        {
            vm.IsDetailOpened = false;
            ChatServices.ChatService.Instance.RefreshUsersList();
            vm.GetConversationsHistory();
        }
        private void OnDetailPaseOpened()
        {
            vm.IsDetailOpened = true;
            ChatServices.ChatService.Instance.RefreshUsersList();
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
            detailPage.PageOpenedCallback = OnDetailPaseOpened;
            vm.NavigateToDetail(detailPage, contact.Name);
        }

        void GroupsButtonTapped(object sender, System.EventArgs e)
        {

        }
    }
}
