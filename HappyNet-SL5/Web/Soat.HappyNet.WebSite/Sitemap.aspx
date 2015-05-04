<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="Soat.HappyNet.WebSite.Sitemap" %>
<asp:Repeater runat="server" id="rptUrl">
    <HeaderTemplate><?xml version="1.0" encoding="UTF-8" ?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
    </HeaderTemplate>
    <ItemTemplate>
    <url>
        <loc><%# new Uri(this.Request.Url, this.ResolveUrl(Eval("HtmlUrl").ToString()))%></loc>
        <lastmod><%# Eval("LastModified", "{0:yyyy-MM-dd}")%></lastmod>
        <changefreq><%# Eval("ChangeFrequency")%></changefreq>
        <priority><%# Eval("Priority", "{0:#.#}")%></priority>
    </url>
    </ItemTemplate>
    <FooterTemplate>
</urlset>
    </FooterTemplate>
</asp:Repeater>
