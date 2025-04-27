using BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.IO;
using MongoDB.Driver;


namespace DATA
{
    public class DbContext : IDisposable
    {
        public string ConnStr { get; set; } // מחרוזת ההתחברות לבסיס הנתונים
        public SqlConnection Conn { get; set; } // אובייקט החיבור לבסיס הנתונים

        public DbContext()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString; // שליפת מחרוזת ההתחברות מקובץ ההגדרות
            Conn = new SqlConnection(); // יצירת חיבור חדש
            Conn.ConnectionString = ConnStr; // קביעת מחרוזת ההתחברות
            Conn.Open(); // פתיחת החיבור לבסיס הנתונים
        }
        public DbContext(string ConnStr)
        {
            this.ConnStr = ConnStr; // שמירת מחרוזת ההתחברות
            Conn = new SqlConnection(ConnStr); // יצירת חיבור חדש עם המחרוזת שקיבלנו
            Conn.Open(); // פתיחת החיבור

        }

        //  פעולה שתסגור את החיבור
        public void Dispose()
        {
            if (Conn != null && Conn.State != ConnectionState.Closed) // אם החיבור פתוח
            {
                Conn.Close(); // סגור את החיבור
                Conn.Dispose(); // שחרור משאבים מהזיכרון
            }
        }

        

        public int ExecuteNonQuery(String Sql)//שאילתות שלא מחזירות ערך כמו עדכון מחיקה אינסרט
        {
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת פקודת SQL
            {
                Console.WriteLine(Sql); // הדפסת השאילתה ל-Console (לצורכי בדיקה)
                return Cmd.ExecuteNonQuery(); // ביצוע הפקודה והחזרת מספר הרשומות שהושפעו
            }
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //Cmd.Connection = Conn;   //הגדרת הצינור בו ישתמש אובייקט הפקודה 
            //Console.WriteLine(Sql);

            //Cmd.CommandText = Sql;// הגדרת השאילתה אותה עלינו לבצע 

            //int RetVal = Cmd.ExecuteNonQuery();//פונקציה משמשת שאילתה שלא מושכונות נתונים כמו הוספה עדכון מחיקה 
            //Cmd.Dispose();//משמש לשחרור הזיכרון
            //return RetVal;//החזרת מספר הרשומות שהושפעו מהשאילתה 
        }
        public int GetMaxId(string TableName, string PrimaryKeyName)//מזינים שם טבלה ומפתח ראשי וזה נותן את הID הגדול
        {
            string Sql = $"SELECT MAX({PrimaryKeyName}) FROM {TableName}"; // שאילתה למציאת ה-ID המקסימלי
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת הפקודה
            {
                object result = Cmd.ExecuteScalar(); // קבלת התוצאה
                return result != DBNull.Value ? Convert.ToInt32(result) : -1; // החזרת התוצאה או -1 אם לא קיים
            }
            //int MaxId = -1;
            //string Sql = $"SELECT MAX( {PrimaryKeyName}) FROM {TableName} ";
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //MaxId = (int)Cmd.ExecuteScalar();
            //Cmd.Dispose();
            ////   Close();
            //return MaxId;
        }
        public DataTable ExecuteWithParams(string Sql, List<SqlParameter> Params)
        {
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת פקודת SQL
            {
                for (int i = 0; i < Params.Count; i++) // מעבר על כל הפרמטרים
                {
                    Cmd.Parameters.Add(Params[i]); // הוספת כל פרמטר לפקודה
                }

                using (SqlDataAdapter Da = new SqlDataAdapter(Cmd)) // יצירת מתאם נתונים
                {
                    DataTable Dt = new DataTable(); // יצירת טבלת תוצאות
                    Da.Fill(Dt); // מילוי הטבלה בתוצאות מהמסד
                    return Dt; // החזרת הטבלה
                }
            }
            //DataTable Dt = new DataTable();
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //for (int i = 0; i < Params.Count; i++)
            //{
            //    Cmd.Parameters.Add(Params[i]);
            //}
            //SqlDataAdapter Da = new SqlDataAdapter(Cmd);
            //Da.Fill(Dt);
            //Cmd.Dispose();
            ////  Close();
            //return Dt;
        }
        public int ExecuteNonQueryWithParams(string Sql, List<SqlParameter> Params)
        {
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת הפקודה
            {
                for (int i = 0; i < Params.Count; i++) // מעבר על כל הפרמטרים
                {
                    Cmd.Parameters.Add(Params[i]); // הוספת הפרמטרים
                }
                return Cmd.ExecuteNonQuery(); // ביצוע הפקודה
            }
            //int RecCount = 0;
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //for (int i = 0; i < Params.Count; i++)
            //{
            //    Cmd.Parameters.Add(Params[i]);
            //}
            //RecCount = Cmd.ExecuteNonQuery();
            //Cmd.Dispose();
            ////  Close();
            //return RecCount;
        }
        public string GetValueByKey(string TableName, string KeyName, string ValueName, string KeyValue)
        {
            string Sql = $"SELECT TOP 1 {ValueName} FROM {TableName} WHERE {KeyName}='{KeyValue}'"; // בניית שאילתה
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת פקודה
            {
                object result = Cmd.ExecuteScalar(); // ביצוע הפקודה והחזרת ערך
                return result?.ToString(); // החזרת הערך כמחרוזת או null אם ריק
            }
            //string RetValue = null;
            //string Sql = $"SELECT top 1 {ValueName} FROM {TableName} where {KeyName}='{KeyValue}'  ";
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //RetValue = (string)(Cmd.ExecuteScalar() + "");
            //Cmd.Dispose();
            ////   Close();
            //return RetValue;
        }

