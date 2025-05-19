using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BLL;

namespace Library
{
    public partial class Login : Page
    {
        // פעולה זו מתבצעת כאשר הדף נטען (פעם ראשונה)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;        // מסתיר את הודעת השגיאה בהתחלה
                pnlPassword.Visible = false;     // מסתיר את שורת הסיסמה בהתחלה
            }
        }

        // שלב 1 – לחיצה על כפתור "המשך" לבדיקה אם המייל קיים במערכת
        protected void btnEmailCheck_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            List<BLL.User> users = BLL.User.Get(); // שליפת כל המשתמשים מהדאטה
            User u = users.FirstOrDefault(x => x.Email == email); // חיפוש משתמש עם המייל שהוזן

            if (u != null)
            {
                ViewState["UserId"] = u.UserId;  // שומר את מזהה המשתמש ב-ViewState
                pnlError.Visible = false;        // מסתיר את הודעת השגיאה אם המייל קיים
                pnlPassword.Visible = true;      // מציג את שדה הסיסמה
            }
            else
            {
                pnlError.Visible = true;         // מציג את הודעת השגיאה אם המייל לא קיים
                pnlPassword.Visible = false;     // מסתיר את שדה הסיסמה
            }
        }

        // שלב 2 – לחיצה על כפתור "התחבר" לאחר הזנת סיסמה
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ViewState["UserId"] == null)
                return; // אם המייל לא נמצא, לא נכנסים להמשך

            int userId = (int)ViewState["UserId"];
            BLL.User u = BLL.User.GetById(userId); // שליפת המשתמש לפי מזהה

            if (u != null && u.UserPass == txtPassword.Text.Trim()) // אם הסיסמה נכונה
            {
                Session["User"] = u; // שומר את המשתמש ב-session
                if (u.IsAdmin)
                    Response.Redirect("LibraryAdmin/Default.aspx"); // אם המנהל נכנס
                else
                    Response.Redirect("User/Default.aspx"); // אם משתמש רגיל נכנס
            }
            else
            {
                lblError.Text = "סיסמה שגויה. נסה שוב."; // הודעת שגיאה אם הסיסמה לא נכונה
                pnlError.Visible = true;
            }
        }
    }
}
