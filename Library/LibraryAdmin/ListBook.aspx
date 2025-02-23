<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="ListBook.aspx.cs" Inherits="Library.LibraryAdmin.ListBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת הספרים</h1>
    <div class="card-body">
        <!-- table -->
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>שם ספר</th>
                    <th>שם מלא</th>
                    <th>מחבר הספר</th>
                    <th>תאור הספר</th>
                    <th>אורך הספר</th>
                    <th>מיקום הספר</th>
                    <th>מושאלים</th>
                    <th>כמות במלאי</th>
                    <th>מצב הספר קיים/הושאל</th>
                    <th>פעולות</th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("BookId") %></td>
                            <td><%# Eval("BookName") %></td>
                            <td><%# Eval("BookAuthor") %></td>
                            <td><%# Eval("BookDescription") %></td>
                            <td><%# Eval("BookLang") %></td>
                            <td><%# Eval("Location") %></td>
                            <td><%# Eval("BorrowedBooks") %></td>
                            <td><%# Eval("AvailableQuantity") %></td>
                            <td><%# Eval("Status") %></td>
<td>&nbsp;
    <%# (Convert.ToInt32(Eval("AvailableQuantity")) > 0) ? 
        "<a id='LinkAddBorrow' runat='server' class='btn btn-sm btn-primary' href='AddBorrow.aspx?BookId=" + Eval("BookId") + "'>השאלה</a>" : "" 
    %>
</td>

                       
                            

                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <script src='js/jquery.dataTables.min.js'></script>
   <script src='js/dataTables.bootstrap4.min.js'></script>
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
               if ($('#MainTbl').length) {
                   $('#MainTbl').DataTable(
                       {
                           autoWidth: true,
                           //"lengthMenu": [
                           //  [16, 32, 64, -1]
                           //  [16, 32, 64, "All"]
                           //    ],


                           language: {
                               url: 'https://cdn.datatables.net/plug-ins/2.0.8/i18n/he.json'
                           }
                       }); 
               }
           });

         
            
       </script>
</asp:Content>











