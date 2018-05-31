using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RaccoonMsgr.ViewModels;
using System.Threading.Tasks;
using Xamanimation;

namespace RaccoonMsgr.Views
{
    public partial class Messages : ContentPage
    {
        MessagesViewModel vm;
        public Messages()
        {
            InitializeComponent();
            vm = new MessagesViewModel(Navigation, "Messages");
            BindingContext = vm;
            //vm.IsBusy = true;
            vm.OverlayText = "Pulling messages";
            //DelayActionAsync(6000, () => vm.IsBusy = false);
            if (vm.MessagesList.Count > 0)
            {
                //Move to end of list by default when opening Messages view
                var target = vm.MessagesList[vm.MessagesList.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            }

            vm.MessagesList.CollectionChanged += (sender, e) =>
            {
                var target = vm.MessagesList[vm.MessagesList.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            };

            vm.MessagesList.Add(new Models.Message() { Text = "Hola que tal!", IsTextIn = true, MessageDateTime = new DateTime() });
        }
        public async Task DelayActionAsync(int delay, Action action)
        {
            await Task.Delay(delay);

            action();
        }
        protected override void OnAppearing()
        {

            //this.Animate(new FlipAnimation());
        }

    }

}
