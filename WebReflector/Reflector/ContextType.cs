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
                return m_fields;
            }
        }
        List<ContextTypeProperty> m_properties = null;
        public List<ContextTypeProperty> Properties
        {
            get
            {
                return m_properties;
            }
        }
        List<ContextTypeEvent> m_events = null;
        public List<ContextTypeEvent> Events
        {
            get
            {
                return m_events;
            }
        }
        List<ContextTypeConstructor> m_constructors = null;
        public List<ContextTypeConstructor> Constructors
        {
            get
            {
                return m_constructors;
            }
        }

        Type m_type;
        public Type Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                InitList(m_type.GetFields().ToList(), ref m_fields, f => new ContextTypeField() { Field = f, Type = this });
                InitList(m_type.GetProperties().ToList(), ref m_properties, p => new ContextTypeProperty() { Property = p, Type = this });
                InitList(m_type.GetEvents().ToList(), ref m_events, e => new ContextTypeEvent() { Event = e, Type = this });
                InitList(m_type.GetConstructors().ToList(), ref m_constructors, c => new ContextTypeConstructor() { Constructor = c, Type = this });
            }
        }

        public ContextNamespace Namespace { get; set; }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}/{1}", Namespace.Uri, Type.Name);
            }
        }

        void InitList<LT, NT>(List<LT> typeList, ref List<NT> newList, Converter<LT, NT> conv)
        {
            if (newList == null)
            {
                newList = typeList.ToList().ConvertAll(conv);
            }
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
