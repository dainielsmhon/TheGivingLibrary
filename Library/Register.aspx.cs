using System;
using DAL;
using BLL;

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

                // בדיקת תקינות אימייל
                if (!TxtEmail.Text.Contains("@") || !TxtEmail.Text.Contains("."))
                {
                    lblMessage.Text = "כתובת מייל לא תקינה.";
                    return;
                }

                // בדיקה שאורך הסיסמה לפחות 4 תווים
                if (TxtPassword.Text.Length < 4)
                {
                    lblMessage.Text = "הסיסמה חייבת להכיל לפחות 4 תווים.";
                    return;
                }

                // בדיקת התאמה בין שדות הסיסמה
                if (TxtPassword.Text != TxtConfirmPassword.Text)
                {
                    lblMessage.Text = "הסיסמאות אינן תואמות.";
                    return;
                }

                // בדיקה אם המייל כבר רשום במערכת
                var users = UserDAL.Get();
                if (users.Exists(u => u.Email.Equals(TxtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    lblMessage.Text = "כתובת מייל כבר קיימת.";
                    return;
                }

                // יצירת אובייקט משתמש חדש
                User newUser = new User()
                {
                    UserId = -1,
                    Name = TxtName.Text,
                    Email = TxtEmail.Text,
                    Phone = TxtPhone.Text,
                    Adress = TxtAdress.Text,
                    JoinDate = DateTime.Now,
                    UserPass = TxtPassword.Text.Trim(),
                    IsAdmin = false
                };

                // שמירה במסד נתונים
                UserDAL.Save(newUser);

                // הפניה לדף התחברות לאחר הרשמה מוצלחת
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "שגיאה בהרשמה: " + ex.Message;
            }
        }
    }
}
