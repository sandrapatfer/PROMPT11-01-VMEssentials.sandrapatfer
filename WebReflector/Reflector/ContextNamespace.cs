using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    public class ContextNamespace : IContextEntity
    {
        public Context Context { get; set; }
        public List<ContextNamespace> ChildNamespaces { get; internal set; }
        ContextNamespace m_parentNamespace;
        public List<ContextType> Types { get; internal set; }
        public string Name { get; set; }
        public bool IsRoot { get { return m_parentNamespace == null; } }

        public ContextNamespace()
        {
            Types = new List<ContextType>();
            ChildNamespaces = new List<ContextNamespace>();
            Name = ".";
            m_parentNamespace = null;
        }
        public ContextNamespace(string name)
            : this()
        {
            Name = name;
        }

        public string Uri
        {
            get
            {
                if (IsRoot)
                    return string.Format(@"{0}/.", Context.NamespaceUri);
                else
                    return string.Format(@"{0}/{1}", Context.NamespaceUri, FullName);
            }
        }
        public string FullName
        {
            get
            {
                if (m_parentNamespace != null)
                    if (m_parentNamespace.IsRoot)
                        return Name;
                    else
                        return string.Format(@"{0}.{1}", m_parentNamespace.FullName, Name);
                else
                    return string.Empty;
            }
        }

        public ContextNamespace FindOrCreateNamespace(string[] name)
        {
            if (name.Count() == 0)
                return null;
            else
            {
                ContextNamespace child = ChildNamespaces.Find(c => c.Name == name[0]);
                if (child == null)
                {
                    child = new ContextNamespace(name[0]) { m_parentNamespace = this, Context = Context };
                    ChildNamespaces.Add(child);
                }
                if (name.Count() > 1)
                    return child.FindOrCreateNamespace(name.Skip(1).ToArray());
                else
                    return child;
            }
        }

        public ContextNamespace Find(string[] name)
        {
            if (name.Count() == 0)
                return null;
            else
            {
                ContextNamespace child = ChildNamespaces.Find(c => c.Name == name[0]);
                if (child != null)
                    if (name.Count() > 1)
                        return child.Find(name.Skip(1).ToArray());
                return child;
            }
        }

        public void OrderChilds()
        {
            ChildNamespaces.ForEach(n => n.OrderChilds());
            ChildNamespaces = ChildNamespaces.OrderBy(n => n.Name).ToList();
        }

        public ContextType GetType(string name)
        {
            ContextType type = Types.Find(t => t.Type.Name == name);
            if (type == null)
                throw new TypeNotFoundReflectorException() { ErrorType = name };
            return type;
        }
    }
}
