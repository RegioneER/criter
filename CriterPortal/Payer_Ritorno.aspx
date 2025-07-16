<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payer_Ritorno.aspx.cs" Inherits="Payer_Ritorno" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Timer runat="server" ID="Timer1" Interval="10000" OnTick="Timer1_Tick">
            </asp:Timer>
            <div>
                <asp:Label runat="server" ID="lblEsitoDescrizione" />
            </div>
            <div id="imagesDiv" runat="server" style="margin-top: 10px">
                <img alt="Caricamento in corso" src="images/loader.gif" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
