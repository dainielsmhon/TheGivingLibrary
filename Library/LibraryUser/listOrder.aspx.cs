using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryUser
{
    public partial class listOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // בדיקה האם המשתמש מחובר, אחרת מפנה למסך התחברות
            if (Session["UserId"] == null || Session["IsAdmin"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack) // טוען את הנתונים רק בפעם הראשונה
            {
                FillData();
            }
        }

        public void FillData()
        {
            int userId = Convert.ToInt32(Session["UserId"]); // מזהה המשתמש המחובר
            bool isAdmin = Convert.ToBoolean(Session["IsAdmin"]); // בדיקה אם מנהל

            List<Order> orders;

            if (isAdmin)
            {
                orders = Order.GetAllOrders(); // שליפת כל ההזמנות למנהל
            }
            else
            {
                orders = OrderDAL.GetByUser(userId); // שליפת ההזמנות של המשתמש בלבד
            }

            // מיון כך שההזמנות שלא התקבלו יוצגו קודם
            orders = orders.OrderBy(o => o.Status).ToList();

            rptOrders.DataSource = orders; // הצמדת הנתונים לרפיטר
            rptOrders.DataBind();
        }

        // פעולה שמופעלת כשנלחץ כפתור "קבל"
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Receive")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                var order = Order.GetById(orderId);

                if (order != null)
                {
                    // אם ההזמנה כבר התקבלה – לא לעשות כלום
                    if (order.Status == 1)
                        return;

                    // עדכון סטטוס להזמנה התקבלה
                    order.Status = 1;
                    order.Save();

                    // עדכון מלאי הספרים
                    var book = Book.GetById(order.BookId);
                    if (book != null)
                    {
                        book.AvailableQuantity += order.Quantity;
                        book.TotalQuantity += order.Quantity;
                        book.Save();
                    }

                    FillData(); // רענון הטבלה
                }
            }
        }
    }
}