<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryUser/UserMaster.Master" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="Library.LibraryUser.AddOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header">
            <strong class="card-title">הזמנת ספרים</strong>
        </div>

        <div class="card-body">
            <asp:HiddenField ID="HidBookId" runat="server" />
            <div class="form-row">
                <!-- אני בוחר ספק מהרשימה זה מתרענן ונותן רשימת ספרים ששייכים לספק -->
                <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                    <asp:ListItem Text="בחר ספק" Value="0"></asp:ListItem>
                </asp:DropDownList>


                <br />
                <br />


                <!-- הרשימה השנייה: בוחר ספר מתוך רשימה שתעודכן לפי הספק שנבחר -->
                <asp:DropDownList ID="ddlBooks" runat="server" CssClass="form-control">
                    <asp:ListItem Text="בחר ספר" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-6 mb-3">
                <label for="TxtQuantity">כמות הספרים</label>
                <div class="input-group">
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-danger" id="btnDecrease" onclick="decreaseQuantity()">-</button>
                    </span>

                    <!-- שדה טקסט שמציג את הכמות הנוכחית של הספרים -->
                    <asp:TextBox ID="TxtQuantity" runat="server" CssClass="form-control text-center" Text="1" Width="50px"  />

                    <span class="input-group-btn">
                        <button type="button" class="btn btn-success" id="btnIncrease" onclick="increaseQuantity()">+</button>
                    </span>
                </div>
            </div>

            <!-- כפתור לשמירה של ההזמנה -->
            <asp:Button ID="BtnSave" runat="server" Text="שמור" class="btn btn-success" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ScriptCnt" runat="server">
    <script type="text/javascript">
        // פונקציה להגדלת הכמות
        function increaseQuantity() {
            var quantity = document.getElementById('<%= TxtQuantity.ClientID %>').value;
            quantity = parseInt(quantity) + 1;
            document.getElementById('<%= TxtQuantity.ClientID %>').value = quantity;
        }

        // פונקציה להקטנת הכמות
        function decreaseQuantity() {
            var quantity = document.getElementById('<%= TxtQuantity.ClientID %>').value;
            if (quantity > 1) {
                quantity = parseInt(quantity) - 1;
                document.getElementById('<%= TxtQuantity.ClientID %>').value = quantity;
            }
        }
    </script>
</asp:Content>
