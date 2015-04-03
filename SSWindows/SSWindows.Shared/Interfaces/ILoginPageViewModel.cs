using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SSWindows.Models;

namespace SSWindows.Interfaces
{
    public interface ILoginPageViewModel : IViewModel
    {
        Person Person { get; set; }
        Task<string> Login();
        Task<string> Register();
    }
}
