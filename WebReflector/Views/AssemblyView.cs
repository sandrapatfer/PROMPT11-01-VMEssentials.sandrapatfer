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
                h1(string.Format("Assembly: {0}", m_assembly.Name)),
                ul(li(string.Format("Friendly name: {0}", m_assembly.Name)),
                   li(string.Format("Public key: {0}", m_assembly.PublicKey)),
                    li("Type List",
                        ul(m_assembly.TypesByNamespace, (nspace, typeList) => li(a(nspace.FriendlyFullName, nspace.Uri), ul(typeList.ConvertAll(t => li(a(t))).ToArray()))))),
                li(a("Back to List of Assemblies", m_assembly.Context.AssemblyUri))
                );
        }
    }
}
