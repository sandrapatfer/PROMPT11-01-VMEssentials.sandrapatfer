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
                h1(string.Format("Type: {0}", m_type.Type.Name)),
                ul(li("Assembly", a(m_type.Assembly)),
                   li("Namespace", a(m_type.Namespace)),
                   li(a("Constructors", m_type.ConstructorsUri)),
                   li("Methods", ul(m_type.Methods.ConvertAll(m => li(a(m))).ToArray())),
                   li("Fields", ul(m_type.Fields.ConvertAll(m => li(a(m))).ToArray())),
                   li("Properties", ul(m_type.Properties.ConvertAll(m => li(a(m))).ToArray())),
                   li("Events", ul(m_type.Events.ConvertAll(m => li(a(m))).ToArray()))
                   ));

            
            /*                h2("Methods"),
                table(tr(th("Name"), th("Type")),
                    m_type.Methods.ConvertAll(f => tr(td(a(f.Name, f.Uri)), td(f.TypeName))).ToArray())
                h2("Fields"),

                /*table(tr(th("Name"), th("Type")),
                    m_type.Fields.ConvertAll(f => tr(td(a(f.Name, f.Uri)), td(f.TypeName))).ToArray())*/
        }
    }
}
