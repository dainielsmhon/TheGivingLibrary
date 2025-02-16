using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class ListBorrow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();  // טוען את רשימת הספרים
            }
        }



        private void FillData()
        {
            var borrows= Borrow.Get(); // טוען את כל הספרים
            Repeater1.DataSource = borrows;  // תוודא שהשימוש הוא בשם נכון של הרפיטר
            Repeater1.DataBind();
        }
    }
}