using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector
{
    public class ReflectorConfig
    {
        Dictionary<string, string> m_contextList;

        public static ReflectorConfig Singleton { get; set; }

        ReflectorConfig()
        {
            Singleton = this;
        }


        // percorrer as pastas e criar um dicionario com os namespaces das assemblies
        public void RegisterContext(string context, string path)
        {
            m_contextList.Add(context, path);
        }

        public Assembly GetAssembly(string context, string assemblyName)
        {
            if (m_contextList.ContainsKey(context))
            {
                try
                {
                    return Assembly.Load(m_contextList[context] + assemblyName);
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
