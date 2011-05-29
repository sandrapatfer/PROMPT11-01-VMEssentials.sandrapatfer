using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    class TemplateHandler<T>
    {
        private string m_template;
        private List<TemplatePart> m_templateParts = new List<TemplatePart>();
        public string Template
        {
            get
            {
                return m_template;
            }
            set
            {
                m_template = value;
                string[] uriParts = m_template.Split(new char[] { '/' });
                uriParts.ToList().ForEach(p => { if (!string.IsNullOrEmpty(p)) m_templateParts.Add(new TemplatePart(p)); });
            }
        }
        //public IHandler Handler { get; set; }
        public T Handler { get; set; }

        public bool MapsTemplate(string uri)
        {
            string[] uriParts = uri.Split(new char[] { '/' });
            if (uriParts.Length < 2)
                throw new UriParsingException() { Uri = uri };
            else
            {
                if (m_templateParts.Count == 0 && string.IsNullOrEmpty(uriParts[1]))
                    // special case for root
                    return true;
                if (uriParts.Length - 1 == m_templateParts.Count)
                {
                    for (int i = 1; i < uriParts.Length; i++)
                    {
                        if (!m_templateParts[i-1].Maps(uriParts[i]))
                            return false;
                    }
                    return true;
                }
                else
                    return false;
            }
        }

        public Dictionary<string, string> MapUriParameters(string uri)
        {
            if (m_templateParts.Count != 0)
            {
                var parameters = new Dictionary<string, string>();
                string[] uriParts = uri.Split(new char[] { '/' });
                if (uriParts.Length - 1 == m_templateParts.Count)
                {
                    for (int i = 1; i < uriParts.Length; i++)
                    {
                        if (!m_templateParts[i - 1].IsConstant)
                            parameters.Add(m_templateParts[i - 1].Value, uriParts[i]);
                    }
                }
                return parameters;
            }
            else
                return null;
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
