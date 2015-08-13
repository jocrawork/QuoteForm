<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="QuoteForm.Products" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- disabled EventValidation above, probably a bit of a hack -->
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.quicksearch.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input#Filter').quicksearch('#products tbody tr');
        });
    </script>

    <div class="well" style="text-align:center">    
        <asp:DropDownList ID="CategoryDDL" runat="server" />
        <asp:TextBox ID="Name" runat="server" placeholder="Name"/>
        <asp:TextBox ID="PartNumber" runat="server" placeholder="Part Number"/>
        <asp:TextBox ID="Price" runat="server" placeholder="Price"/>
        <asp:TextBox ID="Cost" runat="server" placeholder="Cost"/>
        <asp:TextBox ID="DefaultQuantity" runat="server" placeholder="Quantity" />

        <asp:Button class="btn btn-success" ID="Add" runat="server" Text="Add" OnClick="Add_Click" />
    </div>

    <div>
        <input type="text" id="Filter" placeholder="Search"/>
    </div>

    <div>
        <!-- TODO: make this filterable -->
        <asp:Repeater ID="repProducts" runat="server" OnItemCommand="repProducts_ItemCommand"> 
            <HeaderTemplate>
                <table id=products style="width:100%">
                    <thead>
                        <tr style="font-weight: bold"><td>Category</td><td>Name</td><td>Part Number</td><td>Price</td><td>Cost</td><td>Qty</td><td>Modify</td></tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>'/>
                        <td><asp:Label ID="Category" runat="server" Text='<%#Eval("Category") %>' /></td>
                        <td><asp:Label ID="Name" runat="server" Text='<%#Eval("Name") %>' /></td>
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%#Eval("PartNumber") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%#Eval("Price") %>' /></td>
                        <td><asp:Label ID="Cost" runat="server" Text='<%#Eval("Cost") %>' /></td>
                        <td><asp:Label ID="DefaultQuantity" runat="server" Text='<%#Eval("DefaultQuantity") %>' /></td>
                        <td><asp:Button ID="Edit" runat="server" Text="Edit" class="btn btn-primary" CommandName="Edit" disabled="disabled" CommandArgument='<%# Eval("Id") %>'/>
                            <asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
