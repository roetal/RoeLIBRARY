<%@ Page Title="ראשי" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SiteExample._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="~/Css/RoeStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <div class="content-container">
        <div dir="rtl">
           <%=msg%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
