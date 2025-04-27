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
        // שליפת שאלה לפי מזהה
        public static Borrow GetById(int id)
        {
            Borrow Tmp = null;
            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש שייסגר אוטומטית
            {
                string Sql = $"SELECT * FROM T_Borrow WHERE BorrowId={id}";
                DataTable Dt = Db.Execute(Sql);

                if (Dt.Rows.Count > 0)
                {
                    Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[0]["BorrowId"] + ""),
                        BookId = int.Parse(Dt.Rows[0]["BookId"] + ""),
                        BookName = Dt.Rows[0]["BookName"] + "",
                        UserId = int.Parse(Dt.Rows[0]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[0]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[0]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[0]["ActualReturnDate"] + ""),
                        Status = int.Parse(Dt.Rows[0]["Status"] + ""),
                        Notse = Dt.Rows[0]["Notse"] + ""
                    };
                }
            }
            return Tmp;
        }

        // שליפת כל השאלות
        public static List<Borrow> Get()
        {
            List<Borrow> LstTmp = new List<Borrow>();
            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש שייסגר אוטומטית
            {
                string Sql = $"SELECT * FROM T_Borrow ORDER BY Status DESC, BorrowId DESC";
                DataTable Dt = Db.Execute(Sql);
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
                    LstTmp.Add(Tmp);
                }
            }
            return LstTmp;
        }

        // מחיקת שאלה לפי מזהה
        public static int Delete(int id)
        {
            int RecCount = 0;
            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש שייסגר אוטומטית
            {
                string Sql = $"DELETE FROM T_Borrow WHERE BorrowId={id}";
                RecCount = Db.ExecuteNonQuery(Sql);
            }
            return RecCount;
        }

        // שמירת שאלה (הוספה או עדכון)
        public static int Save(Borrow Tmp)
        {
            int RecCount = 0;
            using (DbContext Db = new DbContext()) // פתיחת חיבור חדש שייסגר אוטומטית
            {
                string Sql = "";

                if (Tmp.BorrowId == -1)
                {
                    Sql = "INSERT INTO T_Borrow (BookId, BookName, UserId, BorrowDate, ReturnDatePlan, ActualReturnDate, Status, Notse) VALUES ";
                    Sql += $"({Tmp.BookId}, N'{Tmp.BookName}', {Tmp.UserId}, '{Tmp.BorrowDate:yyyy-MM-dd}', '{Tmp.ReturnDatePlan:yyyy-MM-dd}', '{Tmp.ActualReturnDate:yyyy-MM-dd}', {Tmp.Status}, N'{Tmp.Notse}')";
                }
                else
                {
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

                RecCount = Db.ExecuteNonQuery(Sql);

                if (Tmp.BorrowId == -1)
                {
                    Tmp.BorrowId = Db.GetMaxId("T_Borrow", "BorrowId");
                }
            }
            return RecCount;
        }
    }
}
