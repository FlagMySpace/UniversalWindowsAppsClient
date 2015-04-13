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
using SSWindows.Common;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class ForgotPageViewModel : ViewModel, IForgotPageViewModel
    {
        public ForgotPageViewModel(INavigationService navigationService, IError error)
        {
            NavigationService = navigationService;
            Error = error;
        }

        public INavigationService NavigationService { get; set; }
        public IError Error { get; set; }

        public IView View
        {
            get;
            set;
        }

        public string Email { get; set; }

        public async Task SendRequest(string emailAddress)
        {
            await ForgotPage.ShowResetProgress();

            try
            {
                await ParseUser.RequestPasswordResetAsync(emailAddress);
            }
            catch (Exception e)
            {
                Error.CaptureError(e);
            }

            await ForgotPage.HideResetProgress();

            if (!await Error.InvokeError())
            {
                await
                    new MessageDialog(String.Format("Please check your inbox at {0}", emailAddress), "Success")
                        .ShowAsync();

                if (NavigationService.CanGoBack())
                {
                    NavigationService.GoBack();
                }
            }
        }

        public IForgotPage ForgotPage { get; set; }
    }
}
