using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace WebReflector.Reflector
{
    // Each context is a folder with several assemblies

    public class Context
    {
        string m_name;
        string m_path;
        List<ContextAssembly> m_assemblies = new List<ContextAssembly>();
        List<ContextNamespace> m_nspaces = new List<ContextNamespace>();

        public string Name
        {
            get
            {
                return m_name;
            }
        }
        public string Uri
        {
            get
            {
                return string.Format(@"{0}{1}", Reflector.UriBase, m_name);
            }
        }

        public string AssemblyUri
        {
            get
            {
                return string.Format(@"{0}{1}/as", Reflector.UriBase, m_name);
            }
        }

        public string NamespaceUri
        {
            get
            {
                return string.Format(@"{0}{1}/ns", Reflector.UriBase, m_name);
            }
        }

        public string ParentUri
        {
            get
            {
                return Reflector.UriBase;
            }
        }

        public List<ContextAssembly> Assemblies
        {
            get
            {
                return m_assemblies;
            }
        }

        public List<ContextNamespace> Namespaces
        {
            get
            {
                return m_nspaces;
            }
        }

        public Context(string name, string path)
        {
            m_name = name;
            m_path = path;

            // TODO tornar este mecanismo lazy? nota que para dar a info do nspace tenho q carregar todas as assemblies
        
            foreach(var dllFile in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);
                    var ctxAss = new ContextAssembly() { Assembly = assembly, Context = this };
                    assembly.GetTypes().ToList().ForEach(t => { var type = new ContextType() { Type = t }; RegisterType(type); ctxAss.RegisterType(type); });
                    m_assemblies.Add(ctxAss);
                }
                catch
                {
                    // ignoring the dlls in the folder that can't be open
                }
            }
            // percorrer as pastas e criar um dicionario com os namespaces das assemblies
        }

        void RegisterType(ContextType type)
        {
            ContextNamespace nspace = m_nspaces.Find(n => n.Name == type.Type.Namespace);
            if (nspace == null)
            {
                nspace = new ContextNamespace() { Name = type.Type.Namespace, Context = this };
                m_nspaces.Add(nspace);
            }
            type.Namespace = nspace;
            nspace.Types.Add(type);
        }

        public ContextAssembly GetAssembly(string name)
        {
            ContextAssembly assembly = m_assemblies.Find(a => a.Assembly.FullName == name);
            if (assembly == null)
                throw new AssemblyNotFoundReflectorException() { ErrorAssembly = name };
            return assembly;
        }

        public ContextNamespace GetNamespace(string nsName)
        {
            ContextNamespace nspace = m_nspaces.Find(n => n.Name == nsName);
            if (nspace == null)
                throw new NamespaceNotFoundReflectorException() { ErrorNamespace = nsName };
            return nspace;
        }
    }
}
