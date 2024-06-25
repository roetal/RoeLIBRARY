using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfServiceLibrary;

namespace SiteExample
{
    public partial class registration : System.Web.UI.Page
    {
        ServiceReferenceRoe.Service1Client client = new ServiceReferenceRoe.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                    string username = Request.Form["username"].ToString();
                    string mail = Request.Form["email"].ToString();
                    string phone = Request.Form["phone"].ToString();
                    string password = Request.Form["pass"].ToString();
                    string confirmpass = Request.Form["confirmpass"].ToString();
                 if(username=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invalid name", "ShowPopup();", true);
                    return;
                }
                 if(mail=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invalid email", "ShowPopup();", true);
                    return;
                }
                 if(phone=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invalid phone number", "ShowPopup();", true);
                    return ;
                }
                 if(password =="" && password != confirmpass)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invalid password", "ShowPopup();", true);
                    return;
                }

                Users user = new Users();
                {
                    user.Username = username;
                    user.Email = mail;
                    user.Phone = phone;
                    user.Password = password;
                }

                

                if(client.checkusername(user))
                {
                    client.AddUser(user);
                    Response.Redirect("Login.aspx?msg=הרישום בוצע בהצלחה !!!");
                  
                }
                else
                {
                    //masssge
                }

              
            }
        }
    }
}