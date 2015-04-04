using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IHomePageViewModel : IViewModel
    {
        Task Logout();
    }
}
