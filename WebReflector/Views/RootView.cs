using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector
{
    public class RootView : IHtmlView
    {

        #region IHtmlView Members

        public string Html
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
