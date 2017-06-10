using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ContactProvider
{
    public class ProviderHttpHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            ProviderFactory.GetCurrentProvider().SetToken();
        }

        #endregion
    }
}
