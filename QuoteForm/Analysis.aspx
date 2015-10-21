<%@ Page Title="Analysis" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="QuoteForm.Analysis" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if(repHW.Items.Count > 0 ) { %>
    <h3 style="text-align:center">Hardware</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repHW" runat="server">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Unit Cost</td><td>Quantity</td><td>Price</td><td>Total Margin</td><td>Margin %</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> 
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="UnitCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Cost")%>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Label ID="Margin" runat="server" Text='<%# ((double)DataBinder.Eval(Container.DataItem, "Product.Price") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost"))*(int)Eval("Quantity")%>' /></td>
                        <td><asp:Label ID="MarginPercent" runat="server" Text='
                            <%# Math.Round((((double)Eval("Total") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost")*(int)Eval("Quantity"))/(double)Eval("Total"))*100,2)%>' /></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>    
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <% } %>

    <% if(repSW.Items.Count > 0 ) { %>
    <h3 style="text-align:center">Software</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repSW" runat="server">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Unit Cost</td><td>Quantity</td><td>Price</td><td>Total Margin</td><td>Margin %</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> 
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="UnitCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Cost") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Label ID="Margin" runat="server" Text='<%# ((double)DataBinder.Eval(Container.DataItem, "Product.Price") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost"))*(int)Eval("Quantity")%>' /></td>
                        <td><asp:Label ID="MarginPercent" runat="server" Text='
                            <%# Math.Round((((double)Eval("Total") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost")*(int)Eval("Quantity"))/(double)Eval("Total"))*100,2)%>' /></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <% } %>
    
    <% if(repCC.Items.Count > 0 ) { %>
    <h3 style="text-align:center">Content</h3>
    <div class="well" style="width:100%">
         <asp:Repeater ID="repCC" runat="server">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Unit Cost</td><td>Quantity</td><td>Price</td><td>Total Margin</td><td>Margin %</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> 
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="UnitCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Cost") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Label ID="Margin" runat="server" Text='<%# ((double)DataBinder.Eval(Container.DataItem, "Product.Price") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost"))*(int)Eval("Quantity")%>' /></td>
                        <td><asp:Label ID="MarginPercent" runat="server" Text='
                            <%# Math.Round((((double)Eval("Total") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost")*(int)Eval("Quantity"))/(double)Eval("Total"))*100,2)%>' /></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>      
    </div>
    <% } %>
    
    <% if(repInst.Items.Count > 0 ) { %>
    <h3 style="text-align:center">Installation</h3>
    <div class="well" style="width:100%">
        <asp:Repeater ID="repInst" runat="server">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Unit Price</td><td>Unit Cost</td><td>Quantity</td><td>Price</td><td>Total Margin</td><td>Margin %</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> 
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="UnitCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Cost") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Label ID="Margin" runat="server" Text='<%# ((double)DataBinder.Eval(Container.DataItem, "Product.Price") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost"))*(int)Eval("Quantity")%>' /></td>
                        <td><asp:Label ID="MarginPercent" runat="server" Text='
                            <%# Math.Round((((double)Eval("Total") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost")*(int)Eval("Quantity"))/(double)Eval("Total"))*100,2)%>' /></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>   
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <% } %>

    <% if(repRec.Items.Count > 0 ) { %>
    <h3 style="text-align:center">Recurring</h3>
     <div class="well" style="width:100%">
        <asp:Repeater ID="repRec" runat="server">
            <HeaderTemplate>
                <table style="width:100%">
                    <tr style="font-weight: bold"><td>Product</td><td>Part Number</td><td>Monthly Price</td><td>Unit Cost</td><td>Quantity</td><td>Price</td><td>Total Margin</td><td>Margin %</td></tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td><asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Name") %>' /></td> 
                        <td><asp:Label ID="PartNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.PartNumber") %>' /></td>
                        <td><asp:Label ID="UnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Price") %>' /></td>
                        <td><asp:Label ID="UnitCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Product.Cost") %>' /></td>
                        <td><asp:Label ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' /></td>
                        <td><asp:Label ID="Price" runat="server" Text='<%# Eval("Total") %>' /></td>
                        <td><asp:Label ID="Margin" runat="server" Text='<%# ((double)DataBinder.Eval(Container.DataItem, "Product.Price") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost"))*(int)Eval("Quantity")%>' /></td>
                        <td><asp:Label ID="MarginPercent" runat="server" Text='
                            <%# Math.Round((((double)Eval("Total") - (double)DataBinder.Eval(Container.DataItem, "Product.Cost")*(int)Eval("Quantity"))/(double)Eval("Total"))*100,2)%>' /></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>   
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <% } %>

    <div class="well" style="width:50%;display:block; margin-left:auto;margin-right:auto">
        <table style="text-align:center; width:100%">
            <tr style="font-weight: bold"><td>Total</td><td>Margin</td><td>Margin %</td></tr>
            <tr>
                <td><asp:Label ID="GrandTotal" runat="server" Text="" /></td>
                <td><asp:Label ID="GrandMargin" runat="server" Text="" /></td>
                <td><asp:Label ID="GrandMarginPercent" runat="server" Text="" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
