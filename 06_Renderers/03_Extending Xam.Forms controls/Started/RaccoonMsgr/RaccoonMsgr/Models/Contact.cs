using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaccoonMsgr.Models
{
    public class Contact : BaseModel
    {

        private Color _backgroundImageColor = Color.Gray;
        private int _unreadMessages = 0;
        private string _name = String.Empty;
        private bool _hasUnreadMessages = false;
        private Message _lastMessage = new Message();
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();

        public Contact()
        {
            _messages.CollectionChanged += (s, a) =>
            {

                RaisePropertyChanged(nameof(LastMessage));
            };
        }
        public Message LastMessage
        {
            get
            {
                return Messages.OrderBy(d => d.MessageDateTime).Last();

            }
            set
            {
                _lastMessage = value;
                RaisePropertyChanged(nameof(LastMessage));
            }
        }
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged(nameof(LastMessage));
                RaisePropertyChanged(nameof(Messages));

            }


        }
        public string ConnectionId { get; set; }
        public ImageSource Image { get; set; } = "user_default.png";
        public int UnreadMessages
        {
            get => _unreadMessages;
            set
            {
                _unreadMessages = value;
                if (UnreadMessages > 0)
                    HasUnreadMessages = true;

                RaisePropertyChanged();
            }
        }
        public bool HasUnreadMessages
        {
            get
            {
                return _hasUnreadMessages;
            }
            set
            {
                _hasUnreadMessages = value;
                RaisePropertyChanged();
            }
        }
        [PrimaryKey]
        [NotNull]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        public Color BackgroundImageColor
        {
            get
            {
                return _backgroundImageColor;
            }
            set
            {
                _backgroundImageColor = value;
                RaisePropertyChanged();
            }
        }

    }
}
