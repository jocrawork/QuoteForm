<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="QuoteForm.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>USER MANAGEMENT</h3>

    <asp:Repeater runat="server" id="repUsers">
        <HeaderTemplate>
            <table style="width:100%">
                <tr style="font-weight:bold">
                    <td>User</td><td>Sales</td><td>Products</td><td>Accounting</td><td>Admin</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr>
                    <td><asp:Label ID="User" runat="server" Text='<%# Eval("Email") %>'/></td>
                    <td><asp:CheckBox ID="Sales" runat="server" /></td>
                    <td><asp:CheckBox ID="Products" runat="server" /></td>
                    <td><asp:CheckBox ID="Accounting" runat="server" /></td>
                    <td><asp:CheckBox ID="Admin" runat="server" /></td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
