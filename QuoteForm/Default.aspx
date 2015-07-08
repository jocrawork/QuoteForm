<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuoteForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <div class="well">
        <span>
            <asp:Textbox runat="server" ID="Owner" type="text" placeholder="Prepared By: "/>
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
            <asp:Label class="h3" runat="server">Customer Address</asp:Label><br />
            <asp:Textbox runat="server" ID="CustContact" type="text" placeholder="Contact"/>
            <asp:Textbox runat="server" ID="CustCompany" type="text" placeholder="Company"/>
            <asp:Textbox runat="server" ID="CustAddress1" type="text" placeholder="Address 1"/>
            <asp:Textbox runat="server" ID="CustAddress2" type="text" placeholder="Address 2"/>
            <asp:Textbox runat="server" ID="CustCityState" type="text" placeholder="City, State, Zip"/>
            <asp:Textbox runat="server" ID="CustFax" type="text" placeholder="Fax"/>
            <asp:Textbox runat="server" ID="CustPhone" type="text" placeholder="Phone"/>
            <asp:Textbox runat="server" ID="CustEmail" type="text" placeholder="Email"/>
        </div>
        <div class="well col-md-4">
            <asp:Label class="h3" runat="server">Billing Address</asp:Label><br />
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
            <asp:Label class="h3" runat="server">Shipping Address</asp:Label><br />
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
        <div style="padding-top:10px">
            <span style="float:left">
                <asp:Button class="btn btn-success float-left" ID="NewQuoteB1" runat="server" Text="New" OnClick="NewQuote"/>
                <asp:Button class="btn btn-primary float:left" ID="CopyQuoteB1" runat="server" Text="Copy" OnClick="CopyQuote"/>
            </span>
            <asp:CheckBox ID="NewLocation" runat="server" text="New Location"/>
            <asp:CheckBox ID="Dealer" runat="server" text="Dealer"/>
            <asp:CheckBox ID="TaxExempt" runat="server" text="Tax Exempt"/>
            <span style="float:right">
                <asp:Button class="btn btn-warning float-left" ID="SaveQuoteB1" runat="server" Text="Save" OnClick="SaveQuote"/>
                <asp:Button class="btn btn-danger float-right" ID="PrintQuoteB1" runat="server" Text="PDF" OnClick="PDFQuote"/>
            </span>
        </div>
    </div>

    <asp:Button ID="AddLine" runat="server" style="display: none" />

    <h3>Hardware & Displays</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repHW" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <asp:Button class="btn btn-success" ID="AddHW" runat="server" Text="Add" OnClientClick="$find('mpe').show(); return false;"/>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3>Hardware Accessories</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repAcc" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <asp:Button class="btn btn-success" ID="AddLine" runat="server" Text="Add" OnClientClick="$find('mpe').show(); return false;"/>
            </FooterTemplate>
        </asp:Repeater>

    </div>
    
    <h3>Software & Maintenance/Warranty</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repSW" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <asp:Button class="btn btn-success" ID="AddLine" runat="server" Text="Add" OnClientClick="$find('mpe').show(); return false;"/>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3>Content Creation & Management Services</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repCC" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <asp:Button class="btn btn-success" ID="AddLine" runat="server" Text="Add" OnClientClick="$find('mpe').show(); return false;"/>
            </FooterTemplate>
        </asp:Repeater>       
    </div>
    
    <h3>Installation & Configuration Services</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repInst" runat="server" OnItemCommand="rep_ItemCommand">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Quantity</td><td>Price</td><td>Delete</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> <!--TODO: make this clickable to edit -->
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Button class="btn btn-danger" ID="DeleteHardware" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>'/></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                <asp:Button class="btn btn-success" ID="AddLine" runat="server" Text="Add" OnClientClick="$find('mpe').show(); return false;"/>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <h3>Totals</h3>
    <div class="well">
        
    </div>

    <cc1:ModalPopupExtender ID="ModalPopupHW" BehaviorID="mpe" runat="server" PopupControlID="HardwarePanel" TargetControlID="AddLine"
            CancelControlID="SubmitHardware" BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="HardwarePanel" runat="server" CssClass="Popup" style="padding-top:10px" align="center" BackColor="Gray" Width ="400px">
            <asp:UpdatePanel ID="HardwareUpdatePanel" runat="server"><ContentTemplate>
            <div style="text-align:center">
                <asp:DropDownList ID="HardwareCatDDL" name="Products" runat="server" OnSelectedIndexChanged="HardwareCatDDL_IndexChanged" AutoPostBack="true" />
                  <br />
                <asp:DropDownList ID="HardwareProdDDL" runat="server" OnSelectedIndexChanged="HardwareProdDDL_IndexChanged" AutoPostBack="true">
                    <asp:ListItem Value=null>--Select a Product--</asp:ListItem> 
                </asp:DropDownList>
            </div>
            <br />
            <div style="text-align:center">
                <asp:TextBox ID="PartNumber" type="text" runat="server" placeholder="Part Number" style="width:100px"/>
                <asp:TextBox ID="UnitPrice" type="text" runat="server" placeholder="Price per Unit" style="width:100px"/>
                <asp:TextBox ID="Quantity" type="text" runat="server" placeholder="Quantity" style="width:100px"/>
                <!-- TODO: make warranties checkbox work -->
            </div>
            <br />
            <asp:CheckBox ID="Warranties" runat="server" text="Include Warranties"/>
            <asp:Button class="btn btn-success" ID="SubmitHardware" runat="server" Text="Submit" OnClick="SaveLineItem" style="width:100%" AutoPostBack="true"/> 
        </ContentTemplate></asp:UpdatePanel></asp:Panel>

    <div class="well clearfix" style="text-align:center; width:100%"> <!-- TODO: buttons not inside div -->
        <span style="float:left">
            <asp:Button class="btn btn-success" ID="NewQuoteB2" runat="server" Text="New" OnClick="NewQuote"/>
            <asp:Button class="btn btn-primary" ID="CopyQuoteB2" runat="server" Text="Copy" OnClick="CopyQuote"/>
        </span>
        <span style="float:right">
            <asp:Button class="btn btn-warning" ID="SaveQuoteB2" runat="server" Text="Save" OnClick="SaveQuote"/>
            <asp:Button class="btn btn-danger" ID="PrintQuoteB2" runat="server" Text="PDF" OnClick="PDFQuote"/>
        </span>
    </div>

</asp:Content>
        