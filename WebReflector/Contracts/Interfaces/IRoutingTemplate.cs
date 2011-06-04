using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public interface IRoutingTemplate
    {
        bool MapsTemplate(string uri, out Dictionary<string, string> parameters);

        string FormatString { get; }
    }
}
