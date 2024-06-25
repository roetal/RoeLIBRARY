using SiteExample.ServiceReferenceRoe;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteExample
{
    public partial class Cart : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartItems();
            }
        }

        private async void LoadCartItems()
        {
            string username = Session["username"]?.ToString();
            if (string.IsNullOrEmpty(username))
            {
                Response.Redirect("viewbooks.aspx?msg=You are a guest, please login");
                return;
            }

            try
            {
                var cartItems = await client.GetCartItemsByUsernameAsync(username);
                if (cartItems != null && cartItems.Any())
                {
                    CartGridView.DataSource = cartItems;
                    CartGridView.DataBind();
                }
                else
                {
                    CartGridView.EmptyDataText = "Your cart is empty.";
                    CartGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        protected async void CartGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteBook")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int cartID))
                {
                    string username = Session["username"]?.ToString();
                    if (!string.IsNullOrEmpty(username))
                    {
                        await client.DeleteBookFromCartAsync(cartID);
                        LoadCartItems();
                    }
                }
                else
                {
                    Console.WriteLine("CommandArgument is not a valid integer.");
                }
            }
            else
            {
                Console.WriteLine("CommandName is not DeleteBook.");
            }
        }
    }
}
