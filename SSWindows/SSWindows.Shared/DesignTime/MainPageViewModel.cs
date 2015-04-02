using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Interfaces;
using SSWindows.Models;

namespace SSWindows.DesignTime
{
    public class MainPageViewModel : IMainPageViewModel
    {
        public Person Person { get; set; }
        public void ValidateLogin()
        {
        }

        public void ValidateRegister()
        {
        }
    }
}
