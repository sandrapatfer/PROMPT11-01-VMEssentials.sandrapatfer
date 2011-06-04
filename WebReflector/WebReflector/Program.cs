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
        static IRouter<IHandler> ConfigHandlerRouter()
        {
            // configure templates
            var rootTemplate = new RoutingTemplate("/");
            var contextTemplate = new RoutingTemplate("/{ctx}");
            var ctxAssembliesTemplate = new RoutingTemplate("/{ctx}/as");
            var ctxNamespacesTemplate = new RoutingTemplate("/{ctx}/ns");
            var assemblyTemplate = new RoutingTemplate("/{ctx}/as/{assemblyName}");
            var namespaceTemplate = new RoutingTemplate("/{ctx}/ns/{namespacePrefix}");
            var typeTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}");
            var typeMethodTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}");
            var typeConstructorsTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/c");
            var typeFieldTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}");
            var typePropertyTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/p/{propName}");
            var typeEventTemplate = new RoutingTemplate("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}");

            // register templates to be mapped to uri on reflection context objects
            ctxAssembliesTemplate.Register(typeof(Reflector.Context), "ContextAssembliesUri");
            ctxNamespacesTemplate.Register(typeof(Reflector.Context), "ContextNamespacesUri");
            typeMethodTemplate.Register(typeof(Reflector.ContextTypeMethod), "MethodUri");
            typeConstructorsTemplate.Register(typeof(Reflector.ContextType), "ConstructorsUri");
            typeFieldTemplate.Register(typeof(Reflector.ContextTypeField), "FieldUri");
            typePropertyTemplate.Register(typeof(Reflector.ContextTypeProperty), "PropertyUri");
            typeEventTemplate.Register(typeof(Reflector.ContextTypeEvent), "EventUri");

            // register templates to be mapped to handlers on requests
            var handlerRouter = new Router<IHandler>();
            handlerRouter.Register(rootTemplate, new RootHandler());
            handlerRouter.Register(contextTemplate, new ContextHandler());
            handlerRouter.Register(ctxAssembliesTemplate, new ContextAssembliesHandler());
            handlerRouter.Register(ctxNamespacesTemplate, new ContextNamespacesHandler());
            handlerRouter.Register(assemblyTemplate, new AssemblyHandler());
            handlerRouter.Register(namespaceTemplate, new NamespaceHandler());
            handlerRouter.Register(typeTemplate, new TypeHandler());
            handlerRouter.Register(typeMethodTemplate, new MethodHandler());
            handlerRouter.Register(typeConstructorsTemplate, new ConstructorsHandler());
            handlerRouter.Register(typeFieldTemplate, new FieldHandler());
            handlerRouter.Register(typePropertyTemplate, new PropertyHandler());
            handlerRouter.Register(typeEventTemplate, new WebReflector.EventHandler());

            return handlerRouter;
        }

        static void Main(string[] args)
        {
            var uriBase = @"http://localhost:8080/";
            Reflector.Reflector.UriBase = uriBase;
            //Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");
            Reflector.Reflector.RegisterContext("v4.0", @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0");

            var handlerRouter = ConfigHandlerRouter();

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
                        var handler = handlerRouter.LookupHandler(HttpUtility.UrlDecode(ctx.Request.RawUrl), out parameters);

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
