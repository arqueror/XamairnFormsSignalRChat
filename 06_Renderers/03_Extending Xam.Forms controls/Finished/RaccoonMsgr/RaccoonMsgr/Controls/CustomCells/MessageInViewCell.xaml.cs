using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RaccoonMsgr.Renderers;

namespace RaccoonMsgr.Controls.CustomCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageInViewCell : ExtendedViewCell
    {
        public MessageInViewCell()
        {
            InitializeComponent();
        }
    }
}
