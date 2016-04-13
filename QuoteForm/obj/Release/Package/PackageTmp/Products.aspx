<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="QuoteForm.Products" EnableEventValidation="false" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- disabled EventValidation above, probably a bit of a hack -->
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <link href="Content/DataTables/css/jquery.dataTables.css" rel="stylesheet"/>
    <script src="scripts/DataTables/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#products').DataTable({
                "bJQueryUI"     : true,
                "bInfo"         : false,
                "iDisplayLength": 25,
                "sLengthMenu": false
            });
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
        <asp:HiddenField ID="HiddenFieldAdd" runat="server" Value=""/>
        <asp:DropDownList ID="CategoryDDL" runat="server" ClientIdMode="Static"/>
        <asp:TextBox ID="Name" runat="server" placeholder="Name" ClientIdMode="Static"/>
        <asp:TextBox ID="PartNumber" runat="server" placeholder="Part Number" ClientIdMode="Static"/>
        <asp:TextBox ID="Price" runat="server" placeholder="Price" ClientIdMode="Static"/>
        <asp:TextBox ID="Cost" runat="server" placeholder="Cost" ClientIdMode="Static"/>
        <asp:TextBox ID="DefaultQuantity" runat="server" placeholder="Quantity" ClientIdMode="Static"/>

        <asp:LinkButton class="btn btn-success" ID="Add" runat="server" OnClick="Add_Click" onClientClick="return EmptyFieldCheck();"><span class="glyphicon glyphicon-floppy-disk"/></asp:LinkButton>

        <div id="EmptyFieldAlert" class="alert alert-danger" style="display:none" runat="server" ClientIDMode="Static">
            <p>Please fill in all fields before trying to add a new Product!</p>
        </div>
    </div>
    <%} %>

    <div>
        <!-- TODO: make this filterable -->
        <asp:Repeater ID="repProducts" runat="server" OnItemCommand="repProducts_ItemCommand"> 
            <HeaderTemplate>
                <table id=products style="width:100%" class="hover stripe compact">
                    <thead>
                        <tr style="font-weight: bold"><td>Category</td><td>Name</td><td>Part Number</td><td>Price</td><td>Cost</td><td>Quantity</td><%if(User.IsInRole("Admin")||User.IsInRole("Products")){ %><td>Options</td><%} %></tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="HiddenFieldTable" runat="server" Value='<%#Eval("Id") %>'/>
                        <td><asp:Label ID="Category" runat="server" Text='<%#Eval("Category") %>' /></td>
                        <td><asp:Label ID="Name" runat="server" Text='<%#Eval("Name") %>' /></td>
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%#Eval("PartNumber") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%#Eval("Price", "{0:C2}") %>' /></td>
                        <td><asp:Label ID="Cost" runat="server" Text='<%#Eval("Cost", "{0:C2}") %>' /></td>
                        <td><asp:Label ID="DefaultQuantity" runat="server" Text='<%#Eval("DefaultQuantity") %>' /></td>
                        <%if(User.IsInRole("Admin")||User.IsInRole("Products")){ %>
                        <td><asp:LinkButton ID="Edit" runat="server" class="btn btn-warning" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'><span class="glyphicon glyphicon-pencil"/></asp:LinkButton>
                            <asp:LinkButton ID="Delete" runat="server" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'><span class="glyphicon glyphicon-trash"/></asp:LinkButton>
                        </td>
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
