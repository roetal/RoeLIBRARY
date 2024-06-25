using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using SiteExample.ServiceReferenceRoe;
using WcfServiceLibrary;

namespace SiteExample
{
    public partial class BookDetails : System.Web.UI.Page
    {
        ServiceReferenceRoe.Service1Client client = new ServiceReferenceRoe.Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookIdQuery = Request.QueryString["bookId"];
                if (int.TryParse(bookIdQuery, out int bookId))
                {
                    LoadBookDetails(bookId);
                    LoadAverageRating(bookId);
                    LoadReviews(bookId);
                    ViewState["BookId"] = bookId; 
                }
                else
                {
                    Response.Redirect("viewbooks.aspx"); 
                }
            }
        }

        private void LoadBookDetails(int bookId)
        {
            Book book = client.GetBookById(bookId);
            if (book != null)
            {
                lblBookName.Text = book.Name;
                lblGenre.Text = book.Genre;
                lblPrice.Text = book.Price.ToString("F2");
                lblAvailableCopies.Text = book.Available_copies.ToString();
                lblBookBio.Text = book.Bio;

                // Set the book image
                SetBookImage(bookId);

                Author author = client.GetAuthorById(book.AuthorID);
                if (author != null)
                {
                    lblAuthorName.Text = author.Name;
                    lblAuthorBio.Text = author.Bio;
                }
                else
                {
                    lblAuthorName.Text = "Unknown Author";
                    lblAuthorBio.Text = "No bio available.";
                }
            }
            else
            {
                Response.Redirect("viewbooks.aspx"); 
            }
        }

        private void SetBookImage(int bookId)
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

        private void LoadAverageRating(int bookId)
        {
            double averageRating = client.GetAverageRating(bookId);
            lblAverageRating.Text = averageRating.ToString("F1");
        }

        private void LoadReviews(int bookId)
        {
            var reviews = client.GetBookReviews(bookId);
            rptReviews.DataSource = reviews;
            rptReviews.DataBind();
        }

        protected void rptReviews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var review = (Reviews)e.Item.DataItem;
                var deleteButton = (Button)e.Item.FindControl("DeleteButton");
                    string username = Session["username"]?.ToString();
                    Users user = client.GetUserWithName(username);
                    if (user != null)
                    {
                        deleteButton.Visible = review.User_id == user.Id;
                    }
            }
        }

        protected async void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (ViewState["BookId"] != null && int.TryParse(ViewState["BookId"].ToString(), out int bookId))
            {
                string username = Session["username"]?.ToString();

                if (!string.IsNullOrEmpty(username))
                {
                    Users user = await client.GetUserWithNameAsync(username);

                    if (user != null)
                    {
                        try
                        {
                            Shoping_cart shoppingCart = new Shoping_cart
                            {
                                Book_id = bookId,
                                User_id = user.Id,
                                AddedDate = DateTime.Now,
                            };
                            await client.AddToCartAsync(shoppingCart);
                            lblSuccessMessage.Text = "Book added to cart. It will be reserved for you for 14 days.";
                        }
                        catch (Exception ex)
                        {
                            // Log the error
                            Console.WriteLine("An error occurred while adding to the cart: " + ex.ToString());
                            lblErrorMessage.Text = "An error occurred while adding the book to the cart.";
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "User not found. Please login again.";
                    }
                }
                else
                {
                    lblErrorMessage.Text = "Please log in to add to cart.";
                }
            }
        }

        protected async void btnAddReview_Click(object sender, EventArgs e)
        {
            if (ViewState["BookId"] != null && int.TryParse(ViewState["BookId"].ToString(), out int bookId))
            {
                string username = Session["username"]?.ToString();

                if (!string.IsNullOrEmpty(username))
                {
                    Users user = await client.GetUserWithNameAsync(username);

                    if (user != null)
                    {
                        try
                        {
                            Reviews review = new Reviews
                            {
                                Book_id = bookId,
                                User_id = user.Id,
                                Description = txtReviewDescription.Text,
                                Rating = int.Parse(ddlRating.SelectedValue),
                                Review_date = DateTime.Now,
                            };
                            await client.AddReviewAsync(review);
                            lblReviewSuccessMessage.Text = "Review submitted successfully.";
                            LoadReviews(bookId); // Reload reviews to show the new one
                            //Response.Redirect("#");// .........................
                        }
                        catch (Exception ex)
                        {
                            // Log the error
                            Console.WriteLine("An error occurred while adding the review: " + ex.ToString());
                            lblReviewErrorMessage.Text = "An error occurred while submitting your review.";
                        }
                    }
                    else
                    {
                        lblReviewErrorMessage.Text = "User not found. Please login again.";
                    }
                }
                else
                {
                    lblReviewErrorMessage.Text = "Please log in to submit a review.";
                }
            }
        }

        protected async void DeleteReview(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;

            string[] arguments = deleteButton.CommandArgument.Split(',');
            int reviewId = int.Parse(arguments[0]);
            int bookId = int.Parse(arguments[1]);
            await client.DeleteUserReviewsAsync(reviewId);
            LoadReviews(bookId);

        }

      

    }
}
