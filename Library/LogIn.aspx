<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Library.Login" %>




<!DOCTYPE html>
<html dir="rtl" lang="he">
<head runat="server">
    <meta charset="utf-8" />
    <title>התחברות</title>
    <link rel="stylesheet" href="css/login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>התחברות</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" Placeholder="אימייל"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" Placeholder="סיסמה"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="התחבר" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
