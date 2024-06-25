using SiteExample.ServiceReferenceRoe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WcfServiceLibrary;

namespace SiteExample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // מאתחל את מונה המבקרים
            Application["counter"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
            //Service1Client client = new Service1Client();
            //client.CloseAdminMenu();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application["counter"] = (int)Application["counter"] + 1;
        }

        void Session_End(object sender, EventArgs e)
        {
            Session["user"] = null;
            Session["admin"] = null;
        }
    }
}