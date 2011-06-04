using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public abstract class ContextTypeItem : ContextTypedItem
    {
        public ContextType ParentType { get; set; }
    }

    public class ContextTypeField : ContextTypeItem
    {
        FieldInfo Field { get; set; }

        public ContextTypeField(FieldInfo field)
        {
            Field = field;
        }

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
                return string.Format(@"{0}/f/{1}", ParentType.Uri, Name);
            }
        }
        protected override Type ItemReflectionType { get { return Field.FieldType; } }
    }

    public class ContextTypeProperty : ContextTypeItem
    {
        PropertyInfo Property { get; set; }

        public ContextTypeProperty(PropertyInfo prop)
        {
            Property = prop;
        }

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
                return string.Format(@"{0}/p/{1}", ParentType.Uri, Name);
            }
        }
        protected override Type ItemReflectionType { get { return Property.PropertyType; } }
    }

    public class ContextTypeEvent : ContextTypeItem
    {
        public EventInfo Event { internal get; set; }

        public ContextTypeEvent(EventInfo ev)
        {
            Event = ev;
        }

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
                return string.Format(@"{0}/e/{1}", ParentType.Uri, Name);
            }
        }
        protected override Type ItemReflectionType { get { return Event.EventHandlerType; } }
    }
}
