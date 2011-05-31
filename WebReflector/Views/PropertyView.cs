using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class PropertyView : HtmlView
    {
        Reflector.ContextTypeProperty m_property;
        public PropertyView(Reflector.ContextTypeProperty p)
        {
            m_property = p;
        }

        public override HtmlNode Body()
        {
            // TODO ver o que sao parametros da propriedade
            return body(
                h1("Property"),
                ul(li(string.Format("Name: {0}", m_property.Name)),
                   li(string.Format("Type: {0}", m_property.TypeName))),
                a("Type", m_property.Type.Uri)
            );
        }
    }
}
