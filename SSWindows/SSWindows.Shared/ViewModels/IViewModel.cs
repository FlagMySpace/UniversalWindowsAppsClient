using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace SSWindows.ViewModels
{
    public interface IViewModel
    {
        INavigationService NavigationService { get; set; }
    }
}
