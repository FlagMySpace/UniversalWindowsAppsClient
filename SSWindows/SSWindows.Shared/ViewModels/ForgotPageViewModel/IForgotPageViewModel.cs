﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SSWindows.ViewModels;

namespace SSWindows.Interfaces
{
    public interface IForgotPageViewModel : IViewModel
    {
        string Email { get; set; }
        Task SendRequest(string emailAddress);
        IForgotPage ForgotPage { get; set; }
    }
}
