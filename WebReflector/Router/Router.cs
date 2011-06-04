using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class Router<H> : IRouter<H>
    {
        class TemplateHandler<TH>
        {
            public IRoutingTemplate Template { get; set; }
            public TH Handler { get; set; }
        }
        List<TemplateHandler<H>> m_templates = new List<TemplateHandler<H>>();

        public Router()
        {
        }

        public H LookupHandler(string uri, out Dictionary<string, string> parameters)
        {
            // identify handler based on uri
            Dictionary<string, string> auxParameters = null;
            var tHandler = m_templates.Find(t => t.Template.MapsTemplate(uri, out auxParameters));
            if (tHandler == null)
                throw new HandlerNotFoundForUriException() { Uri = uri };

            parameters = auxParameters;
            return tHandler.Handler;
        }

        public void Register(IRoutingTemplate template, H handler)
        {
            m_templates.Add(new TemplateHandler<H>() { Template = template, Handler = handler });
        }
    }
}
