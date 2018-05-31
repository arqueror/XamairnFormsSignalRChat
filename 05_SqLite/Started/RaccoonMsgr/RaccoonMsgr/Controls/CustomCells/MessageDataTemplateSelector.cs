using System;
using RaccoonMsgr.Models;
using Xamarin.Forms;

namespace RaccoonMsgr.Controls.CustomCells
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate messageInDataTemplate;
        private readonly DataTemplate messageOutDataTemplate;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;
            return messageVm.IsTextIn ? this.messageInDataTemplate : this.messageOutDataTemplate;
        }


        public MessageDataTemplateSelector()
        {
            this.messageInDataTemplate = new DataTemplate(typeof(MessageInViewCell));
            this.messageOutDataTemplate = new DataTemplate(typeof(MessageOutViewCell));
        }

    }
}
