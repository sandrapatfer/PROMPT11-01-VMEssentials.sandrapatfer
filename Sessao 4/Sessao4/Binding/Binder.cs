using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;

namespace Binding
{
    internal abstract class MemberBinder
    {
        private Type m_type;

        protected MemberBinder(Type t)
        {
            m_type = t;
        }
        public void Bind(object newInstance, string value)
        {
            if (Binder.ConverterList.ContainsKey(m_type.Name))
            {
                object convValue;
                if (((IBindingConverter)Binder.ConverterList[m_type.Name]).TryConvertFrom(value, out convValue))
                    BindObject(newInstance, convValue);
            }
            else
            {
                if (!m_type.IsPrimitive && m_type != typeof(string))
                    ThrowInvalidMemberTypeException();

                var convValue = Convert.ChangeType(value, m_type);
                BindObject(newInstance, convValue);
            }
        }
        protected abstract void BindObject(object newInstance, object value);
        protected abstract void ThrowInvalidMemberTypeException();
        public abstract void ThrowRequiredMemberException();
    }

    internal class PropertyBinder : MemberBinder
    {
        PropertyInfo m_prop;

        public PropertyBinder(PropertyInfo prop)
            :base (prop.PropertyType)
        {
            m_prop = prop;
        }

        protected override void BindObject(object newInstance, object value)
        {
            m_prop.SetValue(newInstance, value, null);
        }
        protected override void ThrowInvalidMemberTypeException()
        {
            throw new InvalidMemberTypeException(m_prop, m_prop.PropertyType);
        }
        public override void ThrowRequiredMemberException()
        {
            throw new RequiredMemberException(m_prop.Name);
        }
    }

    internal class FieldBinder : MemberBinder
    {
        FieldInfo m_field;

        public FieldBinder(FieldInfo field)
            : base(field.FieldType)
        {
            m_field = field;
        }

        protected override void BindObject(object newInstance, object value)
        {
            m_field.SetValue(newInstance, value);
        }
        protected override void ThrowInvalidMemberTypeException()
        {
            throw new InvalidMemberTypeException(m_field, m_field.FieldType);
        }
        public override void ThrowRequiredMemberException()
        {
            throw new RequiredMemberException(m_field.Name);
        }
    }

    internal class TypeMap
    {
        private Hashtable m_memberList;

        public TypeMap(Type typeToMap)
        {
            m_memberList = new Hashtable();

            var props = typeToMap.GetProperties();
            foreach (var prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(false);
                if (attrs.Length > 0 && attrs[0] is BindableAttribute)
                {
                    BindableAttribute attr = attrs[0] as BindableAttribute;
                    var map = new MemberMap() { MemberBinder = new PropertyBinder(prop), Required = attr.Required };
                    m_memberList.Add(attr.Name, map);
                }
            }
            var fields = typeToMap.GetFields();
            foreach (var field in fields)
            {
                object[] attrs = field.GetCustomAttributes(false);
                if (attrs.Length > 0 && attrs[0] is BindableAttribute)
                {
                    BindableAttribute attr = attrs[0] as BindableAttribute;
                    var map = new MemberMap() { MemberBinder = new FieldBinder(field), Required = attr.Required };
                    m_memberList.Add(attr.Name, map);
                }
            }
        }

        public void Bind(string memberName, string memberValue, object objInstance)
        {
            if (m_memberList.ContainsKey(memberName))
            {
                MemberMap map = (MemberMap)m_memberList[memberName];
                map.MemberBinder.Bind(objInstance, memberValue);
                map.Binded = true;
            }
        }

        public void CheckRequired()
        {
            foreach (var mapName in m_memberList.Values)
            {
                MemberMap map = (MemberMap)mapName;
                if (!map.Binded && map.Required)
                    map.MemberBinder.ThrowRequiredMemberException();
            }
        }

        class MemberMap
        {
            public MemberBinder MemberBinder;
            public bool Required;
            public bool Binded;

            public MemberMap()
            {
                Binded = false;
            }
        }
    }
    
    public class Binder
    {
        public static Hashtable ConverterList;

        public Binder()
        {
            ConverterList = new Hashtable();
        }

        public T BindTo1<T>(IEnumerable<KeyValuePair<string, string>> pairs)
            where T : class
        {
            Type typeToBind = typeof(T);
            T newInstance = (T)Activator.CreateInstance(typeToBind);

            foreach (var pair in pairs)
            {
                var field = typeToBind.GetField(pair.Key);
                if (field != null)
                {
                    FieldBinder f = new FieldBinder(field);
                    f.Bind(newInstance, pair.Value);
                }
                else
                {
                    var prop = typeToBind.GetProperty(pair.Key);
                    if (prop != null)
                    {
                        PropertyBinder p = new PropertyBinder(prop);
                        p.Bind(newInstance, pair.Value);
                    }
                }
            }

            return newInstance;
        }


        public T BindTo2<T>(IEnumerable<KeyValuePair<string, string>> pairs)
            where T : class
        {
            Type typeToBind = typeof(T);
            T newInstance = (T)Activator.CreateInstance(typeToBind);
            TypeMap map = new TypeMap(typeToBind);
            foreach (var pair in pairs)
            {
                map.Bind(pair.Key, pair.Value, newInstance);
            }
            map.CheckRequired();
            return newInstance;
        }

        public void WithConverterFor<T>(IBindingConverter conv)
        {
            Type typeToBind = typeof(T);
            ConverterList.Add(typeToBind.Name, conv);
        }
    }
}
