using System;
using System.Collections.Generic;         // עבור רשימות List<>
using System.Linq;                        // LINQ לשאילתות
using System.Web;                         // לצורך HttpContext
using System.Net;                         // עבור NetworkCredential
using System.Net.Mail;                    // לשליחת מייל
using System.Configuration;               // לקריאת AppSettings מ־Web.config
using System.IO;                          // לכתיבת שגיאות לקובץ
using BLL;                                // כולל את Book ו־Borrow מהשכבת לוגיקה

public static class GlobFunc
{
    /// <summary>
    /// שליחת מייל כללי דרך SMTP
    /// </summary>
    public static bool SendEmail(string to, string subject, string bodyHtml)
    {
        try
        {
            // שליפת פרטים מ־web.config
            string from = ConfigurationManager.AppSettings["EMAIL_USER"];
            string password = ConfigurationManager.AppSettings["EMAIL_PASS"];
            string smtpHost = ConfigurationManager.AppSettings["SMTP_HOST"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

            // בניית ההודעה
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = bodyHtml;
            mail.IsBodyHtml = true;

            // הגדרת שרת
            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            // כתיבת שגיאה לקובץ לוג
            string logPath = HttpContext.Current.Server.MapPath("~/log.txt");
            File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex}\n");
            return false;
        }
    }

    /// <summary>
    /// שליחת מייל מהטופס צור קשר בדף הראשי
    /// </summary>
    public static bool SendContactEmail(string name, string email, string message)
    {
        string subject = $"פנייה מהאתר - {name}";
        string body = $"<b>שם:</b> {name}<br/><b>אימייל:</b> {email}<br/><b>הודעה:</b><br/>{message}";
        string to = "danielsimhon931@gmail.com"; // כתובת קבועה למשלוח

        return SendEmail(to, subject, body);
    }

    /// <summary>
    /// סך כל הספרים במערכת
    /// </summary>
    public static int GetTotalBooks()
    {
        return Book.Get().Count;
    }

    /// <summary>
    /// סך כל ההשאלות הפעילות או בכלל (בהתאם למה שיש ב־Borrow)
    /// </summary>
    public static int GetTotalBorrowedBooks()
    {
        return Borrow.Get().Count; // אין Amount – פשוט סופרים
    }

    /// <summary>
    /// עשרת הספרים הכי מושאלים
    /// </summary>
    public static List<Book> GetTop10Books()
    {
        return Borrow.Get()
            .GroupBy(b => b.BookId)
            .OrderByDescending(g => g.Count())
            .Take(10)
            .Select(g =>
            {
                var book = Book.Get().FirstOrDefault(b => b.BookId == g.Key);
                if (book != null)
                    book.BorrowedBooks = g.Count(); // לשדה זה יש ערך מחושב זמני
                return book;
            })
            .Where(b => b != null)
            .ToList();
    }

    /// <summary>
    /// 5 הסופרים הכי מושאלים
    /// </summary>
    public static List<(string AuthorName, int TotalBorrows)> GetTop5Authors()
    {
        return Borrow.Get()
            .Join(Book.Get(), b => b.BookId, bk => bk.BookId, (b, bk) => bk.BookAuthor)
            .GroupBy(author => author)
            .Select(g => (AuthorName: g.Key, TotalBorrows: g.Count()))
            .OrderByDescending(t => t.TotalBorrows)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// חיפוש ספרים לפי מחרוזת בטקסט
    /// </summary>
    public static List<Book> SearchBooks(string query)
    {
        query = query.ToLower(); // לחיפוש לא רגיש לאותיות
        return Book.Get()
            .Where(b =>
                b.BookName.ToLower().Contains(query) ||
                (!string.IsNullOrEmpty(b.BookDescription) && b.BookDescription.ToLower().Contains(query)))
            .ToList();
    }

    /// <summary>
    /// נתונים לעוגת 10 הספרים הכי מושאלים (שם + מספר)
    /// </summary>
    public static List<KeyValuePair<string, int>> GetTop10BooksForChart()
    {
        return Borrow.Get()
            .GroupBy(b => b.BookId)
            .Select(g =>
            {
                Book book = Book.Get().FirstOrDefault(b => b.BookId == g.Key);
                return new KeyValuePair<string, int>(book?.BookName ?? "לא ידוע", g.Count());
            })
            .OrderByDescending(kv => kv.Value)
            .Take(10)
            .ToList();
    }

    /// <summary>
    /// נתונים לעוגת 5 הסופרים הכי מושאלים (שם + מספר)
    /// </summary>
    public static List<KeyValuePair<string, int>> GetTop5AuthorsForChart()
    {
        return Borrow.Get()
            .Join(Book.Get(), b => b.BookId, bk => bk.BookId, (b, bk) => bk)
            .GroupBy(book => book.BookAuthor)
            .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
            .OrderByDescending(kv => kv.Value)
            .Take(5)
            .ToList();
    }
}
