using BLL;
using DAL;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class ListSupplier : Page
    {
       
        
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    FillData();  // טוען את רשימת ספקים
                }
            }



            private void FillData()
            {
                var suppliers = Supplier.Get(); // טוען את כל הספקים
                Repeater1.DataSource = suppliers;  // תוודא שהשימוש הוא בשם נכון של הרפיטר
                Repeater1.DataBind();
            }
        
    }
}
