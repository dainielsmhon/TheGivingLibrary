using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class AddOrder : System.Web.UI.Page
    {
        
        //protected void BtnSave_Click(object sender, EventArgs e)
        //{
            
        //    int quantity = int.Parse(TxtQuantity.Text);  // כמות הספרים
        //    int supplierId = int.Parse(ddlSuppliers.SelectedValue);  // מזהה הספק שנבחר
        //    int bookId = int.Parse(ddlBooks.SelectedValue);  // מזהה הספר שנבחר

        //    //  שמירה של הנתונים בDB 
        //    SaveOrder(supplierId, bookId, quantity);
        //}

        //// פונקציה לשמירה בבסיס הנתונים
        //private void SaveOrder(int supplierId, int bookId, int quantity)
        //{
        //    // 3. יצירת חיבור לדאטה-בייס (באמצעות DbContext)
        //    using (var context = new YourDbContext())
        //    {
        //        // 4. יצירת אובייקט של הזמנה חדשה
        //        var order = new Order
        //        {
        //            SupplierId = supplierId,
        //            BookId = bookId,
        //            Quantity = quantity,
        //            OrderDate = DateTime.Now  // תאריך הזמנה נוכחי
        //        };

        //        // 5. הוספת ההזמנה לבסיס הנתונים
        //        context.Orders.Add(order);

        //        // 6. שמירת השינויים בבסיס הנתונים
        //        context.SaveChanges();
        //  }
        }
}
