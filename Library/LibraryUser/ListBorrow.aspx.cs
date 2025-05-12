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
            var borrows = Borrow.Get(); // טוען את כל ההשאלות

            // הוספתי את המיון לפי סטטוס (מושאל קודם)
            var sortedBorrows = borrows
                .OrderBy(b => b.Status)  // מיון לפי סטטוס (0 - מושאל קודם)
                .ThenBy(b => b.BorrowDate)  // מיון נוסף לפי תאריך השאלה
                .ToList();

            Repeater1.DataSource = sortedBorrows;  // תוודא שהשימוש הוא בשם נכון של הרפיטר
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