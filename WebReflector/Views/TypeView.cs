using System;
using System.Collections.Generic;
using System.Text;
using WebReflector.Reflector;

namespace WebReflector
{
    public class TypeView : HtmlView
    {
        ContextType m_type;
        public TypeView(ContextType t)
        {
            m_type = t;
        }

        public override HtmlNode Body()
        {
            return body(
                h1(string.Format("Type: {0}", m_type.Name)),
                ul(li("Assembly: ", a(m_type.Assembly), 
                      ul(li(string.Format("Name: {0}", m_type.Assembly.Name)),
                         li(string.Format("FullName: {0}", m_type.Assembly.FullName)))),
                   li("Namespace: ", a(m_type.Namespace.FriendlyFullName, m_type.Namespace.Uri)),
                   li(a("Constructors", m_type.ConstructorsUri)),
                   li("Methods", ul(m_type.Methods.ConvertAll(k => li(a(k[0]))).ToArray())),
                   li("Fields", ul(m_type.Fields.ConvertAll(m => li(a(m))).ToArray())),
                   li("Properties", ul(m_type.Properties.ConvertAll(m => li(a(m))).ToArray())),
                   li("Events", ul(m_type.Events.ConvertAll(m => li(a(m))).ToArray()))
                   ));
        }

    }
}
