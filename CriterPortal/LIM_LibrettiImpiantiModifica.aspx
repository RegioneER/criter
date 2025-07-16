<%@ Page Title="Criter - Libretti di impianti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_LibrettiImpiantiModifica.aspx.cs" Inherits="LIM_LibrettiImpiantiModifica" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoCombustibile.ascx" TagName="ConsumoCombustibile" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoEnergiaElettrica.ascx" TagName="ConsumoEnergiaElettrica" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoAcqua.ascx" TagName="ConsumoAcqua" TagPrefix="criter" %>
<%@ Register Src="~/WebUserControls/LibrettiImpianto/UC_ConsumoProdottiChimici.ascx" TagName="ConsumoProdottiChimici" TagPrefix="criter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function SalvaDefinitivo() {
            if (Page_ClientValidate())
                return (confirm('Confermando tale operazione verranno modificati i dati del libretto impianto. Confermi?'));
        }
        
        function RevisionaLibretto() {
            //if (Page_ClientValidate())
            return (confirm('Confermando tale operazione lo stato del libretto verrà reso in revisione e sarà possibile modificare nuovamente i dati. Confermi?'));
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
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggettoDerived" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPersona" Text="Operatore/Addetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblCodiceTargatura" runat="server" ForeColor="Green" Font-Bold="true" />
                            <br />
                            <asp:Image runat="server" ID="imgBarcode" Width="70px" Height="70px" CssClass="imgBarcode" ToolTip="Barcode Codice Targatura" AlternateText="Barcode Codice Targatura" />
                            <asp:Label runat="server" ID="lblIDTargaturaImpianto" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowStatoLibrettoImpianto" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloStatoLibrettoImpianto" Text="Stato libretto impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label ID="lblIDStatoLibrettoImpianto" runat="server" Visible="false" />
                        <asp:Label ID="lblStatoLibrettoImpianto" runat="server" />
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

            <asp:Table Width="900" ID="tblInfoLibrettoImpianto" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
							<h2>1.0 SCHEDA IDENTIFICATIVA DELL’IMPIANTO</h2>
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
                        <asp:Label ID="lblIDCodiceCatastale" runat="server" Visible="false" />
                        <asp:Label ID="lblCodiceCatastale" Font-Bold="true" runat="server" />
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
                            <br /><br />
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
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceFiscaleResponsabile" AssociatedControlID="lblCodiceFiscaleResponsabile" Text="Codice fiscale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCodiceFiscaleResponsabile" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowRagioneSocialeResponsabile" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloRagioneSocialeResponsabile" Text="Ragione sociale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtRagioneSocialeResponsabile" Width="200" MaxLength="100" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
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
                        <asp:TextBox runat="server" ID="txtPartitaIvaResponsabile" Width="200" MaxLength="20" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
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
                            CallbackPageSize="30"
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
                        <asp:Label runat="server" ID="lblTitoloTerzoResponsabile" Text="E' stato nominato un terzo responsabile?" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblfTerzoResponsabile" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="radiobuttonlistClass">
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
            </asp:Table>
            
            <asp:Table Width="900" ID="tblConsumi" runat="server" Visible="false">
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

            <asp:Table Width="900" ID="tblComandiLibrettoImpianto" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto" runat="server" CssClass="buttonClass" Width="300"
                            OnClick="LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto_Click" CausesValidation="false"  Text="ANNULLA" />&nbsp;
						<asp:Button ID="LIM_LibrettiImpianti_btnCloseLibrettoImpianto" runat="server" CssClass="buttonClass" Width="300"
                            OnClick="LIM_LibrettiImpianti_btnCloseLibrettoImpianto_Click" ValidationGroup="vgLibrettoImpianto" Text="SALVA E INVIA LIBRETTO IMPIANTO" CausesValidation="true" OnClientClick="return SalvaDefinitivo();" />                       
                        												                        
                        <asp:ValidationSummary ID="vsSaveLibrettoImpianto" ValidationGroup="vgLibrettoImpianto" runat="server" ShowMessageBox="True"
                            ShowSummary="True" HeaderText="Attenzione, ricontrollare i seguenti campi:" />

                        <asp:CustomValidator ID="cvPodPdr" runat="server" EnableClientScript="true" Display="Dynamic"
                            OnServerValidate="ControllaPodPdr"
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