using SSWindows.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    public sealed partial class ProfilePage : PageBase
    {
        private IProfilePageViewModel _profilePageViewModel;
        private StatusBarProgressIndicator _progressbar;

        public ProfilePage()
        {
            InitializeComponent();

            _profilePageViewModel = DataContext as IProfilePageViewModel;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await ShowProgressBar("updating your profile...");
            ButtonSave.IsEnabled = false;
            await _profilePageViewModel.UpdateProfile(TextBoxOldUsername.Text, PasswordBoxOld.Password);
            ButtonSave.IsEnabled = true;
            await _progressbar.HideAsync();
        }

        private async Task ShowProgressBar(string text)
        {
            _progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            _progressbar.Text = text;
            await _progressbar.ShowAsync();
        }
    }
}
