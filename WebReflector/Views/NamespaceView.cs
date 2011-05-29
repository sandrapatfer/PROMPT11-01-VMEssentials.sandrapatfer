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
            //TODO os tipos devem estar numa arvore de namespaces
//                ul(m_nspace.TypeTree, (nspace, typeList) => li(nspace, ul(typeList.ConvertAll(t => li(t)).ToArray())))

            return body(
                h1(string.Format("Namespace: {0}", m_nspace.Name)),
                ul(m_nspace.Types.ConvertAll(t => li(a(t.Type.Name, t.Uri))).ToArray())
                );
        }
    }
}
