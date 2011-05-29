using System;
using System.Collections.Generic;
using System.Text;

namespace WebReflector
{
    public class TypeView : HtmlView
    {
        Type m_type;
        public TypeView(Type t)
        {
            m_type = t;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Type: {0}", m_type.Name))
            );
        }
    }
}
