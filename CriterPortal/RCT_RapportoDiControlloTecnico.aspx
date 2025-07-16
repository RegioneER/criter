<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_RapportoDiControlloTecnico.aspx.cs" Inherits="RCT_RapportoDiControlloTecnico" %>
<%@ Register TagPrefix="si" Namespace="CriterPortal.SiWebControls" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCCheckbox.ascx" TagPrefix="uc1" TagName="UCCheckbox" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCFirmaDigitaleView.ascx" TagPrefix="uc1" TagName="UCFirmaDigitaleView" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCVerificaEnergeticaGT.ascx" TagPrefix="uc1" TagName="UCVerificaEnergeticaGT" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCVerificaEnergeticaGF.ascx" TagPrefix="uc1" TagName="UCVerificaEnergeticaGF" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCVerificaEnergeticaSC.ascx" TagPrefix="uc1" TagName="UCVerificaEnergeticaSC" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCVerificaEnergeticaCG.ascx" TagPrefix="uc1" TagName="UCVerificaEnergeticaCG" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCBolliniSelector.ascx" TagPrefix="uc1" TagName="UCBolliniSelector" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCRaccomandazioniPrescrizioni.ascx" TagPrefix="uc1" TagName="UCRaccomandazioniPrescrizioni" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCBolliniView.ascx" TagPrefix="uc1" TagName="UCBolliniView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentDisplay" runat="Server">
    <script type="text/javascript" language="javascript">
        function SalvaDefinitivo() {
            if (Page_ClientValidate()) {
                return (confirm('Confermando tale operazione lo stato del rapporto di controllo tecnico verrà reso in attesa di firma  e non sarà più possibile modificare i dati. Confermi?'));
            }
        }

        function AnnullaRapporto() {
            //if (Page_ClientValidate())
            return (confirm('Confermando tale operazione il rapporto di controllo tecnico verrà annullato. Confermi?'));
        }

        function AnnullaInAttesaDiFirma() {
            //if (Page_ClientValidate())
            return (confirm('Confermando tale operazione il rapporto di controllo tecnico verrà nuovamente reso in bozza. Confermi?'));
        }

        function disableBtnPomiager(btn, newText) {
            var $btn = $(btn);

            if ($btn.attr("isSubmitting"))
                return false;

            $btn.attr("isSubmitting", "true");
            $btn.val(newText);
            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:FormView ID="MainFormView" runat="server" DefaultMode="Edit"
                SelectMethod="GetRapporto" UpdateMethod="UpdateRapporto"
                DataKeyNames="IDRapportoControlloTecnico"
                OnItemCreated="MainFormView_ItemCreated"
                RenderOuterTable="false"
                OnDataBound="MainFormView_OnDataBound">
                <EditItemTemplate>
                    <asp:Table ID="tblInfoGenerali" CssClass="TableClass" runat="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>
                                    <asp:Label runat="server" ID="lblModelloRapportoControllo" />
                                </h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 2 %>'>
                            <asp:TableCell Width="100%" ColumnSpan="2" CssClass="riempimento">
                                <uc1:UCFirmaDigitaleView ID="UCFirmaDigitale" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                                Rapporto di controllo
                                <asp:Label runat="server" ID="lblIDSoggetto" Text='<%# Eval("IDSoggetto") %>' Visible="false" />
                                <asp:Label runat="server" ID="lblIDSoggettoDerived" Text='<%# Eval("IDSoggettoDerived") %>' Visible="false" />
                                <asp:Label runat="server" ID="lblGuidInteroImpianto" Text='<%# Eval("guidInteroImpianto") %>' Visible="false" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList Enabled='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 && Eval("guidInteroImpianto") == null  %>'
                                    runat="server"
                                    ID="rblTipologiaControllo"
                                    SelectedValue='<%# Eval("IDTipologiaControllo") %>'
                                    RepeatDirection="Horizontal"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="rblTipologiaControllo_OnSelectedIndexChanged"
                                    DataSourceID="dsTipologiaControllo"
                                    DataValueField="IDTipologiaControllo"
                                    DataTextField="TipologiaControllo">
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server" wicth="35%" CssClass="riempimento2">
                                Stato  del Rapporto di controllo tecnico
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblStatoRCT" Text='<%# Eval("StatoRapportoDiControllo") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell runat="server" wicth="35%" CssClass="riempimento2">
                               Identificativo Rapporto
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblGuidRapportoControlloTecnico" Text='<%# Eval("GuidRapportoTecnico") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>A. DATI IDENTIFICATIVI</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h3>Impianto</h3>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Targa Impianto" />
                            </asp:TableCell><asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="txtTargaImpianto" Text='<%# Eval("CodiceTargatura") %>' runat="server" ForeColor="Green" Font-Bold="True" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza termica nominale totale max (*)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox ID="txtPotenzaTermicaNominaleTotaleMax" Width="100px" ValidationGroup="vgRapportoDiControllo" CssClass="txtClass_o" Text='<%# Eval("PotenzaTermicaNominaleTotaleMax") %>' runat="server" />
                                <asp:RequiredFieldValidator ID="rfvtxtPotenzaTermicaNominaleTotaleMax" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic"
                                    runat="server" InitialValue="" ErrorMessage="Potenza termica nominale totale max: campo obbligatorio"
                                    ControlToValidate="txtPotenzaTermicaNominaleTotaleMax">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtPotenzaTermicaNominaleTotaleMax" ControlToValidate="txtPotenzaTermicaNominaleTotaleMax"
                                    ErrorMessage="Potenza termica nominale totale max: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                                    ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                    EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Comune" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCodiceCatastale" Text='<%# Eval("CodiceCatastale") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Provincia" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblProvincia" Text='<%# Eval("Provincia") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Indirizzo" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="lblIndirizzoImpianto" Text='<%# Eval("Indirizzo") %>' runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Numero civico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="lblCivico" Text='<%# Eval("Civico") %>' runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Palazzo"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPalazzo" Text='<%# Eval("Palazzo") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Scala" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblScala" Text='<%# Eval("Scala") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Interno" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblInterno" Text='<%# Eval("Interno") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Responsabile dell'impianto</h3></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Cognome" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="txtCognomeResponsabile" Text='<%# Eval("CognomeResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Nome" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblNomeResponsabile" Text='<%# Eval("NomeResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="C.F."></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCodiceFiscale" Text='<%# Eval("CodiceFiscaleResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDTipoSoggetto") == 2 %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Ragione Sociale" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblRagioneSociale" Text='<%# Eval("RagioneSocialeResponsabile") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDTipoSoggetto") == 2 %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="P.IVA"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPartitaIVA" Text='<%# Eval("PartitaIVAResponsabile") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Indirizzo"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblIndirizzoResponsabile" Text='<%# Eval("IndirizzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Numero civico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCivicoResponsabile" Text='<%# Eval("CivicoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Comune"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblComuneResponsabile" Text='<%# Eval("ComuneResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Provincia"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblProvinciaResponsabile" Text='<%# Eval("ProvinciaResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Titolo di Responsabilità" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblTipologiaResponsabile" Text='<%# Eval("TipologiaResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Terzo Responsabile (se nominato)</h3></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Ragione Sociale Terzo Responsabile" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblRagioneSocialeTerzoResponsabile" Text='<%# Eval("RagioneSocialeTerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="P.IVA Terzo Responsabile" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPartitaIVATerzoResponsabile" Text='<%# Eval("PartitaIVATerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                    <asp:Label runat="server" Text="Indirizzo" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblIndirizzoTerzoResponsabile" Text='<%# Eval("IndirizzoTerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Numero civico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCivicoTerzoResponsabile" Text='<%# Eval("CivicoTerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Comune" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblComuneTerzoResponsabile" Text='<%# Eval("ComuneTerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Provincia"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblProvinciaTerzoResponsabile" Text='<%# Eval("ProvinciaTerzoResponsabile") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Impresa manutentrice</h3></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Ragione Sociale" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblRagioneSocialeImpresaManutentrice" Text='<%# Eval("RagioneSocialeImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="P.IVA"></asp:Label>
                            </asp:TableCell><asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPartitaIVAImpresaManutentrice" Text='<%# Eval("PartitaIVAImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Indirizzo"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblIndirizzoImpresaManutentrice" Text='<%# Eval("IndirizzoImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                               <asp:Label runat="server" Text="Numero civico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCivicoImpresaManutentrice" Text='<%# Eval("CivicoImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Comune"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblComuneImpresaManutentrice" Text='<%# Eval("ComuneImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Provincia"> </asp:Label>
                            </asp:TableCell><asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblProvinciaImpresaManutentrice" Text='<%# Eval("ProvinciaImpresaManutentrice") %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                                <dx:ASPxButton runat="server" ID="btnSavePartial1" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial1" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ 
                                            if(ASPxClientEdit.ValidateGroup(null)) 
                                            { 
                                                window.setTimeout(function()
                                                { 
                                                   LoadingPanel1.Show(); 
                                                   s.SetText('Attendere, salvataggio in corso...'); 
                                                   s.SetEnabled(false); 
                                                },1) 
                                            } 
                                        }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="lp1" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel1">
                                </dx:ASPxLoadingPanel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                            <h2>B. DOCUMENTAZIONE TECNICA A CORREDO</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Dichiarazione di Conformità presente" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblDichiarazioneConformita" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblDichiarazioneConformita_SelectedIndexChanged"
                                    SelectedValue='<%# Eval("fDichiarazioneConformita") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Si" Selected="True" Value="True" />
                                    <asp:ListItem Text="No" Value="False" />
                                </asp:RadioButtonList>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='1'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni0"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("fDichiarazioneConformita").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='26'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni1"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && bool.Parse(Eval("fDichiarazioneConformita").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='32'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni2"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && bool.Parse(Eval("fDichiarazioneConformita").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='40'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni3"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && bool.Parse(Eval("fDichiarazioneConformita").ToString()) == false %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Libretto impianto presente" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblLibrettoImpiantoPresente" runat="server" Enabled="false" SelectedValue='<%# Eval("fLibrettoImpiantoPresente") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Si" Selected="True" Value="True" />
                                    <asp:ListItem Text="No" Value="False" />
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Libretti uso/manutenzione generatore presenti" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblUsoManutenzioneGeneratore"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblUsoManutenzioneGeneratore_SelectedIndexChanged"
                                    runat="server" SelectedValue='<%# Eval("fUsoManutenzioneGeneratore") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Si" Selected="True" Value="True" />
                                    <asp:ListItem Text="No" Value="False" />
                                </asp:RadioButtonList>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='2'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni4"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("fUsoManutenzioneGeneratore").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='27'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni5"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && bool.Parse(Eval("fUsoManutenzioneGeneratore").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='33'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni6"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && bool.Parse(Eval("fUsoManutenzioneGeneratore").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='41'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni7"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && bool.Parse(Eval("fUsoManutenzioneGeneratore").ToString()) == false %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Libretto impianto compilato in tutte le sue parti" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblLibrettoImpiantoCompilato" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblLibrettoImpiantoCompilato_SelectedIndexChanged"
                                    SelectedValue='<%# Eval("fLibrettoImpiantoCompilato") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Si" Selected="True" Value="True" />
                                    <asp:ListItem Text="No" Value="False" />
                                </asp:RadioButtonList>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='3'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni8"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("fLibrettoImpiantoCompilato").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='28'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni9"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && bool.Parse(Eval("fLibrettoImpiantoCompilato").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='34'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni10"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && bool.Parse(Eval("fLibrettoImpiantoCompilato").ToString()) == false %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='42'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni11"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && bool.Parse(Eval("fLibrettoImpiantoCompilato").ToString()) == false %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                                <%--<asp:Button runat="server" CssClass="buttonSmallClass" Text="Salva dati rapporto di controllo"
                                    ID="btnSavePartial2"
                                    ValidationGroup="vgRapportoDiControllo"
                                    CausesValidation="false"
                                    OnClientClick="return disableBtnPomiager(this, 'Attendere, salvataggio in corso...');"
                                    CommandName="Update" />--%>
                                <dx:ASPxButton runat="server" ID="btnSavePartial2" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial2" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ if(ASPxClientEdit.ValidateGroup(null)) { 
                                        window.setTimeout(function(){ 
                                        LoadingPanel2.Show(); 
                                        s.SetText('Attendere, salvataggio in corso...'); 
                                        s.SetEnabled(false); },1) } }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel2">
                                </dx:ASPxLoadingPanel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                            <h2>C. TRATTAMENTO DELL'ACQUA</h2>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat ="server" Text="Durezza Totale dell'acqua (*)"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox ID="txtDurezzaAcqua" Width="100px" ValidationGroup="vgRapportoDiControllo" CssClass="txtClass_o" Text='<%# Eval("DurezzaAcqua") %>' runat="server" />
                                (°fr)
                                <asp:RequiredFieldValidator ID="rfvtxtDurezzaAcqua" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic"
                                    runat="server" InitialValue="" ErrorMessage="Durezza acqua: campo obbligatorio"
                                    ControlToValidate="txtDurezzaAcqua">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtDurezzaAcqua" ControlToValidate="txtDurezzaAcqua"
                                    ErrorMessage="Durezza acqua: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                                    ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                    EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label ID="lblTrattamentoRiscaldamento" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblTrattamentoRiscaldamento" runat="server" SelectedValue='<%# Eval("TrattamentoRiscaldamento") %>'
                                    AutoPostBack="true" OnSelectedIndexChanged="rblTrattamentoRiscaldamento_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Presente" Value="1" />
                                    <asp:ListItem Text="Assente" Selected="True" Value="0" />
                                    <asp:ListItem Text="Non richiesto" Value="2" />
                                </asp:RadioButtonList>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='4'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni12"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("TrattamentoRiscaldamento").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='35'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni13"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && int.Parse(Eval("TrattamentoRiscaldamento").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='43'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni14"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && int.Parse(Eval("TrattamentoRiscaldamento").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowTrattamentoRiscaldamento" Visible='<%# (int)Eval("TrattamentoRiscaldamento") == 1 %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblListaTrattamentoInRiscaldamento" Text="Tipo trattamento in riscaldamento" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:CheckBoxList ID="cblTipologiaTrattamentoAcquaInvernale" ValidationGroup="vgRapportoDiControllo"
                                    runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow ID="rowTrattamentoTipoAcs">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblTrattamentoAcs" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblTrattamentoAcs" runat="server" SelectedValue='<%# Eval("TrattamentoAcs") %>'
                                    AutoPostBack="true" OnSelectedIndexChanged="rblTrattamentoAcs_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Presente" Value="1" />
                                    <asp:ListItem Text="Assente" Selected="True" Value="0" />
                                    <asp:ListItem Text="Non richiesto" Value="2" />
                                </asp:RadioButtonList>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='5'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni15"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("TrattamentoAcs").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='36'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni16"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && int.Parse(Eval("TrattamentoAcs").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowTrattamentoACS" Visible='<%# (int)Eval("TrattamentoAcs") == 1 %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label Text="Tipo trattamento in ACS" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:CheckBoxList ID="cblTipologiaTrattamentoAcquaAcs" ValidationGroup="vgRapportoDiControllo"
                                    runat="server" RepeatDirection="Horizontal" CssClass="radiobuttonlistClass" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                               <dx:ASPxButton runat="server" ID="btnSavePartial3" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial3" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ if(ASPxClientEdit.ValidateGroup(null)) { 
                                        window.setTimeout(function(){ 
                                        LoadingPanel3.Show(); 
                                        s.SetText('Attendere, salvataggio in corso...'); 
                                        s.SetEnabled(false); },1) } }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel2" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel3">
                                </dx:ASPxLoadingPanel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                            <h2>D. CONTROLLO DELL'IMPIANTO</h2>
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblLocaleInstallazioneIdoneo" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox Value='<%# (int)Eval("LocaleInstallazioneIdoneo") %>' DisableNA="true" runat="server"
                                    ID="chkLocaleInstallazioneIdoneo" AutoPostBack="true" OnCheckedChanged="chkLocaleInstallazioneIdoneo_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='6'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni17"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("LocaleInstallazioneIdoneo").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowGeneratoriIdonei" runat="server">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblGeneratoriIdonei" Text="Per installazione esterna: generatori idonei" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkGeneratoriIdonei" Value='<%# (int)Eval("GeneratoriIdonei") %>'
                                    DisableNA="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkGeneratoriIdonei_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='7'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni18"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("GeneratoriIdonei").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow ID="rowApertureLibere">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblApertureLibere" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkApertureLibere" Value='<%# (int)Eval("ApertureLibere") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkApertureLibere_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='8'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni20"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("ApertureLibere").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow ID="rowDimensioniApertureAdeguate">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblDimensioniApertureAdeguate" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkDimensioniApertureAdeguate" Value='<%# (int)Eval("DimensioniApertureAdeguate") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkDimensioniApertureAdeguate_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='9'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni19"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("DimensioniApertureAdeguate").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowLineeElettricheIdonee">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblLineeElettricheIdonee" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkLineeElettricheIdonee" Value='<%# (int)Eval("LineeElettricheIdonee") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow runat="server" ID="rowCapsulaInsonorizzataIdonea">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Capsula insonorizzante idonea (esame visivo)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkCapsulaInsonorizzazioneIdonea" Value='<%# (int)Eval("CapsulaInsonorizzataIdonea") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow ID="rowScarichiIdonei">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblScarichiIdonei" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkScarichiIdonei" Value='<%# (int)Eval("ScarichiIdonei") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkScarichiIdonei_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='10'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni21"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("ScarichiIdonei").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowRegolazioneTemperaturaAmbiente" runat="server">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Sistema di regolazione temperatura ambiente funzionante" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkRegolazioneTemperaturaAmbiente" Value='<%# (int)Eval("RegolazioneTemperaturaAmbiente") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkRegolazioneTemperaturaAmbiente_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='11'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni22"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("RegolazioneTemperaturaAmbiente").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow ID="rowAssenzaPerditeCombustibile" Visible='<%# (int)Eval("IDTipologiaRCT") == 4 || ((int)Eval("IDTipologiaRCT") == 1 && (int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 4 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 5)) %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblAssenzaPerditeCombustibile" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkAssenzaPerditeCombustibile" Value='<%# (int)Eval("AssenzaPerditeCombustibile") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkAssenzaPerditeCombustibile_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='12'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni23"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("AssenzaPerditeCombustibile").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowCoibentazioniIdonee">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblCoibentazioniIdonee" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkCoibentazioniIdonee" Value='<%# (int)Eval("CoibentazioniIdonee") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow ID="rowTenutaImpiantoIdraulico" Visible='<%# (int)Eval("IDTipologiaRCT") == 3 || (int)Eval("IDTipologiaRCT") == 4 || ((int)Eval("IDTipologiaRCT") == 1 && (int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 2 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 3)) %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblTenutaImpiantoIdraulico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkTenutaImpiantoIdraulico" Value='<%# (int)Eval("TenutaImpiantoIdraulico") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkTenutaImpiantoIdraulico_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='13'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni24"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("TenutaImpiantoIdraulico").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowTenutaCircuitoOlioIdonea">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Tenuta circuito olio idonea" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkTenutaCircuitoOlioIdonea" Value='<%# (int)Eval("TenutaCircuitoOlioIdonea") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowFunzionalitàScambiatoreSeparazione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Funzionalità dello scambiatore di calore di separazione fra unità cogenerativa e impianto edificio (se presente) idonea" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkFunzionalitaScambiatoreSeparazione" Value='<%# (int)Eval("FunzionalitàScambiatoreSeparazione") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                                <dx:ASPxButton runat="server" ID="btnSavePartial4" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial4" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ if(ASPxClientEdit.ValidateGroup(null)) { 
                                        window.setTimeout(function(){ 
                                        LoadingPanel4.Show(); 
                                        s.SetText('Attendere, salvataggio in corso...'); 
                                        s.SetEnabled(false); },1) } }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel3" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel4">
                                </dx:ASPxLoadingPanel>

                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>
                                    <asp:Label runat="server" ID="lblTitoloSezioneE" />
                                </h2>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblModello" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblCodiceProgressivo" Text='<%# Eval("CodiceProgressivo") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>
                        <si:SiTableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Data di installazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblDataInstallazione" Text='<%# Eval("DataInstallazione", "{0:dd/MM/yyyy}") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Fabbricante" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="lblFabbricante" Text='<%# Eval("Fabbricante") %>' runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Modello" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="lblModelloGruppo" Text='<%# Eval("Modello") %>' runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Matricola" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label ID="lblMatricola" Text='<%# Eval("Matricola") %>' runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow ID="rowTipoGruppiTermici">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipo gruppo termico" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblTipoGruppiTermici" Text='<%# Eval("TipologiaGruppiTermici") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowTipologiaGeneratoriTermici" Visible='<%# (int)Eval("IDTipologiaRCT") == 1 %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipologia generatore (*)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <dx:ASPxComboBox ID="cmbTipologiaGeneratoriTermici" runat="server" Theme="Default"
                                    TextField="Descrizione" ValueType="System.String" ValueField="IdTipologiaGeneratoriTermici"
                                    Value='<%#: Bind("IdTipologiaGeneratoriTermici") %>'
                                    DataSourceID="dsTipologiaGeneratoriTermici">
                                </dx:ASPxComboBox>
                                <asp:RequiredFieldValidator
                                    ID="rfvcmbTipologiaGeneratoriTermici"
                                    ValidationGroup="vgRapportoDiControllo"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    runat="server"
                                    InitialValue=""
                                    ErrorMessage="Tipologia generatore: campo obbligatorio"
                                    ControlToValidate="cmbTipologiaGeneratoriTermici">&nbsp;*</asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaTermicaNominaleFocolare" Visible='<%# (int)Eval("IDTipologiaRCT") == 1 %>'>
                            <asp:TableCell runat="server" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza termica max al focolare (kW)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPotenzaTermicaNominaleFocolare" Text='<%# Eval("PotenzaTermicaNominaleFocolare") %>' />
                                                                
                                <%--<asp:TextBox ID="txtPotenzaTermicaNominaleFocolare" Width="100px" runat="server" ValidationGroup="vgRapportoDiControllo" CssClass="txtClass_o" Text='<%# Eval("PotenzaTermicaNominaleFocolare") %>' />
                                <asp:RequiredFieldValidator ID="rfvtxtPotenzaTermicaNominaleFocolare" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic"
                                    runat="server" InitialValue="" ErrorMessage="Potenza termica nominale max al focolare: campo obbligatorio"
                                    ControlToValidate="txtPotenzaTermicaNominaleFocolare">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtPotenzaTermicaNominaleFocolare" ControlToValidate="txtPotenzaTermicaNominaleFocolare"
                                    ErrorMessage="Potenza termica nominale max al focolare: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                                    ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                    EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>--%>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowNCircuitiTotali">
                            <asp:TableCell runat="server" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Numero circuiti" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblNCircuitiTotali" Text='<%# Eval("NCircuitiTotali") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaFrigorifera">
                            <asp:TableCell runat="server" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza frigorifera nominale in raffrescamento (kW)" />
                            </asp:TableCell><asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="txtPotenzaFrigorifera" Text='<%# Eval("PotenzaFrigorifera") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaElettricaMorsetti">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza elettrica nominale ai morsetti (kW)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPotenzaElettricaMorsetti" Text='<%#Bind("PotenzaElettricaMorsetti") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaAssorbitaCombustibile">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza assorbita con il combustibile (kW)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="100px" runat="server" ID="txtPotenzaAssorbitaCombustibile" ValidationGroup="vgRapportoDiControllo" CssClass="txtClass_o" Text='<%#Bind("PotenzaAssorbitaCombustibile") %>' />
                                <asp:RequiredFieldValidator ID="rfvtxtPotenzaAssorbitaCombustibile" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic"
                                    runat="server" InitialValue="" ErrorMessage="Potenza assorbita con il combustibile: campo obbligatorio"
                                    ControlToValidate="txtPotenzaAssorbitaCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtPotenzaAssorbitaCombustibile" ControlToValidate="txtPotenzaAssorbitaCombustibile"
                                    ErrorMessage="Potenza assorbita con il combustibile: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                                    ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                    EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblTitoloPotenzaTermicaNominale" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblPotenzaTermicaNominale" Text='<%# Eval("PotenzaTermicaNominale") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaBypass">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza termica a pieno regime con by-pass fumi aperto (se presente) (kW)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="100px" runat="server" CssClass="txtClass" ID="txtPotenzaBypass" Text='<%# Bind("PotenzaByPass") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Servizi" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:CheckBox runat="server" ID="chkClimatizzazioneInvernale" Checked='<%# (Boolean) Eval("fClimatizzazioneInvernale") %>' Text="Climatizzazione Invernale" />
                                <div></div>
                                <asp:CheckBox runat="server" ID="chkClimatizzazioneEstiva" Checked='<%# (Boolean) Eval("fClimatizzazioneEstiva") %>' Text="Climatizzazione Estiva" />
                                <div></div>
                                <asp:CheckBox runat="server" ID="chkProduzioneACS" Checked='<%# (Boolean) Eval("fProduzioneACS") %>' Text="Produzione ACS" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowProvaEseguita">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Prova eseguita in modalità" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButton runat="server" ID="rbRaffrescamento" GroupName="rbgModalitàProva" Text="raffrescamento" Checked='<%# Eval("ProvaRaffrescamento") %>' />
                                <asp:RadioButton runat="server" ID="rbRiscaldamento" GroupName="rbgModalitàProva" Text="riscaldamento" Checked='<%# Eval("ProvaRiscaldamento") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowTipologiaMacchineFrigorifere">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipologia generatore" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblTipologiaMacchineFrigorifere" Text='<%# Bind("TipologiaMacchineFrigorifere") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowTipologiaCogeneratore">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipologia" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblTipologiaCogeneratore" Text='<%# Bind("TipologiaCogeneratore") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowFluidoVettoreEntrata">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Alimentazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <dx:ASPxComboBox ID="cmbFluidoVettoreEntrata" runat="server" Theme="Default"
                                    DataSourceID="dsTipologieFluidotermoVettore"
                                    TextField="TipologiaFluidoTermoVettore" ValueType="System.String"
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbFluidoVettoreEntrata_SelectedIndexChanged"
                                    ValueField="IDTipologiaFluidoTermoVettore"
                                    Value='<%#: Bind("IDTipologiaFluidoTermoVettoreEntrata") %>'>
                                </dx:ASPxComboBox>

                                <asp:Panel runat="server" ID="pnlFluidoVettoreEntrataAltro" Visible="false">
                                    <asp:TextBox Width="65%" runat="server" ID="txtAltroFluidoTermoVettoreEntrata" Text='<%# Bind("AltroTipologiaFluidoTermoVettoreEntrata") %>' CssClass="txtClass_o" ValidationGroup="vgRapportoDiControllo" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtAltroFluidoTermoVettoreEntrata" ForeColor="Red" runat="server" ValidationGroup="vgRapportoDiControllo" EnableClientScript="true" ErrorMessage="Alimentazione: campo obbligatorio"
                                        ControlToValidate="txtAltroFluidoTermoVettoreEntrata">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:Panel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowFluidoVettoreUscita">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Fluido vettore termico in uscita" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <dx:ASPxComboBox ID="cmbFluidoVettore" runat="server" Theme="Default"
                                    DataSourceID="dsTipologieFluidotermoVettoreUscita"
                                    ValueField="IDTipologiaFluidoTermoVettore" ValueType="System.String"
                                    Value='<%#: Bind("IDTipologiaFluidoTermoVettoreUscita") %>'
                                    TextField="TipologiaFluidoTermoVettore"
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbFluidoVettore_SelectedIndexChanged">
                                </dx:ASPxComboBox>

                                <asp:Panel runat="server" ID="pnlFluidoVettoreUscitaAltro" Visible="false">
                                    <asp:TextBox Width="65%" runat="server" ID="txtAltroFluidoTermoVettoreUscita" Text='<%# Bind("AltroTipologiaFluidoTermoVettoreUscita") %>' CssClass="txtClass_o" ValidationGroup="vgRapportoDiControllo" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtAltroFluidoTermoVettoreUscita" ForeColor="Red" runat="server" ValidationGroup="vgRapportoDiControllo" EnableClientScript="true" ErrorMessage="Fluido termico vettore in uscita: campo obbligatorio"
                                        ControlToValidate="txtAltroFluidoTermoVettoreUscita">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:Panel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowTipologiaCombustile">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblTitoloCombustile" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblTipologiaCombustibile" Text='<%# Eval("TipologiaCombustibile") %>' />
                                <asp:Label runat="server" ID="lblIDTipologiaCombustibile" Visible="false" Text='<%# Eval("IDTipologiaCombustibile") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowEvacuazioneFumi">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Modalità di evacuazione fumi" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButton ID="rblEvacuazioneForzata" runat="server" Text="Forzata" GroupName="EvacuazioneGroup" AutoPostBack="true" OnCheckedChanged="rblEvacuazioneNaturaleForzata_CheckedChanged" Checked='<%# Eval("EvacuazioneForzata") %>' />
                                <asp:RadioButton ID="rblEvacuazioneNaturale" runat="server" Text="Naturale" GroupName="EvacuazioneGroup" AutoPostBack="true" OnCheckedChanged="rblEvacuazioneNaturaleForzata_CheckedChanged" Checked='<%# Eval("EvacuazioneNaturale") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowDepressioneCanaleFumo" runat="server" Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && (bool)Eval("EvacuazioneNaturale") == true && (int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 2 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 3) %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Depressione del canale da fumo (Pa)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="100px" ID="txtDepressioneCanaleFumo"
                                    Text='<%# Eval("DepressioneCanaleFumo") %>' AutoPostBack="true" OnTextChanged="txtDepressioneCanaleFumo_TextChanged" ValidationGroup="vgRapportoDiControllo" CssClass="txtClass_o" runat="server" />
                                <cc1:FilteredTextBoxExtender ID="fbeDepressioneCanaleFumo" runat="server" FilterType="Numbers, Custom"
                                     ValidChars="," TargetControlID="txtDepressioneCanaleFumo" />
                                <asp:RequiredFieldValidator ID="rfvtxtDepressioneCanaleFumo" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic"
                                    runat="server" InitialValue="" Enabled='<%# bool.Parse(Eval("EvacuazioneNaturale").ToString()) == true %>'
                                    ErrorMessage="Depressione del canale da fumo (Pa): campo obbligatorio"
                                    ControlToValidate="txtDepressioneCanaleFumo">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtDepressioneCanaleFumo" ControlToValidate="txtDepressioneCanaleFumo"
                                    ErrorMessage="Depressione del canale da fumo: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
                                    ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red"
                                    EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='14'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni25"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && Eval("DepressioneCanaleFumo") != null && ((decimal)Eval("DepressioneCanaleFumo")) < 3 %>' /> <%--&& ((decimal)Eval("DepressioneCanaleFumo") < 1)--%>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowDispositiviComandoRegolazione" runat="server">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Dispositivi di comando e regolazione funzionanti correttamente" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkDispositiviComandoRegolazione" Value='<%# (int)Eval("DispositiviComandoRegolazione") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkDispositiviComandoRegolazione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='15'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni26"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("DispositiviComandoRegolazione").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowDispositiviSicurezza">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Dispositivi di sicurezza non manomessi e/o cortocircuitati" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkDispositiviSicurezza" Value='<%# (int)Eval("DispositiviSicurezza") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkDispositiviSicurezza_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='16'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni27"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("DispositiviSicurezza").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowValvolaSicurezzaSovrappressione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Valvola di sicurezza alla sovrappressione a scarico libero" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkValvolaSicurezzaSovrappressione" Value='<%# (int)Eval("ValvolaSicurezzaSovrappressione") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkValvolaSicurezzaSovrappressione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='17'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni28"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("ValvolaSicurezzaSovrappressione").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowScambiatoreFumiPulito">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Controllato e pulito lo scambiatore lato fumi" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="riempimento" Width="65%">
                                <uc1:UCCheckbox ID="chkScambiatoreFumiPulito" Value='<%# (int)Eval("ScambiatoreFumiPulito") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkScambiatoreFumiPulito_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='18'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni29"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("ScambiatoreFumiPulito").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowRiflussoProdottiCombustione" runat="server">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Presenza riflusso dei prodotti della combustione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkRiflussoProdottiCombustione" Value='<%# (int)Eval("RiflussoProdottiCombustione") %>' DisableNA="true"
                                    runat="server" AutoPostBack="true" OnCheckedChanged="chkRiflussoProdottiCombustione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='19'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni30"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("RiflussoProdottiCombustione").ToString()) == 1 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowConformitaUNI10389" runat="server" Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && (int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 2 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 3 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 4 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 5) %>'>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Risultati controllo, secondo UNI 10389-1, conformi alla legge" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkConformitàUNI10389" Value='<%# (int)Eval("ConformitaUNI10389") %>' AutoPostBack="true" OnCheckedChanged="chkConformitàUNI10389_CheckedChanged" DisableNA="true" runat="server" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='47'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni47"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("ConformitaUNI10389").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowAssenzaPerditeRefrigerante">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Assenza di perdite di gas refrigerante" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkAssenzaPerditeRefrigerante" runat="server" Value='<%# (int)Eval("AssenzaPerditeRefrigerante") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowFiltriPuliti">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Filtri puliti" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkFiltriPuliti" runat="server" Value='<%# (int)Eval("FiltriPuliti") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowLeakDetector">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Presenza apparecchiatura automatica rilevazione diretta fughe refrigerante (leak detector)" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkLeakDetector" runat="server" Value='<%# (int)Eval("LeakDetector") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowScambiatoriLiberi">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Scambiatori di calore puliti e liberi da incrostazioni" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkScambiatoriLiberi" runat="server" Value='<%# (int)Eval("ScambiatoriLiberi") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowParametriTermodinamici">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Presenza apparecchiatura automatica rilevazione indiretta fughe refrigerante (parametri termodinamici)" />
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkParametriTermodinamici" runat="server" Value='<%# (int)Eval("ParametriTermodinamici") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowPotenzaCompatibileProgetto">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Potenza compatibile con i dati di progetto" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkPotenzaCompatibileProgetto" runat="server" Value='<%# (int)Eval("PotenzaCompatibileProgetto") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowStatoCoibentazioniIdonee">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblStatoCoibentazioniIdonee" Text="Stato delle coibentazioni idoneo" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkStatoCoibentazioniIdonee" Value='<%# (int)Eval("StatoCoibentazioniIdonee") %>' DisableNA="true" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowAssenzaTrafilamenti">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Dispositivi di regolazione e controllo funzionanti Assenza di trafilamenti sulla valvola di rregolazione"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkAssenzaTrafilamenti" runat="server" Value='<%# (int)Eval("Assenzatrafilamenti") %>' DisableNA="true" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h3>VALORI MISURATI</h3>
                            </asp:TableCell>
                        </asp:TableRow>
                        <si:SiTableRow ID="rowVerificaEnergeticaGT">
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                <uc1:UCVerificaEnergeticaGT
                                    runat="server" IDTipologiaControllo='<%# Eval("IDTipologiaControllo").ToString() %>'
                                    ID="UCVerificaEnergeticaGT"
                                    TemperaturaFumi='<%# Bind("TemperaturaFumi") %>'
                                    TemperaturaComburente='<%# Bind("TemperaraturaComburente") %>'
                                    O2='<%# Bind("O2") %>'
                                    Co2='<%# Bind("Co2") %>'
                                    Bacharach1='<%# Bind("bacharach1") %>'
                                    Bacharach2='<%# Bind("bacharach2") %>'
                                    Bacharach3='<%# Bind("bacharach3") %>'
                                    CoCorretto='<%# Bind("CoCorretto") %>'
                                    RendimentoCombustione='<%# Bind("RendimentoCombustione") %>'
                                    RendimentoMinimo='<%# Bind("RendimentoMinimo") %>'
                                    Modulotermico='<%# Bind("ModuloTermico") %>'
                                    COFumiSecchi='<%# Bind("COFumiSecchi") %>'
                                    PortataCombustibile='<%# Bind("PortataCombustibile") %>'
                                    RispettaIndiceBacharach='<%# Bind("RispettaIndiceBacharach") %>'
                                    COFumiSecchiNoAria1000='<%# Bind("COFumiSecchiNoAria1000") %>'
                                    RendimentoSupMinimo='<%# Bind("RendimentoSupMinimo") %>'
                                    PotenzaTermicaEffettiva='<%# Bind("PotenzaTermicaEffettiva") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowVerificaEnergeticaGF">
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                <uc1:UCVerificaEnergeticaGF runat="server"
                                    ID="UCVerificaEnergeticaGF" IDTipologiaControllo='<%# Eval("IDTipologiaControllo").ToString() %>'
                                    Surriscaldamento='<%# Bind("TemperaturaSurriscaldamento") %>'
                                    Sottoraffredamento='<%# Bind("TemperaturaSottoraffreddamento") %>'
                                    TemperaturaCondensazione='<%#Bind("TemperaturaCondensazione") %>'
                                    TemperaturaEvaporazione='<%# Bind("TemperaturaEvaporazione") %>'
                                    TInEst='<%#Bind("TInglatoEst") %>'
                                    TOutEst='<%#Bind("TUscLatoEst") %>'
                                    TInUtenze='<%# Bind("TIngLatoUtenze") %>'
                                    TOutUtenze='<%# Bind("TUscLatoUtenze") %>'
                                    NCircuito='<%#Bind("NCircuiti") %>'
                                    PotenzaAssorbita='<%#Bind("PotenzaAssorbita") %>'
                                    TUscitaFluido='<%#Bind("TUscitaFluido") %>'
                                    TBulboUmidoAria='<%#Bind("TBulboUmidoAria") %>'
                                    TIngressoLatoEsterno='<%#Bind("TIngressoLatoEsterno") %>'
                                    TUscitaLatoEsterno='<%#Bind("TUscitaLatoEsterno") %>'
                                    TIngressoLatoMacchina='<%#Bind("TIngressoLatoMacchina") %>'
                                    TUscitaLatoMacchina='<%#Bind("TUscitaLatoMacchina") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowVerificaEnergeticaSC">
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                <uc1:UCVerificaEnergeticaSC runat="server"
                                    ID="UCVerificaEnergeticaSC" IDTipologiaControllo='<%# Eval("IDTipologiaControllo").ToString() %>'
                                    TermperaturaEsterna='<%# Bind("TemperaturaEsterna") %>'
                                    TermperaturaMandataPrimario='<%# Bind("TemperaturamandataPrimario") %>'
                                    TermperaturaRitornoPrimario='<%#Bind("TemperaturaRitornoPrimario") %>'
                                    PotenzaTermica='<%#Bind("PotenzaTermica") %>'
                                    PortataFluidoPrimario='<%# Bind("PortataFluidoPrimario") %>'
                                    TermperaturaMandataSecondario='<%# Bind("TemperaturamandataSecondario") %>'
                                    TermperaturaRitornoSecondario='<%# Bind("TemperaturaRitornoSecondario") %>' />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowVerificaEnergeticaCG">
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                <uc1:UCVerificaEnergeticaCG runat="server"
                                    ID="UCVErificaEnergeticaCG" IDTipologiaControllo='<%# Eval("IDTipologiaControllo").ToString() %>'
                                    TemperaturaAriaComburente='<%#Bind("TemperaturaAriaComburente") %>'
                                    TemperaturaAcquaIngresso='<%#Bind("TemperaturaAcquaIngresso") %>'
                                    TemperaturaAcquaUscita='<%#Bind("TemperaturaAcquaUscita") %>'
                                    PotenzaAiMorsetti='<%# Bind("PotenzaAiMorsetti") %>'
                                    TemperaturaAcquaMotore='<%#Bind("TemperaturaAcquaMotore") %>'
                                    TemperaturaFumiMonte='<%# Bind("TemperaturaFumiMonte") %>'
                                    TemperaturaFumiValle='<%#Bind("TemperaturaFumiValle") %>'
                                    EmissioneMonossido='<%#Bind("EmissioneMonossido") %>'
                                    SovrafrequenzaSogliaInterv1='<%#Bind("SovrafrequenzaSogliaInterv1") %>'
                                    SovrafrequenzaTempoInterv1='<%#Bind("SovrafrequenzaTempoInterv1") %>'
                                    SottofrequenzaSogliaInterv1='<%#Bind("SottofrequenzaSogliaInterv1") %>'
                                    SottofrequenzaTempoInterv1='<%#Bind("SottofrequenzaTempoInterv1") %>'
                                    SovratensioneSogliaInterv1='<%#Bind("SovratensioneSogliaInterv1") %>'
                                    SovratensioneTempoInterv1='<%#Bind("SovratensioneTempoInterv1") %>'
                                    SottotensioneSogliaInterv1='<%#Bind("SottotensioneSogliaInterv1") %>'
                                    SottotensioneTempoInterv1='<%#Bind("SottotensioneTempoInterv1") %>'
                                    SovrafrequenzaSogliaInterv2='<%#Bind("SovrafrequenzaSogliaInterv2") %>'
                                    SovrafrequenzaTempoInterv2='<%#Bind("SovrafrequenzaTempoInterv2") %>'
                                    SottofrequenzaSogliaInterv2='<%#Bind("SottofrequenzaSogliaInterv2") %>'
                                    SottofrequenzaTempoInterv2='<%#Bind("SottofrequenzaTempoInterv2") %>'
                                    SovratensioneSogliaInterv2='<%#Bind("SovratensioneSogliaInterv2") %>'
                                    SovratensioneTempoInterv2='<%#Bind("SovratensioneTempoInterv2") %>'
                                    SottotensioneSogliaInterv2='<%#Bind("SottotensioneSogliaInterv2") %>'
                                    SottotensioneTempoInterv2='<%#Bind("SottotensioneTempoInterv2") %>'
                                    SovrafrequenzaSogliaInterv3='<%#Bind("SovrafrequenzaSogliaInterv3") %>'
                                    SovrafrequenzaTempoInterv3='<%#Bind("SovrafrequenzaTempoInterv3") %>'
                                    SottofrequenzaSogliaInterv3='<%#Bind("SottofrequenzaSogliaInterv3") %>'
                                    SottofrequenzaTempoInterv3='<%#Bind("SottofrequenzaTempoInterv3") %>'
                                    SovratensioneSogliaInterv3='<%#Bind("SovratensioneSogliaInterv3") %>'
                                    SovratensioneTempoInterv3='<%#Bind("SovratensioneTempoInterv3") %>'
                                    SottotensioneSogliaInterv3='<%#Bind("SottotensioneSogliaInterv3") %>'
                                    SottotensioneTempoInterv3='<%#Bind("SottotensioneTempoInterv3") %>'></uc1:UCVerificaEnergeticaCG>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                                <%--                                <asp:Button runat="server" CssClass="buttonSmallClass" Text="Salva dati rapporto di controllo"
                                    ID="btnSavePartial5"
                                    ValidationGroup="vgRapportoDiControllo"
                                    CausesValidation="false"
                                    OnClientClick="return disableBtnPomiager(this, 'Attendere, salvataggio in corso...');"
                                    CommandName="Update"/>--%>
                                <dx:ASPxButton runat="server" ID="btnSavePartial5" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial5" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ if(ASPxClientEdit.ValidateGroup(null)) { 
                                        window.setTimeout(function(){ 
                                        LoadingPanel5.Show(); 
                                        s.SetText('Attendere, salvataggio in corso...'); 
                                        s.SetEnabled(false); },1) } }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel4" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel5">
                                </dx:ASPxLoadingPanel>


                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>F. CHECK-LIST</h2>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                Elenco dei possibili interventi, dei quali va valutata la convenienza economica, che, qualora applicabili all'impianto, potrebbero comportare un miglioramento della prestazione energetica.
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                <asp:CheckBoxList ID="cblCheckList" ValidationGroup="vgRapportoDiControllo"
                                    runat="server" RepeatDirection="Vertical" CssClass="radiobuttonlistClass" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowTitoloSistemiTermoregolazione">
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>G. SISTEMI TERMOREGOLAZIONE E CONTABILIZZAZIONE DEL CALORE (solo per impianti centralizzati)</h2>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowTipoDistribuzione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipo di distribuzione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <dx:ASPxComboBox ID="cmbTipologiaDistribuzione" runat="server" Theme="Default"
                                    DataSourceID="dsTipologiaDistribuzione"
                                    TextField="TipologiaSistemaDistribuzione" ValueType="System.String"
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbTipologiaSistemaDistribuzione_SelectedIndexChanged"
                                    ValueField="IDTipologiaSistemaDistribuzione"
                                    Value='<%#: Bind("IDTipologiaSistemaDistribuzione") %>'>
                                </dx:ASPxComboBox>
                                <asp:Panel runat="server" ID="pnlTipologiaSistemaDistribuzioneAltro" Visible="false">
                                    <asp:TextBox Width="65%" runat="server" ID="txtAltroTipologiaSistemaDistribuzione" Text='<%# Bind("AltroTipologiaSistemaDistribuzione") %>' CssClass="txtClass_o" ValidationGroup="vgRapportoDiControllo" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtAltroTipologiaSistemaDistribuzione" ForeColor="Red" runat="server" ValidationGroup="vgRapportoDiControllo" EnableClientScript="true" ErrorMessage="Altro tipologia sistema di distribuzione: campo obbligatorio"
                                        ControlToValidate="txtAltroTipologiaSistemaDistribuzione">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowContabilizzazione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Contabilizzazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                Unità immobiliari contabilizzate&nbsp;&nbsp;
                                <uc1:UCCheckbox ID="chkContabilizzazione" Value='<%# (int)Eval("Contabilizzazione") %>'
                                    DisableNC="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkContabilizzazione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='23'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni31"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("Contabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='29'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni32"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && int.Parse(Eval("Contabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='37'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni33"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && int.Parse(Eval("Contabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='44'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni34"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && int.Parse(Eval("Contabilizzazione").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowTipoContabilizzazione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Tipo di contabilizzazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <dx:ASPxComboBox ID="cmbTipologiaContabilizzazione" runat="server" Theme="Default"
                                    DataSourceID="dsTipologiaContabilizzazione"
                                    TextField="TipologiaContabilizzazione" ValueType="System.String"
                                    AutoPostBack="false"
                                    ValueField="IDTipologiaContabilizzazione"
                                    Value='<%#: Bind("IDTipologiaContabilizzazione") %>'>
                                </dx:ASPxComboBox>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowTermoregolazione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Termoregolazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                Valvole termostatiche presenti&nbsp;&nbsp;
                                <uc1:UCCheckbox ID="chkTermoregolazione" Value='<%# (int)Eval("Termoregolazione") %>'
                                    DisableNC="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkTermoregolazione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='24'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni35"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("Termoregolazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='30'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni36"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && int.Parse(Eval("Termoregolazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='38'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni37"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && int.Parse(Eval("Termoregolazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='45'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni38"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && int.Parse(Eval("Termoregolazione").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow runat="server" ID="rowCorrettoFunzionamentoContabilizzazione">
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Corretto funzionamento dei sistemi di contabilizzazione e termoregolazione" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <uc1:UCCheckbox ID="chkCorrettoFunzionamentoContabilizzazione" Value='<%# (int)Eval("CorrettoFunzionamentoContabilizzazione") %>'
                                    DisableNC="true" runat="server" AutoPostBack="true" OnCheckedChanged="chkCorrettoFunzionamentoContabilizzazione_CheckedChanged" />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='25'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni39"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 1 && int.Parse(Eval("CorrettoFunzionamentoContabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='31'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni40"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 2 && int.Parse(Eval("CorrettoFunzionamentoContabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='39'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni41"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 3 && int.Parse(Eval("CorrettoFunzionamentoContabilizzazione").ToString()) == 0 %>' />
                                <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='46'
                                    iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server"
                                    ID="UCRaccomandazioniPrescrizioni42"
                                    Visible='<%# (int)Eval("IDTipologiaRCT") == 4 && int.Parse(Eval("CorrettoFunzionamentoContabilizzazione").ToString()) == 0 %>' />
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Osservazioni" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="500" Height="100" ID="txtOsservazioni" CssClass="txtClass" Text='<%# Eval("Osservazioni") %>' runat="server" TextMode="MultiLine" Rows="3" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Raccomandazioni" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="500" Height="100" ID="txtRaccomandazioni" CssClass="txtClass" Text='<%# Eval("Raccomandazioni") %>' runat="server" TextMode="MultiLine" Rows="3" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Prescrizioni" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="500" Height="100" ID="txtPrescrizioni" CssClass="txtClass" Text='<%# Eval("Prescrizioni") %>' runat="server" TextMode="MultiLine" Rows="3" />
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                Il tecnico dichiara, in riferimento ai punti A,B,C,D,E (sopra menzionati), che l'apparecchio può essere messo in servizio ed usato normalmente ai fini dell'efficienza energetica senza compromettere la sicurezza delle persone, degli animali e dei beni.
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="L'impianto può funzionare" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:RadioButtonList ID="rblImpiantoFunzionante" runat="server" SelectedValue='<%# Eval("fImpiantoFunzionante") %>' RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Si" Selected="True" Value="True" />
                                    <asp:ListItem Text="No" Value="False" />
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                Il tecnico declina altresì ogni responsabilità per sinistri a persone, animali o cose derivanti da manomissioni dell'impianto o dell'apparecchio da parte di terzi, ovvero da carenze di manutenzione successiva. In presenza di carenze riscontrate e non eliminate, il responsabile dell'impianto si impegna, entro breve tempo, a provvedere alla loro risoluzione dandone notizia all'operatore incaricato. Si raccomanda un intervento manutentivo entro il
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Data intervento manutentivo raccomandata (gg/mm/aaaa) (*)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="100" runat="server" CssClass="txtClass_o" ValidationGroup="vgRapportoDiControllo" ID="txtDataManutenzioneConsigliata" Text='<%# Eval("DataManutenzioneConsigliata", "{0:dd/MM/yyyy}") %>' />
                                <asp:RequiredFieldValidator ID="rfvtxtDataManutenzioneConsigliata" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data intervento manutentivo raccomandata: campo obbligatorio"
                                    ControlToValidate="txtDataManutenzioneConsigliata">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revtxtDataManutenzioneConsigliata" ValidationGroup="vgRapportoDiControllo" ControlToValidate="txtDataManutenzioneConsigliata" Display="Dynamic" ForeColor="Red" ErrorMessage="Data intervento manutentivo raccomandata: inserire la data nel formato gg/mm/aaaa"
                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Data del presente controllo (gg/mm/aaaa) (*)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:TextBox Width="100" ID="txtDataControllo" CssClass="txtClass_o" Text='<%# Bind("DataControllo", "{0:dd/MM/yyyy}") %>' runat="server" ValidationGroup="vgRapportodiControllo" />
                                <asp:RequiredFieldValidator ID="rfvtxtDataControllo" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data del presente controllo: campo obbligatorio"
                                    ControlToValidate="txtDataControllo">&nbsp;*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator
                                    ID="revtxtDataControllo" ValidationGroup="vgRapportoDiControllo" ControlToValidate="txtDataControllo" Display="Dynamic" ForeColor="Red" ErrorMessage="Data del presente controllo: inserire la data nel formato gg/mm/aaaa"
                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                                <asp:Label runat="server" Text="Ora di arrivo / partenza presso l'impianto (*)" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Table ID="tblOra" Width="50%" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell Width="25%">
                                            <dx:ASPxTimeEdit ID="txtOraArrivo" runat="server" ValidationGroup="vgRapportodiControllo"
                                                DateTime='<%# DateTime.Parse(Eval("OraArrivo", "{0:HH:mm}")) %>'
                                                AllowNull="true" Width="100" CssClass="txtClass_o">
                                                <ClearButton DisplayMode="OnHover" />
                                                <ValidationSettings ErrorDisplayMode="None" />
                                            </dx:ASPxTimeEdit>
                                            <asp:RequiredFieldValidator ID="rfvtxtOraArrivo" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Ora di arrivo: campo obbligatorio"
                                                ControlToValidate="txtOraArrivo">&nbsp;*</asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                        <asp:TableCell Width="25%">
                                            <dx:ASPxTimeEdit ID="txtOraPartenza" runat="server" ValidationGroup="vgRapportodiControllo"
                                                DateTime='<%# DateTime.Parse(Eval("OraPartenza", "{0:HH:mm}")) %>'
                                                AllowNull="true" Width="100" CssClass="txtClass_o">
                                                <ClearButton DisplayMode="OnHover" />
                                                <ValidationSettings ErrorDisplayMode="None" />
                                            </dx:ASPxTimeEdit>
                                            <asp:RequiredFieldValidator ID="rfvtxtOraPartenza" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Ora di partenza: campo obbligatorio"
                                                ControlToValidate="txtOraPartenza">&nbsp;*</asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="35%" CssClass="riempimento2">
                            <asp:Label runat="server" Text="Tecnico che ha effettuato l'intervento" />
                            </asp:TableCell>
                            <asp:TableCell Width="65%" CssClass="riempimento">
                                <asp:Panel runat="server" ID="pnlAddettoView" Visible='<%# Eval("IDSoggettoDerived") != null  %>'>
                                    <asp:Label ID="lblNomeTecnico" Text='<%# Bind("Nome") %>' runat="server" />&nbsp;<asp:Label ID="lblCognomeTecnico" Text='<%# Bind("Cognome") %>' runat="server" />
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlAddettoSet" Visible='<%# Eval("IDSoggettoDerived") == null  %>'>
                                    <dx:ASPxComboBox runat="server" ID="cmbAddetti" Theme="Default" TabIndex="1"
                                        TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32" 
                                        AutoPostBack="false" 
                                        Width="350px" 
                                        DropDownWidth="350px">
                                        <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                                    </dx:ASPxComboBox>
                                    <asp:RequiredFieldValidator
                                        ID="rfvASPxComboBox2"
                                        ValidationGroup="vgRapportoDiControllo"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        runat="server"
                                        InitialValue=""
                                        ErrorMessage="Operatore/Addetto: campo obbligatorio"
                                        ControlToValidate="cmbAddetti">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:Panel>                              
                            </asp:TableCell>
                        </asp:TableRow>

                        <si:SiTableRow Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>'>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento2">
                                <dx:ASPxButton runat="server" ID="btnSavePartial6" Text="Salva dati rapporto di controllo"
                                    CausesValidation="false" ClientInstanceName="btnSavePartial6" CommandName="Update">
                                    <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ if(ASPxClientEdit.ValidateGroup(null)) { 
                                        window.setTimeout(function(){ 
                                        LoadingPanel6.Show(); 
                                        s.SetText('Attendere, salvataggio in corso...'); 
                                        s.SetEnabled(false); },1) } }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel5" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel6">
                                </dx:ASPxLoadingPanel>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow ID="rowSelezioneBollini" Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 && Eval("guidInteroImpianto") == null %>'>
                            <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                                Bollini Calore Pulito necessari per questo Rapporto di controllo 
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                                <asp:Label runat="server" ID="lblImportoBolliniRichiesto" Font-Bold="True" />
                                <br />
                                <uc1:UCBolliniSelector runat="server" ID="UCBolliniSelector" IDSoggettoDerived='<%# (int?)Eval("IDSoggettoDerived") %>' IDSoggetto='<%# (int)Eval("IDSoggetto") %>'
                                    OnSelezioneterminata="UCBolliniSelector_Selezioneterminata" />
                                <asp:ValidationSummary ID="validationSummaryBollini" ValidationGroup="ValidationGroupBollini" runat="server" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server" ColumnSpan="2" CssClass="riempimento5">
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:Table ID="tblComandiRapportoDiControllo" runat="server" Width="100%">
                        <si:SiTableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                                <asp:Button ID="btnSaveRapportoDiControllo" Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>' OnClick="btnSaveRapportoDiControllo_OnClick" runat="server" CssClass="buttonClass" Width="260"
                                    ValidationGroup="vgRapportoDiControllo" CausesValidation="true" Text="SALVA DATI RAPPORTO DI CONTROLLO" />&nbsp;
                                <asp:Button ID="btnSaveRapportoDiControlloDefinitivo" Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>' OnClick="btnSaveRapportoDiControlloDefinitivo_Click" runat="server" CssClass="buttonClass" Width="400"
                                    ValidationGroup="vgRapportoDiControllo" CausesValidation="true" Text="SALVA E CREA RAPPORTO DI CONTROLLO IN ATTESA DI FIRMA" OnClientClick="return SalvaDefinitivo();" />
                                <br />
                                <br />
                                <asp:Button runat="server" ID="btnViewRapportoControllo" Visible='<%# (int)Eval("IDStatoRapportoDiControllo") == 1 %>' CausesValidation="false" CssClass="buttonClass" Text="VISUALIZZA PDF RAPPORTO DI CONTROLLO TECNICO" Width="350" />
                                <asp:ValidationSummary ID="validationSummaryGlobale" ValidationGroup="ValidationGroupGlobale" runat="server" />
                                <asp:ValidationSummary ID="vsSaveRapportoDiControllo" ValidationGroup="vgRapportoDiControllo" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow>
                            <asp:TableCell>
                                <asp:CustomValidator ID="cvRaccomandazioniPrescrizioni" runat="server" EnableClientScript="true" Display="Dynamic"
                                    OnServerValidate="customValidatorRaccomandazioniPrescrizioni" ValidationGroup="vgRapportoDiControllo" />
                            </asp:TableCell>
                        </si:SiTableRow>

                        <si:SiTableRow runat="server" Visible='<%# ((int)Eval("IDStatoRapportoDiControllo") == 2 || (int)Eval("IDStatoRapportoDiControllo") == 3) || Eval("guidInteroImpianto") != null  %>'>
                            <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                                Bollini associati a questo rapporto di controllo
                            </asp:TableCell>
                            <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                                <uc1:UCBolliniView runat="server" ID="UCSBolliniView" 
                                    IDRapportoControlloTecnico='<%# (long)Eval("IDRapportoControlloTecnico") %>'
                                 />
                            </asp:TableCell>
                        </si:SiTableRow>
                    </asp:Table>
                </EditItemTemplate>
            </asp:FormView>
            <asp:Table Width="900" ID="tblComandiRctDefinitivo" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <br />
                        <asp:Button ID="btnAnnullaRapportoDiControlloInAttesaFirma" Visible="false" OnClick="btnAnnullaRapportoDiControlloInAttesaFirma_Click" runat="server" CssClass="buttonClass" Width="350"
                            ValidationGroup="vgRapportoDiControllo" CausesValidation="true" Text="RIPRISTINA RAPPORTO DI CONTROLLO IN BOZZA" OnClientClick="return AnnullaInAttesaDiFirma();" />&nbsp;
                        <asp:Button ID="btnViewDefinitivoRct" runat="server" Visible="false" CssClass="buttonClass" Width="350" CausesValidation="false"
                            Text="VISUALIZZA PDF RAPPORTO DI CONTROLLO TECNICO" />
                        &nbsp;
                        <asp:Button ID="btnAnnullaRapportoControlloTecnico" runat="server" Visible="false" CssClass="buttonClass" Width="350"
                            OnClick="btnAnnullaRapportoControlloTecnico_Click" CausesValidation="false" Text="ANNULLA RAPPORTO DI CONTROLLO TECNICO" OnClientClick="return AnnullaRapporto();" />&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ef:EntityDataSource ID="dsTipologiaContabilizzazione" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="SYS_TipologiaContabilizzazione"
        Select="it.[IDTipologiaContabilizzazione], it.[TipologiaContabilizzazione]"
        Where="it.[fAttivo] = true">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologiaDistribuzione" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="SYS_TipologiaSistemaDistribuzione"
        Select="it.[IDTipologiaSistemaDistribuzione], it.[TipologiaSistemaDistribuzione]"
        Where="it.[fAttivo] = true">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologieFluidotermoVettore" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="SYS_TipologiaFluidoTermoVettore"
        Select="it.[IDTipologiaFluidoTermoVettore], it.[TipologiaFluidoTermoVettore]"
        Where="it.[fAttivo] =true AND it.[IDTipologiaFluidoTermoVettore] IN {1,2,3,4}">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologiaControllo" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="False"
        EntitySetName="SYS_TipologiaControllo"
        Select="it.[IDTipologiaControllo], it.[TipologiaControllo]"
        Where="it.fAttivo=true">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologieFluidotermoVettoreUscita" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="SYS_TipologiaFluidoTermoVettore"
        Select="it.[IDTipologiaFluidoTermoVettore], it.[TipologiaFluidoTermoVettore]"
        Where="it.[fAttivo] =true AND it.[IDTipologiaFluidoTermoVettore] IN {1,2,4}">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologiaGeneratoriTermici" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="False"
        EntitySetName="SYS_TipologiaGeneratoriTermici"
        Select="it.[IdTipologiaGeneratoriTermici], it.[Descrizione]"
        Where="it.fAttivo=true">
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsTipologieMacchinefrigorifere" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel" EnableFlattening="false"
        DefaultContainerName="CriterDataModel"
        EntitySetName="SYS_TipologiaMacchineFrigorifere"
        Select="it.[IDTipologiaMacchineFrigorifere], it.[TipologiaMacchineFrigorifere]"
        Where="it.[fAttivo] = true">
    </ef:EntityDataSource>
</asp:Content>
