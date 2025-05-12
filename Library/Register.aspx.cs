
using System;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime.Internal.Util;
using System.Xml.Linq;
using DAL;
using Library.LibraryAdmin;
using BLL;

namespace Library
{
    public partial class Register : System.Web.UI.Page
    {
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtName.Text) ||
                    string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                    string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    lblMessage.Text = "אנא מלא את כל השדות.";
                    return;
                }

                if (!TxtEmail.Text.Contains("@") || !TxtEmail.Text.Contains("."))
                {
                    lblMessage.Text = "כתובת מייל לא תקינה.";
                    return;
                }

                if (TxtPassword.Text.Length < 6 ||
                    TxtPassword.Text == "123456" ||
                    TxtPassword.Text.ToLower() == "password")
                {
                    lblMessage.Text = "סיסמה חלשה מדי.";
                    return;
                }

                var users = UserDAL.Get();
                if (users.Exists(u => u.Email.Equals(TxtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    lblMessage.Text = "כתובת מייל כבר קיימת.";
                    return;
                }
                User newUser = new User()
                {
                    Name = TxtName.Text,
                    Email = TxtEmail.Text,
                    Phone = TxtPhone.Text,
                    Adress = TxtAdress.Text,
                    JoinDate = DateTime.Now,
                    UserPass = HashPassword(TxtPassword.Text)
                };

                UserDAL.Save(newUser);
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "שגיאה בהרשמה: " + ex.Message;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
