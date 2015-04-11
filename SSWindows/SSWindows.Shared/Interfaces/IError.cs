using System;
using System.Threading.Tasks;

namespace SSWindows.Interfaces
{
    public interface IError
    {
        void CaptureError(Exception ex);
        Task<bool> InvokeError();
    }
}