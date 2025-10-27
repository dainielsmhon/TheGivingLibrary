
using BLL;
using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library.LibraryUser
{
    public partial class AddReturn : Page
    {
        // פעולה שמופעלת כאשר הדף נטען
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // טעינה ראשונית בלבד
            {
                FillData(); // קריאה לפונקציה שמבצעת את ההחזרה בפועל
            }
        }

        // פונקציה שמבצעת את פעולת ההחזרה
        private void FillData()
        {
            string BorrowId = Request["BorrowId"] + ""; // שליפת מזהה ההשאלה מה-URL

            // בדיקה האם המשתמש מחובר
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]); // מזהה המשתמש המחובר
            bool isAdmin = false;

            // בדיקה אם המשתמש הוא מנהל (אם קיים Session מתאים)
            if (Session["IsAdmin"] != null)
                isAdmin = Convert.ToBoolean(Session["IsAdmin"]);

            // אם לא נשלח מזהה BorrowId, חוזר לרשימת ההשאלות
            if (string.IsNullOrEmpty(BorrowId))
            {
                Response.Redirect("ListBorrow.aspx");
                return;
            }

            Borrow Tmp = BLL.Borrow.GetById(int.Parse(BorrowId)); // שליפת ההשאלה מה-DB לפי מזהה

            // אם לא נמצאה השאלה – חזרה לרשימה
            if (Tmp == null)
            {
                Response.Redirect("ListBorrow.aspx");
                return;
            }

            // אם זה משתמש רגיל – הוא יכול להחזיר רק את הספרים שלו
            if (!isAdmin && Tmp.UserId != userId)
            {
                Response.Redirect("ListBorrow.aspx");
                return;
            }

            // אם הספר עדיין מושאל (Status = 0) => מבצע החזרה בפועל
            if (Tmp.Status == 0)
            {
                Tmp.Status = 1; // שינוי הסטטוס להוחזר
                Tmp.ActualReturnDate = DateTime.Now; // קביעת תאריך ההחזרה בפועל
                Tmp.Save(); // שמירה בבסיס הנתונים
                Book.Return(Tmp.BookId); // עדכון מלאי הספר במלאי הכללי
            }

            Response.Redirect("ListBorrow.aspx"); // חזרה לרשימת ההשאלות
        }
    }
}