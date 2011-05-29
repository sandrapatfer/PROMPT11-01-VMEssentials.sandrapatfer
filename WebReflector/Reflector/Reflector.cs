using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace WebReflector.Reflector
{
    public sealed class Reflector
    {
        static Dictionary<string, Context> m_contextList = new Dictionary<string, Context>();

        public static string UriBase { get; set; }

        public static void RegisterContext(string context, string path)
        {
            if (!Directory.Exists(path))
                throw new InvalidPathReflectorException() { ErrorPath = path };

            m_contextList.Add(context, new Context(context, path));
        }

        public static List<Context> GetContextList()
        {
            return m_contextList.Values.ToList();
        }
        public static Context GetContext(string name)
        {
            if (m_contextList.Keys.Contains(name))
                return m_contextList[name];
            else
                throw new ContextNotFoundReflectorException() { ErrorContext = name };
        }
    }
}
