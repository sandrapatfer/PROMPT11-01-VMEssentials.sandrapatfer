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
            // TODO falta return type
            return body(
                h1(string.Format("Method: {0}", m_name)),
                ul(m_methods.ConvertAll(m => li(
                    m.MethodString,
                    ul(m.Parameters.ConvertAll(p => li(string.Format("{0}: {1}", p.Name, p.ParameterType.Name))).ToArray())
                    )).ToArray())
            );
        }
    }
}
