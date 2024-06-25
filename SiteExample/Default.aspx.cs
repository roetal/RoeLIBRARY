using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteExample
{
    public partial class _default : System.Web.UI.Page
    {
        public string msg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] != null) {
                msg = "<h1>Hello "+Session["username"]+"" +
                    " welcome to the library" +
                    "</h1>";
                //msg += "<h2> אתה מבקר מספר " + Application["counter"]+"</h2>";
            } else
            {
                msg = "<h1>Welcome to the library</h1>";
            }
        }
    }
}