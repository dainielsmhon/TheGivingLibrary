<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddBorrow.aspx.cs" Inherits="Library.LibraryAdmin.AddBorrow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h2 ID="LblBookName" runat="server">BookName</h2>
    <asp:HiddenField ID="HidBookId" runat="server" />

    <h1>בחר משתמש</h1>
    <div class="card-body">
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>תז משתמש</th>
                    <th>שם לקוח</th>
                    <th>כתובת מייל</th>
                    <th>מספר נייד</th>
                    <th>כתובת</th>
                    <td><%# Eval("JoinDate", "{0:yyyy-MM-dd}") %></td>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("UserId") %></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td><%# Eval("Adress") %></td>
                            <td><%# Eval("JoinDate", "{0:yyyy-MM-dd}") %></td>
                            <td>
                                <a class="btn btn-sm btn-primary" href='AddBorrow.aspx?UserId=<%# Eval("UserId") %>&BookId=<%# Request.QueryString["BookId"] %>'>השאלת ספר</a>
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
