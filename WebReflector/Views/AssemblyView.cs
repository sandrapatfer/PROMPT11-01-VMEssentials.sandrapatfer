using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class AssemblyView : HtmlView
    {
        Reflector.ContextAssembly m_assembly;
        public AssemblyView(Reflector.ContextAssembly assembly)
        {
            m_assembly = assembly;
        }
        
        public override HtmlNode Body()
        {
            return body(
                h1("Assembly"),
                ul(li(string.Format("Friendly name: {0}", m_assembly.Name)),
                   li(string.Format("Public key: {0}", m_assembly.PublicKey))),
                ul(m_assembly.TypesByNamespace, (nspace, typeList) => li(nspace, ul(typeList.ConvertAll(t => li(t)).ToArray())))
                );
        }
    }
}
