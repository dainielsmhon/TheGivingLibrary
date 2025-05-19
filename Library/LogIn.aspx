<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Library.Login" %>

<!DOCTYPE html>
<html lang="he" dir="rtl">
<head>
    <meta charset="UTF-8">
    <title>התחברות - הסיפריה הנדיבה</title>
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
            background-color: rgba(255, 255, 255, 0.92);
            padding: 30px 40px;
            border-radius: 12px;
            max-width: 400px;
            width: 100%;
            box-shadow: 0 0 20px rgba(0,0,0,0.2);
        }

        .form-group label {
            display: block;
            margin-bottom: 6px;
            font-weight: bold;
        }

        .form-group input {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px;
            width: 100%;
            border-radius: 6px;
            cursor: pointer;
        }

        .register-link {
            text-align: center;
            margin-top: 15px;
        }

        .password-field {
            position: relative;
        }

        .eye-icon {
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
        }

        .or-separator {
            text-align: center;
            margin: 15px 0;
            position: relative;
        }

        .social-login button {
            width: 100%;
            margin-bottom: 10px;
            padding: 10px;
            border-radius: 6px;
            border: 1px solid #ddd;
            background-color: #f9f9f9;
            cursor: pointer;
            font-size: 15px;
            display: flex;
            align-items: center;
            justify-content: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>התחברות</h2>

            <asp:Panel ID="pnlError" runat="server" Visible="false">
                <p style="color:red; text-align:center;">המייל לא קיים במערכת או סיסמה שגויה</p>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
            </asp:Panel>

            <!-- שדה המייל -->
            <div class="form-group">
                <label>כתובת מייל</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            </div>

            <asp:Button ID="btnEmailCheck" runat="server" CssClass="btn-primary" Text="המשך" OnClick="btnEmailCheck_Click" />

            <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                <div class="form-group">
                    <label>סיסמה</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" CssClass="btn-primary" Text="התחבר" OnClick="btnLogin_Click" />

                <!-- כפתור "שכחתי סיסמה" -->
                <div style="text-align: center; margin-top: 10px;">
                    <a href="javascript:void(0);" onclick="showForgotPassword()">שכחתי סיסמה</a>
                </div>

                <!-- כפתור הצגת סיסמה -->
                <div style="text-align: center; margin-top: 10px;">
                    <a href="javascript:void(0);" onclick="togglePasswordVisibility()">הצג סיסמה</a>
                </div>
            </asp:Panel>

            <div class="or-separator"><span>או</span></div>

            <div class="social-login">
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" height="20" /> התחבר עם Google</button>
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/0/05/Facebook_Logo_%282019%29.png" height="20" /> התחבר עם Facebook</button>
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg" height="20" /> התחבר עם Apple</button>
            </div>

            <div class="register-link">
                משתמש חדש? <a href="Register.aspx">צור חשבון</a>
            </div>
        </div>
    </form>

    <script>
        // פונקציה שתראה את שדה הסיסמה רק לאחר שלב הזנת המייל
        function togglePasswordVisibility() {
            var passwordField = document.getElementById("txtPassword");
            if (passwordField.type === "password") {
                passwordField.type = "text";
            } else {
                passwordField.type = "password";
            }
        }

        // הצגת שדה "שכחתי סיסמה" רק אחרי שלב הסיסמה
        function showForgotPassword() {
            var email = document.getElementById("txtEmail").value;
            if (email) {
                // אם המייל קיים, נשלח סיסמה למייל
                alert("הסיסמה נשלחה למייל שלך.");
            } else {
                alert("הכנס מייל קודם.");
            }
        }
    </script>
</body>
</html>
