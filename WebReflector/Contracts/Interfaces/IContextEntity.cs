using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public interface IContextEntity
    {
        string Uri { get; }
        string Name { get; }
    }
}
