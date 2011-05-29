using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebReflector.Reflector
{
    public class ContextAssembly
    {
        public Assembly Assembly { get; set; }
        public List<ContextType> Types { get; internal set; }

        public ContextAssembly()
        {
            Types = new List<ContextType>();
        }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}as/{1}", Reflector.UriBase, Assembly.FullName);
            }
        }
    }
}
