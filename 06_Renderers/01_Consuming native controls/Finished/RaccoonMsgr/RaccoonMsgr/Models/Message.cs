using System;
using MvvmHelpers;
using SQLite;

namespace RaccoonMsgr.Models
{
    public class Message : ObservableObject
    {
        public string ConversationId { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        string _text;

        public DateTime MessageDateTime
        {
            get { return _messageDateTime; }
            set { SetProperty(ref _messageDateTime, value); }
        }

        DateTime _messageDateTime;

        public string TimeDisplay => MessageDateTime.ToLocalTime().ToString();

        public bool IsTextIn
        {
            get { return _isTextIn; }
            set { SetProperty(ref _isTextIn, value); }
        }
        bool _isTextIn;
    }
}
