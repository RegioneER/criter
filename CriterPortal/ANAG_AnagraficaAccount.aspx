<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ANAG_AnagraficaAccount.aspx.cs" Inherits="ANAG_AnagraficaAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoAccount" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>
                            <asp:Label runat="server" Text="ACCOUNT UTENTE" ID="lblTitoloAccount" />
                        </h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoAccesso" AssociatedControlID="lblTipoAccesso" Text="Tipo di accesso" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblTipoAccesso" />&nbsp;&nbsp;&nbsp;
                        <asp:Image runat="server" ID="imgSpid" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceSoggetto" AssociatedControlID="lblTipoAccesso" Text="Codice Soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ForeColor="Green" Font-Bold="true" ID="lblCodiceSoggetto" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloUsername" AssociatedControlID="lblUsername" Text="Username" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblUsername" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPassword" AssociatedControlID="lblPassword" Text="Password" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblPassword" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataUltimaModificaPassword" AssociatedControlID="lblDataUltimaModificaPassword" Text="Data ultima modifica password" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblDataUltimaModificaPassword" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataScadenzaPassword" AssociatedControlID="lblDataScadenzaPassword" Text="Data scandenza password" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblDataScadenzaPassword" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataUltimoAccesso" AssociatedControlID="lblDataUltimoAccesso" Text="Data ultimo accesso" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblDataUltimoAccesso" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloApiKey" AssociatedControlID="lblDataUltimoAccesso" Text="Api key" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblApiKey" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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