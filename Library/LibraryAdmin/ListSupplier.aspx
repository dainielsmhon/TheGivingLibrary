<%@ Page Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.master" AutoEventWireup="true" CodeBehind="ListSupplier.aspx.cs" Inherits="Library.LibraryAdmin.ListSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת ספקים</h1>
    <div class="card-body">
        <!-- table -->
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>תז ספק</th>
                    <th>שם ספק</th>
                    <th>כתובת</th>
                    <th>נייד</th>
                    <th>כתובת אתר</th>
                    <th>כתובת מייל</th>
                    <th>איש קשר</th>
                    <th>פעולות</th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("SupplierId") %></td>
                            <td><%# Eval("SupplierName") %></td>
                            <td><%# Eval("SAddress") %></td>
                            <td><%# Eval("SPhone") %></td>
                            <td><%# Eval("SWeb") %></td>
                            <td><%# Eval("SEmail") %></td>
                            <td><%# Eval("Contact") %></td>
                            <td>                      <div class="dropdown">
                        <button class="btn btn-sm dropdown-toggle more-vertical" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                          <span class="text-muted sr-only">Action</span>
                        </button>
                        <div class="dropdown-menu dropdown-menu-right">
                          <a class="dropdown-item" href="AddSupplier.aspx?SupplierId=<%# Eval("SupplierId") %>">עריכה</a>
<%--                          <a class="dropdown-item" href="AddBorrow.aspx?SupplierId=<%# Eval("SupplierId") %>">הוספה</a>--%>
                         <%-- <a class="dropdown-item" href="AddReturn.aspx?BookId=<%# Eval("BookId") %>">החזרה</a>--%>
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
