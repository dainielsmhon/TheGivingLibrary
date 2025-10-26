<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Library.Login" %>


<!DOCTYPE html>
<html lang="he" dir="rtl">
<head>
    <meta charset="UTF-8">
    <title>התחברות - הספרייה הנדיבה</title>
    <link href="https://fonts.googleapis.com/css2?family=Varela+Round&display=swap" rel="stylesheet" />
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: 'Varela Round', sans-serif;
        }

        body {
            background-image: url('LibraryAdmin/assets/images/library-bg.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-container {
            background-color: rgba(255, 255, 255, 0.93);
            padding: 35px 40px;
            border-radius: 12px;
            max-width: 480px;
            width: 100%;
            box-shadow: 0 0 20px rgba(0,0,0,0.2);
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            margin-bottom: 6px;
            font-weight: bold;
        }

        .form-group input {
            width: 100%;
            padding: 12px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 14px;
            width: 100%;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
            margin-top: 5px;
        }

        .register-link {
            text-align: center;
            margin-top: 15px;
        }

        .register-link a {
            color: #007bff;
            text-decoration: none;
            font-weight: bold;
        }

        .register-link a:hover {
            text-decoration: underline;
        }

        .error-text {
            color: red;
            text-align: center;
            margin-bottom: 10px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>התחברות</h2>

            <!-- הודעת שגיאה -->
            <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="error-text">
                <asp:Label ID="lblError" runat="server" />
            </asp:Panel>

            <!-- כתובת מייל -->
            <div class="form-group">
                <label>כתובת מייל</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
            </div>

            <!-- כפתור לבדיקה -->
            <asp:Button ID="btnEmailCheck" runat="server" CssClass="btn-primary" Text="המשך" OnClick="btnEmailCheck_Click" />

            <!-- סיסמה -->
            <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                <div class="form-group">
                    <label>סיסמה</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                </div>

                <asp:Button ID="btnLogin" runat="server" CssClass="btn-primary" Text="התחבר" OnClick="btnLogin_Click" />
            </asp:Panel>

            <div class="register-link">
                משתמש חדש? <a href="Register.aspx">צור חשבון</a>
            </div>
        </div>
    </form>
</body>
</html>
