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
                ul(
                    a("Assembly list", m_context.AssemblyUri),
                    a("Namespace list", m_context.NamespaceUri)
                ),
                back(m_context.ParentUri)
            );
        }
    }
}
