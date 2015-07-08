<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="QuoteForm.Products" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- disabled EventValidation above, probably a bit of a hack -->
    
    <div class="well" style="text-align:center">    
        <asp:DropDownList ID="CategoryDDL" runat="server">
 
            </asp:DropDownList>

        <!--<asp:ListItem Value=null>--Select a Category--</asp:ListItem>
                <asp:ListItem Value="Media Players">Media Players</asp:ListItem>
                <asp:ListItem Value="Indoor Displays">Indoor Displays</asp:ListItem>
                <asp:ListItem Value="Outdoor Displays">Outdoor Displays</asp:ListItem>

                <asp:ListItem Value="Data Cables">Data Cables</asp:ListItem>
                <asp:ListItem Value="Audio/Video Cables">Audio/Video Cables</asp:ListItem>
                <asp:ListItem Value="Extenders/Converters">Extenders/Converters</asp:ListItem>
                <asp:ListItem Value="Splitters">Splitters</asp:ListItem>
                <asp:ListItem Value="Speakers">Speakers</asp:ListItem>
                <asp:ListItem Value="UPS Batteries">UPS Batteries</asp:ListItem>
                <asp:ListItem Value="Install Accessories">Install Accessories</asp:ListItem>
                <asp:ListItem Value="Mounts & Accessories">Mounts & Accessories</asp:ListItem>

                <asp:ListItem Value="Software - VitalCast">Software - VitalCast</asp:ListItem>
                <asp:ListItem Value="Software - Quickcom">Software - Quickcom</asp:ListItem>
                <asp:ListItem Value="Software - Dashboard">Software - Dashboard</asp:ListItem>

                <asp:ListItem Value="Content Creation">Content Creation</asp:ListItem>
                <asp:ListItem Value="Hosting Services">Hosting Services</asp:ListItem>

                <asp:ListItem Value="Installation">Installation</asp:ListItem>
                <asp:ListItem Value="Warranties">Warranties</asp:ListItem> -->

            <asp:TextBox ID="Name" runat="server" placeholder="Name"></asp:TextBox>
        <asp:TextBox ID="PartNumber" runat="server" placeholder="Part Number"></asp:TextBox>
        <asp:TextBox ID="Price" runat="server" placeholder="Price"></asp:TextBox>
        <asp:TextBox ID="Cost" runat="server" placeholder="Cost"></asp:TextBox>

        <asp:Button class="btn btn-success" ID="Add" runat="server" Text="Add" OnClick="Add_Click" />
    </div>

    <div>
        <!-- TODO: make this filterable -->
        <asp:Repeater ID="repProducts" runat="server" OnItemCommand="repProducts_ItemCommand"> 
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Category</td><td>Name</td><td>Part Number</td><td>Price</td><td>Cost</td><td>Modify</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>'/>
                        <td><asp:Label ID="Category" runat="server" Text='<%#Eval("Category") %>' /></td>
                        <td><asp:Label ID="Name" runat="server" Text='<%#Eval("Name") %>' /></td>
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%#Eval("PartNumber") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%#Eval("Price") %>' /></td>
                        <td><asp:Label ID="Cost" runat="server" Text='<%#Eval("Cost") %>' /></td>
                        <td><asp:Button ID="Edit" runat="server" Text="Edit" class="btn btn-primary" CommandName="Edit" disabled="disabled" CommandArgument='<%# Eval("Id") %>'/>
                            <asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
