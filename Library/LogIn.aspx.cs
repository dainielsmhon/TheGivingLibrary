using System;
using System.Web.UI;
using BLL;

namespace Library
{
    public partial class Login : Page
    {
        // פעולה זו נטענת בעת פתיחת הדף בפעם הראשונה
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlError.Visible = false;      // מסתיר את הודעת השגיאה בהתחלה
                pnlPassword.Visible = false;   // מסתיר את שדה הסיסמה בהתחלה
            }
        }

        // שלב 1 – בדיקת כתובת המייל
        protected void btnEmailCheck_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // בודק אם קיים מייל כזה במערכת
            int userId = BLL.User.CheckUserByEmail(email);

            if (userId != -1)
            {
                // נמצא משתמש קיים
                ViewState["UserId"] = userId;  // שומר את המספר שלו זמנית
                pnlError.Visible = false;
                pnlPassword.Visible = true;    // מציג את שדה הסיסמה
            }
            else
            {
                // לא נמצא משתמש כזה
                pnlError.Visible = true;
                lblError.Text = "המייל לא קיים במערכת. אנא צור משתמש חדש.";
                pnlPassword.Visible = false;

                // מציע מעבר להרשמה אוטומטית
                ScriptManager.RegisterStartupScript(this, GetType(), "redirectToRegister",
                    "setTimeout(function(){ window.location='Register.aspx'; }, 2500);", true);
            }
        }

        // שלב 2 – בדיקת סיסמה והתחברות
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ViewState["UserId"] == null)
                return; // אם אין משתמש שמור, לא נמשיך

            int userId = (int)ViewState["UserId"];
            User u = BLL.User.GetById(userId); // שליפה לפי מזהה

            if (u != null && u.UserPass == txtPassword.Text.Trim())
            {
                // סיסמה נכונה → שמירה מלאה של כל הנתונים ב־Session
                Session["User"] = u;
                Session["UserId"] = u.UserId;
                Session["UserName"] = u.Name;
                Session["IsAdmin"] = u.IsAdmin;

                // ניתוב לפי סוג המשתמש
                if (u.IsAdmin)
                    Response.Redirect("LibraryAdmin/Default.aspx"); // דף מנהל
                else
                    Response.Redirect("LibraryUser/Default.aspx");  // דף משתמש רגיל
            }
            else
            {
                // סיסמה שגויה
                pnlError.Visible = true;
                lblError.Text = "סיסמה שגויה. נסה שוב.";
            }
        }
    }
}
