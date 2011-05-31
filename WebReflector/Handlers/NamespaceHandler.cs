using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class NamespaceHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new NamespaceView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespacePrefix}"]));
        }
    }
}
