using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL; // ייבוא שכבת הלוגיקה העסקית (עסקים, ספרים, ספקים וכו')

namespace Library.LibraryAdmin
{
    public partial class AddOrder : System.Web.UI.Page
    {
        // פעולה שמופעלת כאשר הדף נטען
        protected void Page_Load(object sender, EventArgs e)
        {
            // נבדוק האם זו הפעם הראשונה שנטען הדף
            if (!IsPostBack)
            {
                FillSuppliers(); // קריאה לפונקציה שתמלא את רשימת הספקים
            }
        }

        // פונקציה שמביאה את כל הספקים ומציגה אותם ברשימה הנפתחת
        public void FillSuppliers()
        {
            // שליפת כל הספקים ממחלקת Supplier שב־BLL
            var suppliers = Supplier.Get();

            // הגדרת מקור הנתונים לרשימת הספקים
            ddlSuppliers.DataSource = suppliers;
            ddlSuppliers.DataTextField = "SupplierName"; // מה יוצג למשתמש
            ddlSuppliers.DataValueField = "SupplierId";  // הערך הפנימי
            ddlSuppliers.DataBind();

            // הוספת אפשרות ברירת מחדל – "בחר ספק"
            ddlSuppliers.Items.Insert(0, new ListItem("בחר ספק", "0"));
        }

        // פעולה שמתרחשת כאשר המשתמש משנה ספק
        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // נבדוק שהמשתמש לא בחר את ברירת המחדל (0)
            if (ddlSuppliers.SelectedValue != "0")
            {
                // המרה למספר של מזהה הספק
                int supplierId = int.Parse(ddlSuppliers.SelectedValue);

                // שליפת הספרים ששייכים לספק שנבחר
                var books = Book.GetBooksBySupplier(supplierId);

                // הצגת רשימת הספרים בתיבה
                ddlBooks.DataSource = books;
                ddlBooks.DataTextField = "BookName"; // מציג שם ספר
                ddlBooks.DataValueField = "BookId";  // שומר מזהה ספר
                ddlBooks.DataBind();

                // הוספת אפשרות ברירת מחדל גם לרשימת הספרים
                ddlBooks.Items.Insert(0, new ListItem("בחר ספר", "0"));
            }
        }

        // פעולה שמתבצעת כאשר לוחצים על הכפתור "שמור"
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // בדיקה שהמשתמש בחר ספק וספר
            if (ddlSuppliers.SelectedValue == "0" || ddlBooks.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('יש לבחור ספק וספר לפני ביצוע ההזמנה.');", true);
                return;
            }

            // יצירת אובייקט הזמנה חדש
            var order = new Order();
            order.OrderId = -1; // זהות זמנית עד שמתווסף למסד הנתונים
            order.SupplierId = int.Parse(ddlSuppliers.SelectedValue); // מזהה ספק
            order.BookId = int.Parse(ddlBooks.SelectedValue);         // מזהה ספר
            order.Quantity = int.Parse(TxtQuantity.Text);             // כמות
            order.OrderDate = DateTime.Now;                           // תאריך הזמנה
            order.Status = 0;                                         // סטטוס "לא התקבלה"
            order.UserId = 100; // משתמש קיים בטבלת T_Users

            // שמירה דרך שכבת הלוגיקה (BLL)
            order.Save();

            // שליחת מייל לספק שנבחר
            Supplier supplier = Supplier.GetById(order.SupplierId); // שליפת פרטי הספק

            if (supplier != null && !string.IsNullOrEmpty(supplier.SEmail))
            {
                // יצירת נושא ותוכן המייל
                string subject = "הזמנה חדשה ממערכת הספרייה";
                string body = $"<h2>שלום {supplier.SupplierName},</h2>" +
                              $"<p>נוצרה עבורך הזמנה חדשה.</p>" +
                              $"<p>מספר ספר: {order.BookId}<br/>כמות: {order.Quantity}<br/>תאריך: {order.OrderDate.ToShortDateString()}</p>";

                // קריאה לפונקציה ששולחת את המייל בפועל
                bool sent = GlobFunc.SendEmail(supplier.SEmail, subject, body);

                // בדיקה האם המייל נשלח בהצלחה
                if (sent)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('ההזמנה נשמרה ונשלח מייל לספק: {supplier.SEmail}'); window.location='ListOrder.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('ההזמנה נשמרה אך המייל לא נשלח. בדוק את ההגדרות.'); window.location='ListOrder.aspx';", true);
                }
            }
            else
            {
                // אם אין כתובת מייל לספק
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('ההזמנה נשמרה אך לא נשלח מייל מאחר ואין כתובת לספק.'); window.location='ListOrder.aspx';", true);
            }
        }
    }
}
