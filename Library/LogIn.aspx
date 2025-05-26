<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Library.Login" %>

<!DOCTYPE html>
<html lang="he" dir="rtl">
<head>
    <meta charset="UTF-8">
    <title>התחברות - הסיפריה הנדיבה</title>
    <link href="https://fonts.googleapis.com/css2?family=Varela+Round&display=swap" rel="stylesheet" />
    <style>
        /* עיצוב כללי */
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: 'Varela Round', sans-serif;
        }

        /* עיצוב הרקע של הדף */
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

        /* עיצוב הקונטיינר של הטופס */
        .login-container {
            background-color: rgba(255, 255, 255, 0.92);
            padding: 30px 40px;
            border-radius: 12px;
            max-width: 500px; /* הגדלת הרוחב של הקונטיינר */
            width: 100%;
            box-shadow: 0 0 20px rgba(0,0,0,0.2);
        }

        /* עיצוב השדות של הטופס */
        .form-group label {
            display: block;
            margin-bottom: 6px;
            font-weight: bold;
        }

        .form-group input {
            width: 100%; /* מאפשר לשדה למלא את כל הרוחב */
            padding: 15px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
        }

        /* עיצוב כפתור התחברות */
        .btn-primary {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 15px;
            width: 100%;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
        }

        /* עיצוב לינק למשתמש חדש */
        .register-link {
            text-align: center;
            margin-top: 15px;
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

            <!-- הודעת שגיאה, תוצג אם יש טעות במייל או סיסמה -->
            <asp:Panel ID="pnlError" runat="server" Visible="false">
                <p style="color:red; text-align:center;">המייל לא קיים במערכת או סיסמה שגויה</p>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
            </asp:Panel>

            <!-- שדה למייל -->
            <div class="form-group">
                <label>כתובת מייל</label>
               <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" Style="width: 400px; font-size: 18px;" />

            </div>

            <!-- כפתור להמשך (שלב ראשון) -->
            <asp:Button ID="btnEmailCheck" runat="server" CssClass="btn-primary" Text="המשך" OnClick="btnEmailCheck_Click" AutoPostBack="true" />

            <!-- שדה סיסמה, מוצג רק לאחר שמזינים מייל תקין -->
            <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                <div class="form-group">
                    <label>סיסמה</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" />
                </div>

                <!-- כפתור להתחברות -->
                <asp:Button ID="btnLogin" runat="server" CssClass="btn-primary" Text="התחבר" OnClick="btnLogin_Click" AutoPostBack="true" />

                <!-- קישור לשכחתי סיסמה, רק אחרי שמילאנו את המייל והסיסמה -->
                <div style="text-align: center; margin-top: 10px;">
                    <a href="javascript:void(0);" onclick="showForgotPassword()">שכחתי סיסמה</a>
                </div>
            </asp:Panel>

            <div class="or-separator"><span>או</span></div>

            <!-- כפתורים להתחברות עם רשתות חברתיות -->
            <div class="social-login">
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" height="20" /> התחבר עם Google</button>
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/0/05/Facebook_Logo_%282019%29.png" height="20" /> התחבר עם Facebook</button>
                <button><img src="https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg" height="20" /> התחבר עם Apple</button>
            </div>

            <!-- קישור למשתמש חדש -->
            <div class="register-link">
                משתמש חדש? <a href="Register.aspx">צור חשבון</a>
            </div>
        </div>
    </form>

    <script>
        // הצגת שדה "שכחתי סיסמה" רק אחרי שלב הסיסמה
        function showForgotPassword() {
            var email = document.getElementById("txtEmail").value;
            if (email) {
                // אם המייל קיים במערכת
                alert("הסיסמה נשלחה למייל שלך.");
            } else {
                // אם המייל לא הוזן עדיין
                alert("הכנס מייל קודם.");
            }
        }
    </script>
</body>
</html>
