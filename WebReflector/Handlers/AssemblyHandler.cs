using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class AssemblyHandler : IHandler
    {
        #region IHandler Members

        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new AssemblyView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetAssembly(parameters["{assemblyName}"]));
        }

        #endregion
    }
}
