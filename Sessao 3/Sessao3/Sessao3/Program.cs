using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sessao3
{
    class Program
    {
        static void Teste1(string dirPath, DateTime modified, int minSize)
        {
            Utils.ProcessFiles(new DirectoryInfo(dirPath),
            arg => { return (arg.LastWriteTime < modified && arg.Length > minSize); },
            arg => { Console.WriteLine("Path: {0} LastWrite: {1} Size: {2}", arg.FullName, arg.LastWriteTime, arg.Length); });

        }

        static void Main(string[] args)
        {
            // Parte I
            Teste1(@"c:\SPF\", System.Convert.ToDateTime("2011-01-01"), 10000);
        }
    }
}
