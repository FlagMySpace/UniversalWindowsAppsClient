using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace SSWindows.Interfaces
{
    public interface IForgotPageViewModel
    {
        string Email { get; set; }
        Task<string> SendRequest(string emailAddress);
        INavigationService NavigationService { get; set; }
    }
}
