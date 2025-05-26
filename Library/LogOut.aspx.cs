using System;

namespace Library
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ניקוי כל נתוני הסשן
            Session.Clear();
            Session.Abandon();

            // ניתוב לדף התחברות
            Response.Redirect("Login.aspx");
        }
    }
}

