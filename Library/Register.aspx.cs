using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Library
{
    public partial class Register : System.Web.UI.Page
    {
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // בדיקה אם שדות חובה ריקים
                if (string.IsNullOrWhiteSpace(TxtName.Text) ||
                    string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                    string.IsNullOrWhiteSpace(TxtPassword.Text) ||
                    string.IsNullOrWhiteSpace(TxtConfirmPassword.Text))
                {
                    lblMessage.Text = "אנא מלא את כל השדות.";
                    return;
                }

                // בדיקת תקינות מייל בסיסית
                if (!TxtEmail.Text.Contains("@") || !TxtEmail.Text.Contains("."))
                {
                    lblMessage.Text = "כתובת מייל לא תקינה.";
                    return;
                }

                // בדיקת אורך סיסמה
                if (TxtPassword.Text.Length < 4)
                {
                    lblMessage.Text = "הסיסמה חייבת להכיל לפחות 4 תווים.";
                    return;
                }

                // בדיקה ששתי הסיסמאות תואמות
                if (TxtPassword.Text.Trim() != TxtConfirmPassword.Text.Trim())
                {
                    lblMessage.Text = "הסיסמאות אינן תואמות.";
                    return;
                }

                // בדיקה אם כתובת המייל כבר קיימת במערכת (לפי מה שלמדת)
                int userId = BLL.User.CheckUserByEmail(TxtEmail.Text.Trim());

                if (userId != -1)
                {
                    // מייל כבר רשום
                    lblMessage.Text = "כתובת מייל כבר קיימת במערכת.";
                    return;
                }

                // יצירת משתמש חדש
                User newUser = new User()
                {
                    UserId = -1, // חדש
                    Name = TxtName.Text.Trim(),
                    Email = TxtEmail.Text.Trim(),
                    Phone = TxtPhone.Text.Trim(),
                    Adress = TxtAdress.Text.Trim(),
                    JoinDate = DateTime.Now,
                    UserPass = TxtPassword.Text.Trim(),
                    IsAdmin = false
                };

                // שמירה בבסיס הנתונים
                newUser.Save();

                // הודעת הצלחה + מעבר לדף התחברות
                lblMessage.Text = "נרשמת בהצלחה! מועבר לדף ההתחברות...";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirectLogin",
                    "setTimeout(function(){ window.location='Login.aspx'; }, 2000);", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "שגיאה בהרשמה: " + ex.Message;
            }
        }
    }
}

