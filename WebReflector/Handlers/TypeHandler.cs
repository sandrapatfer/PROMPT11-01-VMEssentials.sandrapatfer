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
 //           Assembly typeAssembly = ReflectorConfig.Singleton.GetAssembly(parameters["ctx"], parameters["assemblyName"]);

            return new TypeView(typeof(string));
        }

        #endregion
    }
}
