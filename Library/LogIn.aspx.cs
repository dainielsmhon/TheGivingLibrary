using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Amazon.Runtime.Internal.Util;
using DAL;
using Library.LibraryAdmin;

namespace Library
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            string hashedPassword = HashPassword(password);

            var user = UserDAL.Get().Find(u => u.Email == email && u.UserPass == hashedPassword);

            if (user != null)
            {
                Session["User"] = user;

                if (user.Email == "danielsimhon931@gmail.com")
                    Response.Redirect("~/LibraryAdmin/Default.aspx");
                else
                    Response.Redirect("~/LibraryUser/Default.aspx");
            }
            else
            {
                lblMessage.Text = "אימייל או סיסמה שגויים";
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