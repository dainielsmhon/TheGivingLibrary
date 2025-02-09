using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class AddSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillData();
            }
        }

        public void FillData()
        {
            Supplier Tmp = null;
            string SupplierId = Request["SupplierId"] + "";

            // אם אין ID, אז מדובר בהוספת ספק חדש
            if (string.IsNullOrEmpty(SupplierId))
            {
                SupplierId = "-1"; // הוספת ספק חדש
            }
            else
            {
                // אם יש ID, ננסה לשלוף את הספק מה-BLL
                Tmp = Supplier.GetById(int.Parse(SupplierId));
                if (Tmp == null)
                {
                    SupplierId = "-1"; // אם לא נמצא ספק, הוסף ספק חדש
                }
            }

            HidSupplierId.Value = SupplierId; // שמירה ב-`HiddenField` של ה-SupplierId

            if (Tmp != null) // אם הספק נמצא, מדובר בעריכה
            {
                // מילוי השדות לפי הנתונים הקיימים
                TxtSupplierName.Text = Tmp.SupplierName;
                TxtSupplierAddress.Text = Tmp.SAddress;
                TxtSupplierPhone.Text = Tmp.SPhone;
                TxtSupplierWeb.Text = Tmp.SWeb;
                TxtSupplierEmail.Text = Tmp.SEmail;
                TxtSupplierContact.Text = Tmp.Contact;
                TxtSupplierAdded.Text = Tmp.Added.ToString("yyyy-MM-dd");
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // יצירת אובייקט Supplier עם הנתונים מהטופס
            Supplier Tmp = new Supplier()
            {
                SupplierId = int.Parse(HidSupplierId.Value), // שימוש ב-`HiddenField` לשמירת ה-SupplierId
                SupplierName = TxtSupplierName.Text,
                SAddress = TxtSupplierAddress.Text,
                SPhone = TxtSupplierPhone.Text,
                SWeb = TxtSupplierWeb.Text,
                SEmail = TxtSupplierEmail.Text,
                Contact = TxtSupplierContact.Text,
                Added = DateTime.Parse(TxtSupplierAdded.Text)
            };

            // שמירת או עדכון הספק
            Tmp.Save();
            Response.Redirect("ListSupplier.aspx"); // הפניה לדף רשימת הספקים אחרי השמירה
        }
    }
}