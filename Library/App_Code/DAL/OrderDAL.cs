using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class OrderDAL
    {
        // פונקציה לקבל את כל ההזמנות
        public static List<Order> Get()
        {
            List<Order> orders = new List<Order>();
            DbContext Db = new DbContext();
            string Sql = "SELECT * FROM T_Orders";  // שאילתה לשלוף את כל ההזמנות
            DataTable Dt = Db.Execute(Sql);

            foreach (DataRow row in Dt.Rows)
            {
                Order order = new Order()
                {
                    OrderId = int.Parse(row["OrderId"].ToString()),
                    SupplierId = int.Parse(row["SupplierId"].ToString()),
                    BookId = int.Parse(row["BookId"].ToString()),
                    Quantity = int.Parse(row["Quantity"].ToString()),
                    OrderDate = DateTime.Parse(row["OrderDate"].ToString())
                };
                orders.Add(order);
            }

            return orders;
        }

        // פונקציה לשמור הזמנה חדשה או לעדכן קיימת
        public static int Save(Order order)
        {
            DbContext Db = new DbContext();
            string Sql = "";

            if (order.OrderId == 0)  // אם אין OrderId, אנחנו מוסיפים הזמנה חדשה
            {
                Sql = "INSERT INTO T_Orders (SupplierId, BookId, Quantity, OrderDate) VALUES ";
                Sql += $"({order.SupplierId}, {order.BookId}, {order.Quantity}, '{order.OrderDate:yyyy-MM-dd}')";
            }
            else  // אם יש OrderId, אנחנו מעדכנים את ההזמנה
            {
                Sql = $"UPDATE T_Orders SET SupplierId = {order.SupplierId}, BookId = {order.BookId}, Quantity = {order.Quantity}, OrderDate = '{order.OrderDate:yyyy-MM-dd}' WHERE OrderId = {order.OrderId}";
            }

            return Db.ExecuteNonQuery(Sql);  // מבצע את השאילתה
        }

        // פונקציה למחוק הזמנה
        public static int Delete(int orderId)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Orders WHERE OrderId = {orderId}";
            return Db.ExecuteNonQuery(Sql);  // מבצע את השאילתה למחיקת ההזמנה
        }

        // פונקציה לקבלת הזמנה לפי מזהה
        public static Order GetById(int orderId)
        {
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM T_Orders WHERE OrderId = {orderId}";
            DataTable Dt = Db.Execute(Sql);

            if (Dt.Rows.Count > 0)
            {
                DataRow row = Dt.Rows[0];
                return new Order()
                {
                    OrderId = int.Parse(row["OrderId"].ToString()),
                    SupplierId = int.Parse(row["SupplierId"].ToString()),
                    BookId = int.Parse(row["BookId"].ToString()),
                    Quantity = int.Parse(row["Quantity"].ToString()),
                    OrderDate = DateTime.Parse(row["OrderDate"].ToString())
                };
            }

            return null;  // אם לא נמצאה הזמנה
        }
    }
}
