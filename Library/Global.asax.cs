using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Library
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            User tmp;

            // הגדרת מחרוזת התחברות מתוך קובץ הקונפיג
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            // הגדרת שאילתה לשליפת כל המשתמשים מהטבלה
            string Sql = "SELECT * FROM t_Users";

            // יצירת אובייקט חיבור לבסיס הנתונים
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConnStr; // הצמדת מחרוזת ההתחברות
            Conn.Open(); // פתיחת החיבור

            // יצירת אובייקט לפקודת SQL
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Conn; // הצמדת הפקודה לחיבור
            Cmd.CommandText = Sql; // הגדרת שאילתה לביצוע

            // יצירת קורא נתונים שיבצע את הקריאה בפועל
            SqlDataReader Dr = Cmd.ExecuteReader();

            // רשימה לאחסון כל המשתמשים שנשלפו מהדאטהבייס
            List<User> LstUsers = new List<User>();

            // מעבר על כל שורה שחזרה מהשאילתה
            while (Dr.Read())
            {
                tmp = new User()
                {
                    UserId = (int)Dr["UserId"],
                    UserName = Dr["UserName"] + "",
                    UserPass = Dr["UserPass"] + "",
                    Email = Dr["Email"] + ""
                };
                LstUsers.Add(tmp); // הוספת המשתמש לרשימה
            }

            Dr.Close(); // סגירת הקורא
            Conn.Close(); // סגירת החיבור

            // שמירת הרשימה בזיכרון של האפליקציה (זמין בכל זמן)
            Application["User"] = LstUsers;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

      
            // פונקציה שפועלת כאשר האפליקציה מתחילה (טעינה ראשונית של האתר)
     
    }

}
