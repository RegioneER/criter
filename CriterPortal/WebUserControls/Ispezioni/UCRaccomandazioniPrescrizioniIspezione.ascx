<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCRaccomandazioniPrescrizioniIspezione.ascx.cs" Inherits="WebUserControls_Ispezioni_UCRaccomandazioniPrescrizioniIspezione" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--<asp:Label runat="server" ID="lblIspezione" Visible="false" />--%>
<asp:Label runat="server" ID="lbliDTipologiaRaccomandazionePrescrizioneIspezione" Visible="false" />
<asp:Label runat="server" ID="lblfActive" Visible="false" />
<br />
<asp:Table runat="server" ID="tblRaccomandazioniPrescrizioni">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">
            <asp:Label runat="server" ID="lblRaccomandazioniPrescrizioni" Visible="false" Text="É necessario inserire una Raccomandazione o una Prescrizione selezionandola dall’elenco sotto riportato. Selezionando il pulsante “Salva dati rapporto di verifica ispettiva” l’opzione selezionata sarà riportata automaticamente nella rispettiva sezione del RVI. Altrimenti, inserire manualmente un testo nell’apposita sezione del RVI." />
            <asp:Label runat="server" ID="lblRaccomandazioni" Visible="false" Text="É necessario inserire una Raccomandazione selezionandola dall’elenco sotto riportato. Selezionando il pulsante “Salva dati rapporto di verifica ispettiva” l’opzione selezionata sarà riportata automaticamente nella rispettiva sezione del RVI. Altrimenti, inserire manualmente un testo nell’apposita sezione del RVI." />
            <asp:Label runat="server" ID="lblPrescrizioni" Visible="false" Text="É necessario inserire una Prescrizione selezionandola dall’elenco sotto riportato. Selezionando il pulsante “Salva dati rapporto di verifica ispettiva” l’opzione selezionata sarà riportata automaticamente nella rispettiva sezione del RVI. Altrimenti, inserire manualmente un testo nell’apposita sezione del RVI." />
        </asp:TableCell>
    </asp:TableRow>   
    <asp:TableRow>
        <asp:TableCell runat="server" ID="cellRaccomandazioni" Visible="false">
            <dx:ASPxListBox ID="lbRaccomandazioni" runat="server" EnableViewState="true" EnableCallbackMode="false" SelectionMode="CheckColumn" Width="250" Height="200"
                ValueType="System.String" ClientInstanceName="lbRaccomandazioni" BackColor="#f5f2f2" AutoPostBack="true" OnSelectedIndexChanged="lbRaccomandazioni_SelectedIndexChanged" 
                Caption="Raccomandazioni">
                <CaptionSettings Position="Top" ShowColon="false" />
                <ItemStyle Wrap="True" />
            </dx:ASPxListBox>
        </asp:TableCell>
        <asp:TableCell runat="server" ID="cellPrescrizioni" Visible="false">
            <dx:ASPxListBox ID="lbPrescrizioni" runat="server" EnableViewState="true" EnableCallbackMode="false" SelectionMode="CheckColumn" Width="250" Height="200"
                ValueType="System.String" ClientInstanceName="lbPrescrizioni" BackColor="#f5f2f2" AutoPostBack="true" OnSelectedIndexChanged="lbPrescrizioni_SelectedIndexChanged" 
                Caption="Prescrizioni">
                <ItemStyle Wrap="True" />
                <CaptionSettings Position="Top" ShowColon="false" />
            </dx:ASPxListBox>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>