<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Library.LibraryAdmin.book" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת משתמשים</h1>
    <div class="card-body">
        <!-- table -->
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>תז משתמש</th>
                    <th>שם לקוח</th>
                    <th>שם משתמש</th>
                    <th>סיסמה</th>
                    <th>כתובת מייל</th>
                    <th>מספר נייד</th>
                    <th>כתובת</th>
                    <th>תאריך הצטרפות</th>
                    <th>פעולות</th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("UserId") %></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("UserName") %></td>
                            <td><%# Eval("UserPass") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td><%# Eval("Adress") %></td>
                            <td><%# Eval("JoinDate") %></td>
                            <td>                      <div class="dropdown">
                        <button class="btn btn-sm dropdown-toggle more-vertical" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          <span class="text-muted sr-only">Action</span>
                        </button>
                        <div class="dropdown-menu dropdown-menu-right">
                          <a class="dropdown-item" href="UserAdd.aspx?UserId=<%# Eval("UserId") %>">עריכה</a>
<%--                          <a class="dropdown-item" href="UserId.aspx?UserId=<%# Eval("UserId") %>">הוספה</a>--%>
                         <%-- <a class="dropdown-item" href="UserId.aspx?UserId=<%# Eval("UserId") %>">החזרה</a>--%>
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
