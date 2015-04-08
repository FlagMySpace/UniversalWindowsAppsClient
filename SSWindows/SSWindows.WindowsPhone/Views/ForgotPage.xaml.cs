using Windows.UI.Xaml.Input;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgotPage : PageBasePhone
    {
        private readonly IForgotPageViewModel _forgotPageViewModel;

        public ForgotPage()
        {
            InitializeComponent();

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
                await HideProgressBar();
            }
        }
    }
}