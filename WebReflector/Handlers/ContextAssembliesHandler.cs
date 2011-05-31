using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextAssembliesHandler : IHandler
    {
        #region IHandler Members

        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new ContextAssembliesView(Reflector.Reflector.GetContext(parameters["{ctx}"]));
        }

        #endregion
    }
}
