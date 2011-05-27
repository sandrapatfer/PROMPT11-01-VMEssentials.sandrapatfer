using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebReflector
{
    public class TypeView : IHtmlView
    {
        Type m_type;
        HtmlElement m_content;

        public TypeView(Type t)
        {
            m_type = t;

//            var doc = new HtmlDocument();
//            m_content.h1(string.Format("Type: {0}", t.Name));
        }

        #region IHtmlView Members

        public string Html
        {
            get { throw new NotImplementedException(); }
        }

        public void AddContent(HtmlElement node)
        {
            node.h1(string.Format("Type: {0}", m_type.Name));
        }

        #endregion
    }
}
