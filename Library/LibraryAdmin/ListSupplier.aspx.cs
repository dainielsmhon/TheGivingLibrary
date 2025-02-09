using BLL;
using DAL;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class ListSupplier : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSuppliersGrid();
            }
        }

        private void BindSuppliersGrid()
        {
            // קריאה ל-BLL כדי לקבל את כל הספקים
            var suppliers = Supplier.Get();  // פונקציה שמחזירה את כל הספקים
            gvSuppliers.DataSource = suppliers;
            gvSuppliers.DataBind();
        }

        // אירוע של מחיקה
        protected void gvSuppliers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // קבלת ה-SupplierId מתוך DataKeys של השורה
            int supplierId = Convert.ToInt32(gvSuppliers.DataKeys[e.RowIndex].Value);

            // קריאה למחיקת הספק
            SupplierDAL.Delete(supplierId);

            // עדכון הרשימה לאחר המחיקה
            BindSuppliersGrid();
        }

        // אירוע של עריכה
        protected void gvSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // קבלת ה-SupplierId מתוך CommandArgument
                int supplierId = Convert.ToInt32(e.CommandArgument);

                // הפניה לדף עריכת ספק עם ה-SupplierId
                Response.Redirect($"AddSupplier.aspx?SupplierId={supplierId}");
            }
        }

        // אירוע של מחיקה עבור כפתור Remove
        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            // שולף את ה-SupplierId מתוך CommandArgument
            string Cid = ((Button)sender).CommandArgument;

            // ממיר אותו ל-ID של ספק
            int supplierId = int.Parse(Cid);

            // קריאה למחיקת הספק
            SupplierDAL.Delete(supplierId);

            // עדכון הרשימה לאחר המחיקה
            BindSuppliersGrid();
        }
    }
}
