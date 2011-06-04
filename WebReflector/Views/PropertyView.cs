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
                h1(string.Format("Property of type: {0}", m_property.ParentType.Name)),
                ul(li("Property", ul(li(string.Format("Name: {0}", m_property.Name)),
                                  li("Type: ", a(m_property.ItemType)))),
                   li(a("Back to Type", m_property.ParentType.Uri)))
            );
        }
    }
}
