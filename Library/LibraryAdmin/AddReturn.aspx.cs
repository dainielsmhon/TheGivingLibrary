using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class AddReturn : System.Web.UI.Page
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

            Borrow Tmp = null;

            string BorrowId;

            BorrowId = Request["BorrowId"] + "";
            


            if (string.IsNullOrEmpty(BorrowId))
            {
                BorrowId = "-1"; //הוספת משתמש חדש
            }
            else
            {
                Tmp = BLL.Borrow.GetById(int.Parse(BorrowId));
                if (Tmp != null)
                {
                   Tmp.Status = 1;
                    Tmp.Save();
                    Book.Return(Tmp.BookId);
                }
            }





            Response.Redirect("ListBorrow.aspx");
        }
    }
}