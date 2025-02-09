<%@ Page Title="" Language="C#" MasterPageFile="~/LibraryAdmin/NewAdminMaster.Master" AutoEventWireup="true" CodeBehind="AddBorrow.aspx.cs" Inherits="Library.LibraryAdmin.AddBorrow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
     <h1>פרטי הספר</h1>
    
    <div>
        <strong>מספר מזהה (ID):</strong>
        <asp:TextBox ID="TxtBookId" runat="server" placeholder="הכנס ID ספר"></asp:TextBox>
        <asp:Button ID="BtnCheckBook" runat="server" Text="בדוק ספר" OnClick="BtnCheckBook_Click" /><br /><br />
    </div>
  
   <!-- הצגת פרטי הספר -->
    <div>
        <strong>שם:</strong> <%# Eval("BookName") %><br />
        <strong>מחבר:</strong> <%# Eval("BookAuthor") %><br />
        <strong>שנה:</strong> <%# Eval("Year") %><br />
        <strong>תיאור:</strong> <%# Eval("BookDescription") %><br />
        <strong>שפה:</strong> <%# Eval("BookLang") %><br />
        <strong>מיקום:</strong> <%# Eval("Location") %><br />
        <strong>כמות זמינה:</strong> <%# Eval("AvailableQuantity") %><br />
        <strong>סטטוס:</strong> <%# Eval("Status") %><br />
    </div>

    <!-- כפתורים -->
    <div>
        <asp:Button ID="BtnBorrow" runat="server" Text="השאלת ספר" OnClick="BtnBorrow_Click" />
        <asp:Button ID="BtnReturn" runat="server" Text="החזרת ספר" OnClick="BtnReturn_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
</asp:Content>
