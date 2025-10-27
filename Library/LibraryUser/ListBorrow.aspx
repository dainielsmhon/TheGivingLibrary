<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryUser/UserMaster.Master" AutoEventWireup="true" CodeBehind="ListBorrow.aspx.cs" Inherits="Library.LibraryUser.ListBorrow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- טעינת עיצובי DataTables -->
    <link rel="stylesheet" href="css/dataTables.bootstrap4.css">
    <link rel="stylesheet" href="css/StyleD2.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1>רשימת השאלות</h1>

    <div class="card-body">
        <!-- טבלת הנתונים -->
        <table class="table datatables" id="MainTbl">
            <thead>
                <tr>
                    <th>מספר השאלה</th>
                    <th>מספר ספר</th>
                    <th>שם ספר</th>
                    <th>ת"ז משאיל</th>
                    <th>תאריך השאלה</th>
                    <th>תאריך חזרה משוער</th>
                    <th>תאריך החזרה בפועל</th>
                    <th>סטטוס</th>
                    <th>הערות</th>
                    <th>פעולות</th>
                </tr>
            </thead>

            <tbody>
                <!-- רפיטר המציג את רשימת ההשאלות -->
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

                            <!-- שינוי טקסט הסטטוס -->
                            <td><%# (Convert.ToInt32(Eval("Status")) == 0 ? "מושאל" : "הוחזר") %></td>

                            <td><%# Eval("Notse") %></td>

                            <!-- עמודת פעולות -->
                            <td>
                                <!-- כפתור החזרה מוצג רק אם הספר מושאל (Status = 0) -->
                                <asp:LinkButton 
                                    ID="btnReturn" 
                                    runat="server"
                                    CssClass="btn btn-sm btn-primary"
                                    CommandName="Return"
                                    CommandArgument='<%# Eval("BorrowId") %>'
                                    Text="החזרה"
                                    Visible='<%# Convert.ToInt32(Eval("Status")) == 0 %>'>
                                </asp:LinkButton>
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
            // הפעלת DataTables עם תמיכה בעברית
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

                // הוספת רקע שקוף לטבלה אם לא קיים
                if (!tbl.hasClass("table-bg")) {
                    tbl.addClass("table-bg");
                }
            });
        });
    </script>
</asp:Content>
