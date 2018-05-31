using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RaccoonMsgr.ChatServices;
using RaccoonMsgr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RaccoonMsgr.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public bool _isBusy = false;
        public string _overlayText;

        public event EventHandler<Message> OnMessageReceived;
        public ObservableCollection<Message> MessagesList { get; }
        public ICommand SendCommand { get; set; }
        string _outMessage = string.Empty;
        private ChatService _chatServices;
        public Contact Contact { get; set; }

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
                    MessagesList.Add(msg);
                    OnMessageReceived(this, msg);
                }
                else
                {
                    //Store message in DB, so this can be retrieved later from Conversations page
                }
            }
        }
        public MessagesViewModel(INavigation navigation, string pageTitle) : base(navigation, pageTitle)
        {
            MessagesList = new ObservableCollection<Message>();

            //ChatService.Instance.OnMessageReceived += (s, a) =>
            //{
            //    if (a != null)
            //    {
            //        var msg = a as Message;
            //        //check message comes from current conversation user
            //        if (msg.Name == Contact.Name)
            //        {
            //            MessagesList.Add(msg);
            //            OnMessageReceived(this, msg);
            //        }
            //        else
            //        {
            //            //Store message in DB, so this can be retrieved later from Conversations page
            //        }
            //    }

            //};


            SendCommand = new Command(() =>
            {
                if (!String.IsNullOrWhiteSpace(OutMessage))
                {
                    var message = new Message
                    {
                        Text = OutMessage,
                        IsTextIn = false,
                        MessageDateTime = DateTime.Now
                    };


                    MessagesList.Add(message);
                    OutMessage = "";
                    ChatService.Instance.SendMessage(message.Text, Contact.Name);
                }

            });
        }
    }
}
