using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class AddBook : System.Web.UI.Page
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
            Book Tmp = null;
            string BookId ;


            BookId = Request["BookId"] + "";


            if (string.IsNullOrEmpty(BookId))
            {
                BookId = "-1"; //הוספת משתמש חדש
            }
            else
            {
                Tmp = BLL.Book.GetById(int.Parse(BookId));
                if (Tmp == null)
                {
                    BookId = "-1";//הוספת משתמש חדש
                }
            }
            HidBookId.Value = BookId;// שמירת שם משתמש  לעריכה או הוספה בשדה הנסתר
            var suppliers = Supplier.Get();  // מביא את הספקים
            ddlSuppliers.DataSource = suppliers;
            ddlSuppliers.DataTextField = "SupplierName";  // מציג את שם הספק
            ddlSuppliers.DataValueField = "SupplierId";  // משתמש ב-SupplierId
            ddlSuppliers.DataBind();
            if (Tmp != null)//אנחנו במצב עריכה של משתמש לכן יש  למלא את הפרטים
            {
                Tmp.BookId = Tmp.BookId;
                TxtName.Text = Tmp.BookName;
                TxtAuthor.Text = Tmp.BookAuthor;
                TxtDescription.Text = Tmp.BookDescription;
                TxtLang.Text = Tmp.BookLang;
                TxtLocation.Text = Tmp.Location;
                Year.Text = Tmp.Year.ToString("yyyy-MM-dd");
                TxtStatus.Text = Tmp.Status;
                TextAdded.Text = Tmp.Added.ToString("yyyy-MM-dd");
                TxtTakenDate.Text = Tmp.TakenDate.ToString("yyyy-MM-dd");
                TxtReturnDate.Text = Tmp.ReturnDate.ToString("yyyy-MM-dd");
                ddlSuppliers.SelectedValue = Tmp.SupplierId.ToString();//מצביע על הספק 
                ImgPreview.ImageUrl = string.IsNullOrEmpty(Tmp.ImageUrl)
                   ? "/LibraryAdmin/assets/images/library-bg.jpg"
                   : Tmp.ImageUrl;//  הצגת תמונה נוכחית


            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string imageUrl = "/LibraryAdmin/assets/images/library-bg.jpg"; // ברירת מחדל

            if (FileUploadImage.HasFile)
            {
                string fileName = Path.GetFileName(FileUploadImage.FileName); // כולל סיומת
                string savePath = Server.MapPath("~/LibraryAdmin/assets/images/" + fileName);
                FileUploadImage.SaveAs(savePath);
                imageUrl = "/LibraryAdmin/assets/images/" + fileName;
            }
            else if (HidBookId.Value != "-1")
            {
                // שליפה של התמונה הקודמת אם לא הועלתה חדשה
                var existingBook = BLL.Book.GetById(int.Parse(HidBookId.Value));
                if (existingBook != null)
                {
                    imageUrl = existingBook.ImageUrl;
                }
            }

            Book Tmp = new Book()
            {
                BookId = int.Parse(HidBookId.Value),
                BookName = TxtName.Text,
                BookAuthor = TxtAuthor.Text,
                Year = DateTime.Parse(Year.Text),
                BookDescription = TxtDescription.Text,
                BookLang = TxtLang.Text,
                Location = TxtLocation.Text,
                Status = TxtStatus.Text,
                Added = DateTime.Parse(TextAdded.Text),
                TakenDate = DateTime.Parse(TxtTakenDate.Text),
                ReturnDate = DateTime.Parse(TxtReturnDate.Text),
                SupplierId = int.Parse(ddlSuppliers.SelectedValue),
                ImageUrl = imageUrl
            };

            Tmp.Save();

            // רענון כדי לראות את התמונה המעודכנת מידית
            Response.Redirect("AddBook.aspx?BookId=" + Tmp.BookId);
        }
    }
}