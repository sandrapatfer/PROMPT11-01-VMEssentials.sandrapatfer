using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public interface IRouter<T>
    {
        T LookupHandler(string request, out Dictionary<string, string> parameters);
    }
}
