<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="QuoteForm.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>USER MANAGEMENT</h3>

    <asp:DropDownList runat="server" ID="rolesDDL" OnSelectedIndexChanged="LoadUsersInRole" AutoPostBack="true">  
    </asp:DropDownList>
    <asp:DropDownList ID="usersDDL" runat="server" />
    <asp:Button ID="Save" runat="server" OnClick="AddToRole" text="Add" class="btn btn-success"/>

    <div>
        <asp:Repeater runat="server" id="repUsers" OnItemCommand="repUsers_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight:bold">
                        <td>User Name</td><td>Email</td><td>Delete</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Label ID="User" runat="server" Text='<%# Eval("UserName") %>'/></td>
                    <td><asp:Label ID="Email" runat="server" Text='<%# Eval("Email") %>' /></td>
                    <td><asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Remove" CommandArgument='<%# Eval("Email") %>' /><td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <!-- PICK ROLE, LOAD USERS, ADD USER TEXTBOX -->

    <!-- ADD NEW ROLE TO DB
    <div style="display:none">
        <asp:TextBox ID="NewRoleName" runat="server" placeholder="Add Role"/>
        <asp:Button ID="AddRoleBtn" runat="server" OnClick="AddRole" text="Save"/>
    </div>
    -->

</asp:Content>
