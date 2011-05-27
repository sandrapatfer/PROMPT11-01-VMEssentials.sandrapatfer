using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class Router
    {
        #region Properties
        List<TemplateHandler> m_handlers = new List<TemplateHandler>();
        #endregion

        public Router()
        {
            // register templates
            Register("/", new RootHandler());
            Register("/{ctx}", new ContextHandler());
            Register("/{ctx}/as", new ContextAssembliesHandler());
            Register("/{ctx}/ns", new ContextNamespacesHandler());
            Register("/{ctx}/as/{assemblyName}", new AssemblyHandler());
            Register("/{ctx}/ns/{namespacePrefix}", new NamespaceHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}", new TypeHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}", new MethodHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}/c", new ConstructorsHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}", new FieldHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}/p/{propName}", new PropertyHandler());
            Register("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}", new WebReflector.EventHandler());
        }

        public IHtmlView HandleRequest(string uri)
        {
            // identify handler based on uri
            var tHandler = m_handlers.Find(t => t.MapsTemplate(uri));
            if (tHandler == null)
                throw new HandlerNotFoundForUriException() { Uri = uri };

            // map the parameters
            var parameters = tHandler.MapUriParameters(uri);

            // call handle and return response (view?)
            return tHandler.Handler.Handle(parameters);
        }

        void Register(string uri, IHandler handler)
        {
            m_handlers.Add(new TemplateHandler() { Template = uri, Handler = handler });
        }
    }
}
