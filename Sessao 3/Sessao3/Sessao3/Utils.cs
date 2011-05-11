using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sessao3
{
    class Utils
    {
        public static void ProcessFiles(DirectoryInfo rootFolder, Func<FileInfo, bool> predicate, Action<FileInfo> action)
        {
            FileInfo[] files = rootFolder.GetFiles();
            foreach (FileInfo f in files)
            {
               if (predicate(f))
                    action(f);
            }
            DirectoryInfo[] dirs = rootFolder.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                ProcessFiles(d, predicate, action);
            }
        }
    }
}
