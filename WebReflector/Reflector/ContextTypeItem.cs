using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextTypeItem
    {
        public ContextType Type { internal get; set; }
    }

    public class ContextTypeFuncItem : ContextTypeItem
    {
    }

    public class ContextTypeMethod : ContextTypeFuncItem
    {
        public MethodInfo Method { internal get; set; }

        public string Name
        {
            get
            {
                return Method.Name;
            }
        }
    }

    public class ContextTypeConstructor : ContextTypeFuncItem
    {
        public ConstructorInfo Constructor { internal get; set; }

        public string Name
        {
            get
            {
                return Constructor.Name;
            }
        }
    }

    public class ContextTypeField : ContextTypeItem
    {
        public FieldInfo Field { internal get; set; }

        public string Name
        {
            get
            {
                return Field.Name;
            }
        }
        public string Uri
        {
            get
            {
                return string.Format(@"{0}/f/{1}", Type.Uri, Name);
            }
        }
        public string TypeName
        {
            get
            {
                return Field.FieldType.Name;
            }
        }
    }

    public class ContextTypeProperty : ContextTypeItem
    {
        public PropertyInfo Property { internal get; set; }

        public string Name
        {
            get
            {
                return Property.Name;
            }
        }
        public string Uri
        {
            get
            {
                return string.Format(@"{0}/p/{1}", Type.Uri, Name);
            }
        }
    }

    public class ContextTypeEvent : ContextTypeItem
    {
        public EventInfo Event { internal get; set; }

        public string Name
        {
            get
            {
                return Event.Name;
            }
        }
        public string Uri
        {
            get
            {
                return string.Format(@"{0}/e/{1}", Type.Uri, Name);
            }
        }
    }
}
