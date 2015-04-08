using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
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

        public IProfilePage ProfilePage { get; set; }
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

        public async Task UpdateProfile(string currentPassword)
        {
            if (!await ValidatePassword()) return;
            await ProfilePage.ShowUpdateProgress();
            await UpdateSave(currentPassword);
            await VerifyUser();
        }

        private async Task<bool> ValidatePassword()
        {
            if (!String.IsNullOrWhiteSpace(Password) && !Password.Equals(ConfirmPassword))
            {
                await ProfilePage.HideUpdateProgress();
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
            else
            {
                await ProfilePage.HideUpdateProgress();
                await
                    new MessageDialog(
                        "your changes have been saved, if you change your email address, you need to check your email for verification",
                        "Success").ShowAsync();
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
            }
            catch (ParseException e)
            {
                Error.CaptureError(e);
            }

            await ProfilePage.HideUpdateProgress();
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