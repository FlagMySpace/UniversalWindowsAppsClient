using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IProfilePage
    {
        Task ShowProgress(string text);
        Task HideProgress();
    }
}