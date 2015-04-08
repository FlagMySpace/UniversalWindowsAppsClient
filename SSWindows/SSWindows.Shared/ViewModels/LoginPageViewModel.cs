using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
using SSWindows.Common;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class LoginPageViewModel : ViewModel, ILoginPageViewModel
    {
        public LoginPageViewModel(INavigationService navigationService, IError error)
        {
            NavigationService = navigationService;
            Error = error;
        }

        public LoginPageViewModel()
        {
        }

        public IError Error { get; set; }
        public IView View { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string ConfirmPassword { get; set; }
        public INavigationService NavigationService { get; set; }

        public async Task Login()
        {
            try
            {
                await ParseUser.LogInAsync(Username, Password);
            }
            catch (ParseException e)
            {
                Error.CaptureError(e);
            }

            await Error.InvokeError();

            if (ParseUser.CurrentUser != null)
            {
                if (!ParseUser.CurrentUser.Get<bool>("emailVerified"))
                {
                    var dialog =
                        new MessageDialog(
                            "please verify your email address, verification will make sure that you will not lose access to your account (and you will get auto log in enabled)",
                            "Verify Email");
                    await dialog.ShowAsync();
                }
                NavigationService.ClearHistory();
                NavigationService.Navigate(App.Experiences.Home.ToString(), null);
            }
        }

        public async Task Register()
        {
            var user = new ParseUser
            {
                Username = Username,
                Password = Password,
                Email = Email
            };

            try
            {
                await user.SignUpAsync();
            }
            catch (ParseException e)
            {
                Error.CaptureError(e);
            }

            await Error.InvokeError();

            if (ParseUser.CurrentUser != null)
            {
                await new MessageDialog(
                    String.Format(
                        "registration success, please verify your email address by checking your inbox at {0}",
                        Email), "Success").ShowAsync();
            }
        }
    }
}