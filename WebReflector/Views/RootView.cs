using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class RootView : HtmlView
    {
        List<Reflector.Context> m_contextList;
        public RootView(List<Reflector.Context> contextList)
        {
            m_contextList = contextList;
        }

        public override HtmlNode Body()
        {
            return body(
                h1("Context List"),
                ul(m_contextList.ConvertAll(c => li(a(c.Name, c.Uri))).ToArray())
            );
        }
    }
}
