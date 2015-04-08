using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IHomePage
    {
        Task ShowLogoutProgressBar();
        Task HideLogoutProgressBar();
    }
}