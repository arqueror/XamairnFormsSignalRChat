using System;
using System.Collections.ObjectModel;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaccoonMsgr.ViewModels
{
    public class ConnectedViewModel : BaseViewModel
    {
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                RaisePropertyChanged(nameof(Contacts));
            }


        }
        private ChatService _chatServices;

        public ConnectedViewModel(INavigation navigation, string pageTitle) : base(navigation, pageTitle)
        {
            _chatServices = new ChatService();
            _chatServices.OnConnectedUsersReceived += (s, a) =>
            {
                var tmpContacts = new ObservableCollection<Contact>();
                a.ForEach(c =>
                {
                    tmpContacts.Add(new Contact() { Name = c.Name, BackgroundImageColor = Color.Green, UnreadMessages = 0 });
                });
                Contacts = tmpContacts;
            };

        }
        public void StartListening()
        {
            _chatServices.Connect();

        }
        public async System.Threading.Tasks.Task RefreshUsers()
        {
            await _chatServices.RefreshUsersList();

        }

    }
}
