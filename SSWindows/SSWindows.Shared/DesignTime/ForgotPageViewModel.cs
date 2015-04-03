using Microsoft.Practices.Prism.Mvvm;
using SSWindows.Interfaces;

namespace SSWindows.DesignTime
{
    public class ForgotPageViewModel : ViewModel, IForgotPageViewModel
    {
        public string Email { get; set; }

        public async System.Threading.Tasks.Task SendRequest()
        {
        }
    }
}
