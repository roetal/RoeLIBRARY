<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Async="true" Inherits="SiteExample.Cart" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <form id="form1" runat="server">
        <asp:GridView ID="CartGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="CartGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="BookName" HeaderText="Book Name" />
                <asp:BoundField DataField="AuthorName" HeaderText="Author" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="RemainingTime" HeaderText="Time Left" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="DeleteBook" CommandArgument='<%# Eval("CartID") %>' Text="return" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server"></asp:Content>
