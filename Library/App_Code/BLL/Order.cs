using System;
using System.Collections.Generic;
using BLL;
using DAL;
using DATA;

namespace BLL
{
    public class Order
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string SupplierName { get; set; }
        public string BookName { get; set; }
        public int UserId { get; set; } 




        public int Save()
        {
            return OrderDAL.Save(this);  
        }

        
        public static Order GetById(int id)
        {
            return OrderDAL.GetById(id);  
        }

        // פונקציה לקבל את כל ההזמנות
        public static List<Order> Get()
        {
            return OrderDAL.Get();  
        }
        public static List<Order> GetAllOrders()
        {
            return OrderDAL.Get();  
        }

        // פונקציה למחוק הזמנה
        public static int Delete(int id)
        {
            return OrderDAL.Delete(id);  
        }
        // שליפת הזמנות של משתמש מסוים בלבד לפי מזהה המשתמש
        public static List<Order> GetByUser(int userId)
        {
            return OrderDAL.GetByUser(userId);
        }
    }
       
    
}
