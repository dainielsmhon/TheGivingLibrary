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

            Book Tmp = null;
            User Tmp2 = null;

            string BookId;
            string UserId;


            BookId = Request["BookId"] + "";
            UserId = Request["UserId"] + "";


            if (string.IsNullOrEmpty(BookId))
            {
                BookId = "-1"; //הוספת משתמש חדש
            }
            else
            {
                Tmp = BLL.Book.GetById(int.Parse(BookId));
                if (Tmp == null)
                {
                    BookId = "-1";//הוספת משתמש חדש
                }
            }

            if (string.IsNullOrEmpty(UserId))
            {
                UserId = "-1";
            }
            else
            {
                Tmp2 = BLL.User.GetById(int.Parse(UserId));
                if (Tmp2 == null)
                {

                UserId = "-1"; 
                }

            }



            HidBookId.Value = BookId;
            //HidUserId.Value = UserId;// שמירת שם משתמש  לעריכה או הוספה בשדה הנסתר
            //נמלא את כל הטופס בנתונים הראשים שלו

            if (Tmp != null)//אנחנו במצב עריכה של משתמש לכן יש  למלא את הפרטים
            {

                

                LblBookName.InnerHtml = Tmp.BookName;
                if (Tmp2 != null)
                {
                    BLL.Borrow Tmp3 = new Borrow()
                    {
                        BorrowId = -1,
                        BookId = Tmp.BookId,
                        BookName = Tmp.BookName,
                        UserId = Tmp2.UserId,
                        BorrowDate = DateTime.Now,
                        ReturnDatePlan = DateTime.Now.AddDays(14),
                        ActualReturnDate = DateTime.Now.AddDays(365)


                    };
                    Tmp3.Save();
                    Book.Borrow(Tmp.BookId);
                    Response.Redirect("ListBorrow.aspx");
                }
                //LblUserId.InnerHtml = Tmp2.UserId;





            }

            var users = BLL.User.Get();
            //var Books = BLL.Book.Get();// טוען את כל המשתמשים
            Repeater1.DataSource = users;
            // תוודא שהשימוש הוא בשם נכון של הרפיטר
            Repeater1.DataBind();
        }






        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // טוען את הדף
        //    }
        //}

        //protected void BtnCheckBook_Click(object sender, EventArgs e)
        //{
        //    int bookId;
        //    if (!int.TryParse(TxtBookId.Text, out bookId))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
        //        return;
        //    }

        //    if (Book.IsBookAvailable(bookId))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('הספר קיים במערכת וניתן להשאלה.');", true);
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('הספר לא קיים במערכת או שאין עותקים זמינים. נא ללכת לדף הוספת ספר.');", true);
        //    }
        //}

        //protected void BtnBorrow_Click(object sender, EventArgs e)
        //{
        //    int bookId;
        //    if (!int.TryParse(TxtBookId.Text, out bookId))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
        //        return;
        //    }

        //    int result = Book.Borrow(bookId);
        //    if (result > 0)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('השאלת הספר בוצעה בהצלחה.');", true);
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('לא ניתן להשאיל את הספר. בדוק את המלאי.');", true);
        //    }
        //}

        //protected void BtnReturn_Click(object sender, EventArgs e)
        //{
        //    int bookId;
        //    if (!int.TryParse(TxtBookId.Text, out bookId))
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
        //        return;
        //    }

        //    int result = Book.Return(bookId);
        //    if (result > 0)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('החזרת הספר בוצעה בהצלחה.');", true);
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('לא ניתן להחזיר את הספר. בדוק את המערכת.');", true);
        //    }
        //}
    }
}