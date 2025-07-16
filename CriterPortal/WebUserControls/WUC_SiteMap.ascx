<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_SiteMap.ascx.cs" Inherits="WebUserControls_WUC_SiteMap" %>

<div class="Inline">
    <asp:label runat="server" CssClass="Inline" ID="WUC_SiteMap_lbl" />
</div>
<span>
   <asp:SiteMapPath ID="SiteMapPath" PathSeparator="&nbsp;|&nbsp;" SiteMapProvider="SiteMapProvider" runat="server" />
</span>