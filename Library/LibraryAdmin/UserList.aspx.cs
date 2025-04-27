using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace Library.LibraryAdmin
{
    public partial class book : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();  // טוען את רשימת משתמשים
            }
        }



        private void FillData()
        {
            var users = BLL.User.Get(); // טוען את כל המשתמשים
            Repeater1.DataSource = users;  // תוודא שהשימוש הוא בשם נכון של הרפיטר
            Repeater1.DataBind();
        }
    }
}