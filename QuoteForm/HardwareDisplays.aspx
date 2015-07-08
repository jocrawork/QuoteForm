<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HardwareDisplays.aspx.cs" Inherits="QuoteForm.HardwareDisplays" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div style="text-align:center">
                <asp:DropDownList ID="HardwareCatDDL" name="Products" runat="server">
                    <asp:ListItem Value=null>--Select a Category--</asp:ListItem>
                    <asp:ListItem Value="Media Players">Media Players</asp:ListItem>
                    <asp:ListItem Value="MP Warranties">MP Warranties</asp:ListItem>
                    <asp:ListItem Value="Indoor Displays">Indoor Displays</asp:ListItem>
                    <asp:ListItem Value="Outdoor Displays">Outdoor Displays</asp:ListItem>
                    <asp:ListItem Value="Integrated Players">Integrated Players</asp:ListItem>
                    <asp:ListItem Value="Failover Kits">Failover Kits</asp:ListItem>
                    <asp:ListItem Value="Video Wall Players">Video Wall Players</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="HardwareProdDDL" name="Products" runat="server">
                    <asp:ListItem Value=null>--Select a Product--</asp:ListItem>
                </asp:DropDownList>    
            </div>

            <br />
            <!-- TODO: prefill but still be editable -->
            <div style="text-align:center">
                <input type="text" placeholder="Part Number" style="width:100px"/>
                <input type="text" placeholder="Price per Unit" style="width:100px"/>
                <input type="text" placeholder="Quantity" style="width:100px"/>
                <!-- TODO: "include necessary warranties" checkbox -->
            </div>
            <br />
            <asp:CheckBox ID="Warranties" runat="server" text="Include Warranties"/>
            
    </div>
    </form>
</body>
</html>
