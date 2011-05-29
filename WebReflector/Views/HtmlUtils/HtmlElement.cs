using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebReflector
{
    public class HtmlElement : HtmlNode
    {
        protected List<HtmlNode> m_childs;
        public string Tag { get; internal set; }

        public List<HtmlProperty> Properties { get; set; }

        public HtmlElement(string tagName)
        {
            Tag = tagName;
            m_childs = new List<HtmlNode>();
        }

        public HtmlElement(string tagName, string text)
        {
            Tag = tagName;
            m_childs = new List<HtmlNode>() { new HtmlTextNode(text) };
        }

        public HtmlElement(string tagName, params HtmlNode[] childs)
        {
            Tag = tagName;
            m_childs = childs.ToList();
        }

        public HtmlElement(string tagName, List<HtmlNode> childs)
        {
            Tag = tagName;
            m_childs = childs;
        }

        public override void RenderContent(TextWriter stream)
        {
            stream.Write(string.Format("<{0}", Tag));
            if (Properties != null)
                Properties.ForEach(p => p.RenderContent(stream));
            stream.WriteLine(">");
            m_childs.ForEach(n => n.RenderContent(stream));
            stream.WriteLine(string.Format("</{0}>", Tag));
        }

    }
}
