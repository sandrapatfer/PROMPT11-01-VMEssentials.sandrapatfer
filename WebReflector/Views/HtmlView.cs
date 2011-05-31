using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace WebReflector
{
    public abstract class HtmlView : IHtmlView
    {
        protected HtmlElement h1(string text)
        {
            return new HtmlElement("h1", text);
        }
        protected HtmlElement h2(string text)
        {
            return new HtmlElement("h2", text);
        }
        protected HtmlNode body(params HtmlNode[] childs)
        {
            return new HtmlElement("body", childs);
        }
        protected HtmlNode table(params HtmlNode[] rows)
        {
            return new HtmlElement("table", rows);
        }
        protected HtmlNode table(HtmlNode th, params HtmlNode[] rows)
        {
            return new HtmlElement("table", new HtmlNode[] { th }.Union(rows).ToArray());
        }
        protected HtmlNode th(string text)
        {
            return new HtmlElement("th", text);
        }
        protected HtmlNode tr(params HtmlNode[] cells)
        {
            return new HtmlElement("tr", cells);
        }
        protected HtmlNode td(string data)
        {
            return new HtmlElement("td", data);
        }
        protected HtmlNode td(HtmlNode data)
        {
            return new HtmlElement("td", data);
        }
        protected HtmlNode ul(params HtmlNode[] list)
        {
            return new HtmlElement("ul", list);
        }
        protected HtmlNode ul(List<string> list)
        {
            return new HtmlElement("ul", list.ConvertAll(n => new HtmlTextNode(n)).Cast<HtmlNode>().ToList());
        }
        protected HtmlNode ul<TKey, TValue>(Dictionary<TKey, TValue> dic, Func<TKey, TValue, HtmlNode> conv)
        {
            var prms = new List<HtmlNode>();
            foreach (var key in dic.Keys)
            {
                prms.Add(conv(key, dic[key]));
            }
            return new HtmlElement("ul", prms);
        }

        // TODO tentar converter numa função que recebe uma lista e uma função para converter cada item num htmlnode
        protected HtmlNode li(HtmlNode item)
        {
            return new HtmlElement("li", item);
        }
        protected HtmlNode li(string item)
        {
            return new HtmlElement("li", new HtmlTextNode(item));
        }
        protected HtmlNode li(string item, HtmlNode child)
        {
            return new HtmlElement("li", new HtmlTextNode(item), child);
        }
        protected HtmlNode a(string text, string uri)
        {
            return new HtmlElement("a", new HtmlTextNode(text)) { Properties = new List<HtmlProperty> { new HtmlProperty("href", uri) } };
        }
        protected HtmlNode a(IContextEntity entity)
        {
            return new HtmlElement("a", new HtmlTextNode(entity.Name)) { Properties = new List<HtmlProperty> { new HtmlProperty("href", entity.Uri) } };
        }

        #region IHtmlView Members
        public string Html
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public abstract HtmlNode Body();

        public void RenderContent(TextWriter stream)
        {
            // TODO write head

            Body().RenderContent(stream);
        }

    }
}