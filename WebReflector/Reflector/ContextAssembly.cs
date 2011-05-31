using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextAssembly : IContextEntity
    {
        Context m_context;
        public Context Context { set { m_context = value; } }
        Assembly m_assembly;
        public Assembly Assembly { set { m_assembly = value; m_name = m_assembly.GetName().Name; } }
        string m_name;
        public string Name { get { return m_name; } }

        List<ContextType> m_types = new List<ContextType>();

        public ContextAssembly()
        {
        }

        public void RegisterType(ContextType type)
        {
            type.Assembly = this;
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
                return string.Format(@"{0}/{1}", m_context.AssemblyUri, Name);
            }
        }
        public string PublicKey { get { return System.Text.UTF8Encoding.UTF8.GetString(m_assembly.GetName().GetPublicKey()); } }
    }
}
