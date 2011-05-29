using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebReflector
{
    public abstract class HtmlNode
    {
        public abstract void RenderContent(TextWriter stream);
    }
}
