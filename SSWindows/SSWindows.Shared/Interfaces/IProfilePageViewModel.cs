using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IProfilePageViewModel
    {
        Task UpdateProfile(string oldUsername, string oldPassword);
    }
}
