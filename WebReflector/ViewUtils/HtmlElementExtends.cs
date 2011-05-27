using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebReflector
{
    public static class HtmlElementExtends
    {
        public static HtmlElement body(this HtmlElement node, params HtmlElement[] nodes)
        {
            nodes.ToList().ForEach(n => node.AppendChild(n));
            return node;
        }

        public static HtmlElement h1(this HtmlElement node, string hText)
        {
            var elem = node.Document.CreateElement("h1");
            elem.InnerText = hText;
            node.AppendChild(elem);
            return node;
        }
    }
}
