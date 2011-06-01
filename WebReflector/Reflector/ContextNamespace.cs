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
        public ContextNamespace ParentNamespace { get; internal set; }
        public List<ContextType> Types { get; internal set; }
        public string Name { get; set; }
        public bool IsRoot { get { return ParentNamespace == null; } }

        public ContextNamespace()
        {
            Types = new List<ContextType>();
            ChildNamespaces = new List<ContextNamespace>();
            Name = ".";
            ParentNamespace = null;
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
                if (ParentNamespace != null)
                    if (ParentNamespace.IsRoot)
                        return Name;
                    else
                        return string.Format(@"{0}.{1}", ParentNamespace.FullName, Name);
                else
                    return string.Empty;
            }
        }
        public string FriendlyName
        {
            get
            {
                if (IsRoot)
                    return "Root";
                else
                    return Name;
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
                    child = new ContextNamespace(name[0]) { ParentNamespace = this, Context = Context };
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
            else if (string.IsNullOrEmpty(name[0]) && IsRoot)
                return this;
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
            ChildNamespaces.OrderBy(n => n.Name);

            Types.OrderBy(t => t.Type.Name);
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
