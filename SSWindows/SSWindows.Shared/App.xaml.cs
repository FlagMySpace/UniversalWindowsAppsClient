using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using SSWindows.Interfaces;
using SSWindows.Services;
using SSWindows.ViewModels;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace SSWindows
{
    public sealed partial class App : MvvmAppBase
    {
        static readonly UnityContainer Container = new UnityContainer();
        public App()
        {
            InitializeComponent();
        }

        public enum Experiences
        {
            Main
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(Experiences.Main.ToString(), null);
            return Task.FromResult<object>(null);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IEventAggregator>(new EventAggregator());
            Container.RegisterInstance<ISessionStateService>(SessionStateService);
            Container.RegisterInstance<INavigationService>(NavigationService);

            ViewModelLocationProvider.SetDefaultViewModelFactory(viewModelType => Container.Resolve(viewModelType));
            return Task.FromResult<object>(null);
        }
    }
}