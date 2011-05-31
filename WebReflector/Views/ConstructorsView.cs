using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ConstructorsView : HtmlView
    {
        Reflector.ContextType m_type;
        public ConstructorsView(Reflector.ContextType t)
        {
            m_type = t;
        }

        public override HtmlNode Body()
        {
            return body(
                h1("Constructors"),
                ul(m_type.Constructors.ConvertAll(c => li(
                    c.MethodString, 
                    ul(c.Parameters.ConvertAll(p => li(string.Format("{0}: {1}", p.Name, p.ParameterType.Name))).ToArray())
                    )).ToArray())
            );
        }
    }
}
