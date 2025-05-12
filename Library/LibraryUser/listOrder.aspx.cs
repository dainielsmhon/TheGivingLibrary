using BLL;
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
            if (!IsPostBack)// פעולה שטוענת את כל ההזמנות
            {
                FillData();
            }
        }
        public void FillData()
        {


            var orders = Order.GetAllOrders();
            orders = orders.OrderBy(o => o.Status).ToList(); // מיון כך שההזמנות שלא התקבלו יהיו ראשונות
            rptOrders.DataSource = orders;
            rptOrders.DataBind();

        }
        // טיפול בכפתור "קבל" שמשנה את הסטטוס להזמנה שהתקבלה
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Receive")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                var order = Order.GetById(orderId);

                if (order != null)
                {
                    // הגנה כפולה - אל תקבל הזמנה פעמיים!
                    if (order.Status == 1)
                        return;

                    order.Status = 1;
                    order.Save();

                    var book = Book.GetById(order.BookId);
                    if (book != null)
                    {
                        book.AvailableQuantity += order.Quantity;
                        book.TotalQuantity += order.Quantity;
                        book.Save();
                    }

                    FillData();
                }
            }
        }

    }
}