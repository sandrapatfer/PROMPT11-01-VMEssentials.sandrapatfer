using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add(@"http://localhost:8080/");
                listener.Start();
                while (true)
                {
                    var ctx = listener.GetContext();
                    Console.WriteLine("Context received");
                    var w = new StreamWriter(ctx.Response.OutputStream);
                    w.WriteLine("<html><body>Hello</body></html>");
                    w.Close();
                }
            }
        }
    }
}
