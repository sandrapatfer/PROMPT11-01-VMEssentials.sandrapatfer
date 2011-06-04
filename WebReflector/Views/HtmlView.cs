using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

namespace WebReflector
{
    public abstract class HtmlView : IHtmlView
    {
        protected static HtmlElement h1(string text)
        {
            return new HtmlElement("h1", text);
        }
        protected static HtmlElement h2(string text)
        {
            return new HtmlElement("h2", text);
        }
        protected static HtmlNode body(params HtmlNode[] childs)
        {
            return new HtmlElement("body", childs);
        }
        protected static HtmlNode table(params HtmlNode[] rows)
        {
            return new HtmlElement("table", rows);
        }
        protected static HtmlNode table(HtmlNode th, params HtmlNode[] rows)
        {
            return new HtmlElement("table", new HtmlNode[] { th }.Union(rows).ToArray());
        }
        protected static HtmlNode th(string text)
        {
            return new HtmlElement("th", text);
        }
        protected static HtmlNode tr(params HtmlNode[] cells)
        {
            return new HtmlElement("tr", cells);
        }
        protected static HtmlNode td(string data)
        {
            return new HtmlElement("td", data);
        }
        protected static HtmlNode td(HtmlNode data)
        {
            return new HtmlElement("td", data);
        }
        protected static HtmlNode ul(HtmlNode first, params HtmlNode[] list)
        {
            return new HtmlElement("ul", new HtmlNode[] { first }.Union(list).ToArray());
        }
        protected static HtmlNode ul(params HtmlNode[] list)
        {
            return new HtmlElement("ul", list);
        }
        protected static HtmlNode ul(List<string> list)
        {
            return new HtmlElement("ul", list.ConvertAll(n => new HtmlTextNode(n)).Cast<HtmlNode>().ToList());
        }
        protected static HtmlNode ul<TKey, TValue>(SortedDictionary<TKey, TValue> dic, Func<TKey, TValue, HtmlNode> conv)
        {
            var prms = new List<HtmlNode>();
            foreach (var key in dic.Keys)
            {
                prms.Add(conv(key, dic[key]));
            }
            return new HtmlElement("ul", prms);
        }
        protected static HtmlNode li(HtmlNode item)
        {
            return new HtmlElement("li", item);
        }
        protected static HtmlNode li(string item)
        {
            return new HtmlElement("li", new HtmlTextNode(item));
        }
        protected static HtmlNode li(string item, params HtmlNode[] childs)
        {
            return new HtmlElement("li", new HtmlNode[] { new HtmlTextNode(item) }.Union(childs).ToArray() );
        }
        protected static HtmlNode li(HtmlNode item, HtmlNode child)
        {
            return new HtmlElement("li", new HtmlNode[] { item, child });
        }
        protected static HtmlNode li(params HtmlNode[] childs)
        {
            return new HtmlElement("li", childs);
        }
        protected static HtmlNode a(string text, string uri)
        {
            return new HtmlElement("a", new HtmlTextNode(text)) { Properties = new List<HtmlProperty> { new HtmlProperty("href", HttpUtility.UrlEncode(uri)) } };
        }
        protected static HtmlNode a(IContextEntity entity)
        {
            return new HtmlElement("a", new HtmlTextNode(entity.Name)) { Properties = new List<HtmlProperty> { new HtmlProperty("href", HttpUtility.UrlEncode(entity.Uri)) } };
        }

        public abstract HtmlNode Body();

        public void RenderContent(TextWriter stream)
        {
            // TODO write head

            Body().RenderContent(stream);
        }

    }
}