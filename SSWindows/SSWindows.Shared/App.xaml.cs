using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Parse;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace SSWindows
{
    public sealed partial class App : MvvmAppBase
    {
        public enum Experiences
        {
            Login,
            Forgot,
            Home,
            Profile
        }

        private static readonly UnityContainer Container = new UnityContainer();

        public App()
        {
            InitializeComponent();
            ParseClient.Initialize("L3POUewF4K1RgkRtKcZTDLne4Zp2kCgwQUmeW0Ru",
                "5qgD6FO6sAR4NWN5FehNtuaGBqHXIikQSg7yj1fu");
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (ParseUser.CurrentUser != null && ParseUser.CurrentUser.Get<bool>("emailVerified"))
            {
                NavigationService.Navigate(Experiences.Home.ToString(), null);
            }
            else
            {
                NavigationService.Navigate(Experiences.Login.ToString(), null);
            }
            return Task.FromResult<object>(null);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance<IEventAggregator>(new EventAggregator());
            Container.RegisterInstance(SessionStateService);
            Container.RegisterInstance(NavigationService);

            ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType => Container.Resolve(viewModelType));
            return Task.FromResult<object>(null);
        }
    }
}