using System;

namespace Library
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ניקוי כל המשתנים מה־Session
            Session.Clear();
            Session.Abandon();

            // מעבר חזרה לדף התחברות
            Response.Redirect("~/Login.aspx");

        }
    }
}
