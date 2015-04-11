using System.Threading.Tasks;

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