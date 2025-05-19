using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.LibraryAdmin
{
    public partial class TestEmail : System.Web.UI.Page
    {


    }
}

    //    protected void Page_Load(object sender, EventArgs e)
    //    {

//        if (!IsPostBack)
//        {
//            bool success = GlobFunc.SendEmail(
//                "daniesim@pelephone.co.il",
//                "בדיקה מתוך דף TestEmail",
//                "<h2>בדיקה</h2><p>אם אתה רואה את זה, שליחת המייל הצליחה.</p>"
//            );

//            if (success)
//            {
//                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('המייל נשלח בהצלחה!');", true);
//            }
//            else
//            {
//                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('שליחת המייל נכשלה. בדוק את קובץ הלוג או את ההגדרות.');", true);
//            }
//        }
//    }
//}
