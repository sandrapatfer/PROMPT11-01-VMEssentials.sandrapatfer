using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    public class ContextType
    {
        public Type Type { get; set; }

        public ContextNamespace Namespace { get; set; }

        public string Uri
        {
            get
            {
                return string.Format(@"{0}/{1}", Namespace.Uri, Type.Name);
            }
        }

    }
}
