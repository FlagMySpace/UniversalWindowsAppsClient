using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface ILoginPageViewModel : IViewModel
    {
        Task Login();
        Task Register();
    }
}