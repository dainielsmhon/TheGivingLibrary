using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Xml.Linq;



namespace DAL
{
    public class BookDAL
    {
        public static Book GetById(int id)
        {
            Book Tmp = null;
            DbContext Db = new DbContext();
            string Sql = $"SELECT *, (TotalQuantity - AvailableQuantity) AS BorrowedBooks FROM T_Books WHERE BookId={id}";
            DataTable Dt = Db.Execute(Sql);

            if (Dt.Rows.Count > 0)
            {
                Tmp = new Book()
                {
                    BookId = int.Parse(Dt.Rows[0]["BookId"] + ""),
                    BookName = Dt.Rows[0]["BookName"] + "",
                    BookAuthor = Dt.Rows[0]["BookAuthor"] + "",
                    Year = DateTime.Parse(Dt.Rows[0]["Year"] + ""),
                    BookDescription = Dt.Rows[0]["BookDescription"] + "",
                    BookLang = Dt.Rows[0]["BookLang"] + "",
                    Location = Dt.Rows[0]["Location"] + "",
                    Status = Dt.Rows[0]["Status"] + "",
                    BorrowedBooks = int.Parse(Dt.Rows[0]["BorrowedBooks"] + ""), // עכשיו מחושב נכון
                    Added = DateTime.Parse(Dt.Rows[0]["Added"] + ""),
                    TakenDate = DateTime.Parse(Dt.Rows[0]["TakenDate"] + ""),
                    ReturnDate = DateTime.Parse(Dt.Rows[0]["ReturnDate"] + ""),
                    AvailableQuantity = int.Parse(Dt.Rows[0]["AvailableQuantity"] + "")
                };
            }

            return Tmp;
        }

        public static List<Book> Get()
        {
            List<Book> LstTmp = new List<Book>();
            DbContext Db = new DbContext();
            string Sql = "SELECT *, (TotalQuantity - AvailableQuantity) AS BorrowedBooks FROM T_Books";
            DataTable Dt = Db.Execute(Sql);

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Book Tmp = new Book()
                {
                    BookId = int.Parse(Dt.Rows[i]["BookId"] + ""),
                    BookName = Dt.Rows[i]["BookName"] + "",
                    BookAuthor = Dt.Rows[i]["BookAuthor"] + "",
                    Year = DateTime.Parse(Dt.Rows[i]["Year"] + ""),
                    BookDescription = Dt.Rows[i]["BookDescription"] + "",
                    BookLang = Dt.Rows[i]["BookLang"] + "",
                    Location = Dt.Rows[i]["Location"] + "",
                    Status = Dt.Rows[i]["Status"] + "",
                    BorrowedBooks = int.Parse(Dt.Rows[i]["BorrowedBooks"] + ""), // עכשיו מחושב נכון
                    Added = DateTime.Parse(Dt.Rows[i]["Added"] + ""),
                    TakenDate = DateTime.Parse(Dt.Rows[i]["TakenDate"] + ""),
                    ReturnDate = DateTime.Parse(Dt.Rows[i]["ReturnDate"] + ""),
                    AvailableQuantity = int.Parse(Dt.Rows[i]["AvailableQuantity"] + "")
                };
                LstTmp.Add(Tmp);
            }

            return LstTmp;
        }

        public static int Delete(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Books WHERE BookId={id}";
            return Db.ExecuteNonQuery(Sql);
        }

        public static int Save(Book Tmp)
        {
            DbContext Db = new DbContext();
            string Sql = "";

            if (Tmp.BookId == -1)
            {
                Sql = $"INSERT INTO T_Books (BookName, BookAuthor, Year, BookDescription, BookLang, Location, Status, Added, TakenDate, ReturnDate, AvailableQuantity) VALUES ";
                Sql += $"(N'{Tmp.BookName}', N'{Tmp.BookAuthor}', '{Tmp.Year:yyyy-MM-dd}', N'{Tmp.BookDescription}', N'{Tmp.BookLang}', N'{Tmp.Location}', N'{Tmp.Status}', '{Tmp.Added:yyyy-MM-dd}', '{Tmp.TakenDate:yyyy-MM-dd}', '{Tmp.ReturnDate:yyyy-MM-dd}', {Tmp.AvailableQuantity})";
            }
            else
            {
                Sql = $"UPDATE T_Books SET ";
                Sql += $"BookName=N'{Tmp.BookName}', ";
                Sql += $"BookAuthor=N'{Tmp.BookAuthor}', ";
                Sql += $"Year='{Tmp.Year:yyyy-MM-dd}', ";
                Sql += $"BookDescription=N'{Tmp.BookDescription}', ";
                Sql += $"BookLang=N'{Tmp.BookLang}', ";
                Sql += $"Location=N'{Tmp.Location}', ";
                Sql += $"Status=N'{Tmp.Status}', ";
                Sql += $"Added='{Tmp.Added:yyyy-MM-dd}', ";
                Sql += $"TakenDate='{Tmp.TakenDate:yyyy-MM-dd}', ";
                Sql += $"ReturnDate='{Tmp.ReturnDate:yyyy-MM-dd}', ";
                Sql += $"AvailableQuantity={Tmp.AvailableQuantity} ";
                Sql += $"WHERE BookId={Tmp.BookId}";
            }

            int RecCount = Db.ExecuteNonQuery(Sql);

            if (Tmp.BookId == -1)
            {
                Tmp.BookId = Db.GetMaxId("T_Books", "BookId");
            }

            return RecCount;
        }

        public static int BorrowBook(int bookId)
        {
            DbContext Db = new DbContext();
            string Sql = $"UPDATE T_Books SET AvailableQuantity = AvailableQuantity - 1 WHERE BookId = {bookId} AND AvailableQuantity > 0";
            return Db.ExecuteNonQuery(Sql);
        }

        public static int ReturnBook(int bookId)
        {
            DbContext Db = new DbContext();
            string Sql = $"UPDATE T_Books SET AvailableQuantity = AvailableQuantity + 1 WHERE BookId = {bookId}";
            return Db.ExecuteNonQuery(Sql);
        }

        public static bool IsBookAvailable(int bookId)
        {
            DbContext Db = new DbContext();
            string Sql = $"SELECT COUNT(*) FROM T_Books WHERE BookId = {bookId} AND AvailableQuantity > 0";
            int count = (int)Db.ExecuteScalar(Sql);
            return count > 0;
        }
    }
}