using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextType : IContextEntity
    {
        public Type m_type;

        public ContextNamespace Namespace { get; set; }
        public ContextAssembly Assembly { get; set; }

        public ContextType(Type t)
        {
            m_type = t;
        }

        List<ContextTypeField> m_fields = null;
        public List<ContextTypeField> Fields
        {
            get
            {
                if (m_fields == null)
                    InitList(m_type.GetFields(), ref m_fields, f => new ContextTypeField(f) { ParentType = this });
                return m_fields;
            }
        }
        List<ContextTypeProperty> m_properties = null;
        public List<ContextTypeProperty> Properties
        {
            get
            {
                if (m_properties == null)
                    InitList(m_type.GetProperties(), ref m_properties, p => new ContextTypeProperty(p) { ParentType = this });
                return m_properties;
            }
        }
        List<ContextTypeEvent> m_events = null;
        public List<ContextTypeEvent> Events
        {
            get
            {
                if (m_events == null)
                    InitList(m_type.GetEvents(), ref m_events, e => new ContextTypeEvent(e) { ParentType = this });
                return m_events;
            }
        }
        List<ContextTypeConstructor> m_constructors = null;
        public List<ContextTypeConstructor> Constructors
        {
            get
            {
                if (m_constructors == null)
                    InitList(m_type.GetConstructors(), ref m_constructors, c => new ContextTypeConstructor() { Constructor = c, Type = this });
                return m_constructors;
            }
        }

        SortedDictionary<string, List<ContextTypeMethod>> m_methods = null;
        public SortedDictionary<string, List<ContextTypeMethod>> Methods
        {
            get
            {
                if (m_methods == null)
                {
                    m_methods = new SortedDictionary<string, List<ContextTypeMethod>>();
                    m_type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToList().ForEach(
                        m =>
                        {
                            if (!m.IsSpecialName)
                            {
                                var ctx = new ContextTypeMethod() { Method = m, Type = this };
                                if (m_methods.Keys.Contains(m.Name))
                                    m_methods[m.Name].Add(ctx);
                                else
                                    m_methods.Add(m.Name, new List<ContextTypeMethod>() { ctx });
                            }
                        }
                            );
                }
                return m_methods;
            }
        }

        public string Name { get { return m_type.Name; } }
        public string Uri
        {
            get
            {
                return string.Format(@"{0}/{1}", Namespace.Uri, m_type.Name);
            }
        }
        public string ConstructorsUri
        {
            get
            {
                return string.Format(@"{0}/c", Uri);
            }
        }

        void InitList<LT, NT>(LT[] typeList, ref List<NT> newList, Converter<LT, NT> conv)
        {
            newList = typeList.ToList().ConvertAll(conv);
        }

        public List<ContextTypeMethod> GetMethods(string methodName)
        {
            if (!Methods.Keys.Contains(methodName))
                throw new MethodNotFoundReflectorException() { ErrorMethod = methodName };
            return Methods[methodName];
        }

        public ContextTypeField GetField(string fieldName)
        {
            var field = Fields.Find(f => f.Name == fieldName);
            if (field == null)
                throw new FieldNotFoundReflectorException() { ErrorField = fieldName };
            return field;
        }

        public ContextTypeProperty GetProperty(string propertyName)
        {
            var property = m_properties.Find(p => p.Name == propertyName);
            if (property == null)
                throw new PropertyNotFoundReflectorException() { ErrorProperty = propertyName };
            return property;
        }

        public ContextTypeEvent GetEvent(string eventName)
        {
            var ev = m_events.Find(e => e.Name == eventName);
            if (ev == null)
                throw new EventNotFoundReflectorException() { ErrorEvent = eventName };
            return ev;
        }

    }
}
