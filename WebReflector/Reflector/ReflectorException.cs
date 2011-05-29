using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    public class ReflectorException : Exception
    {
    }

    public class InvalidPathReflectorException : ReflectorException
    {
        public string ErrorPath { get; set; }
    }
}
