using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteExample
{
    public partial class logout : Page
    {
        ServiceReferenceRoe.Service1Client client = new ServiceReferenceRoe.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["username"] == null)
            {
                Response.Redirect("errorMsg.aspx?img=illegalAccess.png&msg=גישה לא חוקית לדף זה");
            }
            else
            {
                Session["username"]=null;
                Session.Abandon();
                Session.Clear();
                Response.Redirect("Home.aspx");
                client.CloseAdminMenu();
            }
        }
    }
}