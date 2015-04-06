using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class HomePageViewModel : ViewModel, IHomePageViewModel
    {
        public HomePageViewModel(INavigationService navigationService, IError error)
        {
            NavigationService = navigationService;
            Error = error;
        }

        public IError Error { get; set; }

        public INavigationService NavigationService { get; set; }

        public async Task Logout()
        {
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
        }
    }
}
