using System;
using System.Collections.Generic;
using BLL;
using DAL;

namespace BLL
{
    public class Order
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        // פונקציה לשמור את ההזמנה (הוספה או עדכון)
        public int Save()
        {
            return OrderDAL.Save(this);  // קריאה לפונקציה ב-DAL לשמירה או עדכון
        }

        // פונקציה לקבל הזמנה לפי מזהה
        public static Order GetById(int id)
        {
            return OrderDAL.GetById(id);  // קריאה לפונקציה ב-DAL לקבלת הזמנה לפי מזהה
        }

        // פונקציה לקבל את כל ההזמנות
        public static List<Order> Get()
        {
            return OrderDAL.Get();  // קריאה לפונקציה ב-DAL לקבלת כל ההזמנות
        }

        // פונקציה למחוק הזמנה
        public static int Delete(int id)
        {
            return OrderDAL.Delete(id);  // קריאה לפונקציה ב-DAL למחיקת הזמנה
        }
    }
}
