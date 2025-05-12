<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Library.Register" %>


<!DOCTYPE html>
<html dir="rtl" lang="he">
<head runat="server">
    <meta charset="utf-8" />
    <title>הרשמה</title>
    <link rel="stylesheet" href="css/login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>הרשמה</h2>
            <asp:TextBox ID="TxtName" runat="server" CssClass="input" placeholder="שם מלא"></asp:TextBox>
            <asp:TextBox ID="TxtEmail" runat="server" TextMode="Email" CssClass="input" placeholder="אימייל" onblur="checkEmailAvailability(this.value)" />
            <span id="emailStatus" style="color:red; display:block;"></span>
            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="input" placeholder="סיסמה" onkeyup="checkPasswordStrength(this.value)" />
            <span id="passwordStrength" style="display:block;"></span>
            <asp:TextBox ID="TxtPhone" runat="server" CssClass="input" placeholder="פלאפון"></asp:TextBox>
            <asp:TextBox ID="TxtAdress" runat="server" CssClass="input" placeholder="כתובת"></asp:TextBox>
            <asp:Button ID="BtnRegister" runat="server" Text="הרשמה" CssClass="btn" OnClick="BtnRegister_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>
    </form>
    <script>
        function checkPasswordStrength(password) {
            const strengthText = document.getElementById("passwordStrength");
            let strength = 0;
            if (password.length >= 6) strength++;
            if (/[A-Z]/.test(password)) strength++;
            if (/[a-z]/.test(password)) strength++;
            if (/[0-9]/.test(password)) strength++;
            if (/\W/.test(password)) strength++;

            switch (strength) {
                case 0:
                case 1:
                    strengthText.textContent = "סיסמה חלשה מאוד";
                    strengthText.style.color = "red";
                    break;
                case 2:
                case 3:
                    strengthText.textContent = "סיסמה בינונית";
                    strengthText.style.color = "orange";
                    break;
                default:
                    strengthText.textContent = "סיסמה חזקה";
                    strengthText.style.color = "green";
            }
        }

        function checkEmailAvailability(email) {
            if (email.includes("@") && email.includes(".")) {
                fetch(`CheckEmail.aspx?email=${email}`)
                    .then(response => response.text())
                    .then(data => {
                        const emailStatus = document.getElementById("emailStatus");
                        if (data === "exists") {
                            emailStatus.textContent = "המייל כבר רשום במערכת.";
                        } else {
                            emailStatus.textContent = "המייל פנוי לרישום.";
                            emailStatus.style.color = "green";
                        }
                    });
            }
        }
    </script>
</body>
</html>

