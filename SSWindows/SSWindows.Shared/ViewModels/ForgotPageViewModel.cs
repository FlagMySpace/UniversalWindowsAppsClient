using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class ForgotPageViewModel : ViewModel, IForgotPageViewModel
    {
        public ForgotPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationService NavigationService { get; set; }

        public string Email { get; set; }

        public async System.Threading.Tasks.Task<string> SendRequest(string emailAddress)
        {
            var errors = "";
            try
            {
                await ParseUser.RequestPasswordResetAsync(emailAddress);
            }
            catch (Exception e)
            {
                errors = e.Message;
            }
            return errors;
        }
    }
}
