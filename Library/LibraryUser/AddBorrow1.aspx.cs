using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;




namespace Library.LibraryAdmin
{
    public partial class AddBorrow : Page
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
            string BookId = Request["BookId"] + "";
            string UserId = Request["UserId"] + "";

            Book Tmp = null;
            User Tmp2 = null;

            if (!string.IsNullOrEmpty(BookId) && BookId != "-1")
            {
                Tmp = BLL.Book.GetById(int.Parse(BookId));
                if (Tmp != null)
                {
                    HidBookId.Value = BookId;
                    LblBookName.InnerHtml = Tmp.BookName;
                }
            }

            if (!string.IsNullOrEmpty(UserId) && UserId != "-1")
            {
                Tmp2 = BLL.User.GetById(int.Parse(UserId));

                if (Tmp2 != null && Tmp != null)
                {
                    // אם יש גם ספר וגם משתמש => מבצע השאלה
                    BLL.Borrow Tmp3 = new Borrow()
                    {
                        BorrowId = -1,
                        BookId = Tmp.BookId,
                        BookName = Tmp.BookName,
                        UserId = Tmp2.UserId,
                        BorrowDate = DateTime.Now,
                        ReturnDatePlan = DateTime.Now.AddDays(14),
                        ActualReturnDate = DateTime.Now.AddDays(365),
                        Status = 0,
                        Notse = ""
                    };

                    Tmp3.Save();
                    Book.Borrow(Tmp.BookId); // מעדכן מלאי
                    Response.Redirect("ListBorrow.aspx");
                    return;
                }
            }

            // תמיד טוען את רשימת המשתמשים
            var users = BLL.User.Get();
            Repeater1.DataSource = users;
            Repeater1.DataBind();
        }






    }
}