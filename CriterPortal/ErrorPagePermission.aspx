<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPagePermission.aspx.cs" Inherits="ErrorPagePermission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" Runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <asp:Label runat="server" CssClass="validation-error" ID="lblErrorPermission" Text="Attenzione non si hanno le credenziali per visualizzare la pagina!" />
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

