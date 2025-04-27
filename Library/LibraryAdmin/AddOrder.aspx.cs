using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL; // ייבוא של מחלקות BLL כמו ספרים וספקים

namespace Library.LibraryAdmin
{
    public partial class AddOrder : System.Web.UI.Page
    {
        
        // פעולה טוענת את הספקים ל-DropDownList
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData(); // קריאה לפונקציה אתחול הנתונים
            }
        }

        // פונקציה שמביאה את כל הנתונים הנדרשים (כמו ספקים וספרים)
        public void FillData()
        {
            // קריאה לפונקציה ב-BLL לשליפת הספקים
            var suppliers = Supplier.Get();
            ddlSuppliers.DataSource = suppliers;
            ddlSuppliers.DataTextField = "SupplierName"; // הצגת שם הספק
            ddlSuppliers.DataValueField = "SupplierId"; // שימוש ב-SupplierId כערך ברשימה
            ddlSuppliers.DataBind();

            // נוסיף אפשרות לברירת מחדל שלא תוכל לבחור ספק עם ID = 0
            ddlSuppliers.Items.Insert(0, new ListItem("בחר ספק", "0"));
        }

        // פעולה שמתבצעת כאשר בוחרים ספק מהרשימה
        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // בדיקה שהספק לא שווה ל-0 (בחר ספק)
            if (ddlSuppliers.SelectedValue != "0")
            {
                int selectedSupplierId = int.Parse(ddlSuppliers.SelectedValue);  // מקבלים את ה-SupplierId שנבחר

                // קריאה לפונקציה ב-BLL לשליפת הספרים של הספק הספציפי
                var books = Book.GetBooksBySupplier(selectedSupplierId);

                ddlBooks.DataSource = books;
                ddlBooks.DataTextField = "BookName";  // הצגת שם הספר
                ddlBooks.DataValueField = "BookId";  // שימוש ב-BookId כערך ברשימה
                ddlBooks.DataBind();

                // נוסיף אפשרות לברירת מחדל שלא תוכל לבחור ספר עם ID = 0
                ddlBooks.Items.Insert(0, new ListItem("בחר ספר", "0"));
            }
        }

        // שמירה של ההזמנה
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // יצירת אובייקט הזמנה
            var order = new Order
            {
                SupplierId = int.Parse(ddlSuppliers.SelectedValue),
                BookId = int.Parse(ddlBooks.SelectedValue),
                Quantity = int.Parse(TxtQuantity.Text),
                OrderDate = DateTime.Now,
                OrderId = -1
            };

            // שמירה דרך BLL
            order.Save();


            // שלב חדש – שליחת מייל לספק
            Supplier supplier = Supplier.GetById(order.SupplierId); // שליפת הספק לפי ID
            if (supplier != null && !string.IsNullOrEmpty(supplier.SEmail))
            {
                System.Diagnostics.Debug.WriteLine("כתובת מייל של הספק: " + supplier.SEmail);


                string subject = "הזמנה חדשה ממערכת הספרייה";
                string body = $"<h2>שלום {supplier.SupplierName},</h2><p>הוזנה עבורך הזמנה חדשה.</p><p>מספר ספר: {order.BookId}<br/>כמות: {order.Quantity}<br/>תאריך: {order.OrderDate.ToShortDateString()}</p>";

                bool sent = GlobFunc.SendEmail(supplier.SEmail, subject, body); // שליחת המייל ומעקב הצלחה

                if (sent)
                {
                    // מייל נשלח - הצג פופאפ והמשך להפניה
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('המייל נשלח בהצלחה לכתובת: {supplier.SEmail}'); window.location='ListOrder.aspx';", true);
                }
                else
                {
                    // מייל נכשל - הודעה מתאימה
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('שגיאה בשליחת המייל לספק. בדוק את כתובת המייל או קובץ הלוג.'); window.location='ListOrder.aspx';", true);
                }
            }
            else
            {
                // אין כתובת מייל - הצג הודעה
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('לא נמצאה כתובת מייל עבור הספק. ההזמנה נשמרה, אך לא נשלח מייל.'); window.location='ListOrder.aspx';", true);
            }



            // אפשר להוסיף הפניה לדף רשימת ההזמנות לאחר השמירה
            //Response.Redirect("ListOrder.aspx");
        }
    }
}
