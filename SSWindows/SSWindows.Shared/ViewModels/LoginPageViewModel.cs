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

        public async Task<string> ValidateLogin()
        {
            return await Person.Login();
        }

        public async Task<string> ValidateRegister()
        {
            return await Person.Register();
        }
    }
}
