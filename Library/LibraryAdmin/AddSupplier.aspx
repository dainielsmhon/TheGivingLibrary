<%@ Page Title="הוספת / עריכת ספק" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddSupplier.aspx.cs" Inherits="Library.LibraryAdmin.AddSupplier" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="card shadow mb-4">
        <div class="card-header">
            <strong class="card-title">הוספת / עריכת ספק</strong>
        </div>
        <div class="card-body">
            <asp:HiddenField ID="HidSupplierId" runat="server" />

            <div class="form-group">
                <label for="TxtSupplierName">שם ספק</label>
                <asp:TextBox ID="TxtSupplierName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierAddress">כתובת ספק</label>
                <asp:TextBox ID="TxtSupplierAddress" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierPhone">טלפון ספק</label>
                <asp:TextBox ID="TxtSupplierPhone" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierWeb">אתר אינטרנט</label>
                <asp:TextBox ID="TxtSupplierWeb" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierEmail">מייל ספק</label>
                <asp:TextBox ID="TxtSupplierEmail" runat="server" TextMode="Email" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierContact">איש קשר</label>
                <asp:TextBox ID="TxtSupplierContact" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="TxtSupplierAdded">תאריך הוספה</label>
                <asp:TextBox ID="TxtSupplierAdded" runat="server" TextMode="Date" CssClass="form-control" />
            </div>

            <asp:Button ID="BtnSave" runat="server" Text="שמירה" OnClick="BtnSave_Click" class="btn btn-success" />
        </div>
    </div>
</asp:Content>
