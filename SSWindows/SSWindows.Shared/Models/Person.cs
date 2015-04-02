﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

        private void ValidateUsername()
        {
            if (String.IsNullOrWhiteSpace(Username))
            {
                _strBuilder.Append("- username is empty\n");
                return;
            }
        }

        private void ValidatePassword()
        {
            if (Password == null)
            {
                _strBuilder.Append("- password is empty\n");
                return;
            }
            if (Password.Length < 8)
            {
                _strBuilder.Append("- password at least 8 characters\n");
            }
            if (!Password.Equals(ConfirmPassword))
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

        public string ValidateRegister()
        {
            ValidateUsername();
            ValidatePassword();
            ValidateEmail();
            var result = _strBuilder.ToString();
            _strBuilder.Clear();
            return result;
        }
    }
}
