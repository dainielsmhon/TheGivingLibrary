<%@ Page Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.master" AutoEventWireup="true" CodeBehind="ListSupplier.aspx.cs" Inherits="Library.LibraryAdmin.ListSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center">
            <div class="col-12">
                <h3 class="mb-4">רשימת ספקים</h3>
                <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" 
                              OnRowCommand="gvSuppliers_RowCommand" 
                              OnRowDeleting="gvSuppliers_RowDeleting" 
                              class="table table-bordered" DataKeyNames="SupplierId">
                    <Columns>
                        <asp:BoundField DataField="SupplierId" HeaderText="ID" SortExpression="SupplierId" Visible="False" />
                        <asp:BoundField DataField="SupplierName" HeaderText="שם ספק" SortExpression="SupplierName" />
                        <asp:BoundField DataField="SAddress" HeaderText="כתובת" SortExpression="SAddress" />
                        <asp:BoundField DataField="SPhone" HeaderText="טלפון" SortExpression="SPhone" />
                        <asp:BoundField DataField="SEmail" HeaderText="מייל" SortExpression="SEmail" />

                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnEdit" runat="server" Text="ערוך" CommandName="Edit" 
                                            CommandArgument='<%# Eval("SupplierId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnRemove" runat="server" Text="מחק" CommandName="Delete" 
                                            CommandArgument='<%# Eval("SupplierId") %>' OnClick="BtnRemove_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
</asp:Content>
