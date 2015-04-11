using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using SSWindows.Interfaces;

namespace SSWindows.Common
{
    public class ErrorHandler : IError
    {
        private ExceptionDispatchInfo _capturedException;

        public void CaptureError(Exception ex)
        {
            _capturedException = ExceptionDispatchInfo.Capture(ex);
        }

        public async Task<bool> InvokeError()
        {
            if (_capturedException != null)
            {
                await new MessageDialog(_capturedException.SourceException.Message, "Error").ShowAsync();
                _capturedException = null;
                return true;
            }
            return false;
        }
    }
}