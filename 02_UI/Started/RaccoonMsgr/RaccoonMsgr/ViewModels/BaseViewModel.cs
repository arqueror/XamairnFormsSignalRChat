using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
            if(DetailPage==null)return;
            DetailPage.Title = PageTitle;
            _navigation.PushAsync(DetailPage);
        }
        public void NavigateToDetail(Page detailPage)
        {
            NavigateToDetailCommand(detailPage);
        }
    }
}
