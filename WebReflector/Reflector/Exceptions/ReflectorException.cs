using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class ReflectorException : Exception
    {
    }

    public class ContextNotFoundReflectorException : ReflectorException
    {
        public string ErrorContext { get; set; }
    }

    public class TypeNotFoundReflectorException : ReflectorException
    {
        public string ErrorType { get; set; }
    }
}
