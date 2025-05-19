using System;
using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryUser
{
    public partial class ListBook : System.Web.UI.Page
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
                var books = Book.Get(); // טוען את כל הספרים
                Repeater1.DataSource = books;
                Repeater1.DataBind();
            }
        }
    
}