using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using RaccoonMsgr.Models;
using RaccoonMsgr.Controls;
using RaccoonMsgr.Views.MasterPage;
using RaccoonMsgr.Views;
using Plugin.Toasts;
using System.Threading.Tasks;

namespace RaccoonMsgr.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private readonly INavigation _navigation;
        public string PageTitle { get; private set; }
        public Page DetailPage { get; set; } = null;
        public ICommand ButtonClickedCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(INavigation navigation, string title)
        {
            _navigation = navigation;
            PageTitle = title;
            ButtonClickedCommand = new Command<Page>(NavigateToDetailCommand);
        }
        public BaseViewModel()
        {

        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NavigateToDetailCommand(Page detailPage)
        {
            if (DetailPage == null) return;
            if (!string.IsNullOrEmpty(PageTitle))
                DetailPage.Title = PageTitle;
            _navigation.PushAsync(DetailPage);
        }
        public void NavigateToDetail(Page detailPage, string pageTitle)
        {
            detailPage.Title = pageTitle;
            _navigation.PushAsync(detailPage);
        }
        public void NavigateToDetail()
        {

            if (DetailPage != null)
                NavigateToDetailCommand(DetailPage);
        }
        public async Task ShowToast(string title, string description)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            var options = new NotificationOptions()
            {
                Title = title,
                Description = description,
                IsClickable = false
            };
            await notificator.Notify(options);


            /*
                 Timeout = 1, // Hides by itself
                 Clicked = 2, // User clicked on notification
                 Dismissed = 4, // User manually dismissed notification
                 ApplicationHidden = 8, // Application went to background
                 Failed = 16 // When failed to display the toast
            */
        }
    }
}
