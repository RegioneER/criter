<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_Accertamenti.aspx.cs" Inherits="VER_Accertamenti" %>
<%@ Register Src="~/WebUserControls/WUC_GoogleAutosuggest.ascx" TagPrefix="uc1" TagName="UCGoogleAutosuggest" %>
<%@ Register Src="~/WebUserControls/Raccomandate/UCRaccomandate.ascx" TagPrefix="uc1" TagName="UCRaccomandate" %>
<%@ Register Src="~/WebUserControls/WUC_ChangeResponsabileLibretto.ascx" TagPrefix="uc1" TagName="UCChangeResponsabileLibretto" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoGenerali" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>INFORMAZIONI GENERALI ACCERTAMENTO</h2>
                        <asp:Label runat="server" ID="lblIDAccertatore" Visible="false" />
                        <asp:Label runat="server" ID="lblIDCoordinatore" Visible="false" />
                        <asp:Label runat="server" ID="lblIDAgenteAccertatore" Visible="false" />
                        <asp:Label runat="server" ID="lblIDCodiceCatastale" Visible="false" />
                        <asp:Label runat="server" ID="lblIDRapportoControllo" Visible="false" />
                        <asp:Label runat="server" ID="lblfEmailConfermaAccertamento" Visible="false" />
                        <asp:Label runat="server" ID="lblDataInvioEmail" Visible="false" />
                        <asp:Label runat="server" ID="lblRispostaEmail" Visible="false" />
                        <asp:Label runat="server" ID="lblIDTipoAccertamento" Visible="false" />
                        <asp:Label runat="server" ID="lblIDIspezione" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloProgrammaAccertamento" AssociatedControlID="lblProgrammaAccertamento" Text="Programma accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblProgrammaAccertamento" Font-Bold="true" ForeColor="Green" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblCodiceAccertamento" AssociatedControlID="lblCodiceAccertamento" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblCodiceAccertamento" Font-Bold="true" ForeColor="Green" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblDataAccertamento" AssociatedControlID="lblDataAccertamento" Text="Data accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataAccertamento" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblAccertatore" AssociatedControlID="lblAccertatore" Text="Accertatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblAccertatore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCoordinatore" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblCoordinatore" AssociatedControlID="lblCoordinatore" Text="Coordinatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblCoordinatore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow runat="server" ID="rowAgenteAccertatore" >
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblAgenteAccertatore" AssociatedControlID="lblAgenteAccertatore" Text="Agente Accertatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblAgenteAccertatore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblStatoAccertamento" AssociatedControlID="lblStatoAccertamento" Text="Stato accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblStatoAccertamento" ForeColor="Green" />
                        <asp:Label runat="server" ID="lblIDStatoAccertamento" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowEmailConfermaAccertamento" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloEmailConfermaAccertamento" AssociatedControlID="lblEmailConfermaAccertamento" Text="Email inviata per conferma accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblEmailConfermaAccertamento" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowEmailRispostaAccertamento" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloEmailRispostaAccertamento" AssociatedControlID="lblEmailRispostaAccertamento" Text="Risposta manutentore per conferma accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblEmailRispostaAccertamento" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowGiorniRealizzazioneInterventi" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloGiorniRealizzazioneInterventi" AssociatedControlID="txtGiorniRealizzazioneInterventi" Text="Limite temporale per la realizzazione degli interventi richiesti" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtGiorniRealizzazioneInterventi" ValidationGroup="vgAccertamento" Width="100" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtGiorniRealizzazioneInterventi" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                            ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: campo obbligatorio"
                            ControlToValidate="txtGiorniRealizzazioneInterventi">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtGiorniRealizzazioneInterventi" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                            ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: non valido" 
                            ControlToValidate="txtGiorniRealizzazioneInterventi"
                            ValidationExpression="^(0|[1-9]\d*)$">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Impresa di Manutenzione/Installazione</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblImpresaManutenzione" AssociatedControlID="lblImpresaManutenzione" Text="Impesa di manutenzione/installazione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblImpresaManutenzione" Font-Bold="true" runat="server" /><br />
                        <asp:Label ID="lblImpresaManutenzioneIndirizzo" runat="server" /><br />
                            Telefono:<asp:Label ID="lblImpresaManutenzioneTelefono" runat="server" /><br />
                            Email:<asp:Label ID="lblImpresaManutenzioneEmail" runat="server" /><br />
                            Email Pec:<asp:Label ID="lblImpresaManutenzioneEmailPec" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Responsabile</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblResponsabile" AssociatedControlID="UCChangeResponsabileLibretto" Text="Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <%--<asp:Label ID="lblResponsabile" Font-Bold="true" runat="server" /><br />--%>
                        <uc1:UCChangeResponsabileLibretto runat="server" ID="UCChangeResponsabileLibretto" />
                        <br />
                        <uc1:UCGoogleAutosuggest runat="server" ID="UCGoogleAutosuggest" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTerzoResponsabile" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblTerzoResponsabile" AssociatedControlID="lblTerzoResponsabile" Text="Terzo Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblTerzoResponsabile" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Impianto</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblImpianto" AssociatedControlID="lblDatiImpiantoCodiceTargatura" Text="Dati Impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        Codice targatura:&nbsp;<asp:Label ID="lblDatiImpiantoCodiceTargatura" Font-Bold="true" runat="server" /><br />
                        <br />
                        <dx:ASPxHyperLink runat="server" ID="btnViewLibrettoImpianto" Text="Visualizza dati Libretto di Impianto"
                            Target="_blank" ClientInstanceName="btnViewLibrettoImpianto" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDatiImpiantoPod" AssociatedControlID="lblPod" Text="POD" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label ID="lblPod" Font-Bold="true" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDatiImpiantoPdr" AssociatedControlID="lblPdr" Text="PDR" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label ID="lblPdr" Font-Bold="true" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzo" AssociatedControlID="lblIndirizzo" Text="Indirizzo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblIndirizzo" /><br />
                        <asp:Label runat="server" ID="lblEmailPecComune" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <h3><asp:Label runat="server" ID="lblTitoloDatiRCTIspezione" /></h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowTipoIspezione">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDescrizioneTipoIspezione" AssociatedControlID="lblTipoIspezione" Text="Tipo Ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblTipoIspezione" Font-Bold="true" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDatiIspettore">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDescrizioneDatiIspettore" AssociatedControlID="lblIspettore" Text="Dati Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIspettore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDatiIspezioneAccertamentoOriginale">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label1" AssociatedControlID="lblTipoIspezione" Text="Accertamento Rct" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxHyperLink runat="server" ID="lnkAccertamentoRct" Text="Visualizza dati dell'Accertamento Rct" 
                            ClientInstanceName="lnkAccertamentoRct" Target="_blank" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblRapportoControlloTecnico" AssociatedControlID="lblDatiImpiantoCodiceTargatura" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblTitoloPotenzaTermicaNominale" Text="Potenza termica nominale totale max: " Font-Bold="true" />
                        <asp:Label runat="server" ID="lblPotenzaTermicaNominale" /><br />
                        <asp:Label runat="server" ID="lblTitoloTipologiaCombustibile" Text="Tipologia combustibile: " Font-Bold="true" />
                        <asp:Label runat="server" ID="lblTipologiaCombustibile" />
                        <asp:Label runat="server" ID="lblIDTipologiaCombustibile" Visible="false" /><br /><br />
                        
                        <asp:DataGrid ID="dgRapporti" CssClass="Grid" Width="100%" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="5" AllowSorting="False" 
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDRapportoControlloTecnico"
                            OnItemDataBound="dgRapporti_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDRapportoControlloTecnico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTipologiaRCT" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoAzienda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoManutentore" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220" HeaderText="Rapporti di Controllo">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="lnkRapportoControllo" Target="_blank" Text="Visualizza dati Rapporto di Controllo Tecnico" />
                                        <%--<asp:Label ID="lblTipoProceduraAccertamento" runat="server" Text='<%# string.Format("{0} {1}", "Procedura ", Eval("ProceduraAccertamento")) %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" HeaderText="Data Firma">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataFirma" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("DataFirma")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" HeaderText="Data Controllo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataControllo" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("DataControllo")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>

                        <dx:ASPxHyperLink runat="server" ID="btnViewIspezione" Text="Visualizza dati Ispezione"
                            Target="_blank" ClientInstanceName="btnViewIspezione" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDistributore0" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Distributore di combustibile</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDistributore" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDistributore" AssociatedControlID="ddlTipologiaDistributore" Text="Distributore di combustibile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTipologiaDistributore" Width="350" 
                            TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <%--<asp:RequiredFieldValidator ID="rfvddlTipologiaDistributore" ValidationGroup="vgAccertamento" ForeColor="Red" runat="server" InitialValue="0" 
                            ErrorMessage="Distributore di combustibile: campo obbligatorio"
                            ControlToValidate="ddlTipologiaDistributore">&nbsp;*</asp:RequiredFieldValidator>--%>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <h3><asp:Label runat="server" ID="lblTitoloDatiNonConformita" /></h3>
                    </asp:TableCell>
                </asp:TableRow>
                                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:Table ID="tblNonConformita" Width="895px" Font-Size="XX-Small" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="50" Font-Bold="true" CssClass="riempimento4">
                                   Procedura
                                </asp:TableCell>
                                <asp:TableCell Width="100" Font-Bold="true" HorizontalAlign="Center" CssClass="riempimento4">
                                    Non confor. RCT
                                </asp:TableCell>
                                <asp:TableCell Width="700" HorizontalAlign="Left" Font-Bold="true" CssClass="riempimento4">
                                    Conferma non conformità
                                </asp:TableCell>
                                <asp:TableCell Width="50" HorizontalAlign="Center" Font-Bold="true" CssClass="riempimento4">
                                    Stampa
                                </asp:TableCell>
                            </asp:TableRow>
                            <%-- Osservazioni --%>
                            <asp:TableRow>
                                <asp:TableCell CssClass="riempimento4">
                                    &nbsp;
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Center" CssClass="riempimento4">
                                    Osservazioni<br />
                                    <asp:Image runat="server" ID="imgEsitoControlloOsservazioni" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4">
                                    <asp:Button ID="btnNewOsservazione" runat="server" Width="130" CssClass="buttonSmallSaveClass" Text="Nuova Osservazione"
                                        OnClick="btnNewOsservazione_Click" OnClientClick="javascript:return confirm('Confermi di inserire una nuova osservazione?');"
                                         />
                                    <asp:DataGrid ID="dgOsservazioni" CssClass="Grid" Width="700px" GridLines="None" HorizontalAlign="Center"
                                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" 
                                            AllowSorting="false" AllowPaging="false" ShowHeader="false"
                                            AutoGenerateColumns="False" runat="server" DataKeyField="IDNonConformita"
                                            OnItemDataBound="dgOsservazioni_ItemDataBound">
                                            <ItemStyle CssClass="GridItem" />
                                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                            <Columns>
                                                <asp:BoundColumn DataField="IDNonConformita" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="Tipo" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDTipologiaRisoluzioneAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDTipologiaEventoAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:TemplateColumn ItemStyle-Width="300">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkOsservazioneConferma" AutoPostBack="true" TextAlign="Right" 
                                                                OnCheckedChanged="chkOsservazioneConferma_CheckedChanged" Checked='<%#Eval("fOsservazioneConferma") %>' />&nbsp;
                                                        <asp:Label runat="server" ID="lblOsservazioneRct" Text='<%#Eval("OsservazioneRct") %>' />
                                                        <br />
                                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtOsservazione" CssClass="txtClass" Width="300" Height="100" 
                                                            Rows="3" Text='<%#Eval("Osservazione") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblTipologiaRisoluzioneOsservazione" AutoPostBack="true" 
                                                            OnSelectedIndexChanged="rblTipologiaRisoluzioneOsservazione_SelectedIndexChanged" TabIndex="1" runat="server" 
                                                            RepeatDirection="Horizontal" /><br />
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzioneOsservazioneGiorni" Visible="false">
                                                            <asp:TextBox runat="server" ID="txtGiorni" Text='<%#Eval("Giorni") %>' Width="100" CssClass="txtClass_o" />
                                                            <asp:RequiredFieldValidator
                                                                ID="rfvtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: campo obbligatorio"
                                                                ControlToValidate="txtGiorni">&nbsp;*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator
                                                                ID="revtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: non valido" 
                                                                ControlToValidate="txtGiorni"
                                                                ValidationExpression="^(0|[1-9]\d*)$">&nbsp;*</asp:RegularExpressionValidator>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzioneOsservazioneEvento" Visible="false">
                                                            <asp:DropDownList ID="ddlTipologiaEventoAccertamento" Width="350" 
                                                                TabIndex="1" ValidationGroup="vgAccertamento" runat="server" CssClass="selectClass_o" />
                                                            <asp:RequiredFieldValidator ID="rfvddlTipologiaEventoAccertamento" ValidationGroup="vgAccertamento" ForeColor="Red" runat="server" InitialValue="0" 
                                                                ErrorMessage="Evento accertamento: campo obbligatorio"
                                                                ControlToValidate="ddlTipologiaEventoAccertamento">&nbsp;*</asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4" HorizontalAlign="Center" RowSpan="2">
                                    <asp:ImageButton runat="server" ID="ImgPdfRaccomandazioni" BorderStyle="None" ToolTip="Visualizza documento Accertamento" 
                                        AlternateText="Visualizza documento Accertamento" ImageUrl="~/images/Buttons/pdf.png" TabIndex="1" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <%-- Raccomandazioni --%>
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center" CssClass="riempimento4">
                                    <asp:Label runat="server" ID="lblProceduraRaccomandazioni" /><br />
                                    <asp:ImageButton runat="server" ID="imgHelperRaccomandazioni" ImageUrl="~/images/buttons/information.png" BorderStyle="None" 
                                        CausesValidation="false" OnClientClick="return WindowHelperRaccomandazioni.Show();" />
                                    <dx:ASPxPopupControl ID="ASPxPopupControlHelper" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" CloseAnimationType="Fade" 
                                        EnableViewState="False" PopupElementID="popupAreaHelper" 
                                        PopupHorizontalAlign="WindowCenter" ContentUrl="~/VER_AccertamentiHelper.aspx?tipo=1"
                                        PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"  
                                        Width="905px" Height="600px" MinWidth="905px" MinHeight="600px" 
                                        ClientInstanceName="WindowHelperRaccomandazioni" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                                        <ContentStyle Paddings-Padding="0" />
                                        <ClientSideEvents Shown="WindowHelperRaccomandazioni" />
                                    </dx:ASPxPopupControl>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Center" CssClass="riempimento4">
                                    Raccomandazioni<br />
                                    <asp:Image runat="server" ID="imgEsitoControlloRaccomandazioni" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4">
                                    <asp:Button ID="btnNewRaccomandazione" runat="server" Width="130" CssClass="buttonSmallSaveClass" Text="Nuova Raccomandazione"
                                        OnClick="btnNewRaccomandazione_Click" OnClientClick="javascript:return confirm('Confermi di inserire una nuova raccomandazione?');"
                                         />
                                    <asp:DataGrid ID="dgRaccomandazioni" CssClass="Grid" Width="700px" GridLines="None" HorizontalAlign="Center"
                                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" 
                                            AllowSorting="false" AllowPaging="false" ShowHeader="false"
                                            AutoGenerateColumns="False" runat="server" DataKeyField="IDNonConformita"
                                            OnItemDataBound="dgRaccomandazioni_ItemDataBound">
                                            <ItemStyle CssClass="GridItem" />
                                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                            <Columns>
                                                <asp:BoundColumn DataField="IDNonConformita" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="Tipo" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDTipologiaRisoluzioneAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDTipologiaEventoAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:TemplateColumn ItemStyle-Width="300">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkRaccomandazioneConferma" AutoPostBack="true" TextAlign="Right" 
                                                                OnCheckedChanged="chkRaccomandazioneConferma_CheckedChanged" Checked='<%#Eval("fRaccomandazioneConferma") %>' />&nbsp;
                                                        <asp:Label runat="server" ID="lblRaccomandazioneRct" Text='<%#Eval("RaccomandazioneRct") %>' />
                                                        <br />
                                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRaccomandazione" CssClass="txtClass" Width="300" Height="100" 
                                                            Rows="3" Text='<%#Eval("Raccomandazione") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblTipologiaRisoluzioneRaccomandazione" AutoPostBack="true" 
                                                            OnSelectedIndexChanged="rblTipologiaRisoluzioneRaccomandazione_SelectedIndexChanged" TabIndex="1" runat="server" 
                                                            RepeatDirection="Horizontal" /><br />
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzioneRaccomandazioneGiorni" Visible="false">
                                                            <asp:TextBox runat="server" ID="txtGiorni" Text='<%#Eval("Giorni") %>' Width="100" CssClass="txtClass_o" />
                                                            <asp:RequiredFieldValidator
                                                                ID="rfvtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: campo obbligatorio"
                                                                ControlToValidate="txtGiorni">&nbsp;*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator
                                                                ID="revtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: non valido" 
                                                                ControlToValidate="txtGiorni"
                                                                ValidationExpression="^(0|[1-9]\d*)$">&nbsp;*</asp:RegularExpressionValidator>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzioneRaccomandazioneEvento" Visible="false">
                                                            <asp:DropDownList ID="ddlTipologiaEventoAccertamento" Width="350" 
                                                                TabIndex="1" ValidationGroup="vgAccertamento" runat="server" CssClass="selectClass_o" />
                                                            <asp:RequiredFieldValidator ID="rfvddlTipologiaEventoAccertamento" ValidationGroup="vgAccertamento" ForeColor="Red" runat="server" InitialValue="0" 
                                                                ErrorMessage="Evento accertamento: campo obbligatorio"
                                                                ControlToValidate="ddlTipologiaEventoAccertamento">&nbsp;*</asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                </asp:TableCell>
                            </asp:TableRow>
                            <%-- Prescrizioni --%>
                            <asp:TableRow>
                                <asp:TableCell CssClass="riempimento4" HorizontalAlign="Center" RowSpan="2">
                                    <asp:Label runat="server" ID="lblProceduraPrescrizioni" /><br />
                                    <asp:ImageButton runat="server" ID="imgHelperPrescrizioni" ImageUrl="~/images/buttons/information.png" BorderStyle="None" 
                                        CausesValidation="false" OnClientClick="return WindowHelperPrescrizioni.Show();" />
                                    <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" CloseAnimationType="Fade" 
                                        EnableViewState="False" PopupElementID="popupAreaHelper" 
                                        PopupHorizontalAlign="WindowCenter" ContentUrl="~/VER_AccertamentiHelper.aspx?tipo=2"
                                        PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"  
                                        Width="905px" Height="600px" MinWidth="905px" MinHeight="600px" 
                                        ClientInstanceName="WindowHelperPrescrizioni" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                                        <ContentStyle Paddings-Padding="0" />
                                        <ClientSideEvents Shown="WindowHelperPrescrizioni" />
                                    </dx:ASPxPopupControl>
                                </asp:TableCell>
                                <asp:TableCell HorizontalAlign="Center" CssClass="riempimento4">
                                    Prescrizioni<br />
                                    <asp:Image runat="server" ID="imgEsitoControlloPrescrizioni" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4">
                                    <asp:Button ID="btnNewPrescrizione" runat="server" Width="130" CssClass="buttonSmallSaveClass" Text="Nuova Prescrizione"
                                        OnClick="btnNewPrescrizione_Click" OnClientClick="javascript:return confirm('Confermi di inserire una nuova prescrizione?');"
                                         />
                                    <asp:DataGrid ID="dgPrescrizioni" CssClass="Grid" Width="700px" GridLines="None" HorizontalAlign="Center"
                                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" 
                                            AllowSorting="false" AllowPaging="false" ShowHeader="false"
                                            AutoGenerateColumns="False" runat="server" DataKeyField="IDNonConformita"
                                            OnItemDataBound="dgPrescrizioni_ItemDataBound">
                                            <ItemStyle CssClass="GridItem" />
                                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                            <Columns>
                                                <asp:BoundColumn DataField="IDNonConformita" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="Tipo" Visible="false" ReadOnly="True" />
                                                <%--<asp:BoundColumn DataField="IDTipologiaRisoluzioneAccertamento" Visible="false" ReadOnly="True" />
                                                <asp:BoundColumn DataField="IDTipologiaEventoAccertamento" Visible="false" ReadOnly="True" />--%>
                                                <asp:TemplateColumn ItemStyle-Width="300">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkPrescrizioneConferma" AutoPostBack="true" TextAlign="Right" 
                                                                OnCheckedChanged="chkPrescrizioneConferma_CheckedChanged" Checked='<%#Eval("fPrescrizioneConferma") %>' />&nbsp;
                                                        <asp:Label runat="server" ID="lblPrescrizioneRct" Text='<%#Eval("PrescrizioneRct") %>' />
                                                        <br />
                                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtPrescrizione" CssClass="txtClass" Width="680" Height="100" 
                                                            Rows="3" Text='<%#Eval("Prescrizione") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--<asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="rblTipologiaRisoluzionePrescrizione" AutoPostBack="true" 
                                                            OnSelectedIndexChanged="rblTipologiaRisoluzionePrescrizione_SelectedIndexChanged" TabIndex="1" runat="server" 
                                                            RepeatDirection="Horizontal" /><br />
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzionePrescrizioneGiorni" Visible="false">
                                                            <asp:TextBox runat="server" ID="txtGiorni" Text='<%#Eval("Giorni") %>' Width="100" CssClass="txtClass_o" />
                                                            <asp:RequiredFieldValidator
                                                                ID="rfvtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: campo obbligatorio"
                                                                ControlToValidate="txtGiorni">&nbsp;*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator
                                                                ID="revtxtGiorni" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: non valido" 
                                                                ControlToValidate="txtGiorni"
                                                                ValidationExpression="^(0|[1-9]\d*)$">&nbsp;*</asp:RegularExpressionValidator>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlTipologiaRisoluzionePrescrizioneEvento" Visible="false">
                                                            <asp:DropDownList ID="ddlTipologiaEventoAccertamento" Width="350" 
                                                                TabIndex="1" ValidationGroup="vgAccertamento" runat="server" CssClass="selectClass_o" />
                                                            <asp:RequiredFieldValidator ID="rfvddlTipologiaEventoAccertamento" ValidationGroup="vgAccertamento" ForeColor="Red" runat="server" InitialValue="0" 
                                                                ErrorMessage="Evento accertamento: campo obbligatorio"
                                                                ControlToValidate="ddlTipologiaEventoAccertamento">&nbsp;*</asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>--%>
                                            </Columns>
                                        </asp:DataGrid>
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4" HorizontalAlign="Center" RowSpan="2">
                                    <asp:ImageButton runat="server" ID="ImgPdfPrescrizioni" BorderStyle="None" ToolTip="Visualizza documento Accertamento" 
                                        AlternateText="Visualizza documento Accertamento" ImageUrl="~/images/Buttons/pdf.png" TabIndex="1" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <%-- Impianto non funzionante --%>
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign="Center" CssClass="riempimento4">
                                    L'impianto può funzionare&nbsp;<asp:Label runat="server" ID="lblImpiantoNonFUnzionate" /><br />
                                    <asp:Image runat="server" ID="imgEsitoControlloImpiantoNonFunzionante" />
                                </asp:TableCell>
                                <asp:TableCell CssClass="riempimento4">
                                    <asp:Table runat="server" ID="tblImpiantoNonFunzionante" CssClass="TableClassNoBorder">
                                         <asp:TableRow>
                                            <asp:TableCell CssClass="riempimento4">
                                                <asp:Label runat="server" ID="lblIDNonConformita" Visible="false" />
                                                <%--<asp:CheckBox runat="server" ID="chkImpiantoFunzionanteConferma" AutoPostBack="true" BorderStyle="None"
                                                    OnCheckedChanged="chkImpiantoFunzionanteConferma_CheckedChanged" />--%>
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="riempimento4">
                                                <asp:Table runat="server" ID="tblImpiantoFunzionante">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="345">
                                                            <asp:RadioButtonList ID="rblImpiantoFunzionanteConferma" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="rblImpiantoFunzionanteConferma_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" 
                                                                RepeatColumns="1" CssClass="radiobuttonlistClass">
                                                                <asp:ListItem Text="PERICOLO: Sospensione immediata della fornitura di combustibile" Selected="True" Value="True" />
                                                                <asp:ListItem Text="RISCHIO: Sospensione differita della fornitura di combustibile" Value="False" />
                                                            </asp:RadioButtonList>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="345">
                                                            <asp:RadioButtonList ID="rblTipologiaRisoluzioneImpiantoFunzionante" Enabled="false" TabIndex="1" runat="server" 
                                                                RepeatDirection="Horizontal" />
                                                            <asp:Panel runat="server" ID="pnlTipologiaRisoluzionePrescrizioneGiorni" Visible="false">
                                                                <asp:TextBox runat="server" ID="txtGiorniImpiantoFunzionante" Text='<%#Eval("Giorni") %>' ValidationGroup="vgAccertamento" Width="100" CssClass="txtClass_o" />
                                                                <%--<asp:RequiredFieldValidator
                                                                    ID="rfvtxtGiorniImpiantoFunzionante" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                    ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: campo obbligatorio"
                                                                    ControlToValidate="txtGiorniImpiantoFunzionante">&nbsp;*</asp:RequiredFieldValidator>--%>
                                                                <asp:RegularExpressionValidator
                                                                    ID="revtxtGiorniImpiantoFunzionante" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                                    ErrorMessage="Limite temporale per la realizzazione degli interventi richiesti: non valido" 
                                                                    ControlToValidate="txtGiorniImpiantoFunzionante"
                                                                    ValidationExpression="^(0|[1-9]\d*)$">&nbsp;*</asp:RegularExpressionValidator>
                                                            </asp:Panel>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>

                                                <asp:RadioButtonList ID="rblTipologiaImpiantoFunzionanteAccertamento" 
                                                    TabIndex="1" runat="server" Width="620" 
                                                    RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                                                <br />
                                                <asp:Label runat="server" Text="Riferimenti Normativi" Font-Bold="true" /><br />
                                                <asp:TextBox runat="server" ID="txtNoteImpiantoFunzionante" Width="615" Height="100" 
                                                    CssClass="txtClass" TextMode="MultiLine" Rows="3" />
                                                <asp:RequiredFieldValidator
                                                    ID="rfvtxtNoteImpiantoFunzionante" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                                                    ErrorMessage="Note sul funzionamento dell'impianto: campo obbligatorio"
                                                    ControlToValidate="txtNoteImpiantoFunzionante">&nbsp;*</asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                    
                                    
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowSavePartial" runat="server" Visible="false">
                                <asp:TableCell ColumnSpan="4" HorizontalAlign="Right" CssClass="riempimento5">
                                    <dx:ASPxButton runat="server" ID="btnSavePartial" CssClass="buttonSmallSaveClass" Text="Salva dati accertamento"
                                        CausesValidation="false" ClientInstanceName="btnSavePartial" OnClick="btnSavePartial_Click">
                                        <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                            Click="function(s,e){ 
                                                if(ASPxClientEdit.ValidateGroup(null)) 
                                                { 
                                                    window.setTimeout(function()
                                                    { 
                                                       LoadingPanel.Show(); 
                                                       s.SetText('Attendere, salvataggio in corso...'); 
                                                       s.SetEnabled(false); 
                                                    },1) 
                                                } 
                                            }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxLoadingPanel ID="lp" runat="server" Text="Attendere, salvataggio in corso..."
                                        ClientInstanceName="LoadingPanel">
                                    </dx:ASPxLoadingPanel>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                
                <%--<asp:TableRow runat="server" ID="rowSanzioni" Visible="false">
                    <asp:TableCell ColumnSpan="2">
                        <ucAccertamentiSanzioni:UCAccertamentiSanzioni ID="UCSanzione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>--%>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Storico cambio stati accertamento</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                         <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" 
                            PageSize="100" AllowSorting="false" AllowPaging="false"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDAccertamentoStato"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDAccertamentoStato" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoAccertamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteUltimaModifica" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Data" ItemStyle-Width="190px" HeaderText="Data" ReadOnly="True" />
                                <asp:BoundColumn DataField="Utente" HeaderText="Utente" ItemStyle-Width="300px" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoAccertamento" HeaderText="Stato accertamento" ItemStyle-Width="500px" ReadOnly="True" />
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Note analisi accertamento</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                       <dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale"
                            AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
                            KeyFieldName="IDAccertamentoNote" 
                            OnBeforePerformDataSelect="grdPrincipale_BeforePerformDataSelect" 
                            OnRowDeleting="grdPrincipale_RowDeleting" 
                            OnRowInserting="grdPrincipale_RowInserting" 
                            OnRowUpdating="grdPrincipale_RowUpdating" 
                            OnCommandButtonInitialize="grdPrincipale_CommandButtonInitialize" 
                            OnCellEditorInitialize="grdPrincipale_CellEditorInitialize" 
                            OnDetailRowGetButtonVisibility="DetailGrid_DetailRowGetButtonVisibility"
                            OnRowUpdated="grdPrincipale_RowUpdated"
                            OnStartRowEditing="grdPrincipale_StartRowEditing" 
                            OnDataBound="DetailGrid_DataBound" 
                            OnRowValidating="grdPrincipale_RowValidating" 
                            Styles-AlternatingRow-CssClass="GridAlternativeItem"  Styles-Row-CssClass="GridItem">
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="5" Width="40px">
                                </dx:GridViewCommandColumn>
                                
                                <dx:GridViewDataDateColumn FieldName="Data" Caption="Data" VisibleIndex="1">
                                    <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <PropertiesDateEdit Width="160px">
                                        <ValidationSettings CausesValidation="True">
                                            <RequiredField ErrorText="Inserire la data" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesDateEdit>  
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataMemoColumn FieldName="Nota" Caption="Nota" VisibleIndex="2">
                                    <EditFormSettings Visible="True" CaptionLocation="Top" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <PropertiesMemoEdit Width="450px" Height="200px">
                                        <ValidationSettings CausesValidation="True">
                                            <RequiredField ErrorText="Inserire la nota" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesMemoEdit>
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataColumn FieldName="COM_Utenti.COM_AnagraficaSoggetti.Nome" Caption="Nome" VisibleIndex="3">
                                    <EditFormSettings Visible="False" />
                                    <HeaderStyle CssClass="GridHeader" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="COM_Utenti.COM_AnagraficaSoggetti.Cognome" Caption="Cognome" VisibleIndex="4">
                                    <EditFormSettings Visible="False" />
                                    <HeaderStyle CssClass="GridHeader" />
                                </dx:GridViewDataColumn>
                            </Columns>
                            <SettingsEditing EditFormColumnCount="2"></SettingsEditing>
                            <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Styles>
                                <RowHotTrack Cursor="pointer"></RowHotTrack>
                            </Styles>
                        </dx:ASPxGridView>
                       <ef:EntityDataSource ID="dsGridPrincipale" runat="server" 
                            ConnectionString="name=CriterDataModel" 
                            ContextTypeName="DataLayer.CriterDataModel" 
                            DefaultContainerName="CriterDataModel" EnableFlattening="False" 
                            EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
                            EntitySetName="VER_AccertamentoNote" Include="COM_Utenti.COM_AnagraficaSoggetti"
                            AutoGenerateWhereClause="false"
                            Where="it.IDAccertamento=@IDAccertamento"
                            OrderBy="it.Data">
                            <WhereParameters>
                                <asp:Parameter Name="IDAccertamento" Type="Int32" DefaultValue="0" />
                            </WhereParameters>
                        </ef:EntityDataSource>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                       <uc1:UCRaccomandate runat="server" ID="UCRaccomandate" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowTestoConfermaEmailIntestazione">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Testo email per conferma Accertamento</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowTestoConfermaEmail">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:TextBox Width="890" Height="200" ID="txtTestoEmail" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                        <asp:RequiredFieldValidator ID="rfvtxtTestoEmail" ForeColor="Red" runat="server" ValidationGroup="vgAccertamento" EnableClientScript="true" 
                             ErrorMessage="Testo email per conferma accertamento: campo obbligatorio"
                             ControlToValidate="txtTestoEmail">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Note dell'accertatore</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:TextBox Width="890" Height="200" ID="txtNote" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                          &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnSaveAccertamento" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnSaveAccertamento_Click" ValidationGroup="vgAccertamento" Text="SALVA DATI ACCERTAMENTO" />
                        &nbsp;
                        <asp:Button ID="btnInviaACoordinatore" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnInviaACoordinatore_Click" OnClientClick="javascript:return confirm('Confermi di inviare l\'accertamento al Coordinatore?');" Text="INVIA ACCERTAMENTO A COORDINATORE" />
                         &nbsp;
                        <asp:Button ID="btnInviaInFirma" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnInviaInFirma_Click" OnClientClick="javascript:return validate();" Text="INVIA ACCERTAMENTO IN ATTESA DI FIRMA" />
                        <script type="text/javascript" language="javascript" >
                            function validate() {
                                if (Page_ClientValidate())
                                    return confirm('Confermi di inviare l\'accertamento in attesa di firma?');
                            }
                        </script> &nbsp;
                        <asp:Button ID="btnRimandaACoordinatore" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnRimandaACoordinatore_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'accertamento al Coordinatore?');" Text="RIMANDA ACCERTAMENTO A COORDINATORE" />

                        <asp:Button ID="btnInviaEmailConfermaAccertamento" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnInviaEmailConfermaAccertamento_Click" OnClientClick="javascript:return confirm('Confermi di inviare la email all\'impresa di manutenzione per la conferma dell\'accertamento?');" Text="INVIA EMAIL CONFERMA ACCERTAMENTO" />
                        <br /><br />
                        <asp:Button ID="btnRimandaAdAccertatore" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnRimandaAdAccertatore_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'accertamento all\'Accertatore?');" Text="RIMANDA ACCERTAMENTO AD ACCERTATORE" />&nbsp;
                        <br /><br />
                        <asp:Button ID="btnAccertamentoNonInviato" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnAccertamentoNonInviato_Click" OnClientClick="javascript:return confirm('Confermi di non inviare l\'accertamento?');" Text="ACCERTAMENTO NON INVIATO" />&nbsp;
                        <asp:Button ID="btnAccertamentoRigettato" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="290"
                            OnClick="btnAccertamentoRigettato_Click" OnClientClick="javascript:return confirm('Confermi di rigettare l\'accertamento?');" Text="ACCERTAMENTO RIGETTATO" />&nbsp;
                        <br /><br />
                        <asp:Button ID="btnInviaAdAgenteAccertatore" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="340"
                            OnClick="btnInviaAdAgenteAccertatore_Click" OnClientClick="javascript:return confirm('Confermi di inviare l\'accertamento ad Agente accertatore?');" Text="INVIA ACCERTAMENTO AD AGENTE ACCERTATORE" />
                        &nbsp;
                        <asp:Button ID="btnRimandaAdAgenteAccertatore" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="340"
                            OnClick="btnRimandaAdAgenteAccertatore_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'accertamento ad Agente accertatore?');" Text="RIMANDA ACCERTAMENTO AD AGENTE ACCERTATORE" />
                   
                        <asp:ValidationSummary ID="vsAccertamento" ValidationGroup="vgAccertamento" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
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