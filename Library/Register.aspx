<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Library.Register" %>

<!DOCTYPE html>
<html lang="he" dir="rtl">
<head>
    <meta charset="utf-8" />
    <title>הרשמה - הסיפריה הנדיבה</title>
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

        .register-container {
            background-color: rgba(255, 255, 255, 0.92);
            padding: 30px 40px;
            border-radius: 12px;
            max-width: 500px;
            width: 100%;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
        }

        h2 {
            text-align: center;
            margin-bottom: 25px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group input {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 12px;
            width: 100%;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
        }

        .error-message {
            color: red;
            display: block;
            text-align: center;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <h2>הרשמה</h2>

            <div class="form-group">
                <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" placeholder="שם מלא"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="אימייל"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox ID="TxtPhone" runat="server" CssClass="form-control" placeholder="נייד"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="סיסמה"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="אימות סיסמה"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:TextBox ID="TxtAdress" runat="server" CssClass="form-control" placeholder="כתובת"></asp:TextBox>
            </div>

            <asp:Button ID="BtnRegister" runat="server" Text="הרשם" CssClass="btn-primary" OnClick="BtnRegister_Click" />

            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>
    </form>
</body>
</html>
