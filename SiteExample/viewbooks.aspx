<%@ Page Title="View Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewbooks.aspx.cs" Inherits="SiteExample.viewbooks" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <h1 style="text-align: center;">Book Gallery</h1>
    <div style="margin-bottom: 20px; text-align: center;">
        <input type="text" id="txtSearch" placeholder="Search for a book..." />
        <button type="button" id="btnSearch">Search</button>
        <button type="button" id="btnReset">Reset</button>
    </div>

    <form id="form1" runat="server" >
        <div id="booksContainer" style="display: grid; grid-template-columns: repeat(4, 1fr); gap: 20px;">
            <asp:Repeater ID="RepeaterBooks" runat="server" OnItemDataBound="RepeaterBooks_ItemDataBound">
                <ItemTemplate>
                    <div class="book" style="border: 3px solid #ccc; padding: 10px; text-align: center;">
                        <asp:Image ID="imgBook" runat="server" Width="150px" Height="150px" />
                        <asp:Label ID="lblNoPicture" runat="server" Text="No Picture" Visible="false" /><br />
                        <a href="#" class="bookName" data-book-id='<%# Eval("Id") %>' style="display: block; font-weight: bold;"><%# Eval("Name") %></a>
                        <asp:Button ID="btnMoreDetails" runat="server" Text="More Details" OnClick="btnMoreDetails_Click" CommandArgument='<%# Eval("Id") %>' /><br />
                        <div class="bookDetails" data-book-id='<%# Eval("Id") %>' style="display: none;">
                            <p>Author: <asp:Label ID="lblAuthorName" runat="server" /></p>
                            <p>Genre: <%# Eval("Genre") %></p>
                            <p>Price: $<%# Eval("Price") %></p>
                            <p>Available Copies: <%# Eval("Available_copies") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const bookLinks = document.querySelectorAll(".bookName");
            const booksContainer = document.getElementById("booksContainer");
            const txtSearch = document.getElementById("txtSearch");
            const btnSearch = document.getElementById("btnSearch");
            const btnReset = document.getElementById("btnReset");

            bookLinks.forEach(link => {
                link.addEventListener("click", function (e) {
                    e.preventDefault();
                    const bookId = this.getAttribute("data-book-id");
                    const bookDetails = document.querySelector(`.bookDetails[data-book-id="${bookId}"]`);
                    bookDetails.style.display = (bookDetails.style.display === "none" || !bookDetails.style.display) ? "block" : "none";
                });
            });

            btnSearch.addEventListener("click", function () {
                const searchQuery = txtSearch.value.trim().toLowerCase();
                bookLinks.forEach(link => {
                    const bookName = link.textContent.toLowerCase();
                    const bookContainer = link.closest(".book");
                    bookContainer.style.display = bookName.includes(searchQuery) ? "block" : "none";
                });
            });

            btnReset.addEventListener("click", function () {
                txtSearch.value = "";
                booksContainer.querySelectorAll(".book").forEach(book => {
                    book.style.display = "block";
                });
            });
        });
    </script>
</asp:Content>
