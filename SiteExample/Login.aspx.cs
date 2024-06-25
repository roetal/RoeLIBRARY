using Microsoft.Identity.Client;
using SiteExample.ServiceReferenceRoe;
using System;
using System.Web.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SiteExample
{
    public partial class Login : System.Web.UI.Page
    {
        ServiceReferenceRoe.Service1Client client = new ServiceReferenceRoe.Service1Client();
        public string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    Response.Redirect("errorMsg.aspx?img=illegalAccess.png&msg=גישה לא חוקית לדף זה");
                }
                else if (Request.QueryString["msg"] != null)
                {
                    msg.InnerText = Request.QueryString["msg"].ToString();
                }
            }

            //if (Request.Form["submit"] != null)
            //{
            //    username = Request.Form["username"].ToString();
            //    string password = Request.Form["password"];
            //    if (client.IsExist(username, password))
            //    {
            //        Session["username"] = username;
            //        Response.Redirect("Home.aspx");

            //    }
            //}

        }
            protected void btnSubmit_Click(object sender, EventArgs e)
            {
              
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                if (client.ValidateUser(username, password)!=null)
                {
                    Session["username"] = username;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    // Show error message
                    msg.InnerText = "Invalid username or password!";
                }
             
            }



    }
}
