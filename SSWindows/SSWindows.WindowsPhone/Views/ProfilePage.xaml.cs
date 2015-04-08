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
    public sealed partial class ProfilePage : PageBasePhone, IProfilePage
    {
        private IProfilePageViewModel _profilePageViewModel;

        public ProfilePage()
        {
            InitializeComponent();

            _profilePageViewModel = (IProfilePageViewModel)DataContext;
            _profilePageViewModel.ProfilePage = this;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await _profilePageViewModel.UpdateProfile(PasswordBoxOld.Password);
        }

        public async Task HideUpdateProgress()
        {
            ButtonSave.IsEnabled = ButtonResend.IsEnabled = true;
            await HideProgressBar();
        }

        public async Task ShowUpdateProgress()
        {
            await ShowProgressBar("updating your profile...");
            ButtonSave.IsEnabled = ButtonResend.IsEnabled = false;
        }
    }
}
