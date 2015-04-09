using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using SSWindows.Controls;
using SSWindows.Interfaces;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SSWindows.Views
{
    public sealed partial class ProfilePage : PageBasePhone, IProfilePage
    {
        private readonly IProfilePageViewModel _profilePageViewModel;

        public ProfilePage()
        {
            InitializeComponent();

            _profilePageViewModel = (IProfilePageViewModel) DataContext;
            _profilePageViewModel.ProfilePage = this;
        }

        public async Task HideProgress()
        {
            ButtonSave.IsEnabled = ButtonResend.IsEnabled = true;
            await HideProgressBar();
        }

        public async Task ShowProgress(string text)
        {
            await ShowProgressBar(text);
            ButtonSave.IsEnabled = ButtonResend.IsEnabled = false;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await _profilePageViewModel.UpdateProfile(PasswordBoxOld.Password);
        }

        private void ButtonResend_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            _profilePageViewModel.ResendEmailVerification();
        }
    }
}