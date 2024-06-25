using SiteExample.ServiceReferenceRoe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteExample
{
    public partial class AdminMode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client service1Client = new Service1Client();
            service1Client.OpenAdminMenu();
            Response.Redirect("default.aspx");
            Session.Abandon();
        }
    }
}