using System;
using System.Threading.Tasks;

namespace SSWindows.Common
{
    public interface IError
    {
        void CaptureError(Exception ex);
        Task<bool> InvokeError();
    }
}