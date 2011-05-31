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
                h1("Field"), 
                ul(li(string.Format("Name: {0}", m_field.Name)),
                   li(string.Format("Type: {0}", m_field.TypeName))),
                a("Type", m_field.Type.Uri)
            );
        }
    }
}
