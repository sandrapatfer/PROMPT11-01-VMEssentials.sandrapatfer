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
            // TODO field type devia ser link para tipo...
            return body(
                h1(string.Format("Type: {0}", m_type.Type.Name)),
                h2("Fields"),
                table(tr(th("Name"), th("Type")),
                    m_type.Fields.ConvertAll(f => tr(td(a(f.Name, f.Uri)), td(f.TypeName))).ToArray())
            );
        }
    }
}
