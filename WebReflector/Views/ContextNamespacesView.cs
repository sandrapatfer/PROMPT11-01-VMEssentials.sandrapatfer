using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextNamespacesView : HtmlView
    {
        Reflector.ContextNamespace m_nspaceTree;
        public ContextNamespacesView(Reflector.ContextNamespace nspaceTree)
        {
            m_nspaceTree = nspaceTree;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Context: {0}", m_nspaceTree.Context.Name)),
                li("Namespace List",
                    ul(li(a(m_nspaceTree.FriendlyName, m_nspaceTree.Uri)),
                       m_nspaceTree.ChildNamespaces.ConvertAll(n => li(n)).ToArray())),
                li(a("Back to Context", m_nspaceTree.Context.Uri))
                );
        }

        HtmlNode li(Reflector.ContextNamespace nspace)
        {
            if (nspace.ChildNamespaces.Count == 0)
                return li(a(nspace));
            else
                return li(a(nspace),
                          ul(nspace.ChildNamespaces.ConvertAll(n => li(n)).ToArray()));
        }
    }
}
