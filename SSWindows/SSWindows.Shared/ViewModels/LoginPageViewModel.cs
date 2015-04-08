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
        public ILoginPage LoginPage { get; set; }
        public INavigationService NavigationService { get; set; }

        public async Task Login()
        {
            try
            {
                await LoginPage.ShowLoginProgress();
                await ParseUser.LogInAsync(Username, Password);
            }
            catch (Exception e)
            {
                Error.CaptureError(e);
            }

            await LoginPage.HideLoginProgress();
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
            try
            {
                ValidatePasswordMatch();
                ValidateEmail();

                await LoginPage.ShowRegisterProgress();

                var user = new ParseUser
                {
                    Username = Username,
                    Password = Password,
                    Email = Email
                };

                await user.SignUpAsync();
            }
            catch (Exception e)
            {
                Error.CaptureError(e);
            }

            await LoginPage.HideRegisterProgress();
            await Error.InvokeError();

            if (ParseUser.CurrentUser != null)
            {
                await new MessageDialog(
                    String.Format(
                        "registration success, please verify your email address by checking your inbox at {0}",
                        Email), "Success").ShowAsync();
            }
        }

        private void ValidatePasswordMatch()
        {
            if (!String.IsNullOrWhiteSpace(Password) && !Password.Equals(ConfirmPassword))
            {
                throw new Exception("password mismatch");
            }
        }

        private void ValidateEmail()
        {
            if (String.IsNullOrWhiteSpace(Email))
            {
                throw new Exception("email address field is required");
            }
        }
    }
}