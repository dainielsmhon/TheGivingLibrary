using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class OrderDAL
    {
        // פונקציה לקבלת הזמנה לפי מזהה
        public static Order GetById(int id)
        {
            Order Tmp = null;
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM T_Orders WHERE OrderId = {id}";
            DataTable Dt = Db.Execute(Sql);

            if (Dt.Rows.Count > 0)
            {
                Tmp = new Order()
                {
                    OrderId = int.Parse(Dt.Rows[0]["OrderId"] + ""),
                    SupplierId = int.Parse(Dt.Rows[0]["SupplierId"] + ""),
                    BookId = int.Parse(Dt.Rows[0]["BookId"] + ""),
                    Quantity = int.Parse(Dt.Rows[0]["Quantity"] + ""),
                    OrderDate = DateTime.Parse(Dt.Rows[0]["OrderDate"] + ""),
                    Status = int.Parse(Dt.Rows[0]["Status"] + "")
                };
            }

            return Tmp; // אם לא מצא כלום, מחזיר null
        }

        // פונקציה לקבל את כל ההזמנות
        public static List<Order> Get()
        {
            List<Order> LstTmp = new List<Order>();
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM T_Orders ORDER BY OrderId DESC";
            DataTable Dt = Db.Execute(Sql);

            for (int i = 0; i < Dt.Rows.Count; i++) // עובר על כל השורות שחזרו
            {
                Order Tmp = new Order()
                {
                    OrderId = int.Parse(Dt.Rows[i]["OrderId"] + ""),
                    SupplierId = int.Parse(Dt.Rows[i]["SupplierId"] + ""),
                    BookId = int.Parse(Dt.Rows[i]["BookId"] + ""),
                    Quantity = int.Parse(Dt.Rows[i]["Quantity"] + ""),
                    OrderDate = DateTime.Parse(Dt.Rows[i]["OrderDate"] + ""),
                    Status = int.Parse(Dt.Rows[i]["Status"] + "")
                };
                LstTmp.Add(Tmp); // מוסיף לרשימה
            }

            return LstTmp;
        }

        // פונקציה למחיקת הזמנה
        public static int Delete(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Orders WHERE OrderId = {id}";
            return Db.ExecuteNonQuery(Sql);
        }

        // פונקציה לשמירה (הוספה/עדכון) של הזמנה
        public static int Save(Order Tmp)
        {
            DbContext Db = new DbContext();
            string Sql = "";
            int RecCount = 0;

            if (Tmp.OrderId == -1) // הוספת הזמנה חדשה
            {
                Sql = $"INSERT INTO T_Orders (SupplierId, BookId, Quantity, OrderDate, Status) VALUES ";
                Sql += $"({Tmp.SupplierId}, {Tmp.BookId}, {Tmp.Quantity}, '{Tmp.OrderDate:yyyy-MM-dd}', {Tmp.Status})";
            }
            else // עדכון הזמנה קיימת
            {
                Sql = $"UPDATE T_Orders SET ";
                Sql += $"SupplierId = {Tmp.SupplierId}, ";
                Sql += $"BookId = {Tmp.BookId}, ";
                Sql += $"Quantity = {Tmp.Quantity}, ";
                Sql += $"OrderDate = '{Tmp.OrderDate:yyyy-MM-dd}', ";
                Sql += $"Status = {Tmp.Status} ";
                Sql += $"WHERE OrderId = {Tmp.OrderId}";
            }

            RecCount = Db.ExecuteNonQuery(Sql);

            if (Tmp.OrderId == -1)
            {
                Tmp.OrderId = Db.GetMaxId("T_Orders", "OrderId");
            }

            return RecCount;
        }

        // פונקציה לעדכון סטטוס ההזמנה
        public static int UpdateStatus(int orderId, int newStatus)
        {
            DbContext Db = new DbContext();
            string Sql = $"UPDATE T_Orders SET Status = {newStatus} WHERE OrderId = {orderId}";
            return Db.ExecuteNonQuery(Sql);
        }
    }
}