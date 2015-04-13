using System.Threading.Tasks;
using SSWindows.ViewModels;

namespace SSWindows.Interfaces
{
    public interface ILoginPageViewModel : IViewModel
    {
        Task Login();
        Task Register();
        ILoginPage LoginPage { get; set; }
    }
}