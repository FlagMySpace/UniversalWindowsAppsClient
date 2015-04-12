using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace SSWindows.Controls
{
    public abstract class PageBasePhone : PageBase, IView
    {
        private StatusBarProgressIndicator _progressbar;

        protected PageBasePhone()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                _progressbar = StatusBar.GetForCurrentView().ProgressIndicator;
            }
        }

        public override async Task ShowProgressBar(string text)
        {
            await _progressbar.HideAsync();
            _progressbar.Text = text;
            await _progressbar.ShowAsync();
        }

        public override async Task HideProgressBar()
        {
            await _progressbar.HideAsync();
        }
    }
}
