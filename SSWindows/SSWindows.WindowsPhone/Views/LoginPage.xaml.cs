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
    public sealed partial class LoginPage : PageBasePhone
    {
        private ILoginPageViewModel _loginPageViewModel;

        public LoginPage()
        {
            InitializeComponent();
            _loginPageViewModel = DataContext as ILoginPageViewModel;
        }

        private async void ButtonLogin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = ButtonRegister.IsEnabled = false;
            await ShowProgressBar("Checking credential...");
            await _loginPageViewModel.Login();
            ButtonLogin.IsEnabled = ButtonRegister.IsEnabled = true;
            await HideProgressBar();
        }

        private async void ButtonRegister_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonRegister.IsEnabled = ButtonLogin.IsEnabled = false;
            await ShowProgressBar("Creating credential...");
            await _loginPageViewModel.Register();
            ButtonRegister.IsEnabled = ButtonLogin.IsEnabled = true;
            await HideProgressBar();
        }

        private void HyperlinkButtonForgot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _loginPageViewModel.NavigationService.Navigate(App.Experiences.Forgot.ToString(), null);
        }
    }
}
