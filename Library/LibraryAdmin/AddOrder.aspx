<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="Library.LibraryAdmin.AddOrder" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header">
            <strong class="card-title">הזמנת ספרים</strong>
        </div>

        <div class="card-body">
            <!-- תיבה לבחירת ספק -->
            <label>בחר ספק:</label>
            <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                <asp:ListItem Text="בחר ספק" Value="0"></asp:ListItem>
            </asp:DropDownList>

            <br /><br />

            <!-- תיבה לבחירת ספר -->
            <label>בחר ספר:</label>
            <asp:DropDownList ID="ddlBooks" runat="server" CssClass="form-control">
                <asp:ListItem Text="בחר ספר" Value="0"></asp:ListItem>
            </asp:DropDownList>

            <br /><br />

            <!-- שדה להזנת כמות -->
            <div class="col-md-6 mb-3">
                <label for="TxtQuantity">כמות הספרים</label>
                <div class="input-group">
                    <button type="button" class="btn btn-danger" onclick="decreaseQuantity()">-</button>
                    <asp:TextBox ID="TxtQuantity" runat="server" CssClass="form-control text-center" Text="1" Width="70px" />
                    <button type="button" class="btn btn-success" onclick="increaseQuantity()">+</button>
                </div>
            </div>

            <!-- כפתור שמירה -->
            <asp:Button ID="BtnSave" runat="server" Text="שמור הזמנה" CssClass="btn btn-success" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptCnt" runat="server">
    <script type="text/javascript">
        // פונקציה שמגדילה את הכמות
        function increaseQuantity() {
            var q = document.getElementById('<%= TxtQuantity.ClientID %>').value;
            q = parseInt(q) + 1;
            document.getElementById('<%= TxtQuantity.ClientID %>').value = q;
        }

        // פונקציה שמקטינה את הכמות
        function decreaseQuantity() {
            var q = document.getElementById('<%= TxtQuantity.ClientID %>').value;
            if (q > 1) {
                q = parseInt(q) - 1;
                document.getElementById('<%= TxtQuantity.ClientID %>').value = q;
            }
        }
    </script>
</asp:Content>
