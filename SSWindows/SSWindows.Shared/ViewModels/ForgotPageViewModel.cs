using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public IView View
        {
            get;
            set;
        }

        public string Email { get; set; }

        public async Task<bool> ValidateEmail(string email)
        {
            // Verify email address validity before sending the reset command.
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            if (!match.Success)
            {
                await new MessageDialog("Please write down valid email address format", "Not Valid").ShowAsync();
                return false;
            }
            return true;
        }

        public async System.Threading.Tasks.Task SendRequest(string emailAddress)
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
            var dialog = errors.Length > 0
                ? new MessageDialog(errors, "Reset Failed")
                : new MessageDialog(String.Format("Please check your inbox at {0}", emailAddress), "Success");
            await dialog.ShowAsync();

            // If reset success then navigate back to previous page
            if (!errors.Any())
            {
                NavigationService.GoBack();
            }
        }
    }
}
