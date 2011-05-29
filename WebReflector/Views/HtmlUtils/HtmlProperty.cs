using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebReflector
{
    public class HtmlProperty
    {
        string m_name;
        string m_value;

        public HtmlProperty(string name, string value)
        {
            m_name = name;
            m_value = value;
        }

        public void RenderContent(TextWriter stream)
        {
            stream.Write(" {0}={1}", m_name, m_value);
        }
    }
}
