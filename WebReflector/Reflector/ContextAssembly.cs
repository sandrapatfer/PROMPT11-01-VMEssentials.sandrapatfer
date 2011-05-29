using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextAssembly
    {
        public Context Context { get; set; }
        public Assembly Assembly { get; set; }
        List<ContextType> m_types = new List<ContextType>();

        public ContextAssembly()
        {
        }

        public void RegisterType(ContextType type)
        {
            m_types.Add(type);
        }

        public Dictionary<string, List<string>> TypesByNamespace
        {
            get
            {
                var dic = new Dictionary<string, List<string>>();
                m_types.ForEach(t =>
                {
                    if (dic.ContainsKey(t.Namespace.Name))
                        dic[t.Namespace.Name].Add(t.Type.Name);
                    else
                        dic.Add(t.Namespace.Name, new List<string>() { t.Type.Name });
                });
                return dic;
            }
        }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}/{1}", Context.AssemblyUri, Assembly.FullName);
            }
        }
    }
}
