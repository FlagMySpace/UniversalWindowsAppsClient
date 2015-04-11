using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    public sealed partial class ForgotPage : PageBasePhone, IForgotPage
    {
        private readonly IForgotPageViewModel _forgotPageViewModel;

        public ForgotPage()
        {
            InitializeComponent();

            _forgotPageViewModel = (IForgotPageViewModel) DataContext;
            _forgotPageViewModel.ForgotPage = this;
        }

        private async void ButtonSubmit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await _forgotPageViewModel.SendRequest(TextBoxEmail.Text);
        }

        public async Task ShowResetProgress()
        {
            await ShowProgressBar("Resetting password...");
            ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = false;
        }

        public async Task HideResetProgress()
        {
            ButtonSubmit.IsEnabled = TextBoxEmail.IsEnabled = true;
            await HideProgressBar();
        }
    }
}