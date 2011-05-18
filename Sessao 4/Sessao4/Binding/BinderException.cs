using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Binding
{
    public abstract class BinderException : Exception
    {
        protected BinderException(string msg)
            : base(msg)
        { }
    }

    public class InvalidMemberTypeException : BinderException
    {
        public MemberInfo MemberInfo { get; internal set; }
        public Type MemberType { get; internal set; }

        public InvalidMemberTypeException(MemberInfo mi, Type mt)
            : base(string.Format("Type {0} of member {1} is not primitive", mt, mi.Name))
        {
            this.MemberInfo = mi;
            this.MemberType = mt;
        }
    }

    public class RequiredMemberException : BinderException
    {
        public string MemberName { get; set; }

        public RequiredMemberException(string n)
            : base(string.Format("Member {0} is required", n))
        {
            this.MemberName = n;
        }
    }
}
