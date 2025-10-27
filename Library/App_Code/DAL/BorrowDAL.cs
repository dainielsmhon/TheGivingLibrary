using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class BorrowDAL
    {
        // ---------------------------------------------
        // שליפת השאלה אחת לפי מזהה BorrowId
        // ---------------------------------------------
        public static Borrow GetById(int id)
        {
            Borrow Tmp = null; // ניצור אובייקט ריק של Borrow שימולא בהמשך

            using (DbContext Db = new DbContext()) // יצירת חיבור למסד הנתונים (ייסגר אוטומטית בסוף)
            {
                string Sql = $"SELECT * FROM T_Borrow WHERE BorrowId={id}"; // בניית שאילתה לשליפת רשומה בודדת
                DataTable Dt = Db.Execute(Sql); // הפעלת השאילתה והחזרת תוצאה כטבלת נתונים

                // אם נמצאה לפחות רשומה אחת
                if (Dt.Rows.Count > 0)
                {
                    // ניצור אובייקט Borrow עם הנתונים שחזרו מהמסד
                    Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[0]["BorrowId"] + ""), // מזהה ההשאלה
                        BookId = int.Parse(Dt.Rows[0]["BookId"] + ""), // מזהה הספר
                        BookName = Dt.Rows[0]["BookName"] + "", // שם הספר
                        UserId = int.Parse(Dt.Rows[0]["UserId"] + ""), // מזהה המשתמש
                        BorrowDate = DateTime.Parse(Dt.Rows[0]["BorrowDate"] + ""), // תאריך ההשאלה
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[0]["ReturnDatePlan"] + ""), // תאריך החזרה מתוכנן
                        ActualReturnDate = DateTime.Parse(Dt.Rows[0]["ActualReturnDate"] + ""), // תאריך החזרה בפועל
                        Status = int.Parse(Dt.Rows[0]["Status"] + ""), // מצב ההשאלה (0=מושאל, 1=הוחזר)
                        Notse = Dt.Rows[0]["Notse"] + "" // הערות
                    };
                }
            }

            return Tmp; // החזרת האובייקט שמולא (או null אם לא נמצאה רשומה)
        }

        // ---------------------------------------------
        // שליפת כל ההשאלות של משתמש מסוים לפי מזהה המשתמש
        // ---------------------------------------------
        public static List<Borrow> GetByUser(int userId)
        {
            List<Borrow> LstTmp = new List<Borrow>(); // יצירת רשימה ריקה של אובייקטים מסוג Borrow

            using (DbContext db = new DbContext()) // פתיחת חיבור למסד הנתונים
            {
                // ✅ השאילתה הנכונה לפי שם הטבלה שלך (T_Borrow)
                string sql = $"SELECT * FROM T_Borrow WHERE UserId = {userId}"; // מחזירה את כל ההשאלות של המשתמש
                DataTable Dt = db.Execute(sql); // שליפת הנתונים כטבלה

                // מעבר על כל הרשומות בטבלה
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Borrow Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[i]["BorrowId"] + ""),
                        BookId = int.Parse(Dt.Rows[i]["BookId"] + ""),
                        BookName = Dt.Rows[i]["BookName"] + "",
                        UserId = int.Parse(Dt.Rows[i]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[i]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[i]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[i]["ActualReturnDate"] + ""),
                        Status = int.Parse(Dt.Rows[i]["Status"] + ""),
                        Notse = Dt.Rows[i]["Notse"] + ""
                    };

                    LstTmp.Add(Tmp); // הוספת ההשאלה הנוכחית לרשימה
                }
            }

            return LstTmp; // החזרת כל ההשאלות של המשתמש
        }

        // ---------------------------------------------
        // שליפת כל ההשאלות במערכת (לשימוש מנהל)
        // ---------------------------------------------
        public static List<Borrow> Get()
        {
            List<Borrow> LstTmp = new List<Borrow>(); // רשימה ריקה שתכיל את כל ההשאלות

            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש
            {
                string Sql = $"SELECT * FROM T_Borrow ORDER BY Status DESC, BorrowId DESC";
                // מציג קודם את ההשאלות המוחזרות בסוף והרשומות החדשות למעלה

                DataTable Dt = Db.Execute(Sql); // שליפת הנתונים

                // לולאה שממירה כל שורה בטבלה לאובייקט Borrow
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Borrow Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[i]["BorrowId"] + ""),
                        BookId = int.Parse(Dt.Rows[i]["BookId"] + ""),
                        BookName = Dt.Rows[i]["BookName"] + "",
                        UserId = int.Parse(Dt.Rows[i]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[i]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[i]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[i]["ActualReturnDate"] + ""),
                        Status = int.Parse(Dt.Rows[i]["Status"] + ""),
                        Notse = Dt.Rows[i]["Notse"] + ""
                    };

                    LstTmp.Add(Tmp); // מוסיף כל רשומה לרשימה הסופית
                }
            }

            return LstTmp; // מחזיר את הרשימה עם כל ההשאלות במערכת
        }

        // ---------------------------------------------
        // מחיקת השאלה לפי מזהה BorrowId
        // ---------------------------------------------
        public static int Delete(int id)
        {
            int RecCount = 0; // מונה כמה רשומות נמחקו

            using (DbContext Db = new DbContext()) // פתיחת חיבור
            {
                string Sql = $"DELETE FROM T_Borrow WHERE BorrowId={id}"; // בניית שאילתה למחיקה
                RecCount = Db.ExecuteNonQuery(Sql); // ביצוע ומחיקת הרשומה
            }

            return RecCount; // החזרת מספר הרשומות שנמחקו (אמור להיות 1)
        }

        // ---------------------------------------------
        // שמירת השאלה (הוספה חדשה או עדכון קיימת)
        // ---------------------------------------------
        public static int Save(Borrow Tmp)
        {
            int RecCount = 0; // מונה את מספר הרשומות שהושפעו

            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש
            {
                string Sql = ""; // נשתמש בו כדי לבנות את השאילתה

                // אם BorrowId == -1 → זו רשומה חדשה
                if (Tmp.BorrowId == -1)
                {
                    // שאילתת INSERT להוספת השאלה החדשה
                    Sql = "INSERT INTO T_Borrow (BookId, BookName, UserId, BorrowDate, ReturnDatePlan, ActualReturnDate, Status, Notse) VALUES ";
                    Sql += $"({Tmp.BookId}, N'{Tmp.BookName}', {Tmp.UserId}, '{Tmp.BorrowDate:yyyy-MM-dd}', '{Tmp.ReturnDatePlan:yyyy-MM-dd}', '{Tmp.ActualReturnDate:yyyy-MM-dd}', {Tmp.Status}, N'{Tmp.Notse}')";
                }
                else
                {
                    // שאילתת UPDATE לעדכון השאלה הקיימת
                    Sql = "UPDATE T_Borrow SET ";
                    Sql += $"BookId={Tmp.BookId}, ";
                    Sql += $"BookName=N'{Tmp.BookName}', ";
                    Sql += $"UserId={Tmp.UserId}, ";
                    Sql += $"BorrowDate='{Tmp.BorrowDate:yyyy-MM-dd}', ";
                    Sql += $"ReturnDatePlan='{Tmp.ReturnDatePlan:yyyy-MM-dd}', ";
                    Sql += $"ActualReturnDate='{Tmp.ActualReturnDate:yyyy-MM-dd}', ";
                    Sql += $"Status={Tmp.Status}, ";
                    Sql += $"Notse=N'{Tmp.Notse}' ";
                    Sql += $"WHERE BorrowId={Tmp.BorrowId}";
                }

                // ביצוע השאילתה (הוספה או עדכון)
                RecCount = Db.ExecuteNonQuery(Sql);

                // אם נוספה רשומה חדשה – נקבל את המזהה החדש שלה
                if (Tmp.BorrowId == -1)
                {
                    Tmp.BorrowId = Db.GetMaxId("T_Borrow", "BorrowId"); // החזרת ה-ID החדש שנוצר
                }
            }

            return RecCount; // החזרת מספר הרשומות שהושפעו (אמור להיות 1)
        }
    }
}
