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
            //TODO
            return null;
        }
    }
}
