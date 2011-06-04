using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using WebReflector.Attributes;

namespace WebReflector
{
    public class RoutingTemplate : IRoutingTemplate
    {

        string m_template;

        public RoutingTemplate(string template)
        {
            m_template = template;
            string[] uriParts = m_template.Split(new char[] { '/' });
            uriParts.ToList().ForEach(p => { if (!string.IsNullOrEmpty(p)) m_templateParts.Add(new TemplatePart(p)); });
        }

        private List<TemplatePart> m_templateParts = new List<TemplatePart>();

        public void Register(Type typeToConfigure, string parameterToConfigure)
        {
            // looks for a static property to configure the type
            foreach (var p in typeToConfigure.GetProperties())
            {
                foreach (var a in p.GetCustomAttributes(false))
                {
                    if (a is TemplateAttribute && ((TemplateAttribute)a).Name == parameterToConfigure)
                    {
                        p.SetValue(null, this, null);
                        return;
                    }
                }
            }
        }

        public bool MapsTemplate(string uri, out Dictionary<string, string> parameters)
        {
            parameters = null;
            string[] uriParts = uri.Split(new char[] { '/' });
            if (uriParts.Length < 2)
                throw new UriParsingException() { Uri = uri };
            else
            {
                if (m_templateParts.Count == 0 && string.IsNullOrEmpty(uriParts[1]))
                {
                    // special case for root
                    parameters = null;
                    return true;
                }
                if (uriParts.Length - 1 == m_templateParts.Count)
                {
                    for (int i = 1; i < uriParts.Length; i++)
                    {
                        if (!m_templateParts[i - 1].Maps(uriParts[i]))
                        {
                            return false;
                        }
                    }

                    // TODO remover {} dos parametros
                    parameters = new Dictionary<string, string>();
                    for (int i = 1; i < uriParts.Length; i++)
                    {
                        if (!m_templateParts[i - 1].IsConstant)
                            parameters.Add(m_templateParts[i - 1].Value, uriParts[i]);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string FormatString
        {
            get
            {
                StringBuilder ret = new StringBuilder();
                int paramCount = 0;
                bool first = true;
                foreach (var part in m_templateParts)
                {
                    if (!first)
                        ret.Append('/');
                    else
                        first = false;
                    if (part.IsConstant)
                        ret.AppendFormat(@"{0}", part.Value);
                    else
                    {
                        ret.AppendFormat(@"{{{0}}}", paramCount);
                        paramCount++;
                    }
                }
                return ret.ToString();
            }
        }

        class TemplatePart
        {
            public string Value { get; internal set; }
            public bool IsConstant { get; internal set; }

            public TemplatePart(string part)
            {
                Value = part;
                if (string.IsNullOrEmpty(part))
                    IsConstant = true;
                else if (part.StartsWith("{"))
                    IsConstant = false;
                else
                    IsConstant = true;
            }

            public bool Maps(string uri)
            {
                if (IsConstant)
                    return string.Compare(Value, uri, false) == 0;
                else
                    return true;
            }
        }
    }
}
