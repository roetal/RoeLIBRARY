<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SiteExample.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <div class="login-container">
        <h3>Login</h3>
        <form name="loginFrm" id="loginFrm" method="post" runat="server">
            <table>
                <tr>
                    <td>Username:</td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="input-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div id="msg" runat="server"></div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
