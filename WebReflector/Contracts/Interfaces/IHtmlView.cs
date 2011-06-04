using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebReflector
{
    public interface IHtmlView
    {
        void RenderContent(TextWriter stream);
    }
}
