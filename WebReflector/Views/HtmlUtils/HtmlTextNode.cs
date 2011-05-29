using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebReflector
{
    public class HtmlTextNode : HtmlNode
    {
        public string Text { get; internal set; }

        public HtmlTextNode(string text)
        {
            Text = text;
        }

        public override void RenderContent(TextWriter stream)
        {
            stream.WriteLine(Text);
        }
    }
}
