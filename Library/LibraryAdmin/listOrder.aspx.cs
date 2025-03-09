using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{

    public partial class ListOrder : System.Web.UI.Page
    {
        // פעולה שטוענת את כל ההזמנות
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData(); 
            }
        }

       
        public void FillData()
        {
        //    var orders = Order.Get(); // שליפת ההזמנות מה-BLL
        //    rptOrders.DataSource = orders;
        //    rptOrders.DataBind();
        }

        // טיפול בכפתור "קבל" שמשנה את הסטטוס להזמנה שהתקבלה
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Receive")
            {
                // קבלת ה-ID של ההזמנה שנבחרה
                int orderId = Convert.ToInt32(e.CommandArgument);

                // שליפת ההזמנה ועדכון הסטטוס שלה ל-1 (התקבלה)
                var order = Order.GetById(orderId);
                if (order != null)
                {
                    order.Status = 1; // שינוי סטטוס להזמנה התקבלה
                    order.Save(); // שמירה של ההזמנה עם הסטטוס החדש

                    FillData(); // רענון הנתונים לאחר השינוי
                }
            }
        }
    }
}