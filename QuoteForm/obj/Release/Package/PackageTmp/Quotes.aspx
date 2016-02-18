<%@ Page Title="Quotes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quotes.aspx.cs" Inherits="QuoteForm.Quotes" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.quicksearch.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input#Filter').quicksearch('#quotes tbody tr');
            $('.otherquote').hide();
        });
        function pageLoad(sender,args)
        {
            $('input[type=submit]#myQuotes').click(function () {
                if (!$('#myQuotes').hasClass('active'))
                {
                    $('.otherquote').hide();

                    $('#myQuotes').toggleClass('active');
                    $('#allQuotes').toggleClass('active');
                }
            });
            $('input[type=submit]#allQuotes').click(function () {
                if (!$('#allQuotes').hasClass('active'))
                {
                    $('.otherquote').show();

                    $('#allQuotes').toggleClass('active');
                    $('#myQuotes').toggleClass('active');
                }
            });
        }
    </script>

    <div>
        <input type="text" id="Filter" placeholder="Search"/>
        <div class="btn-group">
            <asp:Button runat="server" ClientIDMode="Static" ID="myQuotes" Text="Mine" class="btn btn-info active" OnClientClick="return false;"/> <!--onClientClick prevents auto postback -->
            <asp:Button runat="server" ClientIDMode="Static" ID="allQuotes" Text="All" class="btn btn-info"  OnClientClick="return false;"/>
        </div>
    </div>

    <!-- TODO: make this filterable -->
    <asp:Repeater ID="repQuotes" runat="server" OnItemCommand="repQuotes_ItemCommand"> 
            <HeaderTemplate>
                <table id="quotes" style="width:100%">
                    <thead>
                        <tr style="font-weight: bold"><td>Customer</td><td>Company</td><td>Date</td><td>Owner</td><td>Number</td><td>Total</td><%if(User.IsInRole("Admin")){ %><td>Delete</td><%} %></tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                     
                    <tr class="<%# QuoteClass(Eval("Owner").ToString()) %>">
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>'/>
                        <td><asp:LinkButton ID="QuoteCustomer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Contact") %>' CommandName="Choose" CommandArgument='<%# Eval("Id") %>'/></td>
                        <td><asp:Label ID="QuoteCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.Company") %>' /></td>
                        <td><asp:Label ID="QuoteDate" runat="server" Text='<%#Eval("Date") %>' /></td>
                        <td><asp:LinkButton ID="Owner" runat="server" Text='<%#Eval("Owner") %>' CommandName="Email" CommandArgument='<%# Eval("Owner") %>'/></td>
                        <td><asp:Label ID="QuoteEmail" runat="server" Text='<%# Eval("PhoneNumber") %>' /></td>
                        <td><asp:Label ID="QuoteTotal" runat="server" Text='<%# GetGrandTotalString((QuoteForm.Models.Quote)Container.DataItem) %>' /></td>
                        <%if(User.IsInRole("Admin")){ %>
                        <td><asp:Button ID="Delete" runat="server" Text="Delete" class="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'/></td>
                        <%} %>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
