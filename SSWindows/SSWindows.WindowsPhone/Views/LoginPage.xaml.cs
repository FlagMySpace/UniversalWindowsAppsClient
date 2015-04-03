using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using SSWindows.Controls;
using SSWindows.Interfaces;
using SSWindows.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SSWindows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : PageBase
    {
        private ILoginPageViewModel _loginPageViewModel;
        public LoginPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _loginPageViewModel = DataContext as ILoginPageViewModel;
        }

        private async void ButtonLogin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            var errors = await _loginPageViewModel.Login();
            if (errors.Any())
            {
                var dialog = new MessageDialog(errors, "Login Failed");
                await dialog.ShowAsync();
            }
            else
            {
                _loginPageViewModel.NavigationService.ClearHistory();
                _loginPageViewModel.NavigationService.Navigate(App.Experiences.Home.ToString(), null);
            }
            ButtonLogin.IsEnabled = true;
        }

        private async void ButtonRegister_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonRegister.IsEnabled = false;
            var errors = await _loginPageViewModel.Register();
            MessageDialog dialog;
            if (errors.Any())
            {
                dialog =
                   new MessageDialog(errors, "Registration Failed");
                await dialog.ShowAsync();
            }
            else
            {
                dialog =
                    new MessageDialog(
                        String.Format(
                            "registration success, please verify your email address by checking your inbox at {0}",
                            _loginPageViewModel.Person.Email), "Success");
            }
            await dialog.ShowAsync();
            ButtonRegister.IsEnabled = true;
        }

        private void HyperlinkButtonForgot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _loginPageViewModel.NavigationService.Navigate(App.Experiences.Forgot.ToString(), null);
        }
    }
}
