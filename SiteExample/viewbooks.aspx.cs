using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;
using SiteExample.ServiceReferenceRoe;
using WcfServiceLibrary;

namespace SiteExample
{
    public partial class viewbooks : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var task = client.GetAllBooksAsync();
                task.Wait();  
                List<Book> books = new List<Book>(task.Result);
                RepeaterBooks.DataSource = books;
                RepeaterBooks.DataBind();
            }
        }

        protected void RepeaterBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Book book = e.Item.DataItem as Book;
                if (book != null)
                {
                    Image imgBook = e.Item.FindControl("imgBook") as Image;
                    Label lblNoPicture = e.Item.FindControl("lblNoPicture") as Label;
                    Label lblAuthorName = e.Item.FindControl("lblAuthorName") as Label;
                    int bookId = book.Id;
                    if (imgBook != null)
                    {
                        byte[] imageBytes = client.getImageBytes(bookId);
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            string base64String = Convert.ToBase64String(imageBytes);
                            imgBook.ImageUrl = $"data:image/png;base64,{base64String}";
                            lblNoPicture.Visible = false;
                        }
                        else
                        {
                            lblNoPicture.Visible = true;
                        }
                    }

                    lblAuthorName.Text = GetAuthorName(book.AuthorID);
                }
            }
        }

        protected string GetAuthorName(int authorId)
        {
            return client.GetAuthorName(authorId);
        }

        protected void btnMoreDetails_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int bookId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"BookDetails.aspx?bookId={bookId}");
        }



    }
}
