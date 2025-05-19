using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BLL;
using System.Configuration;
using System.Data.SqlClient;

namespace Library
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            User tmp;

            // שליפת מחרוזת ההתחברות מתוך Web.config
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            // כתיבת שאילתה לשליפת כל המשתמשים מהטבלה
            string Sql = "SELECT * FROM t_Users";

            // יצירת חיבור למסד נתונים
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConnStr; // הצמדת מחרוזת ההתחברות
            Conn.Open(); // פתיחת החיבור

            // יצירת פקודת SQL
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Conn;      // חיבור בין הפקודה לצינור
            Cmd.CommandText = Sql;      // הצמדת השאילתה לפקודה

            // ביצוע הקריאה לנתונים
            SqlDataReader Dr = Cmd.ExecuteReader();

            // יצירת רשימה לשמירת כל המשתמשים
            List<User> LstUsers = new List<User>();

            // לולאה שמוסיפה כל משתמש לרשימה
            while (Dr.Read())
            {
                tmp = new User()
                {
                    UserId = (int)Dr["UserId"],
                    UserName = Dr["UserName"] + "",
                    UserPass = Dr["UserPass"] + "",
                    Email = Dr["Email"] + ""
                };
                LstUsers.Add(tmp);
            }

            Dr.Close(); // סגירת הקורא
            Conn.Close(); // סגירת החיבור

            // שמירת הרשימה בזיכרון של האפליקציה (לגישה מכל הדפים)
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
    }
}