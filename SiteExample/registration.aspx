<%@ Page Title="הרשמה" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="SiteExample.registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Css/formsCss.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SiteContent" runat="server">
    <div class="registration-container">
        <h3>Sign Up</h3>
        <form id="regForm" method="post" runat="server">
            <div class="row">
                <label for="username">Username</label>
                <input type="text" id="username" name="username">
            </div>
            <div class="row">
                <label for="email">Email</label>
                <input type="email" id="email" name="email">
            </div>
            <div class="row">
                <label for="phone">Phone Number</label>
                <input type="tel" id="phone" name="phone">
            </div>
            <div class="row">
                <label for="pass">Password</label>
                <input type="password" id="pass" name="pass">
            </div>
            <div class="row">
                <label for="confirmpass">Confirm Password</label>
                <input type="password" id="confirmpass" name="confirmpass">
            </div>
            <div class="row">
                <input type="submit" onclick="return validateLogin()" name="submit" value="Sign Up">
            </div>
        </form>
        <div id="errorMsg" runat="server"></div>
    </div>
    <script>
        function validateLogin() {
            const username = document.getElementById('username').value;
            const email = document.getElementById('email').value;
            const phone = document.getElementById('phone').value;
            const pass = document.getElementById('pass').value;
            const confirmpass = document.getElementById('confirmpass').value;

            if (username == "") {
                alert("Username cannot be empty");
                return false;
            }
            if (email == "") {
                alert("Email cannot be empty");
                return false;
            }
            if (phone == "") {
                alert("Phone number cannot be empty");
                return false;
            }
            if (pass == "") {
                alert("Password cannot be empty");
                return false;
            }
            if (pass !== confirmpass) {
                alert("Password and Confirm Password do not match");
                return false;
            }

            alert("Registration successful");
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
