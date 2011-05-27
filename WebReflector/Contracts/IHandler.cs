using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public interface IHandler
    {
        IHtmlView Handle(Dictionary<string, string> parameters);
    }
}
