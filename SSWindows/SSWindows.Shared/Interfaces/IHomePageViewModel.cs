using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace SSWindows.Interfaces
{
    public interface IHomePageViewModel : IViewModel
    {
        Task Logout();
        IHomePage ViewHomePage { get; set; }
    }
}
