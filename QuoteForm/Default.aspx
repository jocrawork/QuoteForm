 <%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuoteForm._Default" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <script type="text/javascript">
        function checkTaxStatus(ddl)
        {
            //alert("function firing");

            if (ddl.value == "exempt")
                $('#taxAlert').show();
            else $('#taxAlert').hide();
        }

    </script>
    
    <div class="well">
        <span>
            <asp:Textbox runat="server" ID="Owner" type="text" placeholder="Prepared By: " class="required"/>
            <asp:Textbox runat="server" ID="Date" type="text"  placeholder="Date (MM/dd/YYYY)" style="text-align:right"/> 
           
        </span>
        <span style="float:right">
            <asp:Textbox runat="server" ID="QuoteLength" type="text" placeholder="Quote Length (Days)"/>
            <asp:DropDownList ID="PaymentTermsDDL" name="Source" runat="server">
                <asp:ListItem Value=null>--Select Payment Terms--</asp:ListItem>
                <asp:ListItem Value="Pre-pay">Pre-pay</asp:ListItem>
                <asp:ListItem Value="Partial">Partial</asp:ListItem>
                <asp:ListItem Value="Net30">Net30</asp:ListItem>
            </asp:DropDownList>
        </span>
        
        

    </div>
    
    <div class="row" id="buyerInfo">
        <div class="well col-md-4">
            <asp:Label class="h3" runat="server">Customer Address</asp:Label> <br />
            <asp:Textbox runat="server" ID="CustContact" type="text" placeholder="Contact" class="required"/>
            <asp:Textbox runat="server" ID="CustCompany" type="text" placeholder="Company" class="required"/>
            <asp:Textbox runat="server" ID="CustAddress1" type="text" placeholder="Address 1"/>
            <asp:Textbox runat="server" ID="CustAddress2" type="text" placeholder="Address 2"/>
            <asp:Textbox runat="server" ID="CustCityState" type="text" placeholder="City, State, Zip"/>
            <asp:Textbox runat="server" ID="CustFax" type="text" placeholder="Fax"/>
            <asp:Textbox runat="server" ID="CustPhone" type="text" placeholder="Phone"/>
            <asp:Textbox runat="server" ID="CustEmail" type="text" placeholder="Email"/>
        </div>
        <div class="well col-md-4">
            <asp:Label class="h3" runat="server">Billing Address</asp:Label> <asp:CheckBox runat="server" ID="BillingSame" Text="Same as Customer" AutoPostBack="true" OnCheckedChanged="BillingAutoFill"/><br />
            <asp:Textbox runat="server" ID="BillContact" type="text" placeholder="Contact"/>
            <asp:Textbox runat="server" ID="BillCompany" type="text" placeholder="Company"/>
            <asp:Textbox runat="server" ID="BillAddress1" type="text" placeholder="Address 1"/>
            <asp:Textbox runat="server" ID="BillAddress2" type="text" placeholder="Address 2"/>
            <asp:Textbox runat="server" ID="BillCityState" type="text" placeholder="City, State, Zip"/>
            <asp:Textbox runat="server" ID="BillFax" type="text" placeholder="Fax"/>
            <asp:Textbox runat="server" ID="BillPhone" type="text" placeholder="Phone"/>
            <asp:Textbox runat="server" ID="BillEmail" type="text" placeholder="Email"/>
        </div>
        <div class="well col-md-4">
            <asp:Label class="h3" runat="server">Shipping Address</asp:Label> <asp:CheckBox runat="server" ID="ShippingSame" Text="Same as Customer" AutoPostBack="true" OnCheckedChanged="ShippingAutoFill"/><br />
            <asp:Textbox runat="server" ID="ShipContact" type="text" placeholder="Contact"/>
            <asp:Textbox runat="server" ID="ShipCompany" type="text" placeholder="Company"/>
            <asp:Textbox runat="server" ID="ShipAddress1" type="text" placeholder="Address 1"/>
            <asp:Textbox runat="server" ID="ShipAddress2" type="text" placeholder="Address 2"/>
            <asp:Textbox runat="server" ID="ShipCityState" type="text" placeholder="City, State, Zip"/>
            <asp:Textbox runat="server" ID="ShipFax" type="text" placeholder="Fax"/>
            <asp:Textbox runat="server" ID="ShipPhone" type="text" placeholder="Phone"/>
            <asp:Textbox runat="server" ID="ShipEmail" type="text" placeholder="Email"/>
        </div>
    </div>

    <!-- ALL VALIDATION -->
    <asp:RequiredFieldValidator 
        ID="CustContactValidator" 
        ControlToValidate="CustContact"
        Display="None"
        runat="server" 
        ErrorMessage="Customer Contact is required"/>
    <asp:RequiredFieldValidator 
        ID="CustCompanyValidator" 
        ControlToValidate="CustCompany"
        Display="None"
        runat="server" 
        ErrorMessage="Customer Company is required"/>
    <asp:RequiredFieldValidator 
        ID="OwnerValidator" 
        ControlToValidate="Owner"
        Display="None"
        runat="server" 
        ErrorMessage="Owner is required"/>
    <asp:RequiredFieldValidator 
        ID="TaxStatusValidator" 
        ControlToValidate="TaxStatusDDL"
        InitialValue=null
        Display="None"
        runat="server" 
        ErrorMessage="Tax Status is required"/>

    <asp:ValidationSummary
        DisplayMode="BulletList"
        runat="server"
        ShowMessageBox="True"
        ShowSummary="False"
        HeaderText="The following fields are required" />



    <div class="well center" style="text-align:center"> 
        <div style="text-align:center">
            <asp:DropDownList ID="SourceDDL" name="Source" runat="server">
                <asp:ListItem Value=null>--Select a Source--</asp:ListItem>
                <asp:ListItem Value="Customer">Customer</asp:ListItem>
                <asp:ListItem Value="Cold Call">Cold Call</asp:ListItem>
                <asp:ListItem Value="Lead">Lead</asp:ListItem>
                <asp:ListItem Value="Mailing">Mailing</asp:ListItem>
                <asp:ListItem Value="Newsletter">Newsletter</asp:ListItem>
                <asp:ListItem Value="Referral">Referral</asp:ListItem>
                <asp:ListItem Value="Web">Web</asp:ListItem>
                <asp:ListItem Value="Seminar">Seminar</asp:ListItem>
                <asp:ListItem Value="Training">Training</asp:ListItem>
                <asp:ListItem Value="Magazine">Magazine</asp:ListItem>
                <asp:ListItem Value="Warm Call">Warm Call</asp:ListItem>
                <asp:ListItem Value="Trade Show">Trade Show</asp:ListItem>
                <asp:ListItem Value="Telemarketing">Telemarketing</asp:ListItem>
                <asp:ListItem Value="Database">Database</asp:ListItem>
                <asp:ListItem Value="Partner">Partner</asp:ListItem>
            </asp:DropDownList>
            <asp:Textbox runat="server" ID="SpecificSource" type="text" placeholder="Specific Source"/>
            <asp:Textbox runat="server" ID="LocationCount" type="text" placeholder="# of Locations"/>
            <asp:Textbox runat="server" ID="POSProvidor" type="text" placeholder="POS Provider"/>
            <asp:Textbox runat="server" ID="InstallDate" type="text" placeholder="Install Date"/>
            <asp:DropDownList ID="BusinessUnitDDL" name="BusinessUnit" runat="server">
                <asp:ListItem Value=null>--Select a Business Unit--</asp:ListItem>
                <asp:ListItem Value="Atlanta">Atlanta</asp:ListItem>
                <asp:ListItem Value="Brand">Brand</asp:ListItem>
                <asp:ListItem Value="Direct">Direct</asp:ListItem>
                <asp:ListItem Value="Entertainment">Entertainment</asp:ListItem>
                <asp:ListItem Value="Global">Global</asp:ListItem>
                <asp:ListItem Value="LASA">LASA</asp:ListItem>
                <asp:ListItem Value="Quest">Quest</asp:ListItem>
                <asp:ListItem Value="Reseller">Reseller</asp:ListItem>
                <asp:ListItem Value="TDS">TDS</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="clearfix" style="padding-top:10px; padding-bottom:10px">
            <span style="float:left">
                <asp:Button class="btn btn-success float-left" ID="NewQuoteB1" runat="server" Text="New" OnClick="NewQuote"/>
                <asp:Button class="btn btn-primary float:left" ID="CopyQuoteB1" runat="server" Text="Copy" OnClick="CopyQuote"/>
            </span>
            <asp:CheckBox ID="NewLocation" runat="server" text="New Location"/>
            <asp:DropDownList ID="TaxStatusDDL" name="BusinessUnit" runat="server" onchange="checkTaxStatus(this)">
                <asp:ListItem Value=null>--Select Tax Status--</asp:ListItem>
                <asp:ListItem Value="taxable">Taxable</asp:ListItem>
                <asp:ListItem Value="exempt">Tax Exempt</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="Dealer" runat="server" text="Dealer"/>
            <span style="float:right">
                <asp:Button class="btn btn-warning float-left" ID="SaveQuoteB1" runat="server" Text="Save" OnClick="SaveQuote"/>
                <asp:Button class="btn btn-danger float-right" ID="PrintQuoteB1" runat="server" Text="PDF" OnClick="PDFQuote"/>
            </span>
        </div>
        <div id="taxAlert" class="alert alert-danger" style="display:none">
            <p>Please inform the customer that they will need to provide Exemption Certificate to Accounting before the order can be processed</p>
        </div>
    </div>

    <!-- faux button to trick repeaters into using javascript -->
    <asp:Button ID="AddLine" runat="server" style="display: none" />

    <h3 style="text-align:center">Hardware</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repHW" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="Hardware" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="Hardware" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadHardwareProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddHardware" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>    
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3 style="text-align:center">Accessories</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repAcc" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="Accessories" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteAccessories" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="Accessories" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadAccessoriesProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddAccessories" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3 style="text-align:center">Software</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repSW" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="Software" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteSoftware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="Software" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadSoftwareProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddSoftware" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3 style="text-align:center">Content</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repCC" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="ContentCreation" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteContent" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="ContentCreation" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadContentProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddContent" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>      
    </div>
    
    <h3 style="text-align:center">Installation</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repInst" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="Installation" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteInstall" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="Installation" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadInstallProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddInstall" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>   
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <h3 style="text-align:center">Recurring</h3>
    <div id="RecurringAlert" style="color:red; text-align:center"> <!-- style="display:none" --> 
            <p>Unit Price is on a per month basis. ie: For a year long item, put 12 for the Quantity</p>
        </div>

    <div class="well" style="width:100%">
        <asp:Repeater ID="repRec" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Monthly Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <asp:HiddenField ID="Category" Value="Recurring" runat="server"/>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteRecurring" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <asp:HiddenField ID="AddCategory" Value="Recurring" runat="server"/>
                        <td><cc1:ComboBox runat="server" ID="AddProduct" AutoCompleteMode="SuggestAppend" AutoPostBack="true" OnSelectedIndexChanged="ProductSelected" OnDataBinding="LoadRecurringProducts"/></td>
                        <td><asp:TextBox runat="server" ID="AddPartNumber"/></td>
                        <td><asp:TextBox runat="server" ID="AddUnitPrice"/></td>
                        <td><asp:TextBox runat="server" ID="AddQuantity"/></td>
                        <td><asp:Button class="btn btn-success" ID="AddRecurring" runat="server" Text="Add" CommandName="Add" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
                </table>   
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3 style="text-align:center">Totals</h3>
    <div class="well" style="text-align:center">
        <asp:Table runat="server" ID="TotalsTable" Width="100%">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Hardware</asp:TableHeaderCell>
                <asp:TableHeaderCell>Accessories</asp:TableHeaderCell>
                <asp:TableHeaderCell>Software</asp:TableHeaderCell>
                <asp:TableHeaderCell>Content Creation</asp:TableHeaderCell>
                <asp:TableHeaderCell>Installation</asp:TableHeaderCell>
                <asp:TableHeaderCell>Recurring</asp:TableHeaderCell>
                <asp:TableHeaderCell>TOTAL</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="HWtotalCell"></asp:TableCell>
                <asp:TableCell ID="ACCtotalCell"></asp:TableCell>
                <asp:TableCell ID="SWtotalCell"></asp:TableCell>
                <asp:TableCell ID="CCtotalCell"></asp:TableCell>
                <asp:TableCell ID="INSTtotalCell"></asp:TableCell>
                <asp:TableCell ID="RECtotalCell"></asp:TableCell>
                <asp:TableCell ID="TotalCell"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <span>
            <asp:TextBox runat="server" ID="Freight" placeholder="Freight Charges"/>
            <asp:TextBox runat="server" ID="SalesTax" placeholder="Sales Tax"/>
        </span>
    </div>

    <div class="well" style="text-align:center; width:100%">
        <span style="float:left">
            <asp:Button class="btn btn-success" ID="NewQuoteB2" runat="server" Text="New" OnClick="NewQuote"/>
            <asp:Button class="btn btn-primary" ID="CopyQuoteB2" runat="server" Text="Copy" OnClick="CopyQuote"/>
        </span>
        <span style="text-align:center">
            <textarea id="InternalNotes" cols="20" rows="3" runat="server" style="width:30%" placeholder="Internal Notes - For Accounting/Admin &#10;Not visible in PDF"></textarea>
            <textarea id="ExternalNotes" cols="20" rows="3" runat="server" style="width:30%" placeholder="External Notes - For Customer/All &#10;Included in PDF"></textarea>
        </span>
        <span style="float:right">
            <asp:Button class="btn btn-warning" ID="SaveQuoteB2" runat="server" Text="Save" OnClick="SaveQuote"/>
            <asp:Button class="btn btn-danger" ID="PrintQuoteB2" runat="server" Text="PDF" OnClick="PDFQuote"/>
        </span>
    </div>

</asp:Content>
        