using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector
{
    public class TypeHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new TypeView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespace}"]).GetType(parameters["{shortName}"]));
        }
    }
}
