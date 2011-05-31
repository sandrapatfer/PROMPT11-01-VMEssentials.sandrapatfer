using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class MethodHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new MethodView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespace}"]).
                GetType(parameters["{shortName}"]).GetMethods(parameters["{methodName}"]));
        }
    }
}
