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
        public bool IsDetailOpened { get; set; } = false;
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();
        public event EventHandler<Models.Message> OnMessagePassingToDetail;
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                RaisePropertyChanged(nameof(Contacts));
            }


        }
        public ConnectedViewModel(INavigation navigation, string pageTitle) : base(navigation, pageTitle)
        {
            ChatService.Instance.OnConnectedUsersReceived += (s, a) =>
            {
                var tmpContacts = new ObservableCollection<Contact>();
                a.ForEach(c =>
                {
                    tmpContacts.Add(new Contact() { Name = c.Name, BackgroundImageColor = Color.Green, UnreadMessages = 0 });
                });
                Contacts = tmpContacts;
            };
            ChatService.Instance.OnMessageReceived += (s, a) =>
            {
                if (a != null)
                {

                    var msg = a as Message;
                    if (!IsDetailOpened)
                        ShowToast(string.Format("New message from {0}", msg.Name), msg.Text);
                    else
                        OnMessagePassingToDetail(this, msg);
                    //todo depending on msg.Name, store it on database conversation
                    //so user can see it later frm Conversations page
                }

            };

        }
        public void StartListening()
        {
            ChatService.Instance.Connect();

        }
        public async System.Threading.Tasks.Task RefreshUsers()
        {
            await ChatService.Instance.RefreshUsersList();

        }

    }
}
