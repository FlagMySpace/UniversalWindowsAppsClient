using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SSWindows.Interfaces;

namespace SSWindows.Interfaces
{
    public interface IViewModel
    {
        INavigationService NavigationService { get; set; }
    }
}
