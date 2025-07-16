<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_Manuali.aspx.cs" Inherits="COM_Manuali" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>MANUALI D'USO</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row0" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale0" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-le-imprese/manuale-impresa-guida-alla-registrazione-preliminare.pdf/@@download/file" Target="_blank" ForeColor="Blue" Text="Manuale utente impresa guida alla registrazione preliminare-Rev.03 (pdf, 2.0 MB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row1" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale1" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-le-imprese/manuale-utente-impresa_guida-allutilizzo-dellapplicativo-criter.pdf/@@download/file" Target="_blank" ForeColor="Blue" Text="Manuale utente impresa: guida all’utilizzo del sistema informatico Criter (pdf, 6.4 MB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row2" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale2" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-le-imprese/manuale-utente-impresa_guida-operativa-alla-compilazione-del-libretto-di-impianto-1-pdf.pdf/@@download/file" Target="_blank" ForeColor="Blue" Text="Manuale utente impresa: guida operativa alla compilazione del libretto di impianto (pdf, 2.8 MB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row3" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale3" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-le-imprese/manuale-utente-impresa_guida-operativa-alla-compilazione-dei-rapporti-di-controllo-tecnico.pdf/@@download/file" Target="_blank" ForeColor="Blue" Text="Manuale utente impresa: guida operativa alla compilazione del Rapporto di controllo di efficienza energetica (pdf, 2.9 MB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row6" Visible="false" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale6" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/guida-operativa-funzionalia-conversione-bollino-calore-pulito.pdf/@@download/file/Guida%20operativa%20funzionali%C3%A0%20conversione%20bollino%20calore%20pulito.pdf" Target="_blank" ForeColor="Blue" Text="Guida alla conversione dei bollini calore pulito da vecchio a nuovo importo (pdf, 1.1 MB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row4" runat="server" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale4" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-i-distributori-di-combustibile/manuale-utente-distributore-di-combustibile.pdf/@@download/file" Target="_blank" ForeColor="Blue" Text="Manuale utente distributore di combustibile (pdf, 876 KB)" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="row5" runat="server" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:HyperLink runat="server" ID="lnkManuale5" NavigateUrl="https://energia.regione.emilia-romagna.it/criter/assistenza/manuali/per-i-distributori-di-combustibile/schemadistributoricriter_rev01.xsd/@@download/file" Target="_blank" ForeColor="Blue" Text="Schema .xsd per il trasferimento dei dati dei distributore di combustibile (xsd, 2 KB)" />
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

