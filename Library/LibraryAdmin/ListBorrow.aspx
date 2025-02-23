<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="ListBorrow.aspx.cs" Inherits="Library.LibraryAdmin.ListBorrow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת השאלות </h1>
    <div class="card-body">
        <!-- table -->
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>מספר השאלה</th>
                    <th>מספר ספר</th>
                    <th>שם ספר</th>
                    <th>תז משאיל</th>
                    <th>תאריך השאלה</th>
                    <th>תאריך חזרה משוער</th>
                    <th>תאריך החזרה בפועל</th>
                    <th>הערות</th>
                    <th>פעולות</th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("BorrowId") %></td>
                            <td><%# Eval("BookId") %></td>
                            <td><%# Eval("BookName") %></td>
                            <td><%# Eval("UserId") %></td>
                            <td><%# Eval("BorrowDate") %></td>
                            <td><%# Eval("ReturnDatePlan") %></td>
                            <td><%# Eval("ActualReturnDate") %></td>
                            <td><%# Eval("Notse") %></td>
                            <td>           
                            <a class="btn btn-sm btn-primary" href="AddBorrow.aspx?UserId=<%# Eval("UserId") %>&BookId=<%# Request.QueryString["BookId"] %>">השאלת ספר</a>
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


