using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;

namespace SSWindows.Interfaces
{
    public interface IMainPageViewModel
    {
        string Username { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
    }
}
