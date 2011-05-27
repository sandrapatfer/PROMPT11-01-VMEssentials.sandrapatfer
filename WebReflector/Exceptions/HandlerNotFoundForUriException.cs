using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class HandlerNotFoundForUriException : SystemException
    {
        public string Uri { get; set; }
    }
}
