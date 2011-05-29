using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    //TODO tornar esta classe generica sobre o IHandler, o handle request deve devolver o handle e quem chama é que invoca o resto
    public sealed class Router<T>
    {
        #region Properties
        //static List<TemplateHandler> m_handlers = new List<TemplateHandler>();
        static List<TemplateHandler<T>> m_handlers = new List<TemplateHandler<T>>();
        #endregion

        public Router()
        {
        }

        public static T LookupHandler(string uri, out Dictionary<string, string> parameters)
        {
            // identify handler based on uri
            var tHandler = m_handlers.Find(t => t.MapsTemplate(uri));
            if (tHandler == null)
                throw new HandlerNotFoundForUriException() { Uri = uri };
            parameters = tHandler.MapUriParameters(uri);
            return tHandler.Handler;
        }

/*        public static IHtmlView HandleRequest(string uri)
        {
            // identify handler based on uri
            var tHandler = m_handlers.Find(t => t.MapsTemplate(uri));
            if (tHandler == null)
                throw new HandlerNotFoundForUriException() { Uri = uri };

            // map the parameters
            var parameters = tHandler.MapUriParameters(uri);

            // call handle and return response (view?)
            return tHandler.Handler.Handle(parameters);
        }*/

        public static void Register(string uri, T handler)
        {
            m_handlers.Add(new TemplateHandler<T>() { Template = uri, Handler = handler });
        }
    }
}
