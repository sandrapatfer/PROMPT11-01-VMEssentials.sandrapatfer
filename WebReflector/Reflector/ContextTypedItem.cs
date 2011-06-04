using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    // Class that represents an item with type
    public abstract class ContextTypedItem : IContextEntity
    {
        public abstract string Name { get; }
        public abstract string Uri { get; }

        IContextEntity m_itemType = null;
        protected abstract Type ItemReflectionType { get; } // using a property to make all the sub-classes initialize the type
        public IContextEntity ItemType
        {
            get
            {
                if (m_itemType == null)
                {
                    m_itemType = Reflector.LookupType(ItemReflectionType.Namespace, ItemReflectionType.Name);
                    if (m_itemType == null)
                    {
                        m_itemType = new ContextTypeNotFound(ItemReflectionType);
                    }
                }
                return m_itemType;
            }
        }
    }

    public class ContextMethodParameter : ContextTypedItem
    {
        string m_name;
        Type m_itemReflectionType = null;

        public ContextMethodParameter(string name, Type type)
        {
            m_name = name;
            m_itemReflectionType = type;
        }

        protected override Type ItemReflectionType { get { return m_itemReflectionType; } }
        public override string Name { get { return m_name; } }

        public override string Uri
        {
            get { return ""; }
        }
    }

    public class ContextMethodReturn : ContextTypedItem
    {
        Type m_itemReflectionType = null;

        public ContextMethodReturn(Type type)
        {
            m_itemReflectionType = type;
        }

        protected override Type ItemReflectionType { get { return m_itemReflectionType; } }

        public override string Name { get { return m_itemReflectionType.Name; } }
        public override string Uri { get { return ""; } }
    }
}
