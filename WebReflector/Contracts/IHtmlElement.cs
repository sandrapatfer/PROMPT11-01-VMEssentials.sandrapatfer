using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public interface IHtmlElement
    {
        void AppendChild(IHtmlElement child);
    }
}
