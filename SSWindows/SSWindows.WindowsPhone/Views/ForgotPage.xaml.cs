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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgotPage : PageBase
    {
        private IForgotPageViewModel _forgotPageViewModel;

        public ForgotPage()
        {
            this.InitializeComponent();

            _forgotPageViewModel = DataContext as IForgotPageViewModel;
        }

        private async void ButtonSubmit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Verify email address validity before sending the reset command.
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(TextBoxEmail.Text);
            if (!match.Success)
            {
                await new MessageDialog("Please write down valid email address format", "Not Valid").ShowAsync();
                return;
            }

            // Reset using email address written
            ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = false;
            var errors = await _forgotPageViewModel.SendRequest(TextBoxEmail.Text);
            var dialog = errors.Length > 0
                ? new MessageDialog(errors, "Reset Failed")
                : new MessageDialog(String.Format("Please check your inbox at {0}", TextBoxEmail.Text), "Success");
            await dialog.ShowAsync();
            ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = true;
            
            // If reset success then navigate back to previous page
            if(!errors.Any())
            {
                _forgotPageViewModel.NavigationService.GoBack();
            }
        }
    }
}
