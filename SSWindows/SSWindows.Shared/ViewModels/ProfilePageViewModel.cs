using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
using SSWindows.Common;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class ProfilePageViewModel : ViewModel, IProfilePageViewModel
    {
        public ProfilePageViewModel(INavigationService navigationService, IError error) : this()
        {
            NavigationService = navigationService;
            Error = error;
        }

        public ProfilePageViewModel()
        {
            MapParseToUser();
        }

        public IError Error { get; set; }
        public INavigationService NavigationService { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsEmailVerified
        {
            get { return ParseUser.CurrentUser.Get<bool>("emailVerified"); }
        }

        public async Task ResendEmailVerification()
        {
            try
            {
                await ProfilePage.ShowProgress("Sending email verification...");
                var email = ParseUser.CurrentUser.Email;
                ParseUser.CurrentUser.Email = "";
                await ParseUser.CurrentUser.SaveAsync();
                ParseUser.CurrentUser.Email = email;
                await ParseUser.CurrentUser.SaveAsync();
                await ProfilePage.HideProgress();
                await
                    new MessageDialog(string.Format("verification email has been sent to {0}", email), "Sent").ShowAsync
                        ();
            }
            catch (Exception e)
            {
                Error.CaptureError(e);
            }

            await Error.InvokeError();
        }

        public IProfilePage ProfilePage { get; set; }

        public async Task UpdateProfile(string currentPassword)
        {
            if (!await ValidatePassword()) return;
            await ProfilePage.ShowProgress("Updating profile...");
            await UpdateSave(currentPassword);
            await VerifyUser();
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode,
            Dictionary<string, object> viewModelState)
        {
            if (ParseUser.CurrentUser == null)
            {
                await new MessageDialog("please login first before updating your profile", "Not Logged In").ShowAsync();
                if (NavigationService.CanGoBack())
                {
                    NavigationService.GoBack();
                }
            }
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }

        private async Task<bool> ValidatePassword()
        {
            if (!String.IsNullOrWhiteSpace(Password) && !Password.Equals(ConfirmPassword))
            {
                await ProfilePage.HideProgress();
                await new MessageDialog("new password and confirm password are mismatch", "Error").ShowAsync();
                return false;
            }
            return true;
        }

        private async Task VerifyUser()
        {
            if (ParseUser.CurrentUser == null)
            {
                NavigationService.ClearHistory();
                NavigationService.Navigate(App.Experiences.Login.ToString(), null);
            }
        }

        private async Task UpdateSave(string currentPassword)
        {
            try
            {
                await ParseUser.LogInAsync(ParseUser.CurrentUser.Username, currentPassword);
                if (!String.IsNullOrWhiteSpace(Username)) ParseUser.CurrentUser.Username = Username;
                if (!String.IsNullOrWhiteSpace(Password)) ParseUser.CurrentUser.Password = Password;
                if (!String.IsNullOrWhiteSpace(Email)) ParseUser.CurrentUser.Email = Email;
                await ParseUser.CurrentUser.SaveAsync();
                MapParseToUser();

                await ProfilePage.HideProgress();
                await
                    new MessageDialog(
                        "your changes have been saved, if you change your email address, you need to check your email for verification",
                        "Success").ShowAsync();
            }
            catch (ParseException e)
            {
                Error.CaptureError(e);
            }

            await ProfilePage.HideProgress();
            await Error.InvokeError();
        }

        private void MapParseToUser()
        {
            var user = ParseUser.CurrentUser;
            if (user != null)
            {
                Username = user.Username;
                Email = user.Email;
            }
        }
    }
}