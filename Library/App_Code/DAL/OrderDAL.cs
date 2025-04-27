using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class OrderDAL
    {
        // שליפת הזמנה לפי מזהה
        public static Order GetById(int id)
        {
            Order Tmp = null;
            using (DbContext Db = new DbContext())
            {
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
            }
            return Tmp; // אם לא מצא כלום, מחזיר null
        }

        // שליפת כל ההזמנות
        public static List<Order> Get()
        {
            List<Order> LstTmp = new List<Order>();
            using (DbContext Db = new DbContext())
            {
                string Sql = $"SELECT * FROM T_Orders ORDER BY OrderId DESC";
                DataTable Dt = Db.Execute(Sql);
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    Order Tmp = new Order();

                    Tmp.OrderId = int.Parse(Dt.Rows[i]["OrderId"] + "");
                    Tmp.SupplierId = int.Parse(Dt.Rows[i]["SupplierId"] + "");
                    Tmp.BookId = int.Parse(Dt.Rows[i]["BookId"] + "");
                    Tmp.Quantity = int.Parse(Dt.Rows[i]["Quantity"] + "");
                    Tmp.OrderDate = DateTime.Parse(Dt.Rows[i]["OrderDate"] + "");
                    Tmp.Status = int.Parse(Dt.Rows[i]["Status"] + "");

                    Supplier Sup = Supplier.GetById(Tmp.SupplierId);
                    if (Sup != null)
                    {
                        Tmp.SupplierName = Sup.SupplierName;
                    }

                    Book Bk = Book.GetById(Tmp.BookId);
                    if (Bk != null)
                    {
                        Tmp.BookName = Bk.BookName;
                    }

                    LstTmp.Add(Tmp);
                }
            }
            return LstTmp;
        }

        // מחיקת הזמנה לפי מזהה
        public static int Delete(int id)
        {
            using (DbContext Db = new DbContext())
            {
                string Sql = $"DELETE FROM T_Orders WHERE OrderId = {id}";
                return Db.ExecuteNonQuery(Sql);
            }
        }

        // שמירת הזמנה חדשה או עדכון קיימת
        public static int Save(Order Tmp)
        {
            int RecCount = 0;
            using (DbContext Db = new DbContext())
            {
                string Sql = "";
                if (Tmp.OrderId == -1)
                {
                    Sql = $"INSERT INTO T_Orders (SupplierId, BookId, Quantity, OrderDate, Status) VALUES ";
                    Sql += $"({Tmp.SupplierId}, {Tmp.BookId}, {Tmp.Quantity}, '{Tmp.OrderDate:yyyy-MM-dd}', {Tmp.Status})";
                }
                else
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

                // *** שימו לב: לא לעדכן מלאי כאן! ***
                // העדכון של הכמויות מתבצע רק בזמן "קבל" בדף listOrder.aspx.cs
            }

            return RecCount;
        }

        // עדכון סטטוס של הזמנה
        public static int UpdateStatus(int orderId, int newStatus)
        {
            using (DbContext Db = new DbContext())
            {
                string Sql = $"UPDATE T_Orders SET Status = {newStatus} WHERE OrderId = {orderId}";
                return Db.ExecuteNonQuery(Sql);
            }
        }
    }
}
