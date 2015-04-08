using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Parse;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class HomePageViewModel : ViewModel, IHomePageViewModel
    {
        #region Constructors
        public HomePageViewModel(INavigationService navigationService, IError error, IEventAggregator eventAggregator)
        {
            NavigationService = navigationService;
            Error = error;
            EventAggregator = eventAggregator;
        }

        public HomePageViewModel()
        {
        }
        #endregion

        public async Task Logout()
        {
            await LogoutDialog();
        }

        public IEventAggregator EventAggregator { get; set; }
        public IError Error { get; set; }
        public INavigationService NavigationService { get; set; }
        public IHomePage HomePage { get; set; }

        public string LogText
        {
            get
            {
                if (ParseUser.CurrentUser != null)
                {
                    return "logout";
                }
                return "login";
            }
        }

        public Visibility IsLoggedIn
        {
            get
            {
                if (ParseUser.CurrentUser != null)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public async Task LogoutDialog()
        {
            var dialog = new MessageDialog("do you want to logout?", "Confirmation");
            dialog.Commands.Add(new UICommand("Yes", async command => await Logout(command)));
            dialog.Commands.Add(new UICommand("No"));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            await dialog.ShowAsync();
        }

        private async Task Logout(IUICommand command)
        {
            await HomePage.ShowLogoutProgressBar();
            try
            {
                await ParseUser.LogOutAsync();
                NavigationService.ClearHistory();
                NavigationService.Navigate(App.Experiences.Login.ToString(), null);
            }
            catch (ParseException ex)
            {
                Error.CaptureError(ex);
            }

            await Error.InvokeError();
            await HomePage.HideLogoutProgressBar();
        }
    }
}