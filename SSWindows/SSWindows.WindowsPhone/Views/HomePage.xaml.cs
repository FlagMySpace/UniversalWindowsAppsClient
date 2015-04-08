using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Parse;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    public sealed partial class HomePage : PageBasePhone, IHomePage
    {
        private readonly IHomePageViewModel _homePageViewModel;

        public HomePage()
        {
            InitializeComponent();
            _homePageViewModel = DataContext as IHomePageViewModel;
            if (_homePageViewModel != null) _homePageViewModel.HomePage = this;
        }

        private void AppBarProfil_Click(object sender, RoutedEventArgs e)
        {
            _homePageViewModel.NavigationService.Navigate(App.Experiences.Profile.ToString(), null);
        }

        private async void AppBarLog_Click(object sender, RoutedEventArgs e)
        {
            if (ParseUser.CurrentUser != null)
            {
                await _homePageViewModel.Logout();
            }
            else
            {
                _homePageViewModel.NavigationService.Navigate(App.Experiences.Login.ToString(), null);
            }
        }

        public async Task ShowLogoutProgressBar()
        {
            await ShowProgressBar("Clearing your credential...");
        }

        public async Task HideLogoutProgressBar()
        {
            await HideProgressBar();
        }
    }
}