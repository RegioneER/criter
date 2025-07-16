<%@ Page Title="Criter - Libretti di impianti" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_LibrettiImpianti.aspx.cs" Inherits="LIM_LibrettiImpianti" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/TerzoResponsabile.ascx" TagName="TerzoResponsabile" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/GruppiTermici.ascx" TagName="GruppiTermici" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/MacchineFrigorifere.ascx" TagName="MacchineFrigorifere" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/ScambiatoriCalore.ascx" TagName="ScambiatoriCalore" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/Cogeneratori.ascx" TagName="Cogeneratori" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/CampiSolariTermici.ascx" TagName="CampiSolariTermici" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/AltriGeneratori.ascx" TagName="AltriGeneratori" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/PompeCircolazione.ascx" TagName="PompeCircolazione" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/ValvoleRegolazione.ascx" TagName="ValvoleRegolazione" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/Accumuli.ascx" TagName="Accumuli" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/CircuitiInterrati.ascx" TagName="CircuitiInterrati" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/ImpiantiVMC.ascx" TagName="ImpiantiVMC" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/RaffreddatoriLiquido.ascx" TagName="RaffreddatoriLiquido" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/RecuperatoriCalore.ascx" TagName="RecuperatoriCalore" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/ScambiatoriCaloreIntermedi.ascx" TagName="ScambiatoriCaloreIntermedi" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/SistemiRegolazione.ascx" TagName="SistemiRegolazione" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/TorriEvaporative.ascx" TagName="TorriEvaporative" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UnitaTrattamentoAria.ascx" TagName="UnitaTrattamentoAria" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/DescrizioneSistemi.ascx" TagName="DescrizioneSistemi" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/VasiEspansione.ascx" TagName="VasiEspansione" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_VerifichePeriodicheGT.ascx" TagName="VerifichePeriodicheGT" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_VerifichePeriodicheGF.ascx" TagName="VerifichePeriodicheGF" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_VerifichePeriodicheSC.ascx" TagName="VerifichePeriodicheSC" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_VerifichePeriodicheCG.ascx" TagName="VerifichePeriodicheCG" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_InterventoControlloEfficienza.ascx" TagName="InterventoControlloEfficienza" TagPrefix="criter" %>

<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoCombustibile.ascx" TagName="ConsumoCombustibile" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoEnergiaElettrica.ascx" TagName="ConsumoEnergiaElettrica" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoAcqua.ascx" TagName="ConsumoAcqua" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoProdottiChimici.ascx" TagName="ConsumoProdottiChimici" TagPrefix="criter" %>

