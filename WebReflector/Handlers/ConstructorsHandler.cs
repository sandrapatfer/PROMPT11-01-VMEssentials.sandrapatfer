using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ConstructorsHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new ConstructorsView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespace}"]).GetType(parameters["{shortName}"]));
        }
    }
}
