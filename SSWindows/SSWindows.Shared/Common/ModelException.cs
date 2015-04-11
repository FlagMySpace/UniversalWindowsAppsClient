using System;
using System.Collections.Generic;
using System.Text;

namespace SSWindows.Common
{
    public class ModelException : Exception
    {
        public int Code { get; set; }

        public ModelException(int code, string message) :
            base(message)
        {
            Code = code;
        }
    }
}
