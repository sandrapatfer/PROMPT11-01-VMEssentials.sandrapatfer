using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class FieldHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new FieldView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespace}"]).
                GetType(parameters["{shortName}"]).GetField(parameters["{fieldName}"]));
        }
    }
}
