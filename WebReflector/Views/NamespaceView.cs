using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class NamespaceView : HtmlView
    {
        Reflector.ContextNamespace m_nspace;
        public NamespaceView(Reflector.ContextNamespace nspace)
        {
            m_nspace = nspace;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Namespace: {0}", m_nspace.IsRoot? m_nspace.FriendlyName : m_nspace.FullName)),
                m_nspace.IsRoot? 
                li("Parent Namespace:") :
                    li("Parent Namespace: ", a(m_nspace.ParentNamespace.FriendlyName, m_nspace.ParentNamespace.Uri)),
                li("Namespace List",
                    ul(m_nspace.ChildNamespaces.ConvertAll(n => li(a(n))).ToArray())),
                li("Type List",
                    ul(m_nspace.Types.ConvertAll(t => li(a(t.Name, t.Uri))).ToArray())),
                li(a("Back to List of Namespaces", m_nspace.Context.NamespaceUri))
                );
        }
    }
}
