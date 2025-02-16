using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using DAL;



namespace BLL
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime Year { get; set; }
        public string BookDescription { get; set; }
        public string BookLang { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime Added { get; set; }
        public int BorrowedBooks { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int AvailableQuantity { get; set; }

        // פונקציה לשמור את הספר (הוספה או עדכון)
        public int Save()
        {
            return BookDAL.Save(this);
        }

        // פונקציה לקבל ספר לפי מזהה
        public static Book GetById(int id)
        {
            return BookDAL.GetById(id);
        }

        // פונקציה לקבל את כל הספרים
        public static System.Collections.Generic.List<Book> Get()
        {
            return BookDAL.Get();
        }

        // פונקציה למחוק ספר
        public static int Delete(int id)
        {
            return BookDAL.Delete(id);
        }

        // פונקציה לשאול ספר - הפחתת 1 מהמלאי
        public static int Borrow(int bookId)
        {
            return BookDAL.BorrowBook(bookId); // קריאה לפונקציה ב-DAL שתפחית 1 מהמלאי
        }

        // פונקציה להחזיר ספר - הוספת 1 למלאי
        public static int Return(int bookId)
        {
            return BookDAL.ReturnBook(bookId); // קריאה לפונקציה ב-DAL שתוסיף 1 למלאי
        }

        // פונקציה לבדוק אם הספר זמין להשאלה
        public static bool IsBookAvailable(int bookId)
        {
            return BookDAL.IsBookAvailable(bookId);
        }
    }
}

