using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using SSWindows.Events;
using SSWindows.Interfaces;
using SSWindows.Models;

namespace SSWindows.ViewModels
{
    public class MainPageViewModel : ViewModel, IMainPageViewModel
    {
        public MainPageViewModel()
        {
            Person = new Person();
        }

        private Person _mPerson = default(Person);
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
            
        }

        public async Task ValidateRegister()
        {
            var errors = await Person.Register();

            var dialog = errors.Length > 0 ? new MessageDialog(errors, "Error!") : new MessageDialog("registration success", "Success");

            await dialog.ShowAsync();
        }
    }
}
