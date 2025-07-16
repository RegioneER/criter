<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_Interventi.aspx.cs" Inherits="VER_Interventi" %>
<%--<%@ Register Src="~/WebUserControls/WUC_GoogleAutosuggest.ascx" TagPrefix="uc1" TagName="UCGoogleAutosuggest" %>--%>
<%@ Register Src="~/WebUserControls/Raccomandate/UCRaccomandate.ascx" TagPrefix="uc1" TagName="UCRaccomandate" %>
<%@ Register Src="~/WebUserControls/WUC_ChangeResponsabileLibretto.ascx" TagPrefix="uc1" TagName="UCChangeResponsabileLibretto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoGenerali" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>INFORMAZIONI GENERALI INTERVENTI SU ACCERTAMENTI</h2>
                        <asp:Label runat="server" ID="lblIDRapportoControllo" Visible="false" />
                        <asp:Label runat="server" ID="lblIDStatoAccertamentoSanzione" Visible="false" />
                        <asp:Label ID="lblCodiceAccertamento" Visible="false" runat="server" />
                        <asp:Label ID="lblIDTipoAccertamento" Visible="false" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblCodiceAccertamento" AssociatedControlID="lblCodiceAccertamento" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxHyperLink runat="server" ID="btnViewAccertamento" Font-Bold="true" ForeColor="Green" ToolTip="Visualizza dati Accertamento"
                            Target="_blank" ClientInstanceName="btnViewAccertamento" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Interventi_lblDataInvioRaccomandata" AssociatedControlID="lblDataInvioRaccomandata" Text="Data invio raccomandata" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataInvioRaccomandata" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Interventi_lblDataRicevimentoRaccomandata" AssociatedControlID="lblDataRicevimentoRaccomandata" Text="Data ricevimento raccomandata" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataRicevimentoRaccomandata" runat="server" />
                        <asp:Button ID="btnViewAggiungiDataRicevimentoRaccomandata" runat="server" Text="Aggiungi data ricevimento raccomandata" 
                                OnClick="btnViewAggiungiDataRicevimentoRaccomandata_Click" Visible="false" CssClass="buttonSmallClass" Width="250px" />
                        <asp:Panel runat="server" ID="pnlDataRicevimentoRaccomandata" Visible="false" >
                            <asp:TextBox ID="txtDataRicevimentoRaccomandata" runat="server" MaxLength="10" Width="80px" ValidationGroup="vgDataRicevimentoRaccomandata" CssClass="txtClass_o" />&nbsp;
                            <asp:RequiredFieldValidator
                                ID="rfvDataRicevimentoRaccomandata" ForeColor="Red" runat="server" ValidationGroup="vgDataRicevimentoRaccomandata" EnableClientScript="true" 
                                ErrorMessage="Data ricevimento raccomandata: campo obbligatorio"
                                ControlToValidate="txtDataRicevimentoRaccomandata">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revDataRicevimentoRaccomandata" ValidationGroup="vgDataRicevimentoRaccomandata" ControlToValidate="txtDataRicevimentoRaccomandata" Display="Dynamic" ForeColor="Red" ErrorMessage="Data ricevimento raccomandata: inserire la data nel formato gg/mm/aaaa"
                                runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>

                            <asp:Button ID="btnUpdateDataRicevimentoRaccomandata" runat="server" Text="Aggiungi data" OnClientClick="javascript:return confirm('Confermi di inserire la data di ricevimento della raccomandata?');" OnClick="btnUpdateDataRicevimentoRaccomandata_Click" CssClass="buttonSmallClass" Width="100px"
                                ValidationGroup="vgDataRicevimentoRaccomandata" />&nbsp;
                            <asp:Button ID="btnAnnullaDataRicevimentoRaccomandata" runat="server" Text="Annulla" CssClass="buttonSmallClass" Width="100px"
                                CausesValidation="false" OnClick="btnAnnullaDataRicevimentoRaccomandata_Click" />
                            <asp:ValidationSummary ID="vsDataRicevimentoRaccomandata" ValidationGroup="vgDataRicevimentoRaccomandata" runat="server" ShowMessageBox="True"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Interventi_lblDataScadenzaIntervento" AssociatedControlID="lblDataScadenzaIntervento" Text="Data scadenza interventi" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataScadenzaIntervento" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Interventi_lblStatoAccertamentoIntervento" AssociatedControlID="lblStatoAccertamentoIntervento" Text="Stato interventi" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblStatoAccertamentoIntervento" ForeColor="Green" />
                        <asp:Label runat="server" ID="lblIDStatoAccertamentoIntervento" Visible="false" />
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
                        <%--<asp:Label ID="lblResponsabile" Font-Bold="true" runat="server" /><br /><br />--%>
                        <%--<uc1:UCGoogleAutosuggest runat="server" ID="UCGoogleAutosuggest" />--%>
                        <uc1:UCChangeResponsabileLibretto runat="server" ID="UCChangeResponsabileLibretto" />
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
                            Target="_blank" ClientInstanceName="btnViewLibrettoImpianto" />&nbsp;&nbsp;&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloComune" AssociatedControlID="lblComune" Text="Comune" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblComune" /><br />
                        <asp:Label runat="server" ID="lblEmailPecComune" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <h3>Dati Rapporto di Controllo Tecnico</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblRapportoControlloTecnico" AssociatedControlID="lblDatiImpiantoCodiceTargatura" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
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
                    </asp:TableCell>
                </asp:TableRow>
                
                 <asp:TableRow runat="server" ID="rowTitleDatiIspezione">
                     <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                         <h3>Dati Ispezione</h3>
                     </asp:TableCell>
                 </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDatiIspezione">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="VER_Accertamenti_lblDatiIspezione" AssociatedControlID="btnViewIspezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxHyperLink runat="server" ID="btnViewIspezione" Text="Visualizza dati Ispezione"
                            Target="_blank" ClientInstanceName="btnViewIspezione" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Documenti Accertamento/Interventi</h3></asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <dx:ASPxFileManager ID="fileManagerDocumenti" runat="server" Height="500" >
                            <Settings AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.docx,.png,.xls,.xlsx,.doc,.pdf,.msg,.p7m" />
                            <SettingsEditing AllowCopy="false" AllowCreate="false" AllowDelete="true" AllowDownload="true" AllowMove="false" AllowRename="true" />
                            <SettingsFolders Visible="false" />
                            <SettingsFileList ShowFolders="false" View="Details" ShowParentFolder="false" />
                            <SettingsBreadcrumbs Visible="false" ShowParentFolderButton="false" Position="Top" />
                            <SettingsUpload UseAdvancedUploadMode="true" Enabled="true" >
                                <AdvancedModeSettings EnableMultiSelect="true" />
                            </SettingsUpload>
                            <Styles FolderContainer-Width="25%" UploadPanel-Height="60" ToolbarItem-Width="1" UploadPanel-Font-Size="10" />
                            <SettingsToolbar ShowPath="false" />
                        </dx:ASPxFileManager>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Non conformità rilevate</h3></asp:TableCell>
                </asp:TableRow>
                                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:Table ID="tblNonConformita" Width="895px" Font-Size="XX-Small" runat="server">
                            <asp:TableRow>
                                <asp:TableCell CssClass="riempimento4">
                                    <asp:DataGrid ID="dgNonConformita" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                        CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" 
                                        AllowSorting="false" AllowPaging="false" ShowHeader="true"
                                        AutoGenerateColumns="False" runat="server" DataKeyField="IDNonConformita"
                                        OnItemDataBound="dgNonConformita_ItemDataBound">
                                        <ItemStyle CssClass="GridItem" />
                                        <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                        <Columns>
                                            <asp:BoundColumn DataField="IDNonConformita" Visible="false" ReadOnly="True" />
                                            <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                            <asp:BoundColumn DataField="fRealizzazioneIntervento" Visible="false" ReadOnly="True" />
                                            <asp:BoundColumn DataField="DataRealizzazioneIntervento" Visible="false" ReadOnly="True" />
                                            <asp:BoundColumn DataField="Tipo" Visible="false" ReadOnly="True" />
                                            <asp:BoundColumn DataField="IDTipologiaInterventoAccertamento" Visible="false" ReadOnly="True" />
                                            <asp:TemplateColumn ItemStyle-Width="60" HeaderText="Procedura">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblProceduraAccertamento" Text='<%#Eval("ProceduraAccertamento") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="140" HeaderText="Tipo non conformità">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTipoNonConformita" Text='<%#Eval("TipoNonConformita") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="370" HeaderText="Non conformità">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblNonConformita" Text='<%#Eval("NonConformita") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="320" HeaderText="Risoluzione non conformità">
                                                <ItemTemplate>
                                                    <asp:Table runat="server" ID="tblRisoluzione">
                                                        <asp:TableRow CssClass="riempimento">
                                                            <asp:TableCell>
                                                                 <asp:Label runat="server" ID="lblTitoloRealizzazioneIntervento" AssociatedControlID="chkRealizzazioneIntervento" Text="Risolto (si/no)" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <dx:ASPxCheckBox ID="chkRealizzazioneIntervento" runat="server" ClientInstanceName="chkRealizzazioneIntervento" 
                                                                    Checked='<%#Eval("fRealizzazioneIntervento") %>' AutoPostBack="true" OnCheckedChanged="chkRealizzazioneIntervento_CheckedChanged" 
                                                                    ToggleSwitchDisplayMode="Always" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="riempimento" runat="server" ID="rowRealizzazioneIntervento0">
                                                            <asp:TableCell>
                                                                 <asp:Label runat="server" ID="lblTitoloTipologiaInterventoAccertamento" AssociatedControlID="rblTipologiaInterventoAccertamento" Text="Tipo intervento" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:RadioButtonList ID="rblTipologiaInterventoAccertamento" TabIndex="1" runat="server" 
                                                                    RepeatDirection="Horizontal" RepeatColumns="1" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="riempimento" runat="server" ID="rowRealizzazioneIntervento1">
                                                            <asp:TableCell>
                                                                 <asp:Label runat="server" ID="lblTitoloDataIntervento" AssociatedControlID="txtDataIntervento" Text="Data intervento" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:TextBox runat="server" ID="txtDataIntervento" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("DataRealizzazioneIntervento")) %>' ValidationGroup="vgIntervento" Width="90" MaxLength="10" CssClass="txtClass_o" />
                                                                <asp:RequiredFieldValidator ID="rfvDataIntervento" ValidationGroup="vgIntervento" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data realizzazione intervento: campo obbligatorio"
                                                                    ControlToValidate="txtDataIntervento">&nbsp;*</asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator
                                                                    ID="revDataIntervento" ValidationGroup="vgIntervento" ControlToValidate="txtDataIntervento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data realizzazione intervento: inserire la data nel formato gg/mm/aaaa"
                                                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="riempimento" runat="server" ID="rowRealizzazioneIntervento2">
                                                            <asp:TableCell>
                                                                 <asp:Label runat="server" ID="lblTitoloNoteIntervento" AssociatedControlID="txtNoteIntervento" Text="Note intervento" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtNoteIntervento" CssClass="txtClass" Width="230" Height="80" 
                                                                    Rows="3" Text='<%#Eval("NoteIntervento") %>' />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow CssClass="riempimento" runat="server" ID="rowRealizzazioneIntervento3">
                                                            <asp:TableCell>
                                                                 <asp:Label runat="server" ID="lblTitoloUtenteRealizzazioneIntervento" AssociatedControlID="lblUtenteRealizzazioneIntervento" Text="Utente" />
                                                            </asp:TableCell>
                                                            <asp:TableCell>
                                                                <asp:Label runat="server" ID="lblIDUtenteRealizzazioneIntervento" Visible="false" Text='<%#Eval("IDUtenteIntervento") %>' />
                                                                <asp:Label runat="server" ID="lblUtenteRealizzazioneIntervento" Text='<%#Eval("UtenteRealizzazioneIntervento") %>' />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf Revoca intervento" 
                                                        AlternateText="Visualizza pdf Revoca intervento" ImageUrl="~/images/Buttons/pdf.png"
                                                        TabIndex="1" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="IDProceduraAccertamento" Visible="false" ReadOnly="True" />
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowSavePartial" runat="server">
                                <asp:TableCell ColumnSpan="4" HorizontalAlign="Right" CssClass="riempimento5">
                                    <dx:ASPxButton runat="server" ID="btnSavePartial" CssClass="buttonSmallSaveClass" Text="Salva dati interventi"
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
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Storico cambio stati interventi</h3></asp:TableCell>
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
                                <asp:BoundColumn DataField="IDStatoAccertamentoIntervento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteUltimaModifica" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Data" ItemStyle-Width="190px" HeaderText="Data" ReadOnly="True" />
                                <asp:BoundColumn DataField="Utente" HeaderText="Utente" ItemStyle-Width="300px" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoAccertamentoIntervento" HeaderText="Stato intervento" ItemStyle-Width="500px" ReadOnly="True" />
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                       <uc1:UCRaccomandate runat="server" ID="UCRaccomandate" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Note per ispezione</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:TextBox Width="890" Height="200" ID="txtNote" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <%--<asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <ucAccertamentiSanzioni:UCAccertamentiSanzioni ID="UCSanzione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>--%>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                          &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnSaveIntervento" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnSaveIntervento_Click" ValidationGroup="vgIntervento" Text="SALVA DATI INTERVENTI" />&nbsp;
                        <asp:Button ID="btnSaveInterventoInAttesaDiRealizzazione" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnInterventoInAttesaDiRealizzazione_Click" ValidationGroup="vgIntervento" Text="INVIA IN ATTESA DI REALIZZAZIONE" OnClientClick="javascript:return confirm('Confermi di inviare l intervento in attesa di realizzazione?');" />&nbsp;

                        <br /><br />
                        <asp:Button ID="btnRevocaIntervento" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnRevocaIntervento_Click" ValidationGroup="vgIntervento" Text="INVIA REVOCA INTERVENTI" OnClientClick="javascript:return confirm('Confermi di inviare la revoca degli Interventi?');" />&nbsp;

                        <asp:Button ID="btnRevocaInterventoSenzaComunicazione" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnRevocaInterventoSenzaComunicazione_Click" ValidationGroup="vgIntervento" Text="INVIA REVOCA INTERVENTI SENZA COMUNIC." OnClientClick="javascript:return confirm('Confermi di inviare la revoca degli Interventi senza comunicazioni?');" />
                        
                        <br /><br />
                        <asp:Button ID="btnInviaInAttesaIspezione" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="280"
                            OnClick="btnInviaInAttesaIspezione_Click" OnClientClick="javascript:return confirm('Confermi di inviare l\'accertamento in attesa Ispezione?');" Text="INVIA ACC. IN ATTESA DI ISPEZIONE" />
                        &nbsp;
                        <asp:Button ID="btnInviaAccertamentoConSanzione" runat="server" TabIndex="1" CssClass="buttonClass" Width="370"
                            OnClick="btnInviaAccertamentoConSanzione_Click" OnClientClick="javascript:return confirm('Confermi di inviare il verbale di accertamento con sanzione in attesa di firma?');" Text="INVIA VERBALE DI ACC. CON SANZIONE IN ATTESA DI FIRMA" /> 
                         
                        <asp:ValidationSummary ID="vsIntervento" ValidationGroup="vgIntervento" runat="server" ShowMessageBox="True"
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