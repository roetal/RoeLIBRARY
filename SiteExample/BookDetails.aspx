<%@ Page Title="Book Details" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="SiteExample.BookDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .book-details {
            border: 1px solid #ccc;
            padding: 20px;
            margin: 20px auto;
            max-width: 600px;
        }
        .book-details img {
            max-width: 200px;
            margin-bottom: 20px;
        }
        .book-details h2 {
            margin-bottom: 20px;
        }
        .book-details p {
            margin: 10px 0;
        }
        .no-picture {
            color: red;
            font-weight: bold;
        }
        .review-section {
            margin-top: 40px;
            border-top: 2px solid #ccc;
            padding-top: 20px;
        }
        .review-item {
            border-bottom: 1px solid #ccc;
            padding: 10px 0;
        }
        .review-item:last-child {
            border-bottom: none;
        }
        .review-rating {
            color: gold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <form id="form1" runat="server">
        <h3>Average Rating: <asp:Label ID="lblAverageRating" runat="server" /></h3>

        <div class="book-details">
            <center>
                 <asp:Image ID="imgBook" runat="server" Width="200px" Height="200px" />
            </center>
            <asp:Label ID="lblNoPicture" runat="server" CssClass="no-picture" Text="No Picture Available" Visible="false"></asp:Label>
            <h2><asp:Label ID="lblBookName" runat="server" /></h2>
            <p><strong>Book Bio:</strong></p>
            <p><asp:Label ID="lblBookBio" runat="server" /></p>
            <p><strong>Genre:</strong> <asp:Label ID="lblGenre" runat="server" /></p>
            <p><strong>Price:</strong> $<asp:Label ID="lblPrice" runat="server" /></p>
            <p><strong>Available Copies:</strong> <asp:Label ID="lblAvailableCopies" runat="server" /></p>
            <p><strong>Author:</strong> <asp:Label ID="lblAuthorName" runat="server" /></p>
            <p><strong>Author Bio:</strong></p>
            <p><asp:Label ID="lblAuthorBio" runat="server" /></p>

            <asp:Label ID="lblSuccessMessage" runat="server" ForeColor="Green" />
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" />
            <asp:Button ID="Button1" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" />

            <h3>Add a Review</h3>
            <asp:TextBox ID="txtReviewDescription" runat="server" TextMode="MultiLine" Rows="4" Width="400px" />
            <br />
            <asp:DropDownList ID="ddlRating" runat="server">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnAddReview" runat="server" Text="Submit Review" OnClick="btnAddReview_Click" />
            <asp:Label ID="lblReviewSuccessMessage" runat="server" ForeColor="Green" />
            <asp:Label ID="lblReviewErrorMessage" runat="server" ForeColor="Red" />
        </div>

        <div class="review-section">
            <h3>Book Reviews:</h3>
            <asp:Repeater ID="rptReviews" runat="server" OnItemDataBound="rptReviews_ItemDataBound">
                <ItemTemplate>
                    <div class="review-item">
                           <p><strong></strong> <span class="review-rating"><%# new String('★', Convert.ToInt32(Eval("Rating"))) %></span></p>
                        <p><strong>Review:</strong> <%# Eval("Description") %></p>
                        <p><strong>Date:</strong> <%# Eval("Review_date", "{0:MMMM dd, yyyy}") %></p>

                        <asp:Button  ID="DeleteButton" runat="server" OnClick="DeleteReview" Visible="false" Text="DeleteReview" CommandArgument='<%# Eval("Id") + "," + Eval("Book_id") %>' />

                  
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
