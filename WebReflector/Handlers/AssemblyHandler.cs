using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class AssemblyHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new AssemblyView(Reflector.Reflector.GetContext(parameters[Utils.ContextTemplatePart]).GetAssembly(parameters["{assemblyName}"]));
        }
    }
}