<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_VerificheIspettive.ascx" TagName="VerificheIspettive" TagPrefix="criter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function SalvaDefinitivo() {
            if (Page_ClientValidate())
                return (confirm('Confermando tale operazione lo stato del libretto verrà reso definitivo e non sarà più possibile modificare i dati. Confermi?'));
        }
        function DuplicaLibretto() {
            if (Page_ClientValidate())
                return (confirm('Confermando tale operazione verranno copiati i dati in un nuovo libretto impianto. Confermi?'));
        }
        function RevisionaLibretto() {
            //if (Page_ClientValidate())
            return (confirm('Confermando tale operazione lo stato del libretto verrà reso in revisione e sarà possibile modificare nuovamente i dati. Si specifica che i libretti in stato di revisione saranno riportati nella versione precedente in automatico dal sistema trascorsi 30 giorni rispetto all’ultima data di revisione. Confermi?'));
        }

        function AnnullaLibretto() {
            //if (Page_ClientValidate())
            return (confirm('Confermando tale operazione il libretto di impianto verrà annullato. Confermi?'));
        }

        function NuovoRapportoControllo() {
            //if (Page_ClientValidate())
            return (confirm('Confermi di creare un nuovo Rapporto di Controllo Tecnico?'));
        }

        function ModificaDatiLibretto() {
            //if (Page_ClientValidate())
            return (confirm('Confermi di voler modificare i dati del Libretto Impianto?'));
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoGenerali" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>INFORMAZIONI GENERALI LIBRETTO IMPIANTO</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" Text="Azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnSelectedIndexChanged="ASPxComboBox_OnSelectedIndexChanged"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="CodiceSoggetto" Caption="Codice" Width="50" />
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Azienda" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPersona" Text="Operatore/Addetto (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Panel ID="pnlSetAddetto" runat="server" Visible="false">
                            <dx:ASPxComboBox runat="server" ID="cmbAddetti" Theme="Default" TabIndex="1"
                                TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32" AutoPostBack="true"
                                Width="350px" OnSelectedIndexChanged="ASPxComboBox2_OnSelectedIndexChanged"
                                DropDownWidth="350px">
                                <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator
                                ID="rfvASPxComboBox2"
                                ValidationGroup="vgLibrettoImpianto"
                                ForeColor="Red"
                                Display="Dynamic"
                                runat="server"
                                InitialValue=""
                                ErrorMessage="Operatore/Addetto: campo obbligatorio"
                                ControlToValidate="cmbAddetti">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" Text="Codice targatura impianto (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Panel ID="pnlSetCodiceTargatura" runat="server" Visible="false">
                            <dx:ASPxComboBox runat="server" ID="cmbTargature" Theme="Default" TabIndex="1"
                                TextField="CodiceTargatura" ValueField="IDTargaturaImpianto" ValueType="System.String"
                                Width="350px"
                                DropDownWidth="350px">
                                <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                            </dx:ASPxComboBox>
                            <asp:RequiredFieldValidator
                                ID="rfvASPxcmbTargature"
                                ValidationGroup="vgLibrettoImpianto"
                                ForeColor="Red"
                                Display="Dynamic"
                                runat="server"
                                InitialValue=""
                                ErrorMessage="Codice targatura impianto: campo obbligatorio"
                                ControlToValidate="cmbTargature">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="PnlViewCodiceTargatura" runat="server" Visible="false">
                            <asp:Label ID="lblCodiceTargatura" runat="server" ForeColor="Green" Visible="false" Font-Bold="true" />&nbsp;&nbsp;<asp:ImageButton runat="server" AlternateText="Stampa Codice Targatura Impianto" ToolTip="Stampa Codice Targatura Impianto" ID="imgStampaTargatura" BorderStyle="None" ImageUrl="~/images/buttons/pdf.png" />
                            <br />
                            <asp:Image runat="server" ID="imgBarcode" Width="70px" Height="70px" CssClass="imgBarcode" ToolTip="Barcode Codice Targatura" AlternateText="Barcode Codice Targatura" />
                            <asp:Label runat="server" ID="lblIDTargaturaImpianto" Visible="false" />
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDataRegistrazioneLibrettoImpianto" Text="Data registrazione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataRegistrazioneLibrettoImpianto" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowStatoLibrettoImpianto" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloStatoLibrettoImpianto" Text="Stato libretto impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIDStatoLibrettoImpianto" runat="server" Visible="false" />
                        <asp:Label ID="lblStatoLibrettoImpianto" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataAnnullamentoLibrettoImpianto" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_DataAnnullamentoLibrettoImpianto" Text="Data annullamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataAnnullamentoLibrettoImpianto" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowNumeroRevisioneLibrettoImpianto" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloNumeroRevisioneLibrettoImpianto" Text="Revisione numero" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblNumeroRevisioneLibrettoImpianto" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDataRevisioneLibrettoImpianto" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataRevisioneLibrettoImpianto" Text="Data revisione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblDataRevisioneLibrettoImpianto" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowSavePreliminary" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpianti_btnSavePreliminary" runat="server" CssClass="buttonClass" Width="200"
                            OnClick="LIM_LibrettiImpianti_btnSavePreliminary_Click" ValidationGroup="vgInfoGeneraliLibrettoImpianto" Text="SALVA DATI E PROCEDI" />
                        <asp:ValidationSummary ID="LIM_LibrettiImpianti_vschkSavePreliminary" ValidationGroup="vgInfoGeneraliLibrettoImpianto" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblInfoLibrettoImpianto" runat="server" Visible="false">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>1.0 SCHEDA IDENTIFICATIVA DELL’IMPIANTO</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.1 TIPOLOGIA INTERVENTO</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDataIntervento" AssociatedControlID="txtDataIntervento" Text="Data intervento (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataIntervento" ValidationGroup="vgLibrettoImpianto" Width="90" MaxLength="10" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvDataTargatura" ValidationGroup="vgLibrettoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data intervento: campo obbligatorio"
                            ControlToValidate="txtDataIntervento">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="rfvDataIntervento" ValidationGroup="vgLibrettoImpianto" ControlToValidate="txtDataIntervento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data intervento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipoIntervento" Text="Tipo intervento (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipoIntervento" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial1" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloIndirizzo" Text="Indirizzo (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzo" Width="400" MaxLength="400" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzo" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Indirizzo: campo obbligatorio"
                            ControlToValidate="txtIndirizzo">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroCivico" AssociatedControlID="txtNumeroCivico" Text="Numero civico (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroCivico" Width="80" MaxLength="50" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivico" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Numero Civico: campo obbligatorio"
                            ControlToValidate="txtNumeroCivico">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPalazzo" Text="Palazzo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPalazzo" Width="80" MaxLength="10" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloScala" Text="Scala" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtScala" Width="80" MaxLength="10" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloInterno" Text="Interno" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtInterno" Width="80" MaxLength="10" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloComune" Text="Comune (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox AutoPostBack="true" ID="RadComboBoxCodiciCatastali" runat="server" TabIndex="1" Visible="false"
                            Theme="Default" EnableViewState="False" OnSelectedIndexChanged="RadComboBoxCodiciCatastali_SelectedIndexChanged"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDCodiceCatastale"
                            OnItemsRequestedByFilterCondition="comboComune_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="comboComune_OnItemRequestedByValue"
                            OnButtonClick="comboComune_ButtonClick"                            
                            TextFormatString="{0}"
                            Width="300px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvRadComboBoxCodiciCatastali" ValidationGroup="vgLibrettoImpiantoComune"
                            ForeColor="Red" Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Comune: campo obbligatorio"
                            ControlToValidate="RadComboBoxCodiciCatastali">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDCodiceCatastale" runat="server" Visible="false" />
                        <asp:Label ID="lblCodiceCatastale" Font-Bold="true" runat="server" />&nbsp;
                        <asp:Button ID="btnAnnullaUpdComuneLibretto" runat="server" CssClass="buttonSmallClass" Text="Annulla modifica" OnClick="btnAnnullaUpdComuneLibretto_Click" Visible="false" />&nbsp;
                        <asp:Button ID="btnUpdComuneLibretto" runat="server" Text="Modifica Comune" CssClass="buttonSmallClass" OnClick="btnUpdComuneLibretto_Click" Visible="false" />
                        <asp:Button ID="btnSaveComuneLibretto" runat="server" Text="Conferma modifica Comune" CssClass="buttonSmallClass" OnClick="btnSaveComuneLibretto_Click" ValidationGroup="vgLibrettoImpiantoComune" OnClientClick="javascript:return confirm('Confermi di modificare il Comune?');" Visible="false" />
                         <asp:ValidationSummary ID="vsSaveCodiceCatastaleLibretto" ValidationGroup="vgLibrettoImpiantoComune" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloProvincia" Text="Provincia" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblProvincia" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDatiCatastali" Text="Dati catastali (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Panel runat="server" ID="pnlInsertDatiCatastali" CssClass="testo0" HorizontalAlign="Right">
                            <asp:LinkButton ID="lnkInsertDatiCatastali" runat="server" Text="Aggiungi dati catastali"
                                OnClick="lnkInsertDatiCatastali_Click" CausesValidation="false" TabIndex="1" />
                            <br />
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlDatiCatastaliInsert" Visible="false" runat="server">
                            <asp:Label runat="server" ID="lblIDLibrettoImpiantoDatiCatastali" Visible="false" />
                            <asp:Table ID="tblDatiCatastali" Width="500" runat="server">
                                <asp:TableRow ID="rowSezioneDatiCatastali" runat="server" Visible="false">
                                    <asp:TableCell Width="100">
                                            Sezione (*)
                                    </asp:TableCell>
                                    <asp:TableCell Width="400">
                                        <asp:DropDownList runat="server" ID="ddlSezioneDatiCatastali" Width="170" ValidationGroup="vgDatiCatastali" CssClass="selectClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvSezioneDatiCatastali" runat="server" ValidationGroup="vgDatiCatastali"
                                            CssClass="testoerr" Display="Dynamic"
                                            InitialValue="0" ErrorMessage="Sezione: campo obbligatorio"
                                            ControlToValidate="ddlSezioneDatiCatastali">&nbsp;*</asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100">
                                            Foglio (*)
                                    </asp:TableCell>
                                    <asp:TableCell Width="400">
                                        <asp:TextBox ID="txtFoglio" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
                                        <asp:RequiredFieldValidator ID="rfvtxtFoglio" runat="server" CssClass="testoerr"
                                            ValidationGroup="vgDatiCatastali" EnableClientScript="true" ErrorMessage="Foglio: campo obbligatorio"
                                            ControlToValidate="txtFoglio">&nbsp;*</asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="ftbeFoglio" runat="server" TargetControlID="txtFoglio"
                                            FilterType="Custom, Numbers" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100">
                                            Mappale (*)
                                    </asp:TableCell>
                                    <asp:TableCell Width="400">
                                        <asp:TextBox ID="txtMappale" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
                                            <asp:RequiredFieldValidator ID="rfvtxtMappale" runat="server" CssClass="testoerr"
                                                ValidationGroup="vgDatiCatastali" EnableClientScript="true" ErrorMessage="Mappale: campo obbligatorio"
                                                ControlToValidate="txtMappale">&nbsp;*</asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="ftbeMappale" runat="server" TargetControlID="txtMappale"
                                            FilterType="Custom, Numbers" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100">
                                            Subalterno (*)
                                    </asp:TableCell>
                                    <asp:TableCell Width="400">
                                        <asp:TextBox ID="txtSubalterno" runat="server" CssClass="txtClass_o" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
                                            <asp:RequiredFieldValidator ID="rfvtxtSubalterno" runat="server" CssClass="testoerr"
                                                ValidationGroup="vgDatiCatastali" EnableClientScript="true" ErrorMessage="Subalterno: campo obbligatorio"
                                                Display="Dynamic" ControlToValidate="txtSubalterno">&nbsp;*</asp:RequiredFieldValidator>
                                        <%--Solo numeri oppure solo il trattino per il subalterno--%>
                                        <asp:RegularExpressionValidator ID="revSubalterno" runat="server" CssClass="testoerr" ValidationGroup="vgDatiCatastali" ErrorMessage="Subalterno: inserire solo numeri oppure il segno - se subalterno non presente"
                                            Display="Dynamic" ControlToValidate="txtSubalterno" ValidationExpression="^\-|\d{0,50}$">&nbsp;*</asp:RegularExpressionValidator>
                                        <cc1:FilteredTextBoxExtender ID="ftbeSubalterno" runat="server" TargetControlID="txtSubalterno"
                                            FilterType="Custom, Numbers" ValidChars="-" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100">
                                            Identificativo
                                    </asp:TableCell>
                                    <asp:TableCell Width="400">
                                        <asp:TextBox ID="txtIdentificativo" runat="server" CssClass="txtClass" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="1" />
                                        <cc1:FilteredTextBoxExtender ID="ftbeIdentificativo" runat="server" TargetControlID="txtIdentificativo"
                                            FilterType="LowercaseLetters, UppercaseLetters" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell Width="500" ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:CustomValidator ID="cvDatiCatastaliPresenti" Enabled="true" runat="server"
                                            Display="Dynamic" ValidationGroup="vgDatiCatastali" EnableClientScript="True" CssClass="testoerr"
                                            OnServerValidate="ControllaDatiCatastaliPresenti"></asp:CustomValidator>

                                        <asp:ValidationSummary ID="vsDatiCatastali" ValidationGroup="vgDatiCatastali" runat="server" CssClass="testoerr" ShowMessageBox="True"
                                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="500" ColumnSpan="2" HorizontalAlign="Center">
                                        <asp:Button ID="btnSaveDatiCatastali" runat="server" CssClass="buttonClass" Width="100"
                                            OnClick="btnSaveDatiCatastali_Click" ValidationGroup="vgDatiCatastali" Text="inserisci" />&nbsp;
                                        <asp:Button ID="btnAnnullaDatiCatastali" runat="server" CssClass="buttonClass" Width="100"
                                            OnClick="btnAnnullaDatiCatastali_Click" CausesValidation="false" ValidationGroup="vgDatiCatastali" Text="annulla" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlDatiCatastaliView" Visible="true" runat="server">
                            <asp:DataGrid ID="dgDatiCatastali" CssClass="Grid" Width="550px" GridLines="None"
                                CellSpacing="1" CellPadding="3" PageSize="10" UseAccessibleHeader="true" AllowSorting="True" AllowPaging="False"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoDatiCatastali">
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridItem" />
                                <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDLibrettoImpiantoDatiCatastali" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDCodiceCatastaleSezione" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataField="Foglio" HeaderText="Foglio" />
                                    <asp:BoundColumn ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataField="Mappale" HeaderText="Mappale" />
                                    <asp:BoundColumn ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataField="Subalterno" HeaderText="Subalterno" />
                                    <asp:BoundColumn ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" DataField="Identificativo" HeaderText="Identificativo" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgUpdate" ToolTip="Modifica dato Catastale" AlternateText="Modifica dato Catastale" ImageUrl="~/images/Buttons/editSmall.png"
                                                OnCommand="RowCommand" CommandName="Modify" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpiantoDatiCatastali") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="Cancella dato Catastale" AlternateText="Cancella dato Catastale" ImageUrl="~/images/Buttons/deleteSmall.png" OnClientClick="javascript:return confirm('Confermi la cancellazione del dato catastale?')"
                                                OnCommand="RowCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpiantoDatiCatastali") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloSingolaUnitaImmobiliare" Text="Singola unità immobiliare" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblSingolaUnitaImmobiliare" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Text="Si" Value="True" />
                            <asp:ListItem Text="No" Selected="True" Value="False" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCategoriaDestinazioneUso" Text="Categoria della destinazioni dell'edificio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblDestinazioneUso" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloVoluneLordoRiscaldato" Text="Volume lordo riscaldato m<sup>3</sup>" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtVolumeLordoRiscaldato" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvVoluneLordoRiscaldato" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Volume lordo riscaldato: campo obbligatorio"
							ControlToValidate="txtVolumeLordoRiscaldato">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revVolumeLordoRiscaldato" ControlToValidate="txtVolumeLordoRiscaldato"
                            ErrorMessage="Volume lordo riscaldato: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloVoluneLordoRaffrescato" Text="Volume lordo raffrescato m<sup>3</sup>" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtVolumeLordoRaffrescato" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvVolumeLordoRaffrescato" runat="server" ForeColor="Red"
							 ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Volume lordo raffrescato: campo obbligatorio"
							 ControlToValidate="txtVolumeLordoRaffrescato">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revVolumeLordoRaffrescato" ControlToValidate="txtVolumeLordoRaffrescato"
                            ErrorMessage="Volume lordo raffrescato: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgLibrettoImpianto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroApe" Text="Numero attestato prestazione energetica (APE)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtNumeroApe" runat="server" Width="150" MaxLength="17" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revNumeroApe" ControlToValidate="txtNumeroApe"
                            ErrorMessage="Numero attestato prestazione energetica (APE) non valido: inserire il codice Ape nella forma 12345-12345-2017 oppure 12345-123456-2017" runat="server"
                            ValidationExpression="^.{16,17}$" ForeColor="Red" ValidationGroup="vgLibrettoImpianto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroPdr" Text="Numero punto riconsegna combustibile (PDR)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtNumeroPdr" runat="server" Width="120" MaxLength="14" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvtxtNumeroPdr" runat="server" ForeColor="Red" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" 
							ErrorMessage="Numero punto riconsegna combustibile (PDR): campo obbligatorio"
							ControlToValidate="txtNumeroPdr">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revNumeroPdr" ControlToValidate="txtNumeroPdr"
                            ErrorMessage="Numero punto riconsegna combustibile (PDR) non valido: inserire 14 cifre" runat="server"
                            ValidationExpression="\d{14}" ForeColor="Red" ValidationGroup="vgLibrettoImpianto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroPod" Text="Numero punto riconsegna energia elettrica (POD)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtNumeroPod" runat="server" Width="120" MaxLength="14" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvtxtNumeroPod" runat="server" ForeColor="Red" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" 
							ErrorMessage="Numero punto riconsegna energia elettrica (POD): campo obbligatorio"
							ControlToValidate="txtNumeroPod">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revNumeroPod" ControlToValidate="txtNumeroPod"
                            ErrorMessage="Numero punto riconsegna energia elettrica (POD) non valido: inserire il codice POD nella forma IT000E12345678" runat="server"
                            ValidationExpression="^[a-zA-Z0-9\s]{14,14}$" ForeColor="Red" ValidationGroup="vgLibrettoImpianto"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial2" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.3 IMPIANTO TERMICO DESTINATO A SODDISFARE I SEGUENTI SERVIZI</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" Width="900" CssClass="riempimento">
                        <asp:Table ID="tblServizi" CssClass="TableClass" Width="880" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="350">
                                    <asp:CheckBox ID="chkClimatizzazioneAcs" runat="server" AutoPostBack="true" OnCheckedChanged="chkClimatizzazioneAcs_CheckedChanged" CssClass="checkboxlistClass" Text="Produzione di acqua calda sanitaria (acs)" />
                                </asp:TableCell>
                                <asp:TableCell Width="630">
                                    <asp:Panel ID="pnlClimatizzazioneAcs" runat="server" Visible="false">
                                        <asp:Label ID="lblClimatizzazionePotenzaAcs" runat="server" CssClass="labelSmallClass" Text="Potenza utile (kW):&nbsp;" />
                                        <asp:TextBox Width="60" ID="txtClimatizzazionePotenzaAcs" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvClimatizzazionePotenzaAcs" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Potenza utile per Acs: campo obbligatorio"
                                            ControlToValidate="txtClimatizzazionePotenzaAcs">&nbsp;*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revClimatizzazionePotenzaAcs" ControlToValidate="txtClimatizzazionePotenzaAcs"
                                            ErrorMessage="Potenza utile per Acs: inserire un numero con la virgola" runat="server"
                                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Width="350">
                                    <asp:CheckBox ID="chkClimatizzazioneInvernale" AutoPostBack="true" OnCheckedChanged="chkClimatizzazioneInvernale_CheckedChanged" runat="server" CssClass="checkboxlistClass" Text="Climatizzazione invernale" />
                                </asp:TableCell>
                                <asp:TableCell Width="630">
                                    <asp:Panel ID="pnlClimatizzazioneInvernale" runat="server" Visible="false">
                                        <asp:Label ID="lblClimatizzazionePotenzaInvernale" runat="server" CssClass="labelSmallClass" Text="Potenza utile (kW):&nbsp;" />
                                        <asp:TextBox Width="60" ID="txtClimatizzazionePotenzaInvernale" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvClimatizzazionePotenzaInvernale" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Potenza utile per Invernale: campo obbligatorio"
                                            ControlToValidate="txtClimatizzazionePotenzaInvernale">&nbsp;*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revClimatizzazionePotenzaInvernale" ControlToValidate="txtClimatizzazionePotenzaInvernale"
                                            ErrorMessage="Potenza utile per Invernale: inserire un numero con la virgola" runat="server"
                                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="350">
                                    <asp:CheckBox ID="chkClimatizzazioneEstiva" runat="server" AutoPostBack="true" OnCheckedChanged="chkClimatizzazioneEstiva_CheckedChanged" CssClass="checkboxlistClass" Text="Climatizzazione Estiva" />
                                </asp:TableCell>
                                <asp:TableCell Width="630">
                                    <asp:Panel ID="pnlClimatizzazioneEstiva" runat="server" Visible="false">
                                        <asp:Label ID="lblClimatizzazionePotenzaEstiva" runat="server" CssClass="labelSmallClass" Text="Potenza utile (kW):&nbsp;" />
                                        <asp:TextBox Width="60" ID="txtClimatizzazionePotenzaEstiva" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvClimatizzazionePotenzaEstiva" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Potenza utile per Estiva: campo obbligatorio"
                                            ControlToValidate="txtClimatizzazionePotenzaEstiva">&nbsp;*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revClimatizzazionePotenzaEstiva" ControlToValidate="txtClimatizzazionePotenzaEstiva"
                                            ErrorMessage="Potenza utile per Estiva: inserire un numero con la virgola" runat="server"
                                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell Width="350">
                                    <asp:CheckBox ID="chkClimatizzazioneAltro" runat="server" AutoPostBack="true" OnCheckedChanged="chkClimatizzazioneAltro_CheckedChanged" CssClass="checkboxlistClass" Text="Altro" />
                                </asp:TableCell>
                                <asp:TableCell Width="630">
                                    <asp:Panel ID="pnlClimatizzazioneAltro" runat="server" Visible="false">
                                        <asp:Label ID="lblClimatizzazioneAltro" runat="server" CssClass="labelSmallClass" Text="Altro:&nbsp;" />
                                        <asp:TextBox Width="400" ID="txtClimatizzazioneAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvClimatizzazioneAltro" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro climatizzazione: campo obbligatorio"
                                            ControlToValidate="txtClimatizzazioneAltro">&nbsp;*</asp:RequiredFieldValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial3" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.4 TIPOLOGIA FLUIDO VETTORE</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaFluidoVettore" Text="Tipo fluido vettore (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaFluidoVettore" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaFluidoVettore_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaFluidoVettoreAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaFluidoVettoreAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaFluidoVettoreAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altra tipologia fluido vettore: campo obbligatorio"
                                ControlToValidate="txtTipologiaFluidoVettoreAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial4" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.5 INDIVIDUAZIONE DELLA TIPOLOGIA DEI GENERATORI</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaGeneratori" Text="Tipologia generatori (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaGeneratori" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaGeneratori_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaGeneratoriAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaGeneratoriAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaGeneratoriAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro tipologia generatori: campo obbligatorio"
                                ControlToValidate="txtTipologiaGeneratoriAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPannelliSolariTermici" Text="Eventuali integrazioni con" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Table ID="tblPannelliSolariTermici" Width="600" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="170">
                                    <asp:CheckBox ID="chkPannelliSolariTermici" AutoPostBack="true" Text="Pannelli solari termici" TextAlign="Right" OnCheckedChanged="chkPannelliSolariTermici_CheckedChanged" runat="server" CssClass="radiobuttonlistClass" />
                                </asp:TableCell>
                                <asp:TableCell Width="230">
                                    <asp:Panel ID="pnlSuperficieTotaleLordaPannelli" runat="server" Visible="false">
                                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloSuperficieTotaleLordaPannelli" Text="Sup. totale lorda m<sup>2</sup> (*)" />
                                        <asp:TextBox Width="60" ID="txtSuperficieTotaleLordaPannelli" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />&nbsp;
										<asp:RequiredFieldValidator ID="rfvSuperficieTotaleLordaPannelli" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Superficie totale lorda: campo obbligatorio"
                                            ControlToValidate="txtSuperficieTotaleLordaPannelli">&nbsp;*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revSuperficieTotaleLordaPannelli" ControlToValidate="txtSuperficieTotaleLordaPannelli"
                                            ErrorMessage="Superficie totale lorda: inserire un numero con la virgola" runat="server"
                                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                                <asp:TableCell Width="200">
									&nbsp;
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="170">
                                    <asp:CheckBox ID="chkAltroPannelli" runat="server" Text="Altro" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="chkAltroPannelli_CheckedChanged" CssClass="radiobuttonlistClass" />
                                </asp:TableCell>
                                <asp:TableCell Width="230">
                                    <asp:Panel ID="pnlAltroPannelli" runat="server" Visible="false">
                                        <asp:TextBox Width="200" ID="txtAltroPannelli" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvAltroPannelli" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altre integrazioni: campo obbligatorio"
                                            ControlToValidate="txtAltroPannelli">&nbsp;*</asp:RequiredFieldValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                                <asp:TableCell Width="200">
                                    <asp:Panel ID="pnlPotenzaUtilePannelli" runat="server" Visible="false">
                                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPotenzaUtilePannelli" Text="Potenza utile (kW) (*)" />&nbsp;
										<asp:TextBox Width="40" ID="txtPotenzaUtilePannelli" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                                        <asp:RequiredFieldValidator ID="rfvPotenzaUtilePannelli" runat="server" ForeColor="Red"
                                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Potenza utile pannelli: campo obbligatorio"
                                            ControlToValidate="txtPotenzaUtilePannelli">&nbsp;*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPotenzaUtilePannelli" ControlToValidate="txtPotenzaUtilePannelli"
                                            ErrorMessage="Potenza utile pannelli: inserire un numero con la virgola" runat="server"
                                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowIntegrazioniPannelli" runat="server" Visible="false">
                                <asp:TableCell Width="600" ColumnSpan="3" HorizontalAlign="Left">
                                    <asp:CheckBox ID="chkClimatizzazionePannelliAcs" runat="server" CssClass="radiobuttonlistClass" Text="Produzione acqua calda sanitaria" />&nbsp;&nbsp;
									<asp:CheckBox ID="chkClimatizzazionePannelliInvernale" runat="server" CssClass="radiobuttonlistClass" Text="Climatizzazione invernale" />&nbsp;&nbsp;&nbsp;
									<asp:CheckBox ID="chkClimatizzazionePannelliEstiva" runat="server" CssClass="radiobuttonlistClass" Text="Climatizzazione estiva" />
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial5" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>1.6 RESPONSABILE DELL’IMPIANTO O DELEGANTE (NEL CASO NOMINA DI TERZO RESPONSABILE)</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipoResponsabile" Text="Tipo responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologiaResponsabili" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipoSoggetto" Text="Tipo soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologiaSoggetti" AutoPostBack="true" OnSelectedIndexChanged="rblTipologiaSoggetti_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowNomeResponsabile" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNomeResponsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeResponsabile" Width="200" MaxLength="100" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Nome responsabile: campo obbligatorio"
                            ControlToValidate="txtNomeResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCognomeResponsabile" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCognomeResponsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeResponsabile" Width="200" MaxLength="100" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Cognome responsabile: campo obbligatorio"
                            ControlToValidate="txtCognomeResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCodiceFiscaleResponsabile" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceFiscaleResponsabile" Text="Codice fiscale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleResponsabile" Width="200" MaxLength="16" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCodiceFiscaleResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Codice fiscale responsabile: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvCodiceFiscaleResponsabile" runat="server" ErrorMessage="Codice fiscale responsabile: non valido"
                            Display="Dynamic" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true"
                            OnServerValidate="ControllaCf">&nbsp;*</asp:CustomValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowRagioneSocialeResponsabile" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloRagioneSocialeResponsabile" Text="Ragione sociale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtRagioneSocialeResponsabile" Width="400" MaxLength="200" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtRagioneSocialeResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Ragione sociale responsabile: campo obbligatorio"
                            ControlToValidate="txtRagioneSocialeResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowPartitaIvaResponsabile" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPartitaIvaResponsabile" Text="Partita iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaResponsabile" Width="200" MaxLength="11" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <cc1:FilteredTextBoxExtender ID="ftbPartitaIva" runat="server" TargetControlID="txtPartitaIvaResponsabile"
                            FilterType="Custom, Numbers" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtPartitaIvaResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Partita iva responsabile: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloIndirizzoResponsabile" Text="Indirizzo (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoResponsabile" Width="400" MaxLength="400" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Indirizzo responsabile: campo obbligatorio"
                            ControlToValidate="txtIndirizzoResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroCivicoResponsabile" AssociatedControlID="txtNumeroCivico" Text="Numero civico (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroCivicoResponsabile" Width="80" MaxLength="50" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Numero civico responsabile: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloComuneResponsabile" Text="Comune (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Theme="Default"
                            AutoPostBack="true" OnSelectedIndexChanged="ASPxComboBox3_OnSelectedIndexChanged"
                            EnableCallbackMode="true"
                            CallbackPageSize="10"
                            FilterMinLength="3"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDCodiceCatastale"
                            OnItemsRequestedByFilterCondition="ASPxComboBox3_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox3_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox3_ButtonClick"
                            TextFormatString="{0}"
                            Width="250px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox3" ValidationGroup="vgLibrettoImpianto"
                            ForeColor="Red" Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Comune del responsabile: campo obbligatorio"
                            ControlToValidate="ASPxComboBox3">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloProvinciaResponsabile" Text="Provincia" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblProvinciaResponsabile" />
                        <asp:Label runat="server" ID="lblIDProvinciaResponsabile" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloEmailResponsabile" Text="Email" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailResponsabile" Width="250" MaxLength="100" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator
							ID="rfvtxtEmailResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Email responsabile: campo obbligatorio"
							ControlToValidate="txtEmailResponsabile">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revEmailResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true"
                            ErrorMessage="Email responsabile: campo non valido"
                            ControlToValidate="txtEmailResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloEmailPecResponsabile" Text="Email pec" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecResponsabile" Width="250" MaxLength="100" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator
							ID="rfvtxtEmailPecResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Email pec responsabile: campo obbligatorio"
							ControlToValidate="txtEmailPecResponsabile">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revEmailPecResponsabile" ForeColor="Red" runat="server" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true"
                            ErrorMessage="Email pec responsabile: campo non valido"
                            ControlToValidate="txtEmailPecResponsabile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTerzoResponsabile" AssociatedControlID="rblfTerzoResponsabile" Text="E' stato nominato un terzo responsabile?" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblfTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="radiobuttonlistClass" AutoPostBack="true" OnSelectedIndexChanged="rblfTerzoResponsabile_SelectedIndexChanged">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial6" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>2.0 TRATTAMENTO ACQUA</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>2.1 CONTENUTO D’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloContentutoAcquaImpiantoClimatizzazione" Text="Contenuto d'acqua dell'impianto di climatizzazione m<sup>3</sup>" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtContentutoAcquaImpiantoClimatizzazione" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvContentutoAcquaImpiantoClimatizzazione" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Contenuto d'acqua dell'impianto di climatizzazione: campo obbligatorio"
							ControlToValidate="txtContentutoAcquaImpiantoClimatizzazione">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revContentutoAcquaImpiantoClimatizzazione" ControlToValidate="txtContentutoAcquaImpiantoClimatizzazione"
                            ErrorMessage="Contenuto d'acqua dell'impianto di climatizzazione: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							2.2 DUREZZA TOTALE DELL’ACQUA
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDurezzaAcquaImpiantoClimatizzazione" Text="Durezza totale dell'acqua °fr" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtDurezzaAcquaImpiantoClimatizzazione" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvDurezzaAcquaImpiantoClimatizzazione" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Durezza totale dell'acqua: campo obbligatorio"
							ControlToValidate="txtDurezzaAcquaImpiantoClimatizzazione">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revDurezzaAcquaImpiantoClimatizzazione" ControlToValidate="txtDurezzaAcquaImpiantoClimatizzazione"
                            ErrorMessage="Durezza totale dell'acqua: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial7" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							2.3 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE INVERNALE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:RadioButtonList ID="rbTrattamentoAcquaInvernale" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbTrattamentoAcquaInvernale_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass">
                            <asp:ListItem Selected="True" Text="Assente" Value="False" />
                            <asp:ListItem Text="Presente" Value="True" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaInvernale" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaTrattamentoAcquaInvernale" Text="Tipologia trattamento acqua (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaTrattamentoAcquaInvernale" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaTrattamentoAcquaInvernale_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaInvernaleDurezzaAcqua" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDurezzaAcquaInvernale" Text="Durezza totale acqua impianto (°fr)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtDurezzaAcquaInvernale" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvDurezzaAcquaInvernale" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Durezza totale acqua impianto invernale: campo obbligatorio"
							ControlToValidate="txtDurezzaAcquaInvernale">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revDurezzaAcquaInvernale" ControlToValidate="txtDurezzaAcquaInvernale"
                            ErrorMessage="Durezza totale acqua impianto invernale: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloProtezioneGelo" Text="Protezione del gelo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rbProtezioneGelo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbProtezioneGelo_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass">
                            <asp:ListItem Selected="True" Text="Assente" Value="False" />
                            <asp:ListItem Text="Presente" Value="True" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaInvernaleProtezioneGelo" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaProtezioneGelo" Text="Tipologia protezione del gelo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTipologiaProtezioneGelo" AutoPostBack="true" OnSelectedIndexChanged="ddlTipologiaProtezioneGelo_SelectedIndexChanged" runat="server" CssClass="selectClass_o" Width="320" />
                        <asp:RequiredFieldValidator ID="rfvddlTipologiaProtezioneGelo" ValidationGroup="vgLibrettoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0" ErrorMessage="Protezione del gelo: campo obbligatorio"
                            ControlToValidate="ddlTipologiaProtezioneGelo">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaInvernalePercentualeGlicole" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPercentualeGlicole" Text="Concentrazione glicole nel fluido termovettore (%)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtPercentualeGlicole" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvPercentualeGlicole" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Concentrazione % glicole nel fluido termovettore: campo obbligatorio"
							ControlToValidate="txtPercentualeGlicole">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revPercentualeGlicole" ControlToValidate="txtPercentualeGlicole"
                            ErrorMessage="Concentrazione % glicole nel fluido termovettore: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaInvernalePhGlicole" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPhGlicole" Text="Ph del liquido presente nell'impianto (Ph)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtPhGlicole" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator ID="rfvPhGlicole" runat="server" ForeColor="Red"
							ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Concentrazione Ph glicole nel fluido termovettore: campo obbligatorio"
							ControlToValidate="txtPhGlicole">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revPhGlicole" ControlToValidate="txtPhGlicole"
                            ErrorMessage="Concentrazione Ph glicole nel fluido termovettore: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial8" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>2.4 TRATTAMENTO DELL’ACQUA CALDA SANITARIA</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:RadioButtonList ID="rbTrattamentoAcquaAcs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbTrattamentoAcquaAcs_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass">
                            <asp:ListItem Selected="True" Text="Assente" Value="False" />
                            <asp:ListItem Text="Presente" Value="True" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaAcs" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaTrattamentoAcquaAcs" Text="Tipologia trattamento acqua (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaTrattamentoAcquaAcs" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaTrattamentoAcquaAcs_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaAcsDurezzaAcqua" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDurezzaAcquaAcs" Text="Durezza totale uscita addolcitore (°fr) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtDurezzaAcquaAcs" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvDurezzaAcquaAcs" runat="server" ForeColor="Red"
                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Durezza totale uscita addolcitore: campo obbligatorio"
                            ControlToValidate="txtDurezzaAcquaAcs">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revDurezzaAcquaAcs" ControlToValidate="txtDurezzaAcquaAcs"
                            ErrorMessage="Durezza totale uscita addolcitore: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial9" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>2.5 TRATTAMENTO DELL’ACQUA DI RAFFREDDAMENTO DELL’IMPIANTO DI CLIMATIZZAZIONE ESTIVA</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:RadioButtonList ID="rbTrattamentoAcquaEstiva" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbTrattamentoAcquaEstiva_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass">
                            <asp:ListItem Selected="True" Text="Assente" Value="False" />
                            <asp:ListItem Text="Presente" Value="True" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaTipologiaCircuitoRaffreddamento" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCircuitoRaffreddamento" Text="Tipologia circuito di raffreddamento (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTipologiaCircuitoRaffreddamento" runat="server" CssClass="selectClass_o" Width="320" />
                        <asp:RequiredFieldValidator ID="rfvddlTipologiaCircuitoRaffreddamento" ValidationGroup="vgLibrettoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0" ErrorMessage="Tipologia circuito di raffreddamento: campo obbligatorio"
                            ControlToValidate="ddlTipologiaCircuitoRaffreddamento">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaTipologiaAcquaAlimento" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAcquaAlimento" Text="Origine acqua di alimento (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTipologiaAcquaAlimento" runat="server" CssClass="selectClass_o" Width="320" />
                        <asp:RequiredFieldValidator ID="rfvddlTipologiaAcquaAlimento" ValidationGroup="vgLibrettoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0" ErrorMessage="Origine acqua di alimento: campo obbligatorio"
                            ControlToValidate="ddlTipologiaAcquaAlimento">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstiva" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaTrattamentoAcquaEstiva" Text="Trattamento acqua esistenti (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaTrattamentoAcquaEstiva" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaTrattamentoAcquaEstiva_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaTipologiaFiltrazione" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaFiltrazione" Text="Tipologia filtrazione (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaFiltrazione" ValidationGroup="vgLibrettoImpianto" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaFiltrazione_SelectedIndexChanged" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaFiltrazioniAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaFiltrazioniAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaFiltrazioniAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro tipologia generatori: campo obbligatorio"
                                ControlToValidate="txtTipologiaFiltrazioniAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaTipologiaAddolcimentoAcqua" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaAddolcimentoAcqua" Text="Trattamento acqua (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaAddolcimentoAcqua" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaAddolcimentoAcqua_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaAddolcimentoAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaAddolcimentoAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaAddolcimentoAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro tipologia addolcimento acqua: campo obbligatorio"
                                ControlToValidate="txtTipologiaAddolcimentoAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaTipologiaCondizionamentoChimico" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaCondizionamentoChimico" Text="Tipologia condizionamento chimico (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaCondizionamentoChimico" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaCondizionamentoChimico_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaCondizionamentoChimicoAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaCondizionamentoChimicoAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaCondizionamentoChimicoAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro tipologia generatori: campo obbligatorio"
                                ControlToValidate="txtTipologiaCondizionamentoChimicoAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaSistemaSpurgoAutomatico" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloSistemaSpurgoAutomatico" Text="Gestione torre raffreddamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkSistemaSpurgoAutomatico" TextAlign="Right" Text="&nbsp;&nbsp;Presenza sistema spurgo automatico (per circuito a recupero parziale)" AutoPostBack="true" OnCheckedChanged="chkSistemaSpurgoAutomatico_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaConducibilitaAcquaIngresso" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloConducibilitaAcquaIngresso" Text="Conducibilità acqua in ingresso (μS/cm) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtConducibilitaAcquaIngresso" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvConducibilitaAcquaIngresso" runat="server" ForeColor="Red"
                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Conducibilità acqua in ingresso: campo obbligatorio"
                            ControlToValidate="txtConducibilitaAcquaIngresso">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revConducibilitaAcquaIngresso" ControlToValidate="txtConducibilitaAcquaIngresso"
                            ErrorMessage="Conducibilità acqua in ingresso: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTrattamentoAcquaEstivaConducibilitaInizioSpurgo" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloConducibilitaInizioSpurgo" Text="Taratura valore conducibilità inizio spurgo (μS/cm) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="60" ID="txtConducibilitaInizioSpurgo" runat="server" MaxLength="8" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvConducibilitaInizioSpurgo" runat="server" ForeColor="Red"
                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Taratura valore conducibilità inizio spurgo: campo obbligatorio"
                            ControlToValidate="txtConducibilitaInizioSpurgo">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revConducibilitaInizioSpurgo" ControlToValidate="txtConducibilitaInizioSpurgo"
                            ErrorMessage="Taratura valore conducibilità inizio spurgo: inserire un numero con la virgola" runat="server"
                            ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                            EnableClientScript="true" ValidationGroup="vgLibrettoImpianto">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial10" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTerzoResponsabileHeader" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>3.0 NOMINA DEL TERZO RESPONSABILE DELL’IMPIANTO TERMICO</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowTerzoResponsabile" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:TerzoResponsabile ID="ctlTerzoResponsabile" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowTerzoResponsabileFooter" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>4.0 GENERATORI</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.1 GRUPPI TERMICI O CALDAIE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:GruppiTermici ID="ctlGruppiTermici" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.4 MACCHINE FRIGORIFERE / POMPE DI CALORE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:MacchineFrigorifere ID="ctlMacchineFrigorifere" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.5 SCAMBIATORI DI CALORE DELLA SOTTOSTAZIONE DI TELERISCALDAMENTO / TELERAFFRESCAMENTO</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ScambiatoriCalore ID="ctlScambiatoriCalore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.6 COGENERATORI / TRIGENERATORI</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:Cogeneratori ID="ctlCogeneratori" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.7 CAMPI SOLARI TERMICI</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:CampiSolariTermici ID="ctlCampiSolariTermici" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>4.8 ALTRI GENERATORI</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:AltriGeneratori ID="ctlAltriGeneratori" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>5.0 SISTEMI DI REGOLAZIONE E CONTABILIZZAZIONE</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>5.1 REGOLAZIONE PRIMARIA (Situazione alla prima installazione o alla ristrutturazione dell’impianto termico)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloSIstemaRegolazione" Text="Sistema di regolazione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkSistemaRegolazioneOnOff" Text="sistema di regolazione ON - OFF" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkSistemaRegolazioneIntegrato" Text="sistema di regolazione con impostazione della curva climatica integrata nel generatore" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkSistemaRegolazioneIndipendente" Text="sistema di regolazione con impostazione della curva climatica indipendente" AutoPostBack="true" OnCheckedChanged="chkSistemaRegolazioneIndipendente_CheckedChanged" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkSistemaRegolazioneMultigradino" Text="sistema di regolazione multigradino" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkSistemaRegolazioneAInverter" Text="sistema di regolazione a Inverter del generatore" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkAltroSistemaRegolazionePrimaria" Text="altri sistemi di regolazione primaria" AutoPostBack="true" OnCheckedChanged="chkAltroSistemaRegolazionePrimaria_CheckedChanged" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlAltroSistemaRegolazionePrimaria" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtAltroSistemaRegolazionePrimaria" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvAltroSistemaRegolazionePrimaria" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro sistema di regolazione primaria: campo obbligatorio"
                                ControlToValidate="txtAltroSistemaRegolazionePrimaria">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowSistemiRegolazioneHeader" runat="server">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							&nbsp;&nbsp;&nbsp;&nbsp;<h3>SISTEMI DI REGOLAZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowSistemiRegolazione" runat="server">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:SistemiRegolazione ID="ctlSistemiRegolazione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label4" Text="Valvole di regolazione non incorporate nel generatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblValvoleRegolazione" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="rblValvoleRegolazione_SelectedIndexChanged"
                            RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="0" Selected="True" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowValvoleRegolazioneHeader" runat="server">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							&nbsp;&nbsp;&nbsp;&nbsp;<h3>VALVOLE DI REGOLAZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowValvoleRegolazione" runat="server">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ValvoleRegolazione ID="ctlValvoleRegolazione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial11" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>5.2 REGOLAZIONE SINGOLO AMBIENTE DI ZONA</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTipologiaTermostatoZona" Text="Termostato di zona o ambiente" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologiaTermostatoZona"
                            ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass" />
                        <asp:RequiredFieldValidator ID="rfvTipologiaTermostatoZona" runat="server" ForeColor="Red"
                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Tipologia termostato di zona: campo obbligatorio"
                            ControlToValidate="rblTipologiaTermostatoZona">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTipologiaAmbiente" Text="Ambiente" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkControlloEntalpico" Text="CONTROLLO ENTALPICO su serranda aria esterna" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkControlloPortataAriaVariabile" Text="CONTROLLO PORTATA ARIA VARIABILE per aria canalizzata" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblValvoleTermostatiche" Text="Valvole termostatiche (rif. UNI EN 215)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblValvoleTermostatiche" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presenti"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assenti"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblValvoleDueVie" Text="Valvole a due vie" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblValvoleDueVie" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presenti"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assenti"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblValvoleTreVie" Text="Valvole a tre vie" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblValvoleTreVie" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presenti"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assenti"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label6" Text="Note" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="400" ID="txtNoteRegolazioneSingoloAmbiente" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial12" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>5.3 SISTEMI TELEMATICI DI TELELETTURA E TELEGESTIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelelettura" Text="Telelettura" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTelelettura" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presenti"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assenti"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelegestione" Text="Telegestione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTelegestione" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presenti"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assenti"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label5" Text="Descrizione del sistema" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <criter:DescrizioneSistemi ID="ctlDescrizioneSistemaTelegestione" IDTipoSistema="1" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial13" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h3>5.4 CONTABILIZZAZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label8" Text="Unità immobiliari contabilizzate" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblUnitaImmobiliariContabilizzate" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="rblUnitaImmobiliariContabilizzate_SelectedIndexChanged"
                            RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDettaglioSistemaContabilizzazione1">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label10" Text="" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkContabilizzazioneRiscaldamento" Text="RISCALDAMENTO" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkContabilizzazioneRaffrescamento" Text="RAFFRESCAMENTO" CssClass="radiobuttonlistClass" /><br />
                        <asp:CheckBox runat="server" ID="chkContabilizzazioneAcquaCalda" Text="ACQUA CALDA SANITARIA" CssClass="radiobuttonlistClass" /><br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDettaglioSistemaContabilizzazione2">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label11" Text="Tipologia sistema" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblIDTipologiaSistemaContabilizzazione" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDescrizioneSistemaContabilizzazione">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="Label9" Text="Descrizione del sistema" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <criter:DescrizioneSistemi ID="ctlDescrizioneSistemaContabilizzazione" IDTipoSistema="2" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial14" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>6.0 SISTEMI DI DISTRIBUZIONE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>6.1 TIPO DI DISTRIBUZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowTipologiaDistribuzione" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoDistribuzione" Text="Tipo di distribuzione (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="chkTipologiaDistribuzione" AutoPostBack="true" OnSelectedIndexChanged="chkTipologiaDistribuzione_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaDistribuzioneAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaDistribuzioneAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaDistribuzioneAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro tipo di distribuzione: campo obbligatorio"
                                ControlToValidate="txtTipologiaDistribuzioneAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial15" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						 <h3>6.2 COIBENTAZIONE RETE DI DISTRIBUZIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCoibentazioneReteDistribuzione" Text="Coibentazione rete di distribuzione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblCoibentazioneReteDistribuzione" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Presente"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Assente"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvCoibentazioneReteDistribuzione" runat="server" ForeColor="Red"
                            ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Coibentazione rete di distribuzione: campo obbligatorio"
                            ControlToValidate="rblCoibentazioneReteDistribuzione">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNoteCoibentazioneReteDistribuzione" Text="Note" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox Width="400" ID="txtNoteCoibentazioneReteDistribuzione" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial16" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>6.3 VASI DI ESPANSIONE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VasiEspansione ID="ctlVasiEspansione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h3>6.4 POMPE DI CIRCOLAZIONE (se non incorporate nel generatore)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:PompeCircolazione ID="ctlPompeCircolazione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>7.0 SISTEMA DI EMISSIONE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipologiaSistemiEmissione" Text="Sistemi di emissione (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipologiaSistemiEmissione" AutoPostBack="true" OnSelectedIndexChanged="cblTipologiaSistemiEmissione_SelectedIndexChanged" ValidationGroup="vgLibrettoImpianto" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                        <asp:Panel ID="pnlTipologiaSistemiEmissioneAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipologiaSistemiEmissioneAltro" runat="server" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaSistemiEmissioneAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" ErrorMessage="Altro sistemi di emissione: campo obbligatorio"
                                ControlToValidate="txtTipologiaSistemiEmissioneAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                        <asp:Button ID="btnSavePartial17" runat="server" CssClass="buttonSmallClass" Text="Salva dati libretto impianto"
                            OnClick="LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click"
                            ValidationGroup="vgLibrettoImpianto"
                            CausesValidation="false"
                            OnClientClick="disableBtn(this.id, 'Attendere, salvataggio in corso...');"
                            UseSubmitBehavior="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>8.0 SISTEMA DI ACCUMULO</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>8.1 ACCUMULI (se non incorporati nel gruppo termico o caldaia)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:Accumuli ID="ctlAccumuli" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>9.0 ALTRI COMPONENTI DELL’IMPIANTO</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.1 TORRI EVAPORATIVE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:TorriEvaporative ID="ctlTorriEvaporative" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.2 RAFFREDDATORI DI LIQUIDO (a circuito chiuso)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:RaffreddatoriLiquido ID="ctlRaffreddatoriLiquido" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.3 SCAMBIATORI DI CALORE INTERMEDI (per acqua di superficie o di falda)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ScambiatoriCaloreIntermedi ID="ctlScambiatoriCaloreIntermedi" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.4 CIRCUITI INTERRATI A CONDENSAZIONE / ESPANSIONE DIRETTA</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:CircuitiInterrati ID="ctlCircuitiInterrati" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.5 UNITÀ DI TRATTAMENTO ARIA</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:UnitaTrattamentoAria ID="ctlUnitaTrattamentoAria" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>9.6 RECUPERATORI DI CALORE (aria ambiente)</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:RecuperatoriCalore ID="ctlRecuperatoriCalore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>10.0 IMPIANTO DI VENTILAZIONE MECCANICA CONTROLLATA</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>10.1 IMPIANTO DI VENTILAZIONE MECCANICA CONTROLLATA</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ImpiantiVMC ID="ctlImpiantiVMC" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>11.0 RISULTATI DELLA PRIMA VERIFICA EFFETTUATA DALL'INSTALLATORE E DELLE VERIFICHE PERIODICHE SUCCESSIVE EFFETTUATE DAL MANUTENTORE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>11.1 GRUPPI TERMICI O CALDAIE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VerifichePeriodicheGT ID="ucCellGT" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>11.2 MACCHINE FRIGORIFERE / POMPE DI CALORE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VerifichePeriodicheGF ID="ucCellGF" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>11.3 SCAMBIATORI DI CALORE</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VerifichePeriodicheSC ID="ucCellSC" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>11.4 COGENERATORI / TRIGENERATORI</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VerifichePeriodicheCG ID="ucCellCG" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>12.0 INTERVENTI DI CONTROLLO EFFICIENZA ENERGETICA</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:InterventoControlloEfficienza
                            ID="InterventoControlloEfficienzaE"
                            runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>13.0 RISULTATI DELLE ISPEZIONI PERIODICHE EFFETTUATE A CURA DELL'ENTE COMPETENTE</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:VerificheIspettive
                            ID="VerificheIspettiveLibretto"
                            runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblConsumi" runat="server" Visible="true">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h2>14.0 REGISTRAZIONE DEI CONSUMI NEI VARI ESERCIZI</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>14.1 CONSUMO DI COMBUSTIBILE</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ConsumoCombustibile ID="ctlConsumoCombustibile" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>14.2 CONSUMO ENERGIA ELETTRICA</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ConsumoEnergiaElettrica ID="ctlConsumoEnergiaElettrica" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>14.3 CONSUMO ACQUA DI REINTEGRO NEL CIRCUITO DELL'IMPIANTO TERMICO</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ConsumoAcqua ID="ctlConsumoAcqua" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
					   <h3>14.4 CONSUMO DI PRODOTTI CHIMICI PER IL TRATTAMENTO ACQUA DEL CIRCUITO DELL'IMPIANTO TERMICO</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        <criter:ConsumoProdottiChimici ID="ctlConsumoProdottiChimici" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblComandiLibrettoImpianto" runat="server" Visible="false">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpianti_btnSaveLibrettoImpianto" runat="server" CssClass="buttonClass" Width="270"
                            OnClick="LIM_LibrettiImpianti_btnSaveLibrettoImpianto_Click" ValidationGroup="vgLibrettoImpianto" Text="SALVA DATI LIBRETTO IMPIANTO" />&nbsp;
						<asp:Button ID="LIM_LibrettiImpianti_btnViewLibrettoImpianto" runat="server" CssClass="buttonClass" Width="270" CausesValidation="false"
                            OnClick="LIM_LibrettiImpianti_btnViewLibrettoImpianto_Click" ValidationGroup="vgLibrettoImpianto" Text="VISUALIZZA PDF LIBRETTO IMPIANTO" />&nbsp;
                        <asp:Button ID="LIM_LibrettiImpianti_btnModifica" runat="server" Visible="false" CssClass="buttonClass" Width="270"
                            OnClick="LIM_LibrettiImpianti_btnModifica_Click" ValidationGroup="vgLibrettoImpianto" Text="MODIFICA DATI LIBRETTO IMPIANTO" CausesValidation="true" OnClientClick="return ModificaDatiLibretto();" />&nbsp;
                        <asp:Button ID="LIM_LibrettiImpianti_btnDismetti" runat="server" Visible="false" CssClass="buttonClass" Width="270"
                            Text="RICHIEDI DISMISSIONE GENERATORI" CausesValidation="false" OnClientClick="return WindowDismissioni.Show();"
                            AutoPostBack="False" UseSubmitBehavior="false" />&nbsp;
                        <dx:ASPxPopupControl ID="ASPxPopupControlDismissioni" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                            CloseAction="CloseButton" CloseAnimationType="Fade"
                            EnableViewState="False" PopupElementID="popupAreaDismissioni"
                            PopupHorizontalAlign="WindowCenter"
                            PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"
                            Width="980px" Height="600px" MinWidth="980px" MinHeight="600px"
                            ClientInstanceName="WindowDismissioni" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                            <ContentStyle Paddings-Padding="0" />
                            <ClientSideEvents Shown="WindowDismissioni" CloseUp="function(s, e) { window.location.reload(true); }" />
                        </dx:ASPxPopupControl>
                        <br />
                        <br />
                        <%-- <asp:Button ID="LIM_LibrettiImpianti_btnCopyLibrettoImpianto" runat="server" CssClass="buttonClass" Width="260" CausesValidation="false"
							OnClick="LIM_LibrettiImpianti_btnCopyLibrettoImpianto_Click" ValidationGroup="vgLibrettoImpianto" Text="DUPLICA DATI LIBRETTO IMPIANTO" OnClientClick="return DuplicaLibretto();" />&nbsp;--%>
                        <asp:Button ID="LIM_LibrettiImpianti_btnCloseLibrettoImpianto" runat="server" CssClass="buttonClass" Width="320"
                            OnClick="LIM_LibrettiImpianti_btnCloseLibrettoImpianto_Click" ValidationGroup="vgLibrettoImpianto" Text="SALVA E INVIA LIBRETTO IMPIANTO DEFINITIVO" CausesValidation="true" OnClientClick="return SalvaDefinitivo();" />&nbsp;
						&nbsp;
						<asp:Button ID="LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto" runat="server" CssClass="buttonClass" Width="270"
                            OnClick="LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto_Click" CausesValidation="false" Text="REVISIONE DATI LIBRETTO IMPIANTO" OnClientClick="return RevisionaLibretto();" />&nbsp;
						&nbsp;
						<asp:Button ID="LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto" runat="server" CssClass="buttonClass" Width="270"
                            OnClick="LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto_Click" CausesValidation="false" Text="ANNULLA LIBRETTO IMPIANTO" OnClientClick="return AnnullaLibretto();" />&nbsp;
                        &nbsp;
                        <asp:Button ID="LIM_LibrettiImpianti_btnNuovoRapportoControllo" runat="server" CssClass="buttonClass" Width="270"
                            OnClick="LIM_LibrettiImpianti_btnNuovoRapportoControllo_Click" ValidationGroup="vgLibrettoImpianto" Text="NUOVO RAPPORTO DI CONTROLLO TECNICO" CausesValidation="true" OnClientClick="return NuovoRapportoControllo();" />&nbsp;
                        
                        <asp:ValidationSummary ID="vsSaveLibrettoImpianto" ValidationGroup="vgLibrettoImpianto" runat="server" ShowMessageBox="True"
                            ShowSummary="True" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>

                        <asp:CustomValidator ID="cvDataRilascio" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaDataIntervento"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto"
                            ErrorMessage="Attenzione: la data di intervento del libretto non può essere superiore a quella corrente" />

                        <asp:CustomValidator ID="cvTipoClimatizzazione" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaTipoClimatizzazione"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto"
                            ErrorMessage="Attenzione: selezionare almeno un servizio nella sezione 1.3" />

                        <asp:CustomValidator ID="cvTipoFluidoVettore" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaTipoFluidoVettore"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto"
                            ErrorMessage="Attenzione: selezionare almeno un tipo di fluido vettore" />

                        <asp:CustomValidator ID="cvTipoGeneratori" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaTipoGeneratori"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto"
                            ErrorMessage="Attenzione: selezionare almeno una tipologia di generatori nella sezione 1.5" />

                        <asp:CustomValidator ID="cvPodPdr" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaPodPdr"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto" />

                        <asp:CustomValidator ID="cvGeneratoriInseriti" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaGeneratoriInseriti"
                            CssClass="testoerr"
                            ValidationGroup="vgLibrettoImpianto" />

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
