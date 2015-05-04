<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Product.aspx.cs" Inherits="Soat.HappyNet.WebSite.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SilverlightContent" runat="server">
<div class="global">
    <asp:Panel>
        <h1><asp:Label runat="server" ID="lblName"></asp:Label></h1>
        <h2><asp:Label runat="server" ID="lblPrice"></asp:Label></h2>
        <h3><asp:Label runat="server" ID="lblDescription"></asp:Label></h3>
        Produits :
        <asp:Repeater ID="rptProducts" runat="server">
            <HeaderTemplate><ul class="menu"></HeaderTemplate>
            <ItemTemplate>
            <li class="sous-menu">
                <asp:Label><%# Eval("Name") %></asp:Label> - 
                <asp:Label><%# String.Format("{0:0.00}", Eval("Price")) %> <%# Eval("Currency") %></asp:Label>
            </li></ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>
</asp:Content>