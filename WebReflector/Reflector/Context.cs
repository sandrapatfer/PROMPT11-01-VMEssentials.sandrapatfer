using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using WebReflector.Attributes;

namespace WebReflector.Reflector
{
    // Each context is a folder with several assemblies

    public class Context : IContextEntity
    {
        string m_name;
        string m_path;
        List<ContextAssembly> m_assemblies = new List<ContextAssembly>();
        public List<ContextAssembly> Assemblies { get { return m_assemblies; } }
        ContextNamespace m_nspaceTree;

        public string Name { get { return m_name; } }
        public string RootUri { get { return Reflector.UriBase; } }
        public ContextNamespace RootNamespace { get { return m_nspaceTree; } }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}", m_name);
            }
        }

        [TemplateAttribute(Name = "ContextAssembliesUri")]
        public static IRoutingTemplate AssemblyUriTemplate { get; set; }

        public string AssemblyUri
        {
            get
            {
                return string.Format(AssemblyUriTemplate.FormatString, m_name);
            }
        }

        [TemplateAttribute(Name = "ContextNamespacesUri")]
        public static IRoutingTemplate NamespaceUriTemplate { get; set; }

        public string NamespaceUri
        {
            get
            {
                return string.Format(NamespaceUriTemplate.FormatString, m_name);
            }
        }

        public Context(string name, string path)
        {
            m_name = name;
            m_path = path;
            m_nspaceTree = new ContextNamespace() { Context = this };

            foreach(var dllFile in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);
                    var ctxAss = new ContextAssembly() { Assembly = assembly, Context = this };
                    assembly.GetTypes().ToList().ForEach(t => RegisterType(t, ctxAss));
                    m_assemblies.Add(ctxAss);
                }
                catch
                {
                    // ignoring the dlls in the folder that can't be open
                }
            }

            // do the ordering of lists after the full load of types
            m_nspaceTree.OrderChilds();
            m_assemblies.OrderBy(a => a.Name);
        }

        void RegisterType(Type type, ContextAssembly ctxAss)
        {
            var ctx = new ContextType(type); 
            ContextNamespace nspace;
            if (type.Namespace != null)
                nspace = m_nspaceTree.FindOrCreateNamespace(type.Namespace.Split('.'));
            else
                nspace = m_nspaceTree;
            ctx.Namespace = nspace;
            nspace.Types.Add(ctx);
            ctxAss.RegisterType(ctx);
        }

        public ContextAssembly GetAssembly(string name)
        {
            ContextAssembly assembly = m_assemblies.Find(a => a.Name == name);
            if (assembly == null)
                throw new AssemblyNotFoundReflectorException() { ErrorAssembly = name };
            return assembly;
        }

        public ContextNamespace GetNamespace(string nsName)
        {
            ContextNamespace nspace = m_nspaceTree.Find(nsName.Split('.'));
            if (nspace == null)
                throw new NamespaceNotFoundReflectorException() { ErrorNamespace = nsName };
            return nspace;
        }
    }
}
