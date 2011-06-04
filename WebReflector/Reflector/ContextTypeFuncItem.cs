using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using WebReflector.Attributes;

namespace WebReflector.Reflector
{
    public abstract class ContextTypeFuncItem : IContextEntity
    {
        public abstract List<ContextMethodParameter> Parameters { get; }
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
        public ContextType Type { get; set; }
        public abstract string Name { get; }
        public abstract string Uri { get; }
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

        [TemplateAttribute(Name = "MethodUri")]
        public static IRoutingTemplate MethodUriTemplate { get; set; }

        public override string Uri
        {
            get
            {
                return string.Format(MethodUriTemplate.FormatString, Type.Namespace.Context.Name, Type.Namespace.FullName, Type.Name, Name);
            }
        }

        private List<ContextMethodParameter> m_parameters = null;
        public override List<ContextMethodParameter> Parameters
        {
            get
            {
                if (m_parameters == null)
                    m_parameters = Method.GetParameters().ToList().ConvertAll(p => new ContextMethodParameter(p.Name, p.ParameterType)).ToList();
                return m_parameters;
            }
        }

        private ContextMethodReturn m_return = null;
        public ContextMethodReturn Return
        {
            get
            {
                if (m_return == null)
                    m_return = new ContextMethodReturn(Method.ReturnType);
                return m_return;
            }
        }

    }

    public class ContextTypeConstructor : ContextTypeFuncItem
    {
        public ConstructorInfo Constructor { internal get; set; }

        public override string Name { get { return Constructor.Name; } }
        public override string Uri { get { return ""; } }

        private List<ContextMethodParameter> m_parameters = null;
        public override List<ContextMethodParameter> Parameters
        {
            get
            {
                if (m_parameters == null)
                    m_parameters = Constructor.GetParameters().ToList().ConvertAll(p => new ContextMethodParameter(p.Name, p.ParameterType)).ToList();
                return m_parameters;
            }
        }
    }
}
