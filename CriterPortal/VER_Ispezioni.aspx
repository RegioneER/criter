<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_Ispezioni.aspx.cs" Inherits="VER_Ispezioni" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCIspezioneDocumenti.ascx" TagPrefix="ucIspDocumenti" TagName="UCIspezioneDocumenti" %>
<%@ Register Src="~/WebUserControls/Raccomandate/UCRaccomandate.ascx" TagPrefix="uc1" TagName="UCRaccomandate" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCIspezioneRapporto.ascx" TagPrefix="ucRI" TagName="UCIspezioneRapporto" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCGruppoVerifica.ascx" TagPrefix="ucGV" TagName="UCGruppoVerifica" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCIspezioneNotificaAperturaIspezione.ascx" TagPrefix="ucNotificaAperturaIspezione" TagName="UCIspezioneNotificaAperturaIspezione" %>
<%@ Register Src="~/WebUserControls/Ispezioni/UCIspezioneQuestionarioQualita.ascx" TagPrefix="ucIQ" TagName="UCIspezioniQuestionari" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate() {
            var isValid = false;
            isValid = Page_ClientValidate('vgIspezioneRapporto');
            if (isValid) {
                isValid = Page_ClientValidate('vgIspezione');
            }

            return isValid;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="890" ID="tblInfoGenerali" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>GESTIONE ISPEZIONE</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceVisita" AssociatedControlID="lblCodiceVisita" Text="Codice visita ispettiva" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblCodiceVisita" runat="server" Font-Bold="true" />
                        <asp:Label runat="server" ID="lblIDRapportoControllo" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoIspezione" AssociatedControlID="lblTipoIspezione" Text="Tipo ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblTipoIspezione" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowAccertamento" Visible="false">
                    <asp:TableCell runat="server" Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAccertamento" Text="Accertamento" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxHyperLink runat="server" ID="btnViewAccertamento" Text="Visualizza dati Accertamento"
                            Target="_blank" ClientInstanceName="btnViewAccertamento" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell CssClass="riempimento2" VerticalAlign="Middle">
                        <asp:Label runat="server" Text="Ispezione" />
                    </asp:TableCell>
                    <asp:TableCell CssClass="riempimento">
                        <dx:ASPxRadioButtonList Width="100%" CssClass="TableClassNoBorder" runat="server" ID="rblIspezioni"
                            EncodeHtml="false" AutoPostBack="true" OnValueChanged="rblIspezioni_ValueChanged" RepeatLayout="OrderedList" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>INFORMAZIONI GENERALI SULL'ISPEZIONE</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <%--<asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDescProgrammaIspezione" AssociatedControlID="lblProgrammaIspezione" Text="Descrizione Programma" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblProgrammaIspezione" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>--%>

                <%--<asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoIspezione" AssociatedControlID="lblTipoIspezione" Text="Tipo di ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblTipoIspezione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>--%>

                <asp:TableRow Visible="false">
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceIspezione" AssociatedControlID="lblCodiceIspezione" Text="Codice ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCodiceIspezione" Font-Bold="true" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="lblIspettore" Text="Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIspettore" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloOsservatore" AssociatedControlID="lblOsservatore" Text="Osservatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblOsservatore" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloStatoIspezione" AssociatedControlID="lblStatoIspezione" Text="Stato Ispezione" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <asp:Label ID="lblStatoIspezione" runat="server" />
                        <asp:Label ID="lblIDStatoIspezione" runat="server" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloNoteIspezione" AssociatedControlID="lblNoteIspezione" Text="Note Ispezione da coordinatore" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <asp:Label ID="lblNoteIspezione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataIspezione" AssociatedControlID="lblDataIspezione" Text="Data ispezione (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblDataIspezione" />
                        &nbsp;<asp:Label runat="server" ID="lblOrarioDaDesc" Visible="false" Text="dalle ore" />&nbsp;<asp:Label runat="server" ID="lblOrarioDa" />
                        &nbsp;<asp:Label runat="server" ID="lblOrarioADesc" Visible="false" Text="alle ore" />&nbsp;<asp:Label runat="server" ID="lblOrarioA" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Impresa di Manutenzione/Installazione</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloImpresaManutenzione" AssociatedControlID="lblImpresaManutenzione" Text="Impresa di manutenzione/installazione" />
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
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Responsabile Impianto</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloResponsabileImpianto" AssociatedControlID="lblResponsabileImpianto" Text="Responsabile Impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblResponsabileImpianto" Font-Bold="true" runat="server" /><br />
                        <asp:Label ID="lblResponsabileImpiantoIndirizzo" runat="server" /><br />
                        Email:<asp:Label ID="lblResponsabileImpiantoEmail" runat="server" /><br />
                        Email Pec:<asp:Label ID="lblResponsabileImpiantoEmailPec" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Impianto</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloImpianto" AssociatedControlID="lblDatiImpiantoCodiceTargatura" Text="Dati Impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        Indririzzo impianto:&nbsp;<asp:Label ID="lblDatiImpiantoIndirizzo" Font-Bold="true" runat="server" /><br />
                        Codice targatura:&nbsp;<asp:Label ID="lblDatiImpiantoCodiceTargatura" Font-Bold="true" runat="server" /><br />
                        Potenza:&nbsp;<asp:Label ID="lblDatiImpiantoPotenza" Font-Bold="true" runat="server" /><br />
                        Combustibile:&nbsp;<asp:Label ID="lblDatiImpiantoCombustibile" Font-Bold="true" runat="server" /><br />
                        <br />
                        <dx:ASPxHyperLink runat="server" ID="btnViewLibrettoImpianto" Text="Visualizza dati Libretto di Impianto"
                            Target="_blank" ClientInstanceName="btnViewLibrettoImpianto" />
                        <%--<br />
                        <dx:ASPxHyperLink runat="server" ID="btnViewRapportoDiControllo" Text="Visualizza dati Rapporto di Controllo"
                            Target="_blank" ClientInstanceName="btnViewRapportoDiControllo" Visible="false" />
                        <asp:Label ID="lblRCTlinq" runat="server" Text="NESSUN RAPPORTO DI CONTROLLO" Visible="false" />--%>

                        <br />
                        <br />
                        <asp:Button ID="LIM_LibrettiImpianti_btnModificaLibretto" runat="server" CssClass="buttonSmallClass" Width="270"
                            Text="MODIFICA DATI LIBRETTO IMPIANTO" CausesValidation="false" OnClientClick="return WindowModificaLibretto.Show();"
                            AutoPostBack="False" UseSubmitBehavior="false" />&nbsp;
                        <dx:ASPxPopupControl ID="ASPxPopupControlModificaLibretto" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                            CloseAction="CloseButton" CloseAnimationType="Fade"
                            EnableViewState="False" PopupElementID="popupAreaModificaLibretto"
                            PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"
                            Width="980px" Height="600px" MinWidth="980px" MinHeight="600px"
                            ClientInstanceName="WindowModificaLibretto" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                            <ContentStyle Paddings-Padding="0" />
                            <ClientSideEvents Shown="WindowModificaLibretto" CloseUp="function(s, e) { window.location.reload(true); }" />
                        </dx:ASPxPopupControl>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowRapportodiControllo">
                    <asp:TableCell runat="server" Width="290" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloRCTlinq" Text="Dati Rapporto di Controllo" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
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

                <asp:TableRow runat="server" ID="rowTitleIspezioniPrecedenti" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Dati Ispezioni Precedenti</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowIspezioniPrecedenti" Visible="false">
                    <asp:TableCell ColumnSpan="2">
                        <asp:DataGrid ID="dgIspezioniPrecedenti" CssClass="Grid" Width="100%" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="5" AllowSorting="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDIspezione"
                            OnItemDataBound="dgIspezioniPrecedenti_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDIspezione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDIspezioneVisita" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" ReadOnly="True" HeaderText="Codice Ispezione" />
                                <asp:BoundColumn DataField="Ispettore" ReadOnly="True" HeaderText="Ispettore" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90" HeaderText="Data Ispezione">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataIspezione" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("DataIspezione")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf RVI"
                                            AlternateText="Visualizza pdf RVI" ImageUrl="~/images/Buttons/pdfSmall.png" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Svolgimento Ispezione</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:CheckBox runat="server" ID="chkIsIspezioneSvolta" AutoPostBack="true" Text="ISPEZIONE SVOLTA?" OnCheckedChanged="chkIsIspezioneSvolta_CheckedChanged" /><br />
                        <asp:DropDownList ID="ddlSvolgimentoIspezione" Width="400" AutoPostBack="true" OnSelectedIndexChanged="ddlSvolgimentoIspezione_SelectedIndexChanged" TabIndex="1" ValidationGroup="vgIspezione" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlSvolgimentoIspezione" ValidationGroup="vgIspezione" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Motivo svolgimento ispezione: campo obbligatorio"
                            ControlToValidate="ddlSvolgimentoIspezione">&nbsp;*</asp:RequiredFieldValidator>

                        <asp:Panel runat="server" ID="pnlAltroSvolgimentoIspezione" Visible="false">
                            <asp:TextBox runat="server" ID="txtAltroSvolgimentoIspezione" ValidationGroup="vgIspezione" Width="390" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvAltroSvolgimentoIspezione" ValidationGroup="vgIspezione" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Altro Motivo svolgimento ispezione: campo obbligatorio"
                                ControlToValidate="txtAltroSvolgimentoIspezione">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Documenti Ispezione</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <ucIspDocumenti:UCIspezioneDocumenti ID="IspezioneDocumenti" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDatiFineIspezione1">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Sezione mancati accessi ispettivi</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDatiFineIspezione2">
                    <asp:TableCell Width="880" ColumnSpan="2" CssClass="riempimento">
                        <asp:Table Width="880" ID="tblInfoFineIspezione" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="550" CssClass="riempimento">
                                    <asp:Label runat="server" ID="lblTitolofIspezioneNonSvolta" AssociatedControlID="chkfIspezioneNonSvolta" Text="Non è stato possibile svolgere l'ispezione per indisponibilità del responsabile d'impianto per la prima volta" />
                                </asp:TableCell>
                                <asp:TableCell Width="330" CssClass="riempimento">
                                    <asp:CheckBox runat="server" ID="chkfIspezioneNonSvolta" AutoPostBack="true" OnCheckedChanged="chkfIspezioneNonSvolta_CheckedChanged" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server" ID="rowDataRipianificazioneIspezione">
                                <asp:TableCell Width="550" CssClass="riempimento">
                                    <asp:Label runat="server" ID="lblTitoloDataRipianificazioneIspezione" AssociatedControlID="txtDataIspezioneRipianificazione" Text="Data ripianificazione ispezione" />
                                </asp:TableCell>
                                <asp:TableCell Width="330" CssClass="riempimento">
                                    <asp:TextBox runat="server" ID="txtDataIspezioneRipianificazione" ValidationGroup="vgIspezione" Width="90" MaxLength="10" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator ID="rfvDataIspezioneRipianificazione" ValidationGroup="vgIspezione" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data ripianificazione ispezione: campo obbligatorio"
                                        ControlToValidate="txtDataIspezioneRipianificazione">&nbsp;*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator
                                        ID="revDataIspezioneRipianificazione" ValidationGroup="vgIspezione" ControlToValidate="txtDataIspezioneRipianificazione" Display="Dynamic" ForeColor="Red" ErrorMessage="Data ripianificazione ispezione: inserire la data nel formato gg/mm/aaaa"
                                        runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>

                                    da:<asp:DropDownList ID="ddlOrarioDa" Width="80" TabIndex="1" ValidationGroup="vgIspezione" runat="server" CssClass="selectClass_o" />
                                    <asp:RequiredFieldValidator ID="rfvddlOrarioDa" ValidationGroup="vgIspezione" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Orario da: campo obbligatorio"
                                        ControlToValidate="ddlOrarioDa">&nbsp;*</asp:RequiredFieldValidator>
                                    &nbsp;
                    a:<asp:DropDownList ID="ddlOrarioA" Width="80" TabIndex="1" ValidationGroup="vgIspezione" runat="server" CssClass="selectClass_o" />
                                    <asp:RequiredFieldValidator ID="rfvddlOrarioA" ValidationGroup="vgIspezione" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Orario a: campo obbligatorio"
                                        ControlToValidate="ddlOrarioA">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server" ID="rowIsPagamentoMai1">
                                <asp:TableCell Width="550" CssClass="riempimento">
                                    <asp:Label runat="server" ID="lblTitoloIsPagamentoMai1" AssociatedControlID="chkIsPagamentoMai1" Text="Pagamento MAI 1" />
                                </asp:TableCell>
                                <asp:TableCell Width="330" CssClass="riempimento">
                                    <asp:CheckBox runat="server" ID="chkIsPagamentoMai1" AutoPostBack="true" OnCheckedChanged="chkIsPagamentoMai1_CheckedChanged" />&nbsp;
                                    <asp:FileUpload ID="UploadFilePagamentoMai1" ToolTip="Scegli file" TabIndex="1" runat="server" Width="190px" />
                                    <asp:Button ID="btnUploadFilePagamentoMai1" runat="server" CssClass="buttonSmallClass" Text="Carica file..."
                                        OnClick="btnUploadFilePagamentoMai1_Click"
                                        CausesValidation="false"
                                        OnClientClick="disableBtn(this.id, 'Caricamento file...');"
                                        UseSubmitBehavior="false" />
                                </asp:TableCell>
                            </asp:TableRow>


                            <asp:TableRow runat="server" ID="rowfIspezioneNonSvolta2">
                                <asp:TableCell Width="550" CssClass="riempimento">
                                    <asp:Label runat="server" ID="lblTitolofIspezioneNonSvolta2" AssociatedControlID="chkfIspezioneNonSvolta2" Text="Non è stato possibile svolgere l'ispezione per indisponibilità del responsabile d'impianto per la seconda volta" />
                                </asp:TableCell>
                                <asp:TableCell Width="330" CssClass="riempimento">
                                    <asp:CheckBox runat="server" ID="chkfIspezioneNonSvolta2" AutoPostBack="true" OnCheckedChanged="chkfIspezioneNonSvolta2_CheckedChanged" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server" ID="rowIsPagamentoMai2">
                                <asp:TableCell Width="550" CssClass="riempimento">
                                    <asp:Label runat="server" ID="lblTitolIsPagamentoMai2" AssociatedControlID="chkIsPagamentoMai2" Text="Pagamento MAI 2" />
                                </asp:TableCell>
                                <asp:TableCell Width="330" CssClass="riempimento">
                                    <asp:CheckBox runat="server" ID="chkIsPagamentoMai2" AutoPostBack="true" OnCheckedChanged="chkIsPagamentoMai2_CheckedChanged" />
                                    <asp:FileUpload ID="UploadFilePagamentoMai2" ToolTip="Scegli file" TabIndex="1" runat="server" Width="190px" />&nbsp;
                                    <asp:Button ID="btnUploadFilePagamentoMai2" runat="server" CssClass="buttonSmallClass" Text="Carica file..."
                                        OnClick="btnUploadFilePagamentoMai2_Click"
                                        CausesValidation="false"
                                        OnClientClick="disableBtn(this.id, 'Caricamento file...');"
                                        UseSubmitBehavior="false" />
                                </asp:TableCell>
                            </asp:TableRow>


                            <%--<asp:TableRow>
                                <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                    RELAZIONE DEL RESPONSABILE DEL GRUPPO DI VERIFICA
                                    Descrizione dettagliata dell’audit (fornire una descrizione dettagliata dei rilievi evidenziando eventualmente il contesto organizzativo, il livello di collaborazione del responsabile verificato ed eventuali limiti oggettivi al corretto svolgimento dell’audit):
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                    <asp:TextBox Width="880" Height="200" ID="txtDescrizioneIspezione" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                                </asp:TableCell>
                            </asp:TableRow>--%>

                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <uc1:UCRaccomandate runat="server" ID="UCRaccomandate" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTitoloAvvisoPecTerzoResponsabile" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        Avviso Pec Terzo Responsabile
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowAvvisoPecTerzoResponsabile" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblAvvisoPecTerzoResponsabile" />
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow runat="server" ID="rowDocumentiPostVerificaTitle" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Documenti post verifica in campo</h3></asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDocumentiPostVerifica" Visible="false">
                    <asp:TableCell ColumnSpan="2">
                        <dx:ASPxFileManager ID="fileManagerDocumenti" runat="server" Height="500" Width="896">
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

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Storico cambio stati ispezione</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="890" ColumnSpan="2" CssClass="riempimento">
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true"
                            PageSize="100" AllowSorting="false" AllowPaging="false"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDIspezioneStato">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDIspezioneStato" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDIspezione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoIspezione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteUltimaModifica" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Data" ItemStyle-Width="190px" HeaderText="Data" ReadOnly="True" />
                                <asp:BoundColumn DataField="Utente" HeaderText="Utente" ItemStyle-Width="300px" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoIspezione" HeaderText="Stato ispezione" ItemStyle-Width="500px" ReadOnly="True" />
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <cc1:Accordion ID="Accordion1"
                            runat="server"
                            SelectedIndex="1"
                            AutoSize="None"
                            FadeTransitions="true"
                            TransitionDuration="250"
                            FramesPerSecond="40"
                            RequireOpenedPane="false"
                            SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" HeaderCssClass="riempimento1" runat="server">
                                    <Header>
                                        <div style="cursor: pointer; border: solid; border-width: 1px; width: 99.8%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                            Rapporto di Ispezione
                                        </div>
                                    </Header>
                                    <Content>
                                        <ucRI:UCIspezioneRapporto ID="IspezioneRapportoControl" runat="server" ValidationGroup="vgIspezione" />
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowGruppoVerifica" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <cc1:Accordion ID="Accordion2"
                            runat="server"
                            SelectedIndex="1"
                            AutoSize="None"
                            FadeTransitions="true"
                            TransitionDuration="250"
                            FramesPerSecond="40"
                            RequireOpenedPane="false"
                            SuppressHeaderPostbacks="true">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane2" HeaderCssClass="riempimento1" runat="server">
                                    <Header>
                                        <div style="cursor: pointer; border: solid; border-width: 1px; width: 99.8%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                            Gruppo di Verifica
                                        </div>
                                    </Header>
                                    <Content>
                                        <ucGV:UCGruppoVerifica ID="IspezioneGruppoVerifica" runat="server" />
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Note</h3></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDatiFineIspezione0">
                    <asp:TableCell Width="880" ColumnSpan="2" CssClass="riempimento">
                        <asp:TextBox Width="890" Height="200" ID="txtNote" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <ucNotificaAperturaIspezione:UCIspezioneNotificaAperturaIspezione ID="UCIspezioneNotificaAperturaIspezione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTitoloNotaCoordinatore" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        Note Coordinatore
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowNotaCoordinatore" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <asp:TextBox Width="880" Height="200" ID="txtNotaCoordinatore" CssClass="txtClass" runat="server" TextMode="MultiLine" Rows="6" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowQuestionariQualita" Visible="false">
                    <asp:TableCell ColumnSpan="2">
                        <ucIQ:UCIspezioniQuestionari ID="UCIspezioniQuestionariQualita" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>



                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                          &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <%--<asp:CustomValidator ID="cvDocumentiIspezioneInseriti" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaDocumentiIspezioneInseriti"
                            CssClass="testoerr"
                            ValidationGroup="vgIspezione" />--%>
                        <asp:Label runat="server" CssClass="testoerr" ID="lblFeedbackDocumentiIspezioneInseriti" ForeColor="Red" Text="<br/><br/>Attenzione: per completare l'ispezione è necessario caricare nella sezione documentazione tutti i documenti obbligatori!<br/><br/><br/>" Visible="false" />
                        <asp:Label runat="server" CssClass="testoerr" ID="lblFeedbackDocumentiDoppiaMai" ForeColor="Red" Text="<br/><br/>Attenzione: per completare l'ispezione è necessario compilare la sezione 'Mancati Accessi Ispettivi'!<br/><br/><br/>" Visible="false" />


                        <asp:Button ID="btnSaveIspezione" runat="server" TabIndex="1" CssClass="buttonClass" Width="330" OnClientClick="return Validate()"
                            OnClick="btnSaveIspezione_Click" ValidationGroup="vgIspezione" Text="SALVA DATI ISPEZIONE" />
                        &nbsp;
                        <asp:Button ID="btnConcludiIspezioneIspettore" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnConcludiIspezioneIspettore_Click" ValidationGroup="vgIspezione" />
                        &nbsp;
                        <asp:Button ID="btnRimandaIspezioneAdIspettore" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClientClick="return WindowRimandaAdIspettore.Show();"
                            AutoPostBack="False" UseSubmitBehavior="false" Text="RIMANDA ISPEZIONE AD ISPETTORE" />

                        <dx:ASPxPopupControl ID="ASPxPopupControlRimandaAdIspettore" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                            CloseAction="CloseButton" CloseAnimationType="Fade"
                            EnableViewState="False" PopupElementID="popupAreaRimandaAdIspettore"
                            PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"
                            Width="980px" Height="400px" MinWidth="980px" MinHeight="400px"
                            ClientInstanceName="WindowRimandaAdIspettore" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                            <ContentStyle Paddings-Padding="0" />
                            <ClientSideEvents Shown="WindowRimandaAdIspettore" CloseUp="function(s, e) { window.location.reload(true); }" />
                        </dx:ASPxPopupControl>


                        <br />
                        <br />
                        <asp:Button ID="btnConcludiIspezioneCoordinatoreConAccertamento" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnConcludiIspezioneCoordinatoreConAccertamento_Click" ValidationGroup="vgIspezione" OnClientClick="javascript:return confirm('Confermi di concludere l\'Ispezione ed inviarlo in accertamento?');" Text="CONCLUDI ISPEZIONE CON ACCERTAMENTO TIPO A" />
                        &nbsp;
                         <asp:Button ID="btnConcludiIspezioneCoordinatoreSenzaAccertamento" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                             OnClick="btnConcludiIspezioneCoordinatoreSenzaAccertamento_Click" OnClientClick="javascript:return confirm('Confermi di concludere l\'Ispezione senza inviarlo in accertamento?');" Text="CONCLUDI ISPEZIONE CON ACCERTAMENTO TIPO B" />
                        <br />
                        <br />
                        <asp:Button ID="btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso_Click" ValidationGroup="vgIspezione" OnClientClick="javascript:return confirm('Confermi di concludere l\'Ispezione in stato doppio mancato accesso?');" Text="INVIA ISPEZIONE CON DOPPIO MANCATO ACCESSO" />
                        &nbsp;
                         <asp:Button ID="btnConcludiIspezioneCoordinatoreUtenteSconosciuto" Visible="false" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                             OnClick="btnConcludiIspezioneCoordinatoreUtenteSconosciuto_Click" OnClientClick="javascript:return confirm('Confermi di concludere l\'Ispezione in stato con utente sconosciuto?');" Text="INVIA ISPEZIONE CON UTENTE SCONOSCIUTO" />
                        <br />
                        <br />
                        <asp:Button ID="btnAnnullaIspezione" runat="server" Visible="false" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnAnnullaIspezione_Click" OnClientClick="javascript:return confirm('Confermi di annullare l\'Ispezione?');" Text="ANNULLA ISPEZIONE" />

                        <asp:ValidationSummary ID="vsIspezione" ValidationGroup="vgIspezione" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadFilePagamentoMai1" />
            <asp:PostBackTrigger ControlID="btnUploadFilePagamentoMai2" />
        </Triggers>
    </asp:UpdatePanel>
    <uc1:WebUSUpdateProgress runat="server" ID="WebUSUpdateProgress" />
    <%--<asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
