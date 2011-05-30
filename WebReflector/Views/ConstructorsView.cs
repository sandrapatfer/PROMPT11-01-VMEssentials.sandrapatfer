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
                h1(string.Format("Constructors of type: {0}", m_type.Type.Name)),
                ul(m_type.Constructors)
            );
        }
    }
}
