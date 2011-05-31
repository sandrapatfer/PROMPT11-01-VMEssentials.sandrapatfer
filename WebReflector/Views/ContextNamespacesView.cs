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
                h1("Namespace List"),
                ul(li(a("Root", m_nspaceTree.Uri)),
                   m_nspaceTree.ChildNamespaces.ConvertAll(n => li(n)).ToArray())

/*                   m_nspaces.ConvertAll(n => 
                       {
                           if (
                       li(a(n.Name, n.Uri))).ToArray())*/
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
