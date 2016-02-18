<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="QuoteForm.Products" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- disabled EventValidation above, probably a bit of a hack -->
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.quicksearch.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input#Filter').quicksearch('#products tbody tr');
        });

        function EmptyFieldCheck()
        {
            var emptyFlag = true;

            if ($("#AddProduct").find("#CategoryDDL").val() == "Default" ||
                        $("#AddProduct").find("#Name").val() == "" ||
                        $("#AddProduct").find("#PartNumber").val() == "" ||
                        $("#AddProduct").find("#Price").val() == "" ||
                        $("#AddProduct").find("#Cost").val() == "" ||
                        $("#AddProduct").find("#DefaultQuantity").val() == "") {
                $('#EmptyFieldAlert').show();
                emptyFlag = false;
            }

            return emptyFlag;
        }
    </script>

    <%if(User.IsInRole("Admin")||User.IsInRole("Products")){ %>
    <div ID="AddProduct" class="well" style="text-align:center">    
        <asp:DropDownList ID="CategoryDDL" runat="server" ClientIdMode="Static"/>
        <asp:TextBox ID="Name" runat="server" placeholder="Name" ClientIdMode="Static"/>
        <asp:TextBox ID="PartNumber" runat="server" placeholder="Part Number" ClientIdMode="Static"/>
        <asp:TextBox ID="Price" runat="server" placeholder="Price" ClientIdMode="Static"/>
        <asp:TextBox ID="Cost" runat="server" placeholder="Cost" ClientIdMode="Static"/>
        <asp:TextBox ID="DefaultQuantity" runat="server" placeholder="Quantity" ClientIdMode="Static"/>

        <asp:Button class="btn btn-success" ID="Add" runat="server" Text="Add" OnClick="Add_Click" onClientClick="return EmptyFieldCheck();"/>

        <div id="EmptyFieldAlert" class="alert alert-danger" style="display:none" runat="server" ClientIDMode="Static">
            <p>Please fill in all fields before trying to add a new Product!</p>
        </div>
    </div>
    <%} %>
    <div>
        <input type="text" id="Filter" placeholder="Search"/>
    </div>

    <div>
        <!-- TODO: make this filterable -->
        <asp:Repeater ID="repProducts" runat="server" OnItemCommand="repProducts_ItemCommand"> 
            <HeaderTemplate>
                <table id=products style="width:100%">
                    <thead>
                        <tr style="font-weight: bold"><td>Category</td><td>Name</td><td>Part Number</td><td>Price</td><td>Cost</td><td>Default Quantity</td><%if(User.IsInRole("Admin")||User.IsInRole("Products")){ %><td>Delete</td><%} %></tr>
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
                        <%if(User.IsInRole("Admin")||User.IsInRole("Products")){ %>
                        <td><asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'/></td>
                        <%} %>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
