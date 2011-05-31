using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new ContextView(Reflector.Reflector.GetContext(parameters["{ctx}"]));
        }
    }
}
