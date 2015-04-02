using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using SSWindows.Events;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class MainPageViewModel : ViewModel, IMainPageViewModel
    {
        private string _mUsername = default(string);
        public string Username
        {
            get { return _mUsername; }
            set
            {
                _mUsername = value;
                SetProperty(ref _mUsername, value);
            }
        }

        private string _mPassword = default(string);
        public string Password
        {
            get { return _mPassword; }
            set
            {
                _mPassword = value;
                SetProperty(ref _mPassword, value);
            }
        }


        private string _mConfirmPassword = default(string);
        public string ConfirmPassword
        {
            get { return _mConfirmPassword; }
            set
            {
                _mConfirmPassword = value;
                SetProperty(ref _mConfirmPassword, value);
            }
        }


        private string _mEmail = default(string);
        public string Email
        {
            get { return _mEmail; }
            set
            {
                _mEmail = value;
                SetProperty(ref _mEmail, value);
            }
        }

    }
}
