using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace RaccoonMsgr.Models
{
    public class Contact : BaseModel
    {
        public Contact()
        {
        }
        private Color _backgroundImageColor = Color.Gray;
        private int _unreadMessages = 0;
        private string _name = String.Empty;
        private bool _hasUnreadMessages = false;

        public Guid Id { get; set; } = Guid.NewGuid();
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
