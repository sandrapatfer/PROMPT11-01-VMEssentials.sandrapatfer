using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextView : HtmlView
    {
        Reflector.Context m_context;

        public ContextView(Reflector.Context context)
        {
            m_context = context;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Context: {0}", m_context.Name)),
                ul(
                    li(a("Assembly list", m_context.AssemblyUri)),
                    li(a("Namespace list", m_context.NamespaceUri)),
                    li(a("Back to context list", m_context.RootUri))
                )
            );
        }
    }
}
