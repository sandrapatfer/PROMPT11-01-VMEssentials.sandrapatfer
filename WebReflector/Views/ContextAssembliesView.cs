using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ContextAssembliesView : HtmlView
    {
        List<Reflector.ContextAssembly> m_assemblies;
        public ContextAssembliesView(List<Reflector.ContextAssembly> assemblies)
        {
            m_assemblies = assemblies;
        }
        
        public override HtmlNode Body()
        {
            return body(
                h1("Assembly List"),
                ul(m_assemblies.ConvertAll(b => li(a(b.Assembly.FullName, b.Uri))).ToArray())
                );
        }
    }
}
