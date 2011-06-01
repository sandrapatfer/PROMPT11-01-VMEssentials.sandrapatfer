using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextType
    {
        List<ContextTypeField> m_fields = null;
        public List<ContextTypeField> Fields
        {
            get
            {
                if (m_fields == null)
                    InitList(Type.GetFields().ToList(), ref m_fields, f => new ContextTypeField() { Field = f, Type = this });
                return m_fields;
            }
        }
        List<ContextTypeProperty> m_properties = null;
        public List<ContextTypeProperty> Properties
        {
            get
            {
                if (m_properties == null)
                    InitList(Type.GetProperties().ToList(), ref m_properties, p => new ContextTypeProperty() { Property = p, Type = this });
                return m_properties;
            }
        }
        List<ContextTypeEvent> m_events = null;
        public List<ContextTypeEvent> Events
        {
            get
            {
                if (m_events == null)
                    InitList(Type.GetEvents().ToList(), ref m_events, e => new ContextTypeEvent() { Event = e, Type = this });
                return m_events;
            }
        }
        List<ContextTypeConstructor> m_constructors = null;
        public List<ContextTypeConstructor> Constructors
        {
            get
            {
                if (m_constructors == null)
                    InitList(Type.GetConstructors().ToList(), ref m_constructors, c => new ContextTypeConstructor() { Constructor = c, Type = this });
                return m_constructors;
            }
        }
        List<ContextTypeMethod> m_methods = null;
        public List<ContextTypeMethod> Methods
        {
            get
            {
                if (m_methods == null)
                    InitList(Type.GetMethods().ToList(), ref m_methods, m => new ContextTypeMethod() { Method = m, Type = this });
                return m_methods;
            }
        }

        public Type Type { get; set; }

        public ContextNamespace Namespace { get; set; }
        public ContextAssembly Assembly { get; set; }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}/{1}", Namespace.Uri, Type.Name);
            }
        }
        public string ConstructorsUri
        {
            get
            {
                return string.Format(@"{0}/c", Uri);
            }
        }

        void InitList<LT, NT>(List<LT> typeList, ref List<NT> newList, Converter<LT, NT> conv)
        {
            if (newList == null)
            {
                newList = typeList.ToList().ConvertAll(conv);
            }
        }

        public List<ContextTypeMethod> GetMethods(string methodName)
        {
            var methods = Methods.FindAll(f => f.Name == methodName);
            if (methods == null || methods.Count == 0)
                throw new MethodNotFoundReflectorException() { ErrorMethod = methodName };
            return methods;
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
