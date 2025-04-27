<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="listOrder.aspx.cs" Inherits="Library.LibraryAdmin.listOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/dataTables.bootstrap4.css">
        
        <link rel="stylesheet" href="assets/css/StyleD2.css">


</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת הזמנות</h1>
    <div class="card-body">
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th style="display:none;">StatusSort</th> <!-- עמודת מיון מוסתרת -->
                    <th>מספר הזמנה</th>
                    <th>שם ספק</th>
                    <th>שם ספר</th>
                    <th>כמות</th>
                    <th>תאריך</th>
                    <th>סטטוס</th>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                    <ItemTemplate>
                        <tr class='<%# "status-" + Eval("Status") %>'>
                            <td style="display:none;"><%# Eval("Status") %></td> <!-- עמודת מיון נסתרת -->
                            <td><%# Eval("OrderId") %></td>
                            <td><%# Eval("SupplierName") %></td>
                            <td><%# Eval("BookName") %></td>
                            <td><%# Eval("Quantity") %></td>
                            <td><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></td>
                            <td><%# (Convert.ToInt32(Eval("Status")) == 0 ? "לא התקבלה" : "התקבלה") %></td>
                            <td>
                                <asp:LinkButton
                                    ID="btnReceive"
                                    runat="server"
                                    CommandName="Receive"
                                    CommandArgument='<%# Eval("OrderId") %>'
                                    CssClass="btn btn-sm btn-success"
                                    Visible='<%# Convert.ToInt32(Eval("Status")) == 0 %>'
                                    Text="קבל" />
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
        function ConfirmDelete() {
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

