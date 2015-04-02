using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Models;

namespace SSWindows.Interfaces
{
    public interface IMainPageViewModel
    {
        Person Person { get; set; }
        void ValidateLogin();
        void ValidateRegister();
    }
}
