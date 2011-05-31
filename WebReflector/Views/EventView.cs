using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class EventView : HtmlView
    {
        Reflector.ContextTypeEvent m_event;
        public EventView(Reflector.ContextTypeEvent e)
        {
            m_event = e;
        }

        public override HtmlNode Body()
        {
            return body(
                h1("Event"),
                ul(li(string.Format("Name: {0}", m_event.Name)),
                   li(string.Format("Type: {0}", m_event.TypeName))),
                a("Type", m_event.Type.Uri)
            );
        }
    }
}
