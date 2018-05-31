using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RaccoonMsgr.Models;
using Xamarin.Forms;

namespace RaccoonMsgr.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public bool _isBusy = false;
        public string _overlayText;

        public ObservableCollection<Message> MessagesList { get; }
        public ICommand SendCommand { get; set; }
        string _outMessage = string.Empty;

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
                        MessageDateTime = DateTime.Now
                    };


                    MessagesList.Add(message);
                    OutMessage = "";
                }

            });
        }
    }
}
