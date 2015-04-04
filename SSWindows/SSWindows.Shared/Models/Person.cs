using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;
using Microsoft.Practices.Prism.Mvvm;
using Parse;

namespace SSWindows.Models
{
    public class Person : BindableBase
    {
        private StringBuilder _strBuilder;

        public Person()
        {
            _strBuilder = new StringBuilder("");

            if (ParseUser.CurrentUser != null)
            {
                var user = ParseUser.CurrentUser;
                Username = user.Username;
                Email = user.Email;
            }
        }

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

        private async void ValidateUsername()
        {
            if (String.IsNullOrWhiteSpace(Username))
            {
                _strBuilder.Append("- username is empty\n");
                return;
            }
        }

        private void ValidatePassword(bool confirm = false)
        {
            if (Password == null)
            {
                _strBuilder.Append("- password is empty\n");
                return;
            }
            if (confirm && !Password.Equals(ConfirmPassword))
            {
                _strBuilder.Append("- passwords mismatch\n");
            }
        }

        private void ValidateEmail()
        {
            if (Email == null)
            {
                _strBuilder.Append("- email address is empty\n");
                return;
            }
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(Email);
            if (!match.Success) _strBuilder.Append("- email address is not valid\n");
        }

        public async Task<string> Register()
        {
            _strBuilder.Clear();
            ValidateUsername();
            ValidatePassword(true);
            ValidateEmail();
            var error = _strBuilder.ToString();
            _strBuilder.Clear();

            // If there is no error in validation, then try to register the user.
            if (!error.Any())
            {
                var user = new ParseUser()
                {
                    Username = Username,
                    Password = Password,
                    Email = Email
                };

                try
                {
                    await user.SignUpAsync();
                }
                catch (ParseException e)
                {
                    switch (e.Code)
                    {
                        case ParseException.ErrorCode.InvalidSessionToken:
                            error = "something bad happens, please try again";
                            break;
                        default:
                            error = e.Message;
                            break;
                    }
                }
            }

            return error;
        }

        public async Task<string> Login()
        {
            _strBuilder.Clear();
            ValidateUsername();
            ValidatePassword();
            var error = _strBuilder.ToString();
            _strBuilder.Clear();

            // If there is no error in validation, then try to log in the user.
            if (!error.Any())
            {
                try
                {
                    await ParseUser.LogInAsync(Username, Password);
                }
                catch (ParseException e)
                {
                    switch (e.Code)
                    {
                        case ParseException.ErrorCode.InvalidSessionToken:
                            error = "something bad happens, please try again";
                            break;
                        default :
                            error = e.Message;
                            break;
                    }
                }
            }

            return error;
        }

        public async Task<string> Update()
        {
            _strBuilder.Clear();
            ValidateUsername();
            ValidatePassword(true);
            ValidateEmail();
            var error = _strBuilder.ToString();
            _strBuilder.Clear();

            // If there is no error in validation, then try to register the user.
            if (!error.Any())
            {
                ParseUser.CurrentUser.Username = Username;
                ParseUser.CurrentUser.Password = Password;
                ParseUser.CurrentUser.Email = Email;

                try
                {
                    await ParseUser.CurrentUser.SaveAsync();
                }
                catch (ParseException e)
                {
                    switch (e.Code)
                    {
                        case ParseException.ErrorCode.InvalidSessionToken:
                            error = "something bad happens, please try again";
                            break;
                        default:
                            error = e.Message;
                            break;
                    }
                }
            }

            return error;
        }
    }
}
