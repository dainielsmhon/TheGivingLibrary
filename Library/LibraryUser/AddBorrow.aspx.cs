
using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;

namespace Library.LibraryUser
{
    public partial class AddBorrow : Page
    {
        // פעולה שמופעלת כאשר הדף נטען
        protected void Page_Load(object sender, EventArgs e)
        {
            // מתבצעת רק בטעינה הראשונה (לא בכל רענון)
            if (!IsPostBack)
            {
                FillData(); // קריאה לפונקציה שטוענת נתונים ומבצעת את ההשאלה
            }
        }

        // פונקציה שטוענת את הנתונים הדרושים ומבצעת את פעולת ההשאלה
        private void FillData()
        {
            string BookId = Request["BookId"] + ""; // קבלת מזהה הספר מה-URL (לדוגמה ?BookId=101)

            // בדיקה האם יש משתמש מחובר
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx"); // אם אין – מעביר לדף התחברות
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]); // שליפת מזהה המשתמש המחובר מתוך ה-Session

            Book TmpBook = null; // משתנה שיאחסן את הספר הנבחר
            User TmpUser = BLL.User.GetById(userId); // שליפת פרטי המשתמש המחובר מתוך ה-DB

            // שליפת פרטי הספר לפי BookId
            if (!string.IsNullOrEmpty(BookId) && BookId != "-1")
            {
                TmpBook = BLL.Book.GetById(int.Parse(BookId)); // שליפת פרטי הספר מה-DB
                if (TmpBook != null)
                {
                    HidBookId.Value = BookId; // שומר את מזהה הספר בשדה חבוי בדף
                    LblBookName.InnerHtml = TmpBook.BookName; // מציג את שם הספר על המסך
                }
            }

            // אם יש גם ספר וגם משתמש => מבצע השאלה בפועל
            if (TmpBook != null && TmpUser != null)
            {
                Borrow TmpBorrow = new Borrow() // יצירת אובייקט חדש של השאלה
                {
                    BorrowId = -1, // מזהה חדש (טרם נשמר)
                    BookId = TmpBook.BookId, // מזהה הספר
                    BookName = TmpBook.BookName, // שם הספר
                    UserId = TmpUser.UserId, // מזהה המשתמש
                    BorrowDate = DateTime.Now, // תאריך ההשאלה
                    ReturnDatePlan = DateTime.Now.AddDays(14), // תאריך החזרה צפוי
                    ActualReturnDate = DateTime.Now.AddDays(365), // עתידי – יעודכן בהחזרה
                    Status = 0, // סטטוס 0 = מושאל
                    Notse = "" // אין הערות
                };

                TmpBorrow.Save(); // שמירה בבסיס הנתונים (דרך BorrowDAL)
                Book.Borrow(TmpBook.BookId); // עדכון מלאי הספר – מוריד עותק אחד
                Response.Redirect("ListBorrow.aspx"); // חזרה לרשימת ההשאלות
                return;
            }

            // במידה ואין ספר או משתמש – מציג רק את המשתמש המחובר
            var users = new List<User> { TmpUser };
            Repeater1.DataSource = users;
            Repeater1.DataBind();
        }
    }
}