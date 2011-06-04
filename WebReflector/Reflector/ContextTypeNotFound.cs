using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    class ContextTypeNotFound : IContextEntity
    {
        public Type m_type;

        public ContextTypeNotFound(Type t)
        {
            m_type = t;
        }

        public string Name { get { return m_type.Name; } }

        public string Uri
        {
            get
            {
                return string.Format(@"http://msdn.microsoft.com/en-us/library/{0}.aspx", m_type.FullName);
            }
        }
    }
}