        public static List<SqlParameter> CreateParameters(object parametersObject)
        {
            var parameters = new List<SqlParameter>(); // יצירת הרשימה

            PropertyInfo[] props = parametersObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance); // שליפת כל הפרופרטיז באובייקט
            for (int i = 0; i < props.Length; i++) // מעבר עם לולאת for רגילה
            {
                parameters.Add(new SqlParameter($"@{props[i].Name}", props[i].GetValue(parametersObject, null))); // הוספת פרמטר לרשימה
            }

            return parameters; // החזרת הרשימה
        }

        public object ExecuteScalar(string Sql)
        { 
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn)) // יצירת הפקודה
            {
                return Cmd.ExecuteScalar(); // ביצוע והחזרת התוצאה
            }

        
        //    SqlCommand Cmd = new SqlCommand();
        //    Cmd.Connection = Conn;   //הגדרת הצינור בו ישתמש אובייקט הפקודה 
        //    Cmd.CommandText = Sql;// הגדרת השאילתה אותה עלינו לבצע 
        //    object RetVal = Cmd.ExecuteScalar();//פונקציה משמשת שאילתה שלא מושכונות נתונים כמו הוספה עדכון מחיקה 
        //    Cmd.Dispose();//משמש לשחרור הזיכרון
        //    return RetVal;//החזרת מספר הרשומות שהושפעו מהשאילתה
        }
        public DataTable Execute(String Sql)//פונקציה זו תשמש לשליפה של הנתונים
        {
            using (SqlCommand Cmd = new SqlCommand(Sql, Conn))// יצירת פקודת SQL
            {
                using (SqlDataAdapter Da = new SqlDataAdapter(Cmd))// יצירת מתאם נתונים
                {

                    DataTable Dt = new DataTable(); // יצירת אובייקט טבלת נתונים
                    Da.Fill(Dt); // מילוי הטבלה
                    return Dt; // החזרת התוצאה
                }
            }
            //SqlCommand Cmd = new SqlCommand(Sql, Conn);
            //Cmd.Connection = Conn;   //הגדרת הצינור בו ישתמש אובייקט הפקודה 
            //Cmd.CommandText = Sql;// הגדרת השאילתה אותה עלינו לבצע 
            //DataTable Dt = new DataTable();//יצירת אובייקט מסוג טבלת נתונים
            //SqlDataAdapter Da = new SqlDataAdapter(Cmd);//הגדרת אובייקט מסוג מתאם נתונים
            //Da.SelectCommand = Cmd;//הגדרת תותח השאילתות אותו יתפעל המתאם
            //Da.Fill(Dt);//מילוי טבלת הנתונים בתוצאות שחזרו מהשאילתה
            //Cmd.Dispose();
            //return Dt;//החזרת טבלת הנתונים שחזרה מהשאילתה 
        }
    }
}