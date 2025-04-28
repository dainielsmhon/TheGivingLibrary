<%@ Page Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.master" AutoEventWireup="true" CodeBehind="ListSupplier.aspx.cs" Inherits="Library.LibraryAdmin.ListSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="assets/css/dataTables.bootstrap4.min.css">
<%--<link rel="stylesheet" href="assets/css/StyleD2.css">--%>





</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת ספקים</h1>
    <div class="card-body">
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>מספר ספק</th>
                    <th>שם ספק</th>
                    <th>מייל</th>
                    <th>כתובת</th>
                    <th>טלפון</th>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>

                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("SupplierId") %></td>
                            <td><%# Eval("SupplierName") %></td>
                            <td><%# Eval("SEmail") %></td>
                            <td><%# Eval("SAddress") %></td>
                            <td><%# Eval("SPhone") %></td>
                            <td>
                                <a class="btn btn-sm btn-primary" href='AddSupplier.aspx?SupplierId=<%# Eval("SupplierId") %>'>עריכה</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/dataTables.bootstrap4.min.js"></script>
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
