using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector
{
    public class TypeHandler : IHandler
    {
        #region IHandler Members

        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            // TODO remover {} dos parametros para ficar mais bonito
            return new TypeView(Reflector.Reflector.GetContext(parameters["{ctx}"]).GetNamespace(parameters["{namespace}"]).GetType(parameters["{shortName}"]));
        }

        #endregion
    }
}
