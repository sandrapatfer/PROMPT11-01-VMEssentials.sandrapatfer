using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public abstract class ContextTypeItem : IContextEntity
    {
        public ContextType Type { get; set; }
        public abstract string Name { get; }
        public abstract string Uri { get; }
    }

    public abstract class ContextTypeFuncItem : ContextTypeItem
    {
        public abstract List<ParameterInfo> Parameters { get; }
        public string MethodString
        {
            get
            {
                StringBuilder prms = new StringBuilder();
                prms.Append(Name);
                prms.Append("(");
                bool first = true;
                Parameters.ForEach(p =>
                {
                    if (first) first = false;
                    else prms.Append(", ");
                    prms.Append(p.Name);
                });
                prms.Append(")");
                return prms.ToString();
            }
        }
    }

    public class ContextTypeMethod : ContextTypeFuncItem
    {
        public MethodInfo Method { internal get; set; }

        public override string Name
        {
            get
            {
                return Method.Name;
            }
        }
        public override string Uri
        {
            get
            {
                return string.Format(@"{0}/m/{1}", Type.Uri, Name);
            }
        }

        public override List<ParameterInfo> Parameters { get { return Method.GetParameters().ToList(); } }
    }

    public class ContextTypeConstructor : ContextTypeFuncItem
    {
        public ConstructorInfo Constructor { internal get; set; }

        public override string Name { get { return Constructor.Name; } }
        public override string Uri { get { return ""; } }

        public override List<ParameterInfo> Parameters { get { return Constructor.GetParameters().ToList(); } }
    }

    public class ContextTypeField : ContextTypeItem
    {
        public FieldInfo Field { internal get; set; }

        public override string Name
        {
            get
            {
                return Field.Name;
            }
        }
        public override string Uri
        {
            get
            {
                return string.Format(@"{0}/f/{1}", Type.Uri, Name);
            }
        }
        public string TypeName { get { return Field.FieldType.Name; } }
    }

    public class ContextTypeProperty : ContextTypeItem
    {
        public PropertyInfo Property { internal get; set; }

        public override string Name
        {
            get
            {
                return Property.Name;
            }
        }
        public override string Uri
        {
            get
            {
                return string.Format(@"{0}/p/{1}", Type.Uri, Name);
            }
        }
        public string TypeName { get { return Property.PropertyType.Name; } }
    }

    public class ContextTypeEvent : ContextTypeItem
    {
        public EventInfo Event { internal get; set; }

        public override string Name
        {
            get
            {
                return Event.Name;
            }
        }
        public override string Uri
        {
            get
            {
                return string.Format(@"{0}/e/{1}", Type.Uri, Name);
            }
        }
        public string TypeName { get { return Event.EventHandlerType.Name; } }
    }
}
