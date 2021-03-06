﻿using System;
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
        public Messages(Contact contact)
        {
            InitializeComponent();
            vm = new MessagesViewModel(Navigation, "Messages");
            BindingContext = vm;
            vm.Contact = contact;
            //vm.IsBusy = true;
            vm.OverlayText = "Pulling messages";
            //DelayActionAsync(6000, () => vm.IsBusy = false);
            if (vm.MessagesList.Count > 0)
            {
                //Move to end of list by default when opening Messages view
                var target = vm.MessagesList[vm.MessagesList.Count - 1];
                MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            }

            vm.OnMessageReceived += (s, msg) =>
            {
                //Execute on main thread. Event raising comes fron another thread
                //Creating a new event takes avoids timing issues between PropertyChanged mechanism and UI
                Device.BeginInvokeOnMainThread(() => MessagesListView.ScrollTo(msg, ScrollToPosition.End, true));

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
        protected override void OnDisappearing()
        {
            if (PageClosedCallback != null) PageClosedCallback();
            //this.Animate(new FlipAnimation());
        }

    }

}
