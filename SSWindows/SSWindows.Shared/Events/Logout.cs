using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.PubSubEvents;

namespace SSWindows.Events
{
    public class Logout : PubSubEvent<string>
    {
    }
}
