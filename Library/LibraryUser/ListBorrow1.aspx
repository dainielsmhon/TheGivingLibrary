<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="ListBorrow1.aspx.cs" Inherits="Library.LibraryAdmin.ListBorrow" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/dataTables.bootstrap4.css">
    <link rel="stylesheet" href="css/StyleD2.css">
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
                    <th>סטטוס</th>
                    <th>הערות</th>
                    <th>פעולות</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="rptBorrow_ItemCommand">
                    <ItemTemplate>
                        <tr class='<%# "status-" + Eval("Status") %>'>
                            <td><%# Eval("BorrowId") %></td>
                            <td><%# Eval("BookId") %></td>
                            <td><%# Eval("BookName") %></td>
                            <td><%# Eval("UserId") %></td>
                            <td><%# Eval("BorrowDate") %></td>
                            <td><%# Eval("ReturnDatePlan") %></td>
                            <td><%# Eval("ActualReturnDate") %></td>

                            <%-- שינוי טקסט סטטוס: 0 = מושאל, 1 = הוחזר --%>
                            <td><%# (Convert.ToInt32(Eval("Status")) == 0 ? "מושאל" : "הוחזר") %></td>

                            <td><%# Eval("Notse") %></td>
                            <td>
                                <%# (Convert.ToInt32(Eval("Status")) == 0) ? 
                                    "<a id='LinkAddReturn' runat='server' class='btn btn-sm btn-primary' href='#' CommandName='Return' CommandArgument='" + Eval("BorrowId") + "'>החזרה</a>" : "" 
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
                        ordering: false,
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
