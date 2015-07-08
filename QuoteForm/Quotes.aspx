<%@ Page Title="Quotes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quotes.aspx.cs" Inherits="QuoteForm.Quotes" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- TODO: make this filterable -->
    <asp:Repeater ID="repQuotes" runat="server" OnItemCommand="repQuotes_ItemCommand"> 
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Customer</td><td>Company</td><td>Date</td><td>Owner</td><td>Total</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>'/>
                        <td><asp:LinkButton ID="QuoteCustomer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Contact") %>' CommandName="Choose" CommandArgument='<%# Eval("Id") %>'/></td>
                        <td><asp:Label ID="QuoteCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Company") %>' /></td>
                        <td><asp:Label ID="QuoteDate" runat="server" Text='<%#Eval("Date") %>' /></td>
                        <td><asp:Label ID="Owner" runat="server" Text='<%#Eval("Owner") %>' /></td>
                        <td><asp:Label ID="QuoteTotal" runat="server" Text='<%#Eval("GrandTotal") %>' /></td>
                        <td><asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
