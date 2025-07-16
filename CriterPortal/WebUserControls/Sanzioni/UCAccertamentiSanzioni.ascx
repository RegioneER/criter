<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAccertamentiSanzioni.ascx.cs" Inherits="WebUserControls_Sanzioni_UCAccertamentiSanzioni" %>
<%@ Register Src="~/WebUserControls/Raccomandate/UCRaccomandate.ascx" TagPrefix="uc1" TagName="UCRaccomandate" %>
<%@ Register Src="~/WebUserControls/WUC_ChangeResponsabileLibretto.ascx" TagPrefix="uc1" TagName="UCChangeResponsabileLibretto" %>

<asp:Label runat="server" ID="lblIDAccertamento" Visible="false" />
<asp:Label runat="server" ID="lblIDTipoAccertamento" Visible="false" />
<asp:Label runat="server" ID="lblTipoPageSanzione" Visible="false" />

<asp:Table ID="tblInfoSanzione" Width="100%" runat="server">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
            <h3>INFORMAZIONI GENERALI NOTIFICHE SANZIONI</h3>
            <asp:Label runat="server" ID="lblIDAccertatore" Visible="false" />
            <asp:Label runat="server" ID="lblIDCoordinatore" Visible="false" />
            <asp:Label runat="server" ID="lblIDAgenteAccertatore" Visible="false" />
            <asp:Label runat="server" ID="lblIDRapportoControllo" Visible="false" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Accertamenti_lblCodiceAccertamentoSanzione" AssociatedControlID="lblCodiceAccertamentoSanzione" Text="Codice sanzione" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label runat="server" ID="lblCodiceAccertamentoSanzione" Font-Bold="true" ForeColor="Green" />
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

    <asp:TableRow runat="server" ID="row0">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="lblTitoloStatoSanzioneAccertamento" AssociatedControlID="lblStatoSanzioneAccertamento" Text="Stato sanzione" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label ID="lblStatoSanzioneAccertamento" runat="server" />
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
            Indirizzo impianto:
            <asp:Label runat="server" ID="lblIndirizzoImpianto" /><br />
            <dx:ASPxHyperLink runat="server" ID="btnViewLibrettoImpianto" Text="Visualizza dati Libretto di Impianto"
                Target="_blank" ClientInstanceName="btnViewLibrettoImpianto" />
            &nbsp;&nbsp;&nbsp;
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Accertamenti_lblDatiIspezione" Text="Dati Ispezione" AssociatedControlID="btnViewIspezione" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <dx:ASPxHyperLink runat="server" ID="btnViewIspezione" Text="Visualizza dati Ispezione"
                Target="_blank" ClientInstanceName="btnViewIspezione" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row10">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Accertamenti_lblRapportoControlloTecnico" Text="Dati Rapporto di Controllo Tecnico" AssociatedControlID="dgRapporti" />
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

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Info sanzione</h3></asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row1">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblDataInvioRaccomandata" AssociatedControlID="lblDataInvioRaccomandata" Text="Data invio raccomandata" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label ID="lblDataInvioRaccomandata" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row2">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblDataRicevimentoRaccomandata" AssociatedControlID="lblDataRicevimentoRaccomandata" Text="Data ricevimento raccomandata" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label ID="lblDataRicevimentoRaccomandata" runat="server" />
            <asp:Button ID="btnViewAggiungiDataRicevimentoRaccomandata" runat="server" Text="Aggiungi data ricevimento raccomandata"
                OnClick="btnViewAggiungiDataRicevimentoRaccomandata_Click" Visible="false" CssClass="buttonSmallClass" Width="250px" />
            <asp:Panel runat="server" ID="pnlDataRicevimentoRaccomandata" Visible="false">
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

    <asp:TableRow runat="server" ID="row3">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblDataScadenzaSanzione" AssociatedControlID="lblDataScadenzaSanzione" Text="Data scadenza ricezione scritti difensivi" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label ID="lblDataScadenzaSanzione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row9">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblDataScadenzaPagamentoRidottoSanzione" AssociatedControlID="lblDataScadenzaPagamentoRidottoSanzione" Text="Data scadenza pagamento misura ridotta" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:Label ID="lblDataScadenzaPagamentoRidottoSanzione" runat="server" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row7">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblNote" AssociatedControlID="txtNote" Text="Note" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox Width="95%" Height="150" ID="txtNote" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="3" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row6">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblDataRicevutaPagamento" AssociatedControlID="txtDataRicevutaPagamento" Text="Data ricevuta scritti difensivi/pagamento misura ridotta" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:TextBox ID="txtDataRicevutaPagamento" runat="server" MaxLength="10" Width="80px" ValidationGroup="vgDataRicevutaPagamento" CssClass="txtClass" />&nbsp;
            <asp:RegularExpressionValidator
                ID="revDataRicevutaPagamento" ValidationGroup="vgDataRicevutaPagamento" ControlToValidate="txtDataRicevutaPagamento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data ricevuta pagamento: inserire la data nel formato gg/mm/aaaa"
                runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row8">
        <asp:TableCell Width="300" CssClass="riempimento2">
            <asp:Label runat="server" ID="VER_Interventi_lblRevocaSanzione" AssociatedControlID="chkRevocaSanzione" Text="Revoca sanzione" />
        </asp:TableCell>
        <asp:TableCell Width="600" CssClass="riempimento">
            <asp:CheckBox runat="server" ID="chkRevocaSanzione" Text="Si" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="chkRevocaSanzione_CheckedChanged" /><br />
            <asp:Panel runat="server" ID="pnlRevocaSanzione">
                <asp:TextBox Width="95%" Height="150" ID="txtMotivoRevocaSanzione" CssClass="txtClass_o" ValidationGroup="vgSanzione" runat="server" TextMode="MultiLine" Rows="3" />
                <asp:RequiredFieldValidator
                    ID="rfvtxtMotivoRevocaSanzione" ForeColor="Red" runat="server" ValidationGroup="vgSanzione" EnableClientScript="true" ErrorMessage="Motivo revoca sanzione: campo obbligatorio"
                    ControlToValidate="txtMotivoRevocaSanzione">&nbsp;*</asp:RequiredFieldValidator>
            </asp:Panel>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row14">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Documentazione sanzione</h3></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="row5">
        <asp:TableCell ColumnSpan="2">
            <asp:Label runat="server" ID="lblCodiceAccertamento" Visible="false" />
            <dx:ASPxFileManager ID="fileManagerDocumenti" runat="server" Height="400" Width="99%">
                <Settings AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.docx,.png,.xls,.xlsx,.doc,.pdf,.msg,.p7m" />
                <SettingsEditing AllowCopy="false" AllowCreate="false" AllowDelete="true" AllowDownload="true" AllowMove="false" AllowRename="true" />
                <SettingsFolders Visible="false" />
                <SettingsFileList ShowFolders="false" View="Details" ShowParentFolder="false" />
                <SettingsBreadcrumbs Visible="false" ShowParentFolderButton="false" Position="Top" />
                <SettingsUpload UseAdvancedUploadMode="true" Enabled="true">
                    <AdvancedModeSettings EnableMultiSelect="true" />
                </SettingsUpload>
                <Styles FolderContainer-Width="25%" UploadPanel-Height="60" ToolbarItem-Width="1" UploadPanel-Font-Size="10" />
                <SettingsToolbar ShowPath="false" />
            </dx:ASPxFileManager>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row11">
        <asp:TableCell ColumnSpan="2">
            <uc1:UCRaccomandate runat="server" ID="UCRaccomandate" />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow runat="server" ID="row12">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Note analisi sanzione</h3></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="row13">
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
                Styles-AlternatingRow-CssClass="GridAlternativeItem" Styles-Row-CssClass="GridItem">
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

                    <dx:GridViewDataColumn FieldName="IsSanzione" Caption="IsSanzione" Visible="false" VisibleIndex="5">
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
                Where="it.IDAccertamento=@IDAccertamento and it.IsSanzione=true"
                OrderBy="it.Data">
                <WhereParameters>
                    <asp:Parameter Name="IDAccertamento" Type="Int32" DefaultValue="0" />
                </WhereParameters>
            </ef:EntityDataSource>
        </asp:TableCell>
    </asp:TableRow>


    <asp:TableRow runat="server" ID="row4">
        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                &nbsp;
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento">
            <asp:ValidationSummary ID="vsSanzione" ValidationGroup="vgSanzione" runat="server" ShowMessageBox="True"
                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>

            <asp:Button ID="btnAnteprimaSanzione" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                OnClientClick="return WindowDocumentoSanzione.Show();" Text="VISUALIZZA VERBALE DI ACC. CON SANZIONE" />

            <dx:ASPxPopupControl ID="WindowDocumentoSanzione"
                runat="server"
                Modal="true"
                AllowDragging="True"
                AllowResize="True"
                CloseAction="CloseButton"
                CloseAnimationType="Fade"
                EnableHierarchyRecreation="true"
                PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"
                Width="905px" Height="600px" MinWidth="905px" MinHeight="600px"
                ClientInstanceName="WindowDocumentoSanzione" FooterStyle-Wrap="True">
                <ContentStyle Paddings-Padding="0" />
                <ClientSideEvents Shown="WindowDocumentoSanzione" />
            </dx:ASPxPopupControl>
            &nbsp;
             <asp:Button ID="btnInviaAccertamentoConSanzione" runat="server" TabIndex="1" CssClass="buttonClass" Width="370"
                 OnClick="btnInviaAccertamentoConSanzione_Click" OnClientClick="javascript:return confirm('Confermi di inviare il verbale di accertamento con sanzione in attesa di firma?');" Text="INVIA VERBALE DI ACC. CON SANZIONE IN ATTESA DI FIRMA" />
            &nbsp;
            <asp:Button ID="btnSalvaAccertamentoConSanzione" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                OnClick="btnSalvaAccertamentoConSanzione_Click" Text="SALVA SANZIONE" />
            &nbsp;
            <asp:Button ID="btnInviaSanzioneRevoca" runat="server" TabIndex="1" CausesValidation="true" ValidationGroup="vgSanzione" CssClass="buttonClass" Width="280"
                OnClick="btnInviaSanzioneRevoca_Click" OnClientClick="javascript:return confirm('Confermi di revocare la sanzione?');" Text="INVIA REVOCA SANZIONE" />
            &nbsp;<br />
            <br />
            <asp:Button ID="btnInviaFascicoloSanzioneInRegione" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                OnClick="btnInviaFascicoloSanzioneInRegione_Click" OnClientClick="javascript:return confirm('Confermi di inviare il fascicolo della sanzione in Regione?');" Text="INVIA FASCICOLO IN REGIONE" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
