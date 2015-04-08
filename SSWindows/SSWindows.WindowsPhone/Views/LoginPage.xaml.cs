using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using Parse;
using SSWindows.Controls;
using SSWindows.Interfaces;
using SSWindows.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SSWindows.Views
{
    public sealed partial class LoginPage : PageBasePhone, ILoginPage
    {
        private ILoginPageViewModel _loginPageViewModel;

        public LoginPage()
        {
            InitializeComponent();
            _loginPageViewModel = DataContext as ILoginPageViewModel;
            _loginPageViewModel.LoginPage = this;
        }

        private async void ButtonLogin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await _loginPageViewModel.Login();
        }

        private async void ButtonRegister_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await _loginPageViewModel.Register();
        }

        private void HyperlinkButtonForgot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _loginPageViewModel.NavigationService.Navigate(App.Experiences.Forgot.ToString(), null);
        }

        private void ToggleControls(bool enabled)
        {
            ButtonLogin.IsEnabled = ButtonRegister.IsEnabled = enabled;
        }

        public async Task ShowLoginProgress()
        {
            ToggleControls(false);
            await ShowProgressBar("Checking credential...");
        }

        public async Task ShowRegisterProgress()
        {
            ToggleControls(false);
            await ShowProgressBar("Creating credential...");
        }

        public async Task HideRegisterProgress()
        {
            ToggleControls(true);
            await HideProgressBar();
        }

        public async Task HideLoginProgress()
        {
            ToggleControls(true);
            await HideProgressBar();
        }
    }
}
