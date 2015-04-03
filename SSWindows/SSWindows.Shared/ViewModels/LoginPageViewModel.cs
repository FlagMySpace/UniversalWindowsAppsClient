using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
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

        public async Task ValidateLogin()
        {
            var errors = await Person.Login();

            var dialog = errors.Length > 0 ? new MessageDialog(errors, "Registration Failed") : new MessageDialog("login success", "Success");

            await dialog.ShowAsync();
        }

        public async Task ValidateRegister()
        {
            var errors = await Person.Register();

            var dialog = errors.Length > 0 ? new MessageDialog(errors, "Login Failed") : new MessageDialog("registration success", "Success");

            await dialog.ShowAsync();
        }
    }
}
