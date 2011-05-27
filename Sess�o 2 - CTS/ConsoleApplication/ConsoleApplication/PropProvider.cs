using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace ConsoleApplication
{
    public interface IPropProvider
    {
        Dictionary<string, object> GetProperties(object obj);
    }

    class DirectoryInfoPropProvider : IPropProvider
    {
        public Dictionary<string, object> GetProperties(object obj)
        {
            DirectoryInfo di = obj as DirectoryInfo;
            var props = new Dictionary<string, object>();
            props.Add("Name", di.Name);
            props.Add("Parent", di.Parent);
            try
            {
                props.Add("ChildFiles", di.GetFiles());
            }
            catch
            {
                // ignore exceptions, just dont add the fields
            }
            return props;
        }
    }
}
