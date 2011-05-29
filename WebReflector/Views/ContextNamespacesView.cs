using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextNamespacesView : HtmlView
    {
        List<Reflector.ContextNamespace> m_nspaces;
        public ContextNamespacesView(List<Reflector.ContextNamespace> nspaces)
        {
            m_nspaces = nspaces;
        }

        public override HtmlNode Body()
        {
            return body(
                h1("Namespace List"),
                ul(m_nspaces.ConvertAll(n => li(a(n.Name, n.Uri))).ToArray())
                );
        }
    }
}
