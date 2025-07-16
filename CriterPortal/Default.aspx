<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Criter" %>
<%@ Register Src="~/WebUserControls/WUC_Tile.ascx" TagName="Tile" TagPrefix="ucTile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <ucTile:Tile id="USCTile" runat="server"></ucTile:Tile>
            <!-- -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
             </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
