using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Models;

namespace SSWindows.Interfaces
{
    public interface ILoginPageViewModel
    {
        Person Person { get; set; }
        Task ValidateLogin();
        Task ValidateRegister();
    }
}
