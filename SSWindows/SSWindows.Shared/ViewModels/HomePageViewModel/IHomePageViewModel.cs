using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SSWindows.ViewModels;

namespace SSWindows.Interfaces
{
    public interface IHomePageViewModel : IViewModel
    {
        Task Logout();
        IHomePage HomePage { get; set; }
    }
}
