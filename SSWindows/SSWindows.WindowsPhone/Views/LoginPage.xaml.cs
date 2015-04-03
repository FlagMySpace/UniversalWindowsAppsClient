﻿using Windows.UI.Xaml.Navigation;
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
        private ILoginPageViewModel _viewModel;
        public LoginPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            _viewModel = DataContext as ILoginPageViewModel;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void ButtonLogin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonLogin.IsEnabled = false;
            await _viewModel.ValidateLogin();
            ButtonLogin.IsEnabled = true;
        }

        private async void ButtonRegister_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ButtonRegister.IsEnabled = false;
            await _viewModel.ValidateRegister();
            ButtonRegister.IsEnabled = true;
        }

        private void HyperlinkButtonForgot_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _viewModel.NavigationService.Navigate(App.Experiences.Forgot.ToString(), null);
        }
    }
}