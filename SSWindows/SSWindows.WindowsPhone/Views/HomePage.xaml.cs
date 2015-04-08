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
            if (_homePageViewModel != null) _homePageViewModel.ViewHomePage = this;
        }

        private async Task Logout(IUICommand command)
        {
            var progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            progressbar.Text = "Clearing your credential...";
            await progressbar.ShowAsync();
            await _homePageViewModel.Logout();
            await progressbar.HideAsync();
        }

        private void AppBarProfil_Click(object sender, RoutedEventArgs e)
        {
            _homePageViewModel.NavigationService.Navigate(App.Experiences.Profile.ToString(), null);
        }

        private void AppBarLog_Click(object sender, RoutedEventArgs e)
        {
            if (ParseUser.CurrentUser != null)
            {
            }
            else
            {
                _homePageViewModel.NavigationService.Navigate(App.Experiences.Login.ToString(), null);
            }
        }
    }
}