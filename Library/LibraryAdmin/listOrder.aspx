<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="listOrder.aspx.cs" Inherits="Library.LibraryAdmin.listOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header">
            <strong class="card-title">רשימת הזמנות</strong>
        </div>

        <div class="card-body">
            <!-- Repeater להצגת כל ההזמנות -->
            <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                <HeaderTemplate>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>מספר הזמנה</th>
                                <th>ID ספק</th>
                                <th>ID ספר</th>
                                <th>כמות</th>
                                <th>תאריך הזמנה</th>
                                <th>סטטוס</th>
                                <th>פעולות</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("OrderId") %></td>
                        <td><%# Eval("SupplierId") %></td>
                        <td><%# Eval("BookId") %></td>
                        <td><%# Eval("Quantity") %></td>
                        <td><%# Eval("OrderDate", "{0:dd/MM/yyyy}") %></td>
                        <td>
                         <td><%# Convert.ToInt32(Eval("Status")) == 0 ? "לא התקבלה" : "התקבלה" %></td>
                        </td>
                        <td>
                            <asp:Button ID="btnReceive" runat="server" Text="קבל" CommandName="Receive" CommandArgument='<%# Eval("OrderId") %>' CssClass="btn btn-success" />

                          <%--  כאשר משתמש לוחץ על הכפתור קבל, מתרחשים הדברים הבאים:--%>

<%--                        CommandName="Receive" נשלח ל-Repeater_ItemCommand.
                        CommandArgument='<%# Eval("OrderId") %>' שולח את ה-OrderId של אותה רשומה.
                        Repeater_ItemCommand מקבל את האירוע ומבצע את הפעולה המתאימה (למשל, שינוי סטטוס להזמנה).--%>
                        </td>
                    </tr>
                </ItemTemplate>
                </tbody>
                <FooterTemplate>
                        
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
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


