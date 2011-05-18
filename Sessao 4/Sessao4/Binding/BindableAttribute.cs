using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Binding
{
    public class BindableAttribute : Attribute
    {
        public bool Required { get; set; }
        public string Name { get; set; }
    }
}
