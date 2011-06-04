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

    public class NotFoundReflectorException : ReflectorException
    {}

    public class ContextNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorContext { get; set; }
    }

    public class AssemblyNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorAssembly { get; set; }
    }

    public class NamespaceNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorNamespace { get; set; }
    }

    public class TypeNotFoundReflectorException : ReflectorException
    {
        public string ErrorType { get; set; }
    }

    public class MethodNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorMethod { get; set; }
    }

    public class FieldNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorField { get; set; }
    }

    public class PropertyNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorProperty { get; set; }
    }

    public class EventNotFoundReflectorException : NotFoundReflectorException
    {
        public string ErrorEvent { get; set; }
    }

}
