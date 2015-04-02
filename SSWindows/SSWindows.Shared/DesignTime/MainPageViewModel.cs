using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Interfaces;

namespace SSWindows.DesignTime
{
    public class MainPageViewModel : IMainPageViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
