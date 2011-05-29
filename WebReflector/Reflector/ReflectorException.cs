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

    public class ContextNotFoundReflectorException : ReflectorException
    {
        public string ErrorContext { get; set; }
    }

    public class AssemblyNotFoundReflectorException : ReflectorException
    {
        public string ErrorAssembly { get; set; }
    }

    public class NamespaceNotFoundReflectorException : ReflectorException
    {
        public string ErrorNamespace { get; set; }
    }
}
