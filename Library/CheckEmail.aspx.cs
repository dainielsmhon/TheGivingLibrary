
using System;
using DAL;

namespace Library
{
    public partial class CheckEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"] + "";
            bool exists = UserDAL.Get().Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            Response.Write(exists ? "exists" : "not_exists");
            Response.End();
        }
    }
}
