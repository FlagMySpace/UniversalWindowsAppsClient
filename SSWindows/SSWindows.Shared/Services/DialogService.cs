using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using SSWindows.Interfaces;

namespace SSWindows.Services
{
    public class DialogService : IDialogService
    {
        public async void Show(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
