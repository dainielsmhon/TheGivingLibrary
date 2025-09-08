using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryUser
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
            // שליפת מזהה המשתמש והאם הוא מנהל מתוך ה־Session
            int userId = Convert.ToInt32(Session["UserId"]);
            bool isAdmin = Convert.ToBoolean(Session["IsAdmin"]);

            List<Borrow> borrows;

            if (isAdmin)
            {
                // אם המשתמש הוא מנהל – טוען את כל ההשאלות
                borrows = Borrow.Get();
            }
            else
            {
                // אחרת – טוען רק את ההשאלות של המשתמש המחובר
                borrows = Borrow.GetByUser(userId);
            }

            // מיון לפי סטטוס ולאחר מכן לפי תאריך השאלה
            var sortedBorrows = borrows
                .OrderBy(b => b.Status)
                .ThenBy(b => b.BorrowDate)
                .ToList();

            // הצגת הנתונים בטבלה
            Repeater1.DataSource = sortedBorrows;
            Repeater1.DataBind();
        }

        // טיפול בכפתור "החזרה" שמשנה את הסטטוס של הספר להחזרה
        protected void rptBorrow_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Return")
            {
                int borrowId = Convert.ToInt32(e.CommandArgument);
                var borrow = Borrow.GetById(borrowId);

                if (borrow != null)
                {
                    // הגנה כפולה - אל תחזיר ספר פעמיים!
                    if (borrow.Status == 1)
                        return;

                    borrow.Status = 1;  // משנה את הסטטוס להחזרה
                    borrow.Save();

                    var book = Book.GetById(borrow.BookId);
                    if (book != null)
                    {
                        book.AvailableQuantity++;  // עדכון הכמות הזמינה של הספר
                        book.Save();
                    }

                    FillData();  // טוען מחדש את הנתונים
                }
            }
        }
    }
}