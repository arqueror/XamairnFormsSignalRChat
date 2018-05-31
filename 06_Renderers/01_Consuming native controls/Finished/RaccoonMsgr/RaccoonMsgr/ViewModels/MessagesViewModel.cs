using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using RaccoonMsgr.Services;
using System.Collections.Generic;
using RaccoonMsgr.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace RaccoonMsgr.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public bool _isBusy = false;
        public string _overlayText;

        public event EventHandler<Message> OnMessageReceived;
        private ObservableCollection<Message> _messagesList;
        public ObservableCollection<Message> MessagesList
        {
            get => _messagesList;
            set
            {
                if (value != null)
                {
                    _messagesList = value;
                    RaisePropertyChanged();
                }
            }
        }
        public ICommand SendCommand { get; set; }
        string _outMessage = string.Empty;
        private ChatService _chatServices;
        private Contact _contact;
        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                RaisePropertyChanged();
                GetConversationMessages();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(); }
        }
        public string OverlayText
        {
            get { return _overlayText; }
            set { _overlayText = value; RaisePropertyChanged(); }
        }
        public string OutMessage
        {
            get { return _outMessage; }
            set { _outMessage = value; RaisePropertyChanged(); }
        }
        public void AddMessage(Message msg)
        {
            if (msg != null)
            {
                //check message comes from current conversation user
                if (msg.Name == Contact.Name)
                {
                    //retrieve messages using ConversationId (DeviceId+username) 
                    DBService.Instance.Get<Message>(x => x.ConversationId == msg.ConversationId).ContinueWith(messages =>
                    {
                        if (messages.Result != null)
                        {
                            MessagesList = messages.Result.OrderBy(m => m.MessageDateTime).ToObservableCollection();

                            OnMessageReceived(this, msg);


                        }

                    });

                }
                else
                {
                    //Store message in DB, so this can be retrieved later from Conversations page
                }
            }
        }
        private async Task GetConversationMessages()
        {
            var conversationId = Contact.Name;
            var messages = await DBService.Instance.Get<Message>(x => x.ConversationId == conversationId);
            if (messages != null)
            {
                MessagesList = messages.OrderBy(m => m.MessageDateTime).ToObservableCollection();
                OnMessageReceived(this, null);
            }

        }
        public MessagesViewModel(INavigation navigation, string pageTitle) : base(navigation, pageTitle)
        {

            MessagesList = new ObservableCollection<Message>();

            SendCommand = new Command(() =>
           {
               if (!String.IsNullOrWhiteSpace(OutMessage))
               {
                   var message = new Message
                   {
                       Text = OutMessage,
                       IsTextIn = false,
                       MessageDateTime = DateTime.Now,
                       ConversationId = Contact.Name
                   };

                   if (!MessagesList.Contains(message))
                       MessagesList.Add(message);
                   //store new message in DB
                   DBService.Instance.Update<Message>(message);
                   OutMessage = "";
                   ChatService.Instance.SendMessage(message.Text, Contact.Name);
                   OnMessageReceived(this, null);
               }

           });
        }
    }
}
