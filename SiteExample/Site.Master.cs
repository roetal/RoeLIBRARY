using System;
using System.Web.Services;
using System.Web.UI;
using SiteExample.ServiceReferenceRoe;
using WcfServiceLibrary;

namespace SiteExample
{
    public partial class Site : MasterPage
    {
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl navbar;
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] guestMenu = { "Home", "Login", "registration", "viewbooks"};
                string[] userMenu = { "Home", "Logout", "viewbooks", "Cart" };
                string[] adminMenu = { "Home", "Logout", "viewbooks", "AdminMode", "Cart" };
                string[] arr = guestMenu;

                string menu = "<a href='#' style='color:orange;'>Hello ";

                string username = Session["username"] as string;
                Users user = client.GetUserWithName(username);
                if (user != null)
                {
                    if (user.IsAdmin)
                    {
                        arr = adminMenu;
                        menu += username + " Admin";
                    }
                    else
                    {
                        arr = userMenu;
                        menu += " " + username;
                    }
                }
                else
                {
                    menu += " Guest";
                }

                menu += "</a>";

                foreach (var item in arr)
                {
                        menu += $"<a href='{item}.aspx'>{item}</a>";
                }

                navbar.InnerHtml = menu;
            }
        }

       
    }
}
