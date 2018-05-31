using System;
using System.Collections.ObjectModel;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using RaccoonMsgr.Services;
using RaccoonMsgr.Utils;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace RaccoonMsgr.ViewModels
{
    public class ConnectedViewModel : BaseViewModel
    {
        public bool IsDetailOpened { get; set; } = false;
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();
        private ObservableCollection<Contact> _conversations = new ObservableCollection<Contact>();

        public event EventHandler<Models.Message> OnMessagePassingToDetail;
        public ObservableCollection<Contact> Conversations
        {
            get { return _conversations; }
            set
            {
                _conversations = value;
                RaisePropertyChanged(nameof(Conversations));
            }


        }
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
            ChatService.Instance.OnConnectedUsersReceived += OnConnectedUsersReceived;
            ChatService.Instance.OnMessageReceived += OnMessageReceivedReceived;
            App.OnApplicationResumed += OnAppResumed;
            GetConversationsHistory();
        }
        private async void OnAppResumed(object sender, EventArgs a)
        {
            ChatService.Instance.OnConnectedUsersReceived -= OnConnectedUsersReceived;
            ChatService.Instance.OnMessageReceived -= OnMessageReceivedReceived;
            ChatService.Instance.OnConnectedUsersReceived += OnConnectedUsersReceived;
            ChatService.Instance.OnMessageReceived += OnMessageReceivedReceived;
        }
        private async void OnMessageReceivedReceived(object sender, Message a)
        {
            if (a != null)
            {

                var msg = a as Message;
                msg.ConversationId = msg.Name;
                // depending on msg.Name (username), store it on database conversation
                //so user can see it later frm Conversations page
                await DBService.Instance.Update<Message>(msg);

                if (!IsDetailOpened)
                {
                    var matchingConv = Conversations.Where(u => u.Name == msg.Name).FirstOrDefault();
                    if (matchingConv != null)
                    {
                        //update already filled collection
                        Conversations[Conversations.IndexOf(matchingConv)].Messages.Add(msg);
                        Conversations[Conversations.IndexOf(matchingConv)].UnreadMessages++;
                    }
                    await ShowToast(string.Format("New message from {0}", msg.Name), msg.Text);
                }
                else
                    OnMessagePassingToDetail(this, msg);
            }
        }
        private void OnConnectedUsersReceived(object sender, ObservableCollection<Contact> a)
        {
            var tmpContacts = new ObservableCollection<Contact>();
            a.ForEach(c =>
            {
                tmpContacts.Add(new Contact() { Name = c.Name, BackgroundImageColor = Color.Green, UnreadMessages = 0 });
            });
            Contacts = tmpContacts;
        }
        public async Task RefreshUsers()
        {
            await ChatService.Instance.RefreshUsersList();

        }
        public async Task GetConversationsHistory()
        {
            var messages = await DBService.Instance.GetAll<Message>();
            messages = messages.OrderBy(user => user.Name).ToList();
            if (messages.Count > 0)
            {
                var tmpCollection = new ObservableCollection<Contact>();
                var currUser = messages[0];
                var currModel = new Contact();
                currModel.Name = currUser.ConversationId;

                messages.ForEach(m =>
                {
                    if (m.ConversationId == currUser.ConversationId)
                    {
                        currModel.Messages.Add(m);
                    }
                    else
                    {
                        //Change currUser and Add to collection old one    
                        tmpCollection.Add(currModel);

                        currUser.ConversationId = m.ConversationId;
                        currModel = new Contact();
                        currModel.Messages.Add(m);
                    }

                });
                //If Only 1 conversation
                if (tmpCollection.Count <= 0)
                {
                    tmpCollection.Add(currModel);
                }
                //Trigger UI refresh
                Conversations = tmpCollection;
            }
        }

    }
}
