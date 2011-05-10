using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;

namespace ConsoleApplication
{
    class IEnumContainer
    {
        public List<string> MyProperty { get; set; }
    }

    class DirectoryComparer : IEqualityComparer<object>
    {
        #region IEqualityComparer Members

        public bool Equals(object x, object y)
        {
            if (x is DirectoryInfo)
            {
                if (y is DirectoryInfo)
                {
                    return ((DirectoryInfo)x).FullName == ((DirectoryInfo)y).FullName;
                }
                else
                    return false;
            }
            else
                return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            if (obj is DirectoryInfo)
                return ((DirectoryInfo)obj).FullName.GetHashCode();
            else
                return obj.GetHashCode();
        }

        #endregion
    }
 

    class Program
    {
        static void DumpProperties(object obj)
        {
            var objType = obj.GetType();
            Console.WriteLine("Type: {0}", objType.Name);
            foreach (var objField in objType.GetProperties())
            {
                Console.WriteLine("Property Name: {0} Value: {1}", objField.Name, objField.GetValue(obj, null));
            }
        }

        static void DumpPropertiesToTable(object obj)
        {
            var objType = obj.GetType();
            StreamWriter wr = new StreamWriter("object.html");
            wr.WriteLine("Type: {0}", objType.Name);
            wr.WriteLine("<html><body><table border=\"1\">");
            wr.WriteLine("<tr><th>Name</th><th>Valor</th></tr>");
            foreach (var objField in objType.GetProperties())
            {
                wr.WriteLine("<tr><td>{0}</td><td>{1}</td></tr>", objField.Name, objField.GetValue(obj, null));
            }
            wr.WriteLine("</table></body></html>");
            wr.Close();
        }

        static Dictionary<object, string> object_list = new Dictionary<object, string>(new DirectoryComparer());

        static string FieldData(object obj, PropertyInfo propInfo)
        {
            if (propInfo.PropertyType.IsPrimitive || propInfo.PropertyType == typeof(string))
                return propInfo.GetValue(obj, null).ToString();
            else
            {
                object prop_value = propInfo.GetValue(obj, null);
                if (prop_value != null)
                {
                    Type iEnumInterfaceType = propInfo.PropertyType.GetInterface("IEnumerable");
                    if (iEnumInterfaceType != null)
                    {
                        return "IEnum";
                    }
                    else
                    {
                        if (object_list.ContainsKey(prop_value))
                            return String.Format("<a href=\"{0}\">{1}</a>", object_list[prop_value], prop_value.ToString());
                        else
                        {
                            string file_name = string.Format(@"prop_{0}.html", Guid.NewGuid().ToString());
                            object_list.Add(prop_value, file_name);
                            DumpPropertiesToFile(prop_value, file_name);
                            return String.Format("<a href=\"{0}\">{1}</a>", file_name, prop_value.ToString());
                        }
                    }
                }
                else
                    return "";
            }
        }

        static void DumpPropertiesToFile(object obj, string file_name)
        {
            var objType = obj.GetType();
            StreamWriter wr = new StreamWriter(file_name);
            wr.WriteLine("Type: {0}", objType.Name);
            wr.WriteLine("<html><body><table border=\"1\">");
            wr.WriteLine("<tr><th>Name</th><th>Valor</th></tr>");
            foreach (var objField in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                wr.WriteLine("<tr><td>{0}</td><td>{1}</td></tr>", objField.Name, FieldData(obj, objField));
            }
            wr.WriteLine("</table></body></html>");
            wr.Close();
        }        

        static void Main(string[] args)
        {
            //DumpPropertiesToTable(new DirectoryInfo(@"c:\program files"));
            //DumpPropertiesToTable(new FileInfo(@"C:\SPF\prompt11\uc01\repos\PROMPT11-01-VMEssentials.sandrapatfer\Sessão 2 - CTS\ConsoleApplication\ConsoleApplication\bin\Debug\object.html"));

            //DumpPropertiesToFile(new DirectoryInfo(@"c:\program files"), @"object.html");

            DumpPropertiesToFile(new DirectoryInfo(@"c:\program files").GetFileSystemInfos(), @"object.html");

            List<string> n = new List<string> { "um", "dois" };
            IEnumContainer c = new IEnumContainer();
            c.MyProperty = n;
            DumpPropertiesToFile(c, @"test.html");
        }
    }
}
