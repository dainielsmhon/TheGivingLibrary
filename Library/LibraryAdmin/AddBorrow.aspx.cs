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
            string BookId;


            BookId = Request["BookId"] + "";


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
            HidBookId.Value = BookId;// שמירת שם משתמש  לעריכה או הוספה בשדה הנסתר
            //נמלא את כל הטופס בנתונים הראשים שלו

            if (Tmp != null)//אנחנו במצב עריכה של משתמש לכן יש  למלא את הפרטים
            {
                Tmp.BookId = Tmp.BookId;
                LblBookName.InnerHtml = Tmp.BookName;
                

            }

            var users = BLL.User.Get(); // טוען את כל המשתמשים
            Repeater1.DataSource = users;  // תוודא שהשימוש הוא בשם נכון של הרפיטר
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