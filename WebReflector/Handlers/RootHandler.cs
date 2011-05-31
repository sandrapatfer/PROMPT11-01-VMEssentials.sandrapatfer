using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class RootHandler : IHandler
    {
        public IHtmlView Handle(Dictionary<string, string> parameters)
        {
            return new RootView(Reflector.Reflector.GetContextList());
        }
    }
}
