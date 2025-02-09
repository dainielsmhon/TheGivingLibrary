using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace Library.LibraryAdmin
{
    public partial class AddBorrow : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // טוען את הדף
            }
        }

        protected void BtnCheckBook_Click(object sender, EventArgs e)
        {
            int bookId;
            if (!int.TryParse(TxtBookId.Text, out bookId))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
                return;
            }

            if (Book.IsBookAvailable(bookId))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('הספר קיים במערכת וניתן להשאלה.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('הספר לא קיים במערכת או שאין עותקים זמינים. נא ללכת לדף הוספת ספר.');", true);
            }
        }

        protected void BtnBorrow_Click(object sender, EventArgs e)
        {
            int bookId;
            if (!int.TryParse(TxtBookId.Text, out bookId))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
                return;
            }

            int result = Book.Borrow(bookId);
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('השאלת הספר בוצעה בהצלחה.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('לא ניתן להשאיל את הספר. בדוק את המלאי.');", true);
            }
        }

        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            int bookId;
            if (!int.TryParse(TxtBookId.Text, out bookId))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('נא להזין מספר מזהה תקין.');", true);
                return;
            }

            int result = Book.Return(bookId);
            if (result > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('החזרת הספר בוצעה בהצלחה.');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('לא ניתן להחזיר את הספר. בדוק את המערכת.');", true);
            }
        }
    }
}