<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="ListBook.aspx.cs" Inherits="Library.LibraryAdmin.ListBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
                <link rel="stylesheet" href="css/dataTables.bootstrap4.css">
<link rel="stylesheet" href="css/StyleD2.css">





    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת הספרים</h1>
    <div class="card-body">
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>תמונה</th>
                    <th>שם ספר</th>
                    <th>שם מלא</th>
                    <th>מחבר הספר</th>
                    <th>תאור הספר</th>
                    <th>אורך הספר</th>
                    <th>מיקום הספר</th>
                    <th>מושאלים</th>
                    <th>כמות במלאי</th>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                  <ItemTemplate>
    <tr class='<%# "status-" + Eval("Status") %>'>
        <td>
            <img src='<%# Eval("ImageUrl") != DBNull.Value && !string.IsNullOrEmpty(Eval("ImageUrl").ToString()) 
                        ? Eval("ImageUrl") 
                        : "assets/images/library-bg.jpg" %>' 
                 style="width: 60px; height: 80px; object-fit: cover;" />
        </td>
        <td><%# Eval("BookName") %></td>
        <td><%# Eval("BookId") %></td> 
        <td><%# Eval("BookAuthor") %></td>
        <td><%# Eval("BookDescription") %></td>
        <td><%# Eval("BookLang") %></td>
        <td><%# Eval("Location") %></td>
        <td><%# Eval("BorrowedBooks") %></td>
        <td><%# Eval("AvailableQuantity") %></td>
        <td>
            <%# (Convert.ToInt32(Eval("AvailableQuantity")) > 0) ? 
                "<a id='LinkAddBorrow' runat='server' class='btn btn-sm btn-primary' href='AddBorrow.aspx?BookId=" + Eval("BookId") + "'>השאלה</a>" : "" %>
            <a class="dropdown-item" href="AddBook.aspx?BookId=<%# Eval("BookId") %>">עריכה</a>
        </td>
    </tr>
</ItemTemplate>

                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
   <script src="js/jquery.dataTables.min.js"></script>
<script src="js/dataTables.bootstrap4.min.js"></script>
    <script>
        var ans = true;
        function ComfirmDelete() {
            ans = confirm("האם אתה בטוח שברצונך למחוק קטגוריה זו?");
            return ans;
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
    <script>
        $(document).ready(function () {
            // מאתחל כל טבלה עם ID אם היא לא מאותחלת עדיין כ־DataTable
            $("table[id]").each(function () {
                const tbl = $(this);

                if (!$.fn.DataTable.isDataTable(this)) {
                    tbl.DataTable({
                        paging: true,
                        autoWidth: true,
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/2.0.8/i18n/he.json'
                        }
                    });
                }

                if (!tbl.hasClass("table-bg")) {
                    tbl.addClass("table-bg");
                }
            });
        });
    </script>
</asp:Content>








