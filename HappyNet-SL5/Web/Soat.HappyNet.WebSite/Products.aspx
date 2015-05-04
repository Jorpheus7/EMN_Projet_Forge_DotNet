<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Products.aspx.cs" Inherits="Soat.HappyNet.WebSite.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SilverlightContent" runat="server">
<div class="global">
    <asp:Repeater ID="rptProducts" runat="server">
        <HeaderTemplate><ul class="menu"></HeaderTemplate>
        <ItemTemplate>
        <li class="sous-menu">
            <asp:HyperLink runat="server"
               NavigateUrl='<%# String.Format("{0}{1}", "Product.aspx?product=", Eval("ProductModelID")) %>'>
               <%# Eval("Name") %>
            </asp:HyperLink> 
            <%# String.Format("{0:0.00}", Eval("LowerPrice")) %> <%# Eval("Currency") %>
        </li></ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
    </asp:Repeater>
</div>
</asp:Content>
