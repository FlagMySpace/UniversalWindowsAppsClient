using SSWindows.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Parse;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    public sealed partial class HomePage : PageBase
    {
        private IHomePageViewModel _homePageViewModel;

        public HomePage()
        {
            this.InitializeComponent();

            _homePageViewModel = DataContext as IHomePageViewModel;
        }

        private async Task Logout(IUICommand command)
        {
            await ParseUser.LogOutAsync();
            _homePageViewModel.NavigationService.ClearHistory();
            _homePageViewModel.NavigationService.Navigate(App.Experiences.Login.ToString(), null);
        }

        private async void AppBarLogout_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("do you want to logout?", "Confirmation");
            dialog.Commands.Add(new UICommand("Yes", async command => await Logout(command)));
            dialog.Commands.Add(new UICommand("No"));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            await dialog.ShowAsync();
        }
    }
}
