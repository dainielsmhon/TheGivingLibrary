﻿<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddReturn.aspx.cs" Inherits="Library.LibraryAdmin.AddReturn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
      <h2 ID="LblBookName" runat="server" >BookName</h2> 
  <asp:HiddenField ID="HidBookId" runat="server" />
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
                    <th>סטטוס</th>
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
                            <td><%# Eval("Status") %></td>
                            <td><%# Eval("Notse") %></td>
                            <td>                      <div class="dropdown">
                        <button class="btn btn-sm dropdown-toggle more-vertical" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          <span class="text-muted sr-only">Action</span>
                        </button>
                        <div class="dropdown-menu dropdown-menu-right">
                      <%--    <a class="dropdown-item" href="ListBorrow.aspx?BorrowId=<%# Eval("BorrowId") %>">עריכה</a>
                          <a class="dropdown-item" href="AddBorrow.aspx?BorrowId=<%# Eval("BorrowId") %>">השאלה</a>
                          --%><a class="dropdown-item" href="AddReturn.aspx?BorrowId=<%# Eval("BorrowId") %>">החזרה</a>
                        </div>                                   
                      </div></td>
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

