using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;

namespace ConsoleApplication
{
    class IEnumContainer<T>
    {
        public List<T> MyProperty { get; set; }
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
        static Dictionary<Type, IPropProvider> providers_list = new Dictionary<Type, IPropProvider>();

        static string FieldData(object fieldObj)
        {
            if (fieldObj == null)
                   return "";

            Type fieldType = fieldObj.GetType();
            if (fieldType.IsPrimitive || fieldType == typeof(string))
                return fieldObj.ToString();
            else
            {
                if (object_list.ContainsKey(fieldObj))
                    return String.Format("<a href=\"{0}\">{1}</a>", object_list[fieldObj], fieldType.ToString());
                else
                {
                    string file_name = string.Format(@"prop_{0}.html", Guid.NewGuid().ToString());
                    object_list.Add(fieldObj, file_name);
                    DumpObjectToFile(fieldObj, file_name);
                    return String.Format("<a href=\"{0}\">{1}</a>", file_name, fieldType.ToString());
                }
            }
        }

        static void DumpListToFile(object obj, string file_name)
        {
            var objType = obj.GetType();
            StreamWriter wr = new StreamWriter(file_name);
            wr.WriteLine("<html><body><table border=\"1\">");
            wr.WriteLine("<tr><th>Name</th></tr>");

            MethodInfo mi = objType.GetMethod("GetEnumerator");
            IEnumerator e = (IEnumerator)mi.Invoke(obj, null);
            while (e.MoveNext())
            {
                Type currentType = e.Current.GetType();
                if (currentType.IsPrimitive || currentType == typeof(string))
                    wr.WriteLine("<tr><td>{0}</td></tr>", e.Current);
                else
                {
                    string file_name_2 = string.Format(@"obj_{0}.html", Guid.NewGuid().ToString());
                    DumpObjectToFile(e.Current, file_name_2);
                    wr.WriteLine("<tr><td><a href=\"{0}\">{1}</a></td></tr>", file_name_2, e.Current);
                }
            }
            wr.WriteLine("</table></body></html>");
            wr.Close();
        }

        static void DumpObjectToFile(object obj, string file_name)
        {
            var objType = obj.GetType();
            Type iEnumInterfaceType = objType.GetInterface("IEnumerable");
            if (iEnumInterfaceType != null)
            {
                DumpListToFile(obj, file_name);
            }
            else
            {
                StreamWriter wr = new StreamWriter(file_name);
                wr.WriteLine("Type: {0}", objType.Name);
                wr.WriteLine("<html><body><table border=\"1\">");
                wr.WriteLine("<tr><th>Name</th><th>Valor</th></tr>");

                if (providers_list.ContainsKey(objType))
                {
                    var props = providers_list[objType].GetProperties(obj);
                    foreach (var prop in props.Keys)
                    {
                        wr.WriteLine("<tr><td>{0}</td><td>{1}</td></tr>", prop, FieldData(props[prop]));
                    }
                }
                else
                {
                    foreach (var objProp in objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        wr.WriteLine("<tr><td>{0}</td><td>{1}</td></tr>", objProp.Name, FieldData(objProp.GetValue(obj, null)));
                    }
                }
                wr.WriteLine("</table></body></html>");
                wr.Close();
            }
        }

        static void Main(string[] args)
        {
            //DumpPropertiesToTable(new DirectoryInfo(@"c:\program files"));
            //DumpPropertiesToTable(new FileInfo(@"C:\SPF\prompt11\uc01\repos\PROMPT11-01-VMEssentials.sandrapatfer\Sessão 2 - CTS\ConsoleApplication\ConsoleApplication\bin\Debug\object.html"));

            //DumpPropertiesToFile(new DirectoryInfo(@"c:\program files"), @"object.html");

            //DumpPropertiesToFile(new DirectoryInfo(@"c:\program files").GetFileSystemInfos(), @"object.html");

            /*List<string> n1 = new List<string> { "um", "dois" };
            List<string> n2 = new List<string> { "tres", "quatro" };
            List<string> n3 = new List<string> { "aaa", "bbb" };
            var n = new List<List<string>> { n1, n2, n3 };
            IEnumContainer<List<string>> c = new IEnumContainer<List<string>>();
            c.MyProperty = n;
            DumpObjectToFile(c, @"test.html");*/

            providers_list.Add(typeof(DirectoryInfo), (IPropProvider)new DirectoryInfoPropProvider());
            DumpObjectToFile(new DirectoryInfo(@"C:\SPF\Fotos\new"), @"object.html");

        }
    }
}
