using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;

namespace WebReflector
{
    class Program
    {
        static void Main(string[] args)
        {
            var uriBase = @"http://localhost:8080/";
            Reflector.Reflector.UriBase = uriBase;
            //Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");
            Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");
            
            // register templates
            Router<IHandler>.Register("/", new RootHandler());
            Router<IHandler>.Register("/{ctx}", new ContextHandler());
            Router<IHandler>.Register("/{ctx}/as", new ContextAssembliesHandler());
            Router<IHandler>.Register("/{ctx}/ns", new ContextNamespacesHandler());
            Router<IHandler>.Register("/{ctx}/as/{assemblyName}", new AssemblyHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespacePrefix}", new NamespaceHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}", new TypeHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}", new MethodHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/c", new ConstructorsHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}", new FieldHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/p/{propName}", new PropertyHandler());
            Router<IHandler>.Register("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}", new WebReflector.EventHandler());

            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add(uriBase);
                listener.Start();
                Console.WriteLine("Waiting requests...");
                while (true)
                {
                    var ctx = listener.GetContext();
                    Console.WriteLine(string.Format("Request: {0}", HttpUtility.UrlDecode(ctx.Request.RawUrl)));
                    var w = new StreamWriter(ctx.Response.OutputStream);

                    try
                    {
                        Dictionary<string, string> parameters;
                        var handler = Router<IHandler>.LookupHandler(HttpUtility.UrlDecode(ctx.Request.RawUrl), out parameters);

                        handler.Handle(parameters).RenderContent(w);
                    }
                    catch
                    {
                        // TODO build the error response
                        w.WriteLine("<body>Error!</body>");
                    }
                    w.Close();
                }
            }
        }
    }
}
