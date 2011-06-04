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
                h1(string.Format("Event of type: {0}", m_event.ParentType.Name)),
                ul(li("Event", ul(li(string.Format("Name: {0}", m_event.Name)),
                                  li("Type: ", a(m_event.ItemType)))),
                   li(a("Back to Type", m_event.ParentType.Uri)))
            );
        }
    }
}
