using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;
using Parse;

namespace SSWindows.Models
{
    public class Person
    {
        private StringBuilder _strBuilder;

        public Person()
        {
            _strBuilder = new StringBuilder("");
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

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
            var invalidSession = false;
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
                            invalidSession = true;
                            error = "something bad happens, please try again";
                            break;
                        default:
                            error = e.Message;
                            break;
                    }
                }
            }

            if (invalidSession)
            {
                await ParseUser.LogOutAsync();
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
            var invalidSession = false;
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
                            invalidSession = true;
                            error = "something bad happens, please try again";
                            break;
                        default :
                            error = e.Message;
                            break;
                    }
                }
            }

            if (invalidSession)
            {
                await ParseUser.LogOutAsync();
            }

            return error;
        }
    }
}
