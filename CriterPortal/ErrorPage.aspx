<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" Runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <asp:Label runat="server" CssClass="validation-error" ID="lblError" Text="Attenzione si è verificato un errore nel Sistema Criter!" />
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

