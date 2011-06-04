using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class FieldView : HtmlView
    {
        Reflector.ContextTypeField m_field;
        public FieldView(Reflector.ContextTypeField f)
        {
            m_field = f;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Field of type: {0}", m_field.ParentType.Name)),
                ul(li("Field", ul(li(string.Format("Name: {0}", m_field.Name)),
                                  li("Type: ", a(m_field.ItemType)))),
                   li(a("Back to Type", m_field.ParentType.Uri)))
            );
        }
    }
}

