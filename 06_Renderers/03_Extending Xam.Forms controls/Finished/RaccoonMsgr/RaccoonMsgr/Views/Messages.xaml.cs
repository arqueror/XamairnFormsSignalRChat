using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RaccoonMsgr.ViewModels;
using System.Threading.Tasks;
using Xamanimation;
using RaccoonMsgr.Models;

namespace RaccoonMsgr.Views
{
    public partial class Messages : ContentPage
    {
        MessagesViewModel vm;
        public Action PageClosedCallback;
        public Action PageOpenedCallback;
        public Messages(Contact contact)
        {
            InitializeComponent();
            vm = new MessagesViewModel(Navigation, "Messages");
            BindingContext = vm;
            vm.Contact = contact;
            vm.IsBusy = true;
            vm.OverlayText = "Pulling messages";
            DelayActionAsync(6000, async () =>
            {
                //Use this only for iOS. Andoid renderer handles FadeOut natively
                if (Device.RuntimePlatform == Device.iOS)
                    await loadingOverlay.Animate(new FadeOutAnimation());
                else
                    vm.IsBusy = false;


            });


            vm.OnMessageReceived += (s, msg) =>
            {
                //Execute on main thread. Event raising comes fron another thread
                //Creating a new event takes avoids timing issues between PropertyChanged mechanism and UI
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (vm.MessagesList.Count > 0)
                    {
                        var target = vm.MessagesList[vm.MessagesList.Count - 1];
                        MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
                        MessagesListViewiOS.ScrollTo(target, ScrollToPosition.End, true);
                    }
                });

            };


            //vm.MessagesList.Add(new Models.Message() { Text = "Hola que tal!", IsTextIn = true, MessageDateTime = new DateTime() });
        }
        public void AddMessage(Message msg)
        {
            vm.AddMessage(msg);
        }
        public async Task DelayActionAsync(int delay, Action action)
        {
            await Task.Delay(delay);

            action();
        }
        protected override void OnAppearing()
        {
            if (PageOpenedCallback != null) PageOpenedCallback();
        }
        protected override void OnDisappearing()
        {
            if (PageClosedCallback != null) PageClosedCallback();
            //this.Animate(new FlipAnimation());
        }

    }

}
