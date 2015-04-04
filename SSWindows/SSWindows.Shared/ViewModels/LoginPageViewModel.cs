using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Parse;
using SSWindows.Events;
using SSWindows.Interfaces;
using SSWindows.Models;

namespace SSWindows.ViewModels
{
    public class LoginPageViewModel : ViewModel, ILoginPageViewModel
    {
        public LoginPageViewModel(INavigationService navigationService)
        {
            Person = new Person();

            NavigationService = navigationService;
        }

        private Person _mPerson = default(Person);

        public LoginPageViewModel()
        {
        }

        public INavigationService NavigationService { get; set; }

        public Person Person
        {
            get { return _mPerson; }
            set
            {
                _mPerson = value;
                SetProperty(ref _mPerson, value);
            }
        }

        public async Task Login()
        {
            var errors = await Person.Login();
            if (errors.Any())
            {
                var dialog = new MessageDialog(errors, "Login Failed");
                await dialog.ShowAsync();
            }
            else
            {
                if (!ParseUser.CurrentUser.Get<bool>("emailVerified"))
                {
                    var dialog =
                        new MessageDialog(
                            "please verify your email address, verification will make sure that you will not lose access to your account (and you will get auto log in enabled)",
                            "Verify Email");
                    await dialog.ShowAsync();
                }
                NavigationService.ClearHistory();
                NavigationService.Navigate(App.Experiences.Home.ToString(), null);
            }
        }

        public async Task Register()
        {
            var errors = await Person.Register();
            MessageDialog dialog;
            if (errors.Any())
            {
                dialog =
                   new MessageDialog(errors, "Registration Failed");
                await dialog.ShowAsync();
            }
            else
            {
                dialog =
                    new MessageDialog(
                        String.Format(
                            "registration success, please verify your email address by checking your inbox at {0}",
                            Person.Email), "Success");
            }
            await dialog.ShowAsync();
        }
    }
}
