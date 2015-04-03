using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Interfaces;
using SSWindows.Models;

namespace SSWindows.DesignTime
{
    public class LoginPageViewModel : ILoginPageViewModel
    {
        public Person Person { get; set; }
        public Task ValidateLogin()
        {
            return null;
        }

        public Task ValidateRegister()
        {
            return null;
        }
    }
}
