using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public sealed class Router<T>
    {
        static List<TemplateHandler<T>> m_handlers = new List<TemplateHandler<T>>();

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

        public static void Register(string uri, T handler)
        {
            m_handlers.Add(new TemplateHandler<T>() { Template = uri, Handler = handler });
        }
    }
}
