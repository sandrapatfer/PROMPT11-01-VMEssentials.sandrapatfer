using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class NamespaceHandler : IHandler
    {
        #region IHandler Members

        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new NamespaceView();
        }

        #endregion
    }
}
