using SSWindows.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
using SSWindows.Controls;
using SSWindows.Interfaces;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgotPage : PageBase
    {
        private IForgotPageViewModel _forgotPageViewModel;
        private StatusBarProgressIndicator _progressbar;

        public ForgotPage()
        {
            this.InitializeComponent();

            _forgotPageViewModel = DataContext as IForgotPageViewModel;
        }

        private async void ButtonSubmit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var valid = await _forgotPageViewModel.ValidateEmail(TextBoxEmail.Text);
            if (valid)
            {
                await ShowProgressBar("Resetting password...");
                ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = false;
                await _forgotPageViewModel.SendRequest(TextBoxEmail.Text);
                ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = true;
                await _progressbar.HideAsync();
            }
        }

        private async Task ShowProgressBar(string text)
        {
            _progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            _progressbar.Text = text;
            await _progressbar.ShowAsync();
        }
    }
}
