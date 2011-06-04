using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class MethodView : HtmlView
    {
        string m_name;
        List<Reflector.ContextTypeMethod> m_methods;
        public MethodView(List<Reflector.ContextTypeMethod> methods)
        {
            m_name = methods[0].Name;
            m_methods = methods;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Methods of type: {0}", m_methods[0].Type.Name)),
                ul(li("Method",
                    ul(m_methods.ConvertAll(m => li(m.MethodString,
                        ul(li("Parameters", 
                              ul(m.Parameters.ConvertAll(p => li(string.Format("{0}: ", p.Name), a(p.ItemType))).ToArray())),
                           li("Returns: ", a(m.Return.ItemType))
                        ))).ToArray())),
                   li(a("Back to Type", m_methods[0].Type.Uri)))
            );
        }
    }
}
