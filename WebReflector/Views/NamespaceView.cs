using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class NamespaceView : HtmlView
    {
        List<string> m_nspaceNames;
        public NamespaceView(List<string> nspaceNames)
        {
            m_nspaceNames = nspaceNames;
        }

        public override HtmlNode Body()
        {
            //TODO
            return null;
        }
    }
}
