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

            public static Borrow GetById(int id)
            {
                Borrow Tmp = null;
                DbContext Db = new DbContext();
                string Sql = $" SELECT * FROM T_Borrow WHERE BorrowId={id}";
                DataTable Dt = Db.Execute(Sql);

                if (Dt.Rows.Count > 0)
                {
                    Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[0]["BorrowId"] + ""),
                        BookId = int.Parse(Dt.Rows[0]["BookId"] + ""),
                        BorrowName = Dt.Rows[0]["BorrowName"] + "",
                        UserId = int.Parse(Dt.Rows[0]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[0]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[0]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[0]["ActualReturnDate"] + ""),
                        Notse = Dt.Rows[0]["Notse"] + "",
                        Status = Dt.Rows[0]["Status"] + "",
                        Added = DateTime.Parse(Dt.Rows[0]["Added"] + ""),
                        TakenDate = DateTime.Parse(Dt.Rows[0]["TakenDate"] + ""),
                        ReturnDate = DateTime.Parse(Dt.Rows[0]["ReturnDate"] + ""),
                    };
                }


                return Tmp;//אם לא מצא כלום מחזיר כלום
            }
            public static List<Borrow> Get()
            {
                List<Borrow> LstTmp = new List<Borrow>();
                DbContext Db = new DbContext();
                string Sql = $" SELECT * FROM T_Borrow ";
                DataTable Dt = Db.Execute(Sql);
                for (int i = 0; i < Dt.Rows.Count; i++)//עובר על כל השורות שחזרו
                {
                    Borrow Tmp = new Borrow()
                    {
                        BorrowId = int.Parse(Dt.Rows[i]["BorrowId"] + ""),
                        BookId = int.Parse(Dt.Rows[i]["BookId"] + ""),
                        BorrowName = Dt.Rows[i]["BorrowName"] + "",
                        UserId = int.Parse(Dt.Rows[i]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[i]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[i]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[i]["ActualReturnDate"] + ""),
                        Notse = Dt.Rows[i]["Notse"] + "",
                        Status = Dt.Rows[i]["Status"] + "",
                        Added = DateTime.Parse(Dt.Rows[i]["Added"] + ""),
                        TakenDate = DateTime.Parse(Dt.Rows[i]["TakenDate"] + ""),
                        ReturnDate = DateTime.Parse(Dt.Rows[i]["ReturnDate"] + ""),
                    };//מוסיף לרשימה 
                    LstTmp.Add(Tmp);
                }


                return LstTmp;



            }
            public static int Delete(int id)
            {
                Borrow Tmp = null;
                DbContext Db = new DbContext();
                string Sql = $" DELETE FROM T_Borrow WHERE BorrowId={id}";
                int RecCount = 0;
                RecCount = Db.ExecuteNonQuery(Sql);

                return RecCount;
            }
            public static int Save(Borrow Tmp)
            {

                int RecCount = 0;
                DbContext Db = new DbContext();
                string Sql = "";
            if (Tmp.BorrowId == -1)
            {
                Sql = $"INSERT INTO t_Borrow (BookId, BorrowName, UserId, BorrowDate, ReturnDatePlan, ActualReturnDate, Notse, Status, Added, TakenDate, ReturnDate) VALUES ";
                Sql += $" ({Tmp.BookId}, N'{Tmp.BorrowName}', {Tmp.UserId}, '{Tmp.BorrowDate:yyyy-MM-dd}', '{Tmp.ReturnDatePlan:yyyy-MM-dd}', '{Tmp.ActualReturnDate:yyyy-MM-dd}', N'{Tmp.Notse}', N'{Tmp.Status}', '{Tmp.Added:yyyy-MM-dd}', '{Tmp.TakenDate:yyyy-MM-dd}', '{Tmp.ReturnDate:yyyy-MM-dd}')";
            }

            else
            {
                Sql = $"UPDATE T_Borrow SET ";
                Sql += $"BookId={Tmp.BookId}, ";
                Sql += $"BorrowName=N'{Tmp.BorrowName}', ";
                Sql += $"UserId={Tmp.UserId}, ";
                Sql += $"BorrowDate='{Tmp.BorrowDate:yyyy-MM-dd}', ";
                Sql += $"ReturnDatePlan='{Tmp.ReturnDatePlan:yyyy-MM-dd}', ";
                Sql += $"ActualReturnDate='{Tmp.ActualReturnDate:yyyy-MM-dd}', ";
                Sql += $"Notse=N'{Tmp.Notse}', ";  // אם Notse הוא מחרוזת
                Sql += $"Status=N'{Tmp.Status}', ";  // אם Status הוא מחרוזת
                Sql += $"Added='{Tmp.Added:yyyy-MM-dd}', ";
                Sql += $"TakenDate='{Tmp.TakenDate:yyyy-MM-dd}', ";
                Sql += $"ReturnDate='{Tmp.ReturnDate:yyyy-MM-dd}' ";
                Sql += $"WHERE BorrowId={Tmp.BorrowId}";
            }


            RecCount = Db.ExecuteNonQuery(Sql);

                if (Tmp.BorrowId == -1)
                {
                    Tmp.BorrowId = Db.GetMaxId("T_Borrow", "BorrowId");
                }


                return RecCount;
            }
        }

    
}