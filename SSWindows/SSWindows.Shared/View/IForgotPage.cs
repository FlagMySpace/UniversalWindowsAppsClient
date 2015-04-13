using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IForgotPage
    {
        Task ShowResetProgress();
        Task HideResetProgress();
    }
}