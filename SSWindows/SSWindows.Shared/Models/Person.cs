using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Parse;
using SSWindows.Interfaces;

namespace SSWindows.Models
{
    public class Person : BindableBase, IPerson
    {
        private string _mConfirmPassword = default(string);
        private string _mEmail = default(string);
        private string _mPassword = default(string);
        private string _mUsername = default(string);
        private readonly StringBuilder _strBuilder;

        public Person()
        {
            _strBuilder = new StringBuilder("");
        }

        public string Username
        {
            get { return _mUsername; }
            set
            {
                _mUsername = value;
                SetProperty(ref _mUsername, value);
            }
        }

        public string Password
        {
            get { return _mPassword; }
            set
            {
                _mPassword = value;
                SetProperty(ref _mPassword, value);
            }
        }

        public string ConfirmPassword
        {
            get { return _mConfirmPassword; }
            set
            {
                _mConfirmPassword = value;
                SetProperty(ref _mConfirmPassword, value);
            }
        }

        public string Email
        {
            get { return _mEmail; }
            set
            {
                _mEmail = value;
                SetProperty(ref _mEmail, value);
            }
        }

        public ParseUser LoggedInUser
        {
            get { return ParseUser.CurrentUser; }
        }

        private void ValidateUsername(bool required = true)
        {
            if (required && String.IsNullOrWhiteSpace(Username))
            {
                _strBuilder.Append("username is empty\n");
            }
            if (String.IsNullOrWhiteSpace(Username)) return;
        }

        private void ValidatePassword(bool confirm = false, bool required = true)
        {
            if (required && String.IsNullOrWhiteSpace(Password))
            {
                _strBuilder.Append("password is empty\n");
            }
            if (String.IsNullOrWhiteSpace(Password)) return;
            if (confirm && !Password.Equals(ConfirmPassword))
            {
                _strBuilder.Append("passwords mismatch\n");
            }
        }

        private void ValidateEmail(bool required = true)
        {
            if (required && String.IsNullOrWhiteSpace(Email))
            {
                _strBuilder.Append("email address is empty\n");
            }
            if (String.IsNullOrWhiteSpace(Email)) return;
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(Email);
            if (!match.Success) _strBuilder.Append("email address is not valid\n");
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
                var user = new ParseUser
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
                        default:
                            error = e.Message;
                            break;
                    }
                }
            }

            return error;
        }

        public async Task<string> Update(string oldUsername, string oldPassword)
        {
            _strBuilder.Clear();
            ValidateUsername(false);
            ValidatePassword(true, false);
            ValidateEmail(false);
            var error = _strBuilder.ToString();
            _strBuilder.Clear();

            // If there is no error in validation, then try to update the user.
            if (!error.Any())
            {
                if (!String.IsNullOrWhiteSpace(Username)) ParseUser.CurrentUser.Username = Username;
                if (!String.IsNullOrWhiteSpace(Password)) ParseUser.CurrentUser.Password = Password;
                if (!String.IsNullOrWhiteSpace(Email)) ParseUser.CurrentUser.Email = Email;

                try
                {
                    await ParseUser.LogInAsync(oldUsername, oldPassword);
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