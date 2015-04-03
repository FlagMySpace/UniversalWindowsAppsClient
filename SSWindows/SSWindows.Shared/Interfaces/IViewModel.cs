using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace SSWindows.Interfaces
{
    public interface IViewModel
    {
        INavigationService NavigationService { get; set; }
    }
}
