using System;
using System.Collections.Generic;
using System.Text;

namespace WebReflector
{
    public class TypeView : HtmlView
    {
        Reflector.ContextType m_type;
        public TypeView(Reflector.ContextType t)
        {
            m_type = t;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Type: {0}", m_type.Type.Name))
            );
        }
    }
}
