using System;
using System.Collections.Generic;

using Xamarin.Forms;
using RaccoonMsgr.ViewModels;
using System.Threading.Tasks;
using Xamanimation;
using RaccoonMsgr.Models;
using RaccoonMsgr.Renderers;

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
            //vm.IsBusy = true;
            vm.OverlayText = "Pulling messages";
            //DelayActionAsync(6000, () => vm.IsBusy = false);


            if (Device.RuntimePlatform == Device.Android)
            {
                //resolve dependency
                var buttonFactory = DependencyService.Get<IFloatingActionButtonRenderer>();
                //Get native control and pas the callback function
                var floatingButton = buttonFactory.CreateFloatingActionButton(() =>
                {
                    DisplayAlert("Tapped", "AndroidFloatingButton", "OK");

                });
                //Add native control to UI
                ControlsGrid.Children.Add(floatingButton);
            }

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
