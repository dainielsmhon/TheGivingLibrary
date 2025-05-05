using System;
using System.Collections.Generic;
using System.Web.UI;
using BLL; // שימוש במחלקות Book ו-Borrow

namespace Library.LibraryAdmin
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPopularBooks();         // שליפת 10 הספרים הכי פופולריים
                LoadStatistics();           // שליפת סטטיסטיקות להצגה בעוגות
                lblWelcome.Text = "ברוך הבא למערכת הספרייה!";
            }
        }

        // טעינת ספרים לרפיטר הראשי
        private void LoadPopularBooks()
        {
            var popularBooks = GlobFunc.GetTop10Books();       // שליפת ספרים הכי מושאלים
            rptPopularBooks.DataSource = popularBooks;         // חיבור לרפיטר
            rptPopularBooks.DataBind();                        // רענון התצוגה
        }

        // טעינת ערכים לעוגות הסטטיסטיקה
        private void LoadStatistics()
        {
            int totalBooks = GlobFunc.GetTotalBooks();                         // סך כל הספרים
            int totalBorrowed = GlobFunc.GetTotalBorrowedBooks();             // סך כל המושאלים
            int totalAvailable = totalBooks - totalBorrowed;                  // חישוב זמינים

            // שמירת ערכים ל-JavaScript דרך ViewState
            ViewState["TotalBooks"] = totalBooks;
            ViewState["TotalBorrowed"] = totalBorrowed;
            ViewState["TotalAvailable"] = totalAvailable;

            // --- עוגת TOP 10 ספרים ---
            var topBooks = GlobFunc.GetTop10Books();
            List<string> bookLabels = new List<string>();
            List<int> bookValues = new List<int>();

            foreach (var book in topBooks)
            {
                bookLabels.Add(book.BookName);
                bookValues.Add(book.BorrowedBooks);
            }

            ViewState["TopBooksLabels"] = $"[{string.Join(",", bookLabels.ConvertAll(b => $"'{b}'"))}]";
            ViewState["TopBooksValues"] = $"[{string.Join(",", bookValues)}]";

            // --- עוגת TOP 5 סופרים ---
            var topAuthors = GlobFunc.GetTop5Authors();
            List<string> authorLabels = new List<string>();
            List<int> authorValues = new List<int>();

            foreach (var author in topAuthors)
            {
                authorLabels.Add(author.AuthorName);
                authorValues.Add(author.TotalBorrows);
            }

            ViewState["TopAuthorsLabels"] = $"[{string.Join(",", authorLabels.ConvertAll(a => $"'{a}'"))}]";
            ViewState["TopAuthorsValues"] = $"[{string.Join(",", authorValues)}]";
        }

        // לחיצה על כפתור החיפוש
        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                lblSearchResult.Text = "אנא הזן מונח חיפוש.";
                return;
            }

            var results = GlobFunc.SearchBooks(query);

            if (results.Count > 0)
            {
                lblSearchResult.Text = $"נמצאו {results.Count} תוצאות.";
                rptPopularBooks.DataSource = results; // מציג את תוצאות החיפוש ברפיטר
                rptPopularBooks.DataBind();
            }
            else
            {
                lblSearchResult.Text = "לא נמצאו תוצאות.";
                rptPopularBooks.DataSource = null;
                rptPopularBooks.DataBind();
            }
        }

        // שליחת הודעה מהטופס צור קשר
        protected void btnContact_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                lblContactResult.Text = "אנא מלא את כל השדות.";
                lblContactResult.CssClass = "alert alert-danger mt-3 d-block";
                return;
            }

            bool sent = GlobFunc.SendContactEmail(name, email, message);

            if (sent)
            {
                lblContactResult.Text = "ההודעה נשלחה בהצלחה.";
                lblContactResult.CssClass = "alert alert-success mt-3 d-block";

                txtName.Text = "";
                txtEmail.Text = "";
                txtMessage.Text = "";

                // כאן מוצגת ההודעה הצפה
                ScriptManager.RegisterStartupScript(this, GetType(), "showPush", "showSuccessPush('ההודעה נשלחה בהצלחה ✅');", true);
            }
            else
            {
                lblContactResult.Text = "אירעה שגיאה בשליחת ההודעה.";
                lblContactResult.CssClass = "alert alert-danger mt-3 d-block";
                ScriptManager.RegisterStartupScript(this, GetType(), "showPush", "showErrorPush('שגיאה בשליחת ההודעה ❌');", true);
            }
        }
    }
}
