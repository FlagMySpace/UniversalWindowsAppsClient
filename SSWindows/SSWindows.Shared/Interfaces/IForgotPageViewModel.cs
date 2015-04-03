using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IForgotPageViewModel
    {
        string Email { get; set; }
        Task SendRequest();
    }
}
