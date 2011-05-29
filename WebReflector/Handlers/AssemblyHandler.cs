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
            var ctx = Reflector.GetContext(parameters["{ctx}"]);
            return new AssemblyView(ctx.GetAssemblies());
        }

        #endregion
    }
}
