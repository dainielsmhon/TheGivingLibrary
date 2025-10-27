using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryUser
{
    // 🔹 מחלקה זו אחראית על הצגת רשימת ההשאלות של המשתמש המחובר
    // כל משתמש רואה אך ורק את ההשאלות שלו, לפי מזהה המשתמש השמור ב-Session
    public partial class ListBorrow : System.Web.UI.Page
    {
        // פעולה זו נטענת בכל פעם שהדף נפתח
        protected void Page_Load(object sender, EventArgs e)
        {
            // נבצע טעינה של הנתונים רק בפעם הראשונה
            // (כדי שלא יתרחש ריענון מיותר בכל postback)
            if (!IsPostBack)
            {
                FillData(); // קריאה לפונקציה שטוענת את רשימת ההשאלות מהמסד
            }
        }

        // -------------------------------------------------------
        // פונקציה שאחראית לשלוף מהמסד רק את ההשאלות של המשתמש המחובר
        // -------------------------------------------------------
        private void FillData()
        {
            // נוודא שקיים משתמש מחובר — אחרת נחזיר אותו לדף התחברות
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            // שליפת מזהה המשתמש מתוך ה-Session
            int userId = Convert.ToInt32(Session["UserId"]);

            // שליפה של כל ההשאלות של המשתמש לפי מזהה
            var borrows = Borrow.GetByUser(userId)
                .OrderBy(b => b.Status)          // מיון לפי סטטוס (מושאל קודם)
                .ThenBy(b => b.BorrowDate)       // מיון משני לפי תאריך השאלה
                .ToList();                       // הפיכת הנתונים לרשימה מלאה

            // חיבור הנתונים לרפיטר שעל הדף
            Repeater1.DataSource = borrows;
            Repeater1.DataBind();
        }

        // -------------------------------------------------------
        // פעולה זו מופעלת כאשר לוחצים על כפתור בתוך ה-Repeater
        // (במקרה שלנו – כפתור "החזרה")
        // -------------------------------------------------------
        protected void rptBorrow_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // נבדוק אם הכפתור שנלחץ הוא מסוג "Return"
            if (e.CommandName == "Return")
            {
                // נקבל את מזהה ההשאלה מתוך ה-CommandArgument של הכפתור
                int borrowId = Convert.ToInt32(e.CommandArgument);

                // הפניה לדף AddReturn.aspx עם מזהה ההשאלה ב-URL
                Response.Redirect($"AddReturn.aspx?BorrowId={borrowId}");
            }
        }
    }
}
