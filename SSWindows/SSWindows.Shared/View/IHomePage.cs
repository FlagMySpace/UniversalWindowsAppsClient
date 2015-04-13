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