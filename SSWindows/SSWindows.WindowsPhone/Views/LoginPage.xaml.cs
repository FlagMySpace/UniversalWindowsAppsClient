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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : PageBase
    {
        private ILoginPageViewModel _loginPageViewModel;
        private StatusBarProgressIndicator _progressbar;

        public LoginPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _loginPageViewModel = DataContext as ILoginPageViewModel;
        }

        private async void ButtonLogin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            await ShowProgressBar("Checking credential...");
            await _loginPageViewModel.Login();
            ButtonLogin.IsEnabled = true;
            await _progressbar.HideAsync();
        }

        private async Task ShowProgressBar(string text)
        {
            _progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            _progressbar.Text = text;
            await _progressbar.ShowAsync();
        }

        private async void ButtonRegister_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonRegister.IsEnabled = false;
            await ShowProgressBar("Creating credential...");
            await _loginPageViewModel.Register();
            ButtonRegister.IsEnabled = true;
            await _progressbar.HideAsync();
        }

        private void HyperlinkButtonForgot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _loginPageViewModel.NavigationService.Navigate(App.Experiences.Forgot.ToString(), null);
        }
    }
}
