<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="Library.LibraryAdmin.AddOrder" %>
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
              
                <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                      <!--  שאני בוחר ספק מהרשימה זה מתרענן ונותן רשימת ספרים ששיכת לספק -->
                    <asp:ListItem Text="בחר ספק" Value="0"></asp:ListItem>
                </asp:DropDownList>

                
                <asp:DropDownList ID="ddlBooks" runat="server" CssClass="form-control">
                    <asp:ListItem Text="בחר ספר" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

           
            <div class="col-md-6 mb-3">
                <label for="TxtQuantity">כמות הספרים</label>
                <div class="input-group">
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-danger" id="btnDecrease" onclick="decreaseQuantity()">-</button>
                        <!--   onclick="decreaseQuantity()   -->
                      
                        <!--   קריאה לפונקציה ב-JavaScript שתפחית את הכמות בלחיצה על הכפתור   -->
                       
                   </span>
                    <!--  כל ID של כפתור זה מזהה יחודי ל JavaScript   -->
                    <asp:TextBox ID="TxtQuantity" runat="server" CssClass="form-control text-center" Text="1" Width="50px" ReadOnly="true" />
                    <!--   ID="TxtQuantity": מזהה השדה שיאפשר לנו לפנות אליו בצד השרת.   -->
                    <!--   הערך מתחיל מ1   -->
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-success" id="btnIncrease" onclick="increaseQuantity()">+</button>
                         
                    </span>
                </div>
            </div>

            
            <asp:Button ID="BtnSave" runat="server" Text="שמור" class="btn btn-success" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
</asp:Content>

<script type="text/javascript">
    // פונקציה להגדלת הכמות
    function increaseQuantity() {
        var quantity = document.getElementById('<%= TxtQuantity.ClientID %>').value;
        //מביא את האלמנט היחודי ID
        // קוד שמתבצע בצד שרת TxtQuantity
        // value ניגש ל TextBox  ומחזיר את הערך הנוכחי  שיש בשדה
        quantity = parseInt(quantity) + 1;//מוסיף 1 לערך הקיים
        document.getElementById('<%= TxtQuantity.ClientID %>').value = quantity;
        //מעדכן את הערך ולא שומר אותו 
    }

    // פונקציה להקטנת הכמות
    function decreaseQuantity() {
        var quantity = document.getElementById('<%= TxtQuantity.ClientID %>').value;
        if (quantity > 1) {
            //רק אם גדול מ 1 יהיה אפשר להוריד
            quantity = parseInt(quantity) - 1;
            document.getElementById('<%= TxtQuantity.ClientID %>').value = quantity;
        }
    }
</script>