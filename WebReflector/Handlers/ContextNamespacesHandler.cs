using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextNamespacesHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new ContextNamespacesView(Reflector.Reflector.GetContext(parameters["{ctx}"]).RootNamespace);
        }
    }
}
