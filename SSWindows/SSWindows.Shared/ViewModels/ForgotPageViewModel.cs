using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Parse;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class ForgotPageViewModel : ViewModel, IForgotPageViewModel
    {
        public string Email { get; set; }

        public async System.Threading.Tasks.Task SendRequest()
        {
            try
            {
                await ParseUser.RequestPasswordResetAsync("email@example.com");
            }
            catch (Exception e)
            {
                var dialog = new MessageDialog(e.Message, "Error");
                dialog.ShowAsync();
            }
        }
    }
}
