using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace SSWindows.Interfaces
{
    public interface IHomePage
    {
        Task ShowLogoutProgress();
        Task HideLogoutProgress();
    }
}


namespace SSWindows.Interfaces
{
    public interface IProfilePage
    {
        Task ShowProgress(string text);
        Task HideProgress();
    }
}

namespace SSWindows.Interfaces
{
    public interface ILoginPage
    {
        Task ShowLoginProgress();
        Task ShowRegisterProgress();
        Task HideLoginProgress();
        Task HideRegisterProgress();
    }
}

namespace SSWindows.Interfaces
{
    public interface IForgotPage
    {
        Task ShowResetProgress();
        Task HideResetProgress();
    }
}
