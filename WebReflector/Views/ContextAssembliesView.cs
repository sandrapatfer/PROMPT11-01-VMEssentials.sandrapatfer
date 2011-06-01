using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextAssembliesView : HtmlView
    {
        Reflector.Context m_context;
        List<Reflector.ContextAssembly> m_assemblies;
        public ContextAssembliesView(Reflector.Context context)
        {
            m_context = context;
            m_assemblies = context.Assemblies;
        }
        
        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Context: {0}", m_context.Name)),
                li("Assembly List", ul(m_assemblies.ConvertAll(b => li(a(b))).ToArray())),
                li(a("Back to Context", m_context.Uri))
            );
        }
    }
}
