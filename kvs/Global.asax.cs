using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace kvs
{
    public class Global : System.Web.HttpApplication
    {
        public object FormsAuthenticaion { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["kvid"] = Guid.NewGuid().ToString();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }

            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetValidUntilExpires(true);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
           // FormsAuthentication.SignOut();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}