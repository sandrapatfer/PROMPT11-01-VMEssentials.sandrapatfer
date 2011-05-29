using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    public class ContextNamespace
    {
        public List<ContextType> Types  { get; internal set; }
        public string Name { get; set; }

        public ContextNamespace()
        {
            Types = new List<ContextType>();
        }

        public ContextType GetType(string name)
        {
            ContextType type = Types.Find(t => t.Type.Name == name);
            if (type != null)
                throw new TypeNotFoundReflectorException() { ErrorType = name };
            return type;
        }
    }
}
