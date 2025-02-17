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
                    BookName = Dt.Rows[0]["BookName"] + "",
                    UserId = int.Parse(Dt.Rows[0]["UserId"] + ""),
                    BorrowDate = DateTime.Parse(Dt.Rows[0]["BorrowDate"] + ""),
                    ReturnDatePlan = DateTime.Parse(Dt.Rows[0]["ReturnDatePlan"] + ""),
                    ActualReturnDate = DateTime.Parse(Dt.Rows[0]["ActualReturnDate"] + ""),
                    Notse = Dt.Rows[0]["Notse"] + ""

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
                        BookName = Dt.Rows[i]["BookName"] + "",
                        UserId = int.Parse(Dt.Rows[i]["UserId"] + ""),
                        BorrowDate = DateTime.Parse(Dt.Rows[i]["BorrowDate"] + ""),
                        ReturnDatePlan = DateTime.Parse(Dt.Rows[i]["ReturnDatePlan"] + ""),
                        ActualReturnDate = DateTime.Parse(Dt.Rows[i]["ActualReturnDate"] + ""),
                        Notse = Dt.Rows[i]["Notse"] + ""

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
                    Sql = $"INSERT INTO t_Borrow (BookId, BookName, UserId, BorrowDate, ReturnDatePlan, ActualReturnDate, Notse) VALUES ";
                    Sql += $" ({Tmp.BookId}, N'{Tmp.BookName}', {Tmp.UserId}, '{Tmp.BorrowDate:yyyy-MM-dd}', '{Tmp.ReturnDatePlan:yyyy-MM-dd}', '{Tmp.ActualReturnDate:yyyy-MM-dd}', N'{Tmp.Notse}')";
                }

                else
                {
                    Sql = $"UPDATE T_Borrow SET ";
                    Sql += $"BookId={Tmp.BookId}, ";
                    Sql += $"BookName=N'{Tmp.BookName}', ";
                    Sql += $"UserId={Tmp.UserId}, ";
                    Sql += $"BorrowDate='{Tmp.BorrowDate:yyyy-MM-dd}', ";
                    Sql += $"ReturnDatePlan='{Tmp.ReturnDatePlan:yyyy-MM-dd}', ";
                    Sql += $"ActualReturnDate='{Tmp.ActualReturnDate:yyyy-MM-dd}', ";
                    Sql += $"Notse=N'{Tmp.Notse}' ";
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

