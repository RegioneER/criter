<%@ Page Title="Criter - Gestione anagrafica soggetti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ANAG_Anagrafica.aspx.cs" Inherits="ANAG_Anagrafica" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoAzienda" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneAzienda" /></h2>
                        <asp:Label runat="server" ID="lblfIscrizione" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowAccreditamentoImpresa" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAccreditamentoAzienda" AssociatedControlID="ImgAccreditamentoAzienda" Text="Stato Accreditamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:ImageButton runat="server" ID="ImgAccreditamentoAzienda" BorderStyle="None" AlternateText="Visualizza accreditamento Impresa" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceSoggetto" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceSoggetto" Text="Codice soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label ID="lblCodiceSoggetto" runat="server" ForeColor="Green" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblAzienda" AssociatedControlID="txtAzienda" Text="Ragione sociale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtAzienda" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtAzienda" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Ragione sociale: campo obbligatorio"
                            ControlToValidate="txtAzienda">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFormaGiuridica" AssociatedControlID="ddlFormaGiuridica" Text="Forma giuridica (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFormaGiuridica" Width="215" TabIndex="1" ValidationGroup="vgAnagrafica" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFormaGiuridica" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Forma giuridica: campo obbligatorio"
                            ControlToValidate="ddlFormaGiuridica">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblRuoliSoggetto" AssociatedControlID="cblRuoliSoggetto" Text="Ruoli" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblRuoliSoggetto" runat="server" TabIndex="1" Width="670" CssClass="checkboxlistClass" RepeatColumns="3" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoSedeLegale" AssociatedControlID="ddlPaeseSedeLegale" Text="Stato sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseSedeLegale" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseSedeLegale_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseSedeLegale" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese sede legale: campo obbligatorio"
                            ControlToValidate="ddlPaeseSedeLegale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoSedeLegale" AssociatedControlID="txtIndirizzoSedeLegale" Text="Indirizzo/numero civico sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoSedeLegale" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
							<asp:TextBox runat="server" ID="txtNumeroCivicoSedeLegale" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoSedeLegale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo sede legale: campo obbligatorio"
                            ControlToValidate="txtIndirizzoSedeLegale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoSedeLegale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico sede legale: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoSedeLegale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapSedeLegale" AssociatedControlID="txtCapSedeLegale" Text="Cap sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapSedeLegale" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapSedeLegale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: campo obbligatorio"
                            ControlToValidate="txtCapSedeLegale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapSedeLegale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: non valido" ControlToValidate="txtCapSedeLegale"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaSedeLegale" AssociatedControlID="txtCittaSedeLegale" Text="Città sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaSedeLegale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaSedeLegale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città sede legale: campo obbligatorio"
                            ControlToValidate="txtCittaSedeLegale">&nbsp;*
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaSedeLegale" AssociatedControlID="ddlProvinciaSedeLegale" Text="Provincia sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaSedeLegale" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaSedeLegale" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia sede legale: campo obbligatorio"
                            ControlToValidate="ddlProvinciaSedeLegale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblPartitaIva" AssociatedControlID="txtPartitaIva" Text="Partita Iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIva" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvPartitaIva" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIva">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleAzienda" AssociatedControlID="txtCodiceFiscaleAzienda" Text="Codice fiscale azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleAzienda" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleAzienda" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Codice fiscale azienda: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleAzienda">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelefono" AssociatedControlID="txtTelefono" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefono" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefono" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefono">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTelefono" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefono" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFax" AssociatedControlID="txtFax" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFax" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <%--<asp:RequiredFieldValidator
								ID="rfvtxtFax" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Fax: campo obbligatorio"
								ControlToValidate="txtFax">&nbsp;*</asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revFax" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFax" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmail" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmail" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmail">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailPec" AssociatedControlID="txtEmailPec" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPec" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPec" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email pec: campo obbligatorio"
                            ControlToValidate="txtEmailPec">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailPec" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPec" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblSitoWeb" AssociatedControlID="txtSitoWeb" Text="Sito web" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSitoWeb" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNumeroAlboImprese" AssociatedControlID="txtNumeroAlboImprese" Text="Numero iscrizione registro imprese (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroAlboImprese" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroAlboImprese" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero iscrizione al registro imprese: campo obbligatorio"
                            ControlToValidate="txtNumeroAlboImprese">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaAlboImprese" AssociatedControlID="ddlProvinciaAlboImprese" Text="Provincia iscrizione al registro imprese (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaAlboImprese" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaAlboImprese" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia iscrizione al registro imprese: campo obbligatorio"
                            ControlToValidate="ddlProvinciaAlboImprese">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>DATI LEGALE RAPPRESENTANTE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowTitoloLegaleRappresentante" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloLegaleRappresentante" AssociatedControlID="ddlTitoloLegaleRappresentante" Text="Titolo legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTitoloLegaleRappresentante" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlTitoloLegaleRappresentante" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Titolo legale rappresentante: campo obbligatorio"
                            ControlToValidate="ddlTitoloLegaleRappresentante">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFunzioneLegaleRappresentante" AssociatedControlID="ddlFunzioneLegaleRappresentante" Text="Funzione legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFunzioneLegaleRappresentante" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFunzioneLegaleRappresentante" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Funzione legale rappresentante: campo obbligatorio"
                            ControlToValidate="ddlFunzioneLegaleRappresentante">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNome" AssociatedControlID="txtNome" Text="Nome legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNome" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNome" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Nome legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtNome">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCognome" AssociatedControlID="txtCognome" Text="Cognome legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognome" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognome" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cognome legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtCognome">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPaeseNascita" AssociatedControlID="ddlPaeseNascita" Text="Stato di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseNascita" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseNascita_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseNascita" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese di nascita: campo obbligatorio"
                            ControlToValidate="ddlPaeseNascita">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDataNascita" AssociatedControlID="txtDataNascita" Text="Data di nascita (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataNascita" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataNascita" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Data di nascita: campo obbligatorio"
                            ControlToValidate="txtDataNascita">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="rfvDataNascita" ValidationGroup="vgAnagrafica" ControlToValidate="txtDataNascita" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di nascita: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaNascita" AssociatedControlID="txtCittaNascita" Text="Città di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaNascita" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaNascita" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città di nascita: campo obbligatorio"
                            ControlToValidate="txtCittaNascita">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaNascita" AssociatedControlID="ddlProvinciaNascita" Text="Provincia di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaNascita" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaNascita" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di nascita del legale rappresentante: campo obbligatorio"
                            ControlToValidate="ddlProvinciaNascita">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscale" AssociatedControlID="txtCodiceFiscale" Text="Codice fiscale legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscale" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscale" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Codice fiscale legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoResidenza" AssociatedControlID="ddlPaeseResidenza" Text="Stato residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseResidenza" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseResidenza_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseResidenza" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese residenza: campo obbligatorio"
                            ControlToValidate="ddlPaeseResidenza">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoResidenza" AssociatedControlID="txtIndirizzoResidenza" Text="Indirizzo/numero civico residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoResidenza" placeholder="" ClientIDMode="Static" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
                            <asp:TextBox runat="server" ID="txtNumeroCivicoResidenza" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" ToolTip="Numero Civico Residenza" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoResidenza" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo residenza: campo obbligatorio"
                            ControlToValidate="txtIndirizzoResidenza">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoResidenza" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico residenza: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoResidenza">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapResidenza" AssociatedControlID="txtCapResidenza" Text="Cap residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapResidenza" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapResidenza" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap residenza: campo obbligatorio"
                            ControlToValidate="txtCapResidenza">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapResidenza" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap residenza: non valido" ControlToValidate="txtCapResidenza"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaResidenza" AssociatedControlID="txtCittaResidenza" Text="Città di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaResidenza" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaResidenza" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città di residenza: campo obbligatorio"
                            ControlToValidate="txtCittaResidenza">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaResidenza" AssociatedControlID="ddlProvinciaResidenza" Text="Provincia di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaResidenza" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaResidenza" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di residenza: campo obbligatorio"
                            ControlToValidate="ddlProvinciaResidenza">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>ABILITAZIONI IMPRESA</h2>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIscrizioneRegistroGasFluorurati" AssociatedControlID="chkIscrizioneRegistroGasFluorurati" Text="Iscritto al Registro dei Gas Fluorurati?" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBox ID="chkIscrizioneRegistroGasFluorurati" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="chkIscrizioneRegistroGasFluorurati_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowIscrizioneRegistroGasFluorurati" runat="server" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNumeroIscrizioneRegistroGasFluorurati" AssociatedControlID="txtNumeroIscrizioneRegistroGasFluorurati" Text="Numero iscrizione al Registro dei Gas Fluorurati" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroIscrizioneRegistroGasFluorurati" Width="100" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroIscrizioneRegistroGasFluorurati" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero di iscrizione al registro dei gas fluorurati: campo obbligatorio"
                            ControlToValidate="txtNumeroIscrizioneRegistroGasFluorurati">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblAbilitazioniSoggetti" AssociatedControlID="cblAbilitazioniSoggetto" Text="Abilitazioni" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblAbilitazioniSoggetto" runat="server" TabIndex="1" Width="670" CssClass="checkboxlistClass" RepeatColumns="1" RepeatDirection="Vertical" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblClassificazioniImpianti" AssociatedControlID="cblClassificazioniImpianto" Text="Categorie DM 37/2008" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblClassificazioniImpianto" runat="server" TabIndex="1" Width="670" CssClass="checkboxlistClass" RepeatColumns="1" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinceCompetenza" AssociatedControlID="cblProvinceCompetenza" Text="Aree provinciali di operatività (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblProvinceCompetenza" runat="server" TabIndex="1" Width="670" CssClass="checkboxlistClass" RepeatColumns="3" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <br />
                        <asp:Label runat="server" ID="lblConsensoElenco" AssociatedControlID="rblPubblicazioneAlbo" Text="Dà il consenso alla pubblicazione del proprio nominativo sull'elenco preposto" />
                        <br />
                        <asp:RadioButtonList ID="rblPubblicazioneAlbo" TabIndex="1" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Selected="True" Value="1" />
                            <asp:ListItem Text="No" Value="0" />
                        </asp:RadioButtonList>
                        <br />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="pnlFilePrivacy" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblLinkPrivacy" AssociatedControlID="imgExportPrivacyDocument" Text="Documento privacy firmato (.P7M)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:HyperLink ID="imgExportPrivacyDocument" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica il documento " Target="_blank" />;
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:TextBox ID="txtPrivacy" runat="server" Height="100" TabIndex="1" Rows="100" ReadOnly="true" TextMode="MultiLine">
Il possesso dei requisiti di cui al regolamento regionale n. 1 del 3 aprile 2017, necessari per la registrazione nell'Elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER, è dichiarato dal legale rappresentante dell’azienda ai sensi dell’art. 46 del DPR 445/2000 sotto propria responsabilità, anche agli effetti delle sanzioni penali previste dall’art. 76 del medesimo DPR 445/2000.

INFORMATIVA AL TRATTAMENTO DEI DATI PERSONALI DEGLI OPERATORI AI SENSI DELL’ART. 13 REG.UE 2016/679

Premessa
La Regione Emilia-Romagna ha conferito alla società “in house” ART-ER S. cons. p. a. le funzioni di Organismo Regionale di Accreditamento ed Ispezione (nel seguito “ART-ER“ o “Organismo“) di cui all’art. 25-quater della L.R. 26/2004, con il compito di gestire ed assicurare il pieno ed efficace funzionamento del CRITER, ovvero del catasto e del sistema di controllo degli impianti termici in conformità alle disposizioni del Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”.
Nell’ambito delle funzioni conferite dalla Regione Emilia-Romagna ad ART-ER è ricompreso il ruolo di Responsabile del Trattamento dei dati ai sensi e per gli effetti dell’art.28 Reg. Ue 2016/679. In tale qualità, pertanto, ART-ER tratta i dati personali registrati tramite l’applicativo informatico CRITER – Catasto Regionale degli Impianti Termici, accessibile per via telematica all’indirizzo http://energia.regione.emilia-romagna.it/criter/catasto-impianti. La società “in house” ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Titolare del trattamento dei dati
Il Titolare del trattamento dei dati personali acquisiti tramite CRITER è la Giunta della Regione Emilia-Romagna, con sede legale in Viale Aldo Moro, 52 – 40127 Bologna. Il Titolare del Trattamento con Delibera n. 2169 del 20/12/2017 ha designato il Responsabile della protezione dei dati (Data Protection Officer) contattabile ai seguenti indirizzi mail dpo@regione.emilia-romagna.it, dpo@postacert.regione.emilia-romagna.it.
Responsabile del trattamento
Il Titolare del Trattamento, ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679, previa verifica della capacità di garantire il rispetto delle vigenti disposizioni in materia di protezione dei dati personali, ivi compreso il profilo della sicurezza dei dati, ha designato la società “in house” ART-ER - Organismo Regionale di Accreditamento ed Ispezione, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, quale Responsabile del trattamento finalizzato alla gestione del sistema di controllo degli impianti termici. ART-ER ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Base giuridica del trattamento
Il trattamento dei dati personali forniti dagli Utenti, ai sensi e per gli effetti dell’art.6 c.1 lett. e) Reg. UE 2016/679, è posto in essere per l’esecuzione di un compito di interesse pubblico o connesso all’esercizio dei pubblici poteri di cui è investito il Titolare del Trattamento (Legge Regionale 23 dicembre 2004, n. 26 “Disciplina della programmazione energetica territoriale ed altre disposizioni in materia di energia”; Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”), pertanto, il relativo trattamento non necessita del consenso dell’interessato.


Tipologia di dati trattati e finalità del trattamento

Dati acquisiti durante la navigazione
I sistemi informatici e le procedure software preposte al funzionamento del presente Sito acquisiscono, nel corso del loro normale esercizio, alcuni dati personali la cui trasmissione è implicita nell’uso dei protocolli di comunicazione di Internet. Si tratta di informazioni che non sono raccolte per essere associate a interessi identificati, ma che per loro stessa natura potrebbero, attraverso elaborazioni ed associazioni con dati detenuti da terzi, permettere di identificare gli utenti.
In questa categoria di dati rientrano gli indirizzi IP o i nomi a dominio dei computer utilizzati dagli utenti che si connettono al Siti, gli indirizzi in notazione URI (Uniform Resource Identifier) delle risorse richieste, l’orario della richiesta, il metodo utilizzato nel sottoporre la richiesta al server, la dimensione del file ottenuto in risposta, il codice numerico indicante lo stato della risposta data dal server (buon fine, errore, ecc.) ed altri parametri relativi al sistema operativo e all’ambiente informatico dell’utente.
Questi dati vengono normalmente salvati nei cosiddetti “logfile” e utilizzati al solo fine di ricavare informazioni statistiche anonime sull’uso dei siti e per controllarne il corretto funzionamento. I dati potrebbero essere utilizzati per l’accertamento di responsabilità in caso di reati informatici ai danni del Sito. Fatta salva questa eventualità, i dati vengono conservati per un tempo non superiore a quello necessario agli scopi per i quali sono raccolti e trattati.

Dati forniti dall’Utente / Operatore in fase di “iscrizione al sistema CRITER”
La registrazione effettuata dall’Utente al fine di poter operare le funzioni previste dalla normativa vigente nell’ambito del sistema CRITER avviene tramite gli appositi moduli web (form) presenti sul Sito, all’interno delle pagine e sezioni predisposte per l’iscrizione https://criter.regione.emilia-romagna.it/IscrizioneCriter.aspx . La registrazione comporta l’acquisizione di tutti i dati riportati nei campi ivi previsti, compilati dai richiedenti, ivi incluse le credenziali che consentono all’Utente / Operatore di accedere a contenuti riservati. Il trattamento dei dati personali forniti dagli Utenti / Operatori in fase di iscrizione a CRITER è posto in essere per le seguenti finalità: 
-	operatore “impresa di manutenzione”: registrazione nell’elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER;
-	operatore “ispettore CRITER”: registrazione ed accreditamento degli ispettori qualificati CRITER cui vengono affidate le funzioni di ispezione sugli impianti termici;
-	operatore “distributore di energia”: registrazione ed accreditamento dei distributori di energia per gli impianti termici degli edifici della Regione Emilia-Romagna per inserimento dati relativi alle utenze servite;
-	operatore “cittadino o amministratore condominiale responsabile di impianto”: registrazione del responsabile di impianto al fine di consentire la consultazione dei documenti relativi al proprio impianto;
-	operatore “ente locale”: registrazione ed accreditamento degli enti locali per la consultazione del catasto CRITER ai fini dell’espletamento delle funzioni di propria competenza.
Il conferimento dei dati personali richiesti nel form di registrazione è necessario ai fini del completamento dell’iter di registrazione/accreditamento. Il mancato od inesatto conferimento dei dati personali potrebbe determinare l’impossibilità di portare a termine la relativa procedura.

Dati inseriti dagli Utenti / Operatori in fase di Registrazione di documenti
Gli operatori sopra indicati, accreditati per l’esercizio delle relative funzioni, sono autorizzati al trattamento dei dati personali degli utenti finali del servizio. Tali dati, inseriti dagli operatori nell’applicativo CRITER, verranno trattati dal Titolare, previo rilascio di informativa al trattamento dei dati ai sensi e per gli effetti degli artt.13 e 14 Reg. UE 2016/679.


Modalità di trattamento
Il trattamento dei dati connessi all’utilizzo di CRITER ha luogo, informaticamente o tramite supporti cartacei, presso la sede del Responsabile del Trattamento ART-ER ed è effettuato da personale autorizzato e specificatamente formato. I servizi web connessi all’utilizzo dei dati trattati da ART-ER inclusi l’hosting degli applicativi informatici ed eventuali operazioni di gestione tecnica e manutenzione – sono forniti dal Titolare del Trattamento (Regione Emilia-Romagna). Il Titolare del Trattamento ed ART-ER, conformemente a quanto prescritto dall’art. 32 Reg. UE 2016/679 ed alle “Linee guida per la governance del sistema informatico regionale” hanno adottato misure tecniche ed organizzative adeguate allo scopo di garantire la sicurezza e la riservatezza dei dati personali trattati.


Destinatari dei dati forniti dagli Utenti
I dati personali forniti dagli Utenti potranno essere comunicati ad enti territoriali ed istituzionali abilitati per legge a richiederne l’acquisizione nonché, esclusivamente per le finalità indicate nella presente informativa, a società terze o professionisti, designati quali Responsabili del Trattamento ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679 o quali autorizzati al Trattamento, di cui il Titolare ed il Responsabile si avvalgono per la gestione del sistema di controllo degli impianti termici, tra i quali, a mero titolo esemplificativo e non esaustivo: imprese di manutenzione accreditate nell’ambito del sistema CRITER per la registrazione dei libretti di impianto e dei rapporti di controllo di efficienza energetica; Ispettori/agenti accertatori della Regione Emilia-Romagna ai fini della registrazione dei rapporti di ispezione e dei verbali di accertamento di violazione delle norme vigenti; Fornitori di servizi informatici incaricati dello sviluppo e della manutenzione ordinaria e straordinaria dell’applicativo informatico CRITER.
L’Utente può richiedere l’elenco dei Responsabili del Trattamento contattando il Titolare. Si precisa espressamente come i dati personali forniti dagli Utenti non siano oggetto di trasferimento verso Paesi terzi (Extra UE).


Periodo di conservazione
I dati forniti dagli Utenti sono conservati per un periodo di tempo non superiore a quello strettamente necessario per il perseguimento delle finalità sottese al trattamento medesimo, nel rispetto dei tempi di conservazione stabiliti dalla vigente normativa di legge. In tal senso, si precisa come i dati forniti siano soggetti a controlli periodici volti a verificarne la pertinenza, la non eccedenza e l’indispensabilità avuto riguardo al rapporto, alla prestazione od all'incarico in corso, da instaurare o cessato. I dati che, a seguito di verifica, risultino eccedenti o non pertinenti saranno cancellati, fatta salva l'eventuale conservazione, a norma di legge, dell'atto o del documento che li contiene.


Diritti degli interessati
Conformemente a quanto previsto dagli artt. 15 ss del Reg. UE 2016/679, l’interessato in ogni momento potrà esercitare, nei confronti del Titolare del Trattamento o di ART-ER - Organismo Regionale di Accreditamento ed Ispezione (Responsabile del Trattamento ex art.28 Reg. UE 2016/679), i propri diritti di: accedere ai dati personali ed ottenerne copia [diritto di accesso]; ottenere la rettifica dei dati, qualora risultino inesatti, ovvero l'integrazione dei dati, qualora risultino incompleti [diritto di rettifica];  ottenere la cancellazione dei dati nei casi previsti dall’art. 17 Reg. Ue 2016/679 [diritto di cancellazione]; ottenere la limitazione del trattamento nei casi previsti dall’art. 18 Reg. Ue 2016/679; [diritto di limitazione]; opporsi, in qualsiasi momento, per motivi connessi alla propria situazione particolare, al trattamento dei dati personali che riguardano l’interessato [diritto di opposizione].
L’apposita istanza può essere presentata contattando: 
-	il Titolare del Trattamento: Regione Emilia-Romagna, Ufficio per le relazioni con il pubblico (Urp), per iscritto o recandosi direttamente presso lo sportello Urp. L’Urp è aperto dal lunedì al venerdì dalle 9 alle 13 in Viale Aldo Moro 52, 40127 Bologna (Italia): telefono 800-662200, fax 051-527.5360, e-mail urp@regione.emilia-romagna.it. 
-	Il Responsabile del Trattamento: ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, mail criter@art-er.it, pec criter.art-er@pec.it.


Diritto di reclamo
L’interessato che ritenga che il trattamento dei dati personali effettuato attraverso l’applicativo CRITER sia posto in essere in violazione di quanto previsto dal Reg. UE 2016/679 ha diritto di proporre reclamo all’Autorità Garante per la protezione dei dati personali, ai sensi dell’art. 77 Reg. UE 2016/679, ovvero di adire le competenti sedi giudiziarie ai sensi dell’art. 79 Reg. UE 2016/679.
                        </asp:TextBox>
                        <br /><br />
                        <asp:Label runat="server" ID="lblPrivacy" AssociatedControlID="rblPrivacy" Text="Il dichiarante conferma di aver preso visione dell'informativa al trattamento dei dati personali" />
                        <br />
                        <asp:RadioButtonList ID="rblPrivacy" TabIndex="1" ValidationGroup="vgAnagrafica" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblPrivacy" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Consenso alla privacy: campo obbligatorio"
                            ControlToValidate="rblPrivacy">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
							   &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessageAzienda" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnullaAzienda" Visible="false" Text="ANNULLA" CausesValidation="false" TabIndex="1" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button ID="btnProcessAzienda" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessAzienda_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaAzienda" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvPartitaIva" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaPiva"
                            ErrorMessage="Partita Iva non valida<br/>" />
                        <asp:CustomValidator ID="cvCodiceFiscale" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaCodiceFiscale"
                            ErrorMessage="Codice fiscale non valido<br/>" />
                        <asp:CustomValidator ID="cvProvinceCompetenza" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaProvinceCompetenza"
                            ErrorMessage="Selezionare almeno un'area provinciale di operatività<br/>" />
                        <asp:CustomValidator ID="cvRuoliSoggetto" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaRuoliSoggetto"
                            ErrorMessage="Selezionare almeno un ruolo<br/>" />
                        <asp:CustomValidator ID="cvClassificazioniImpianto" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaClassificazioniImpianto"
                            ErrorMessage="Selezionare almeno una Categorie DM 37/2008<br/>" />
                        <asp:CustomValidator ID="cvEmailPresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPresente"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvEmailPecPresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPecPresente"
                            ErrorMessage="Email pec già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvPartitaIvaPresente" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaPartitaIvaPresente"
                            ErrorMessage="Partita Iva già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>

            <asp:Table Width="900" ID="tblInfoManutentore" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneOperatoreAddetto" /></h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceSoggettoManutentore" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceSoggettoManutentore" Text="Codice soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label ID="lblCodiceSoggettoManutentore" runat="server" ForeColor="Green" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
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
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox1" ValidationGroup="vgAnagrafica" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Azienda: campo obbligatorio"
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton runat="server" BorderStyle="None" ID="imgImportDatiAzienda" ImageUrl="~/images/buttons/pin.png" ImageAlign="AbsMiddle" OnClientClick="javascript:return confirm('Confermi di importare i dati dell\' azienda all\'operatore/addetto?');" OnClick="imgImportDatiAzienda_Click" ToolTip="Importazione dati dell'azienda" AlternateText="Importazione dati dell'azienda" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNomeManutentore" AssociatedControlID="txtNomeManutentore" Text="Nome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeManutentore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Nome: campo obbligatorio"
                            ControlToValidate="txtNomeManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCognomeManutentore" AssociatedControlID="txtCognomeManutentore" Text="Cognome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeManutentore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cognome: campo obbligatorio"
                            ControlToValidate="txtCognomeManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelefonoManutentore" AssociatedControlID="txtTelefonoManutentore" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoManutentore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoManutentore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTelefonoManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoManutentore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFaxManutentore" AssociatedControlID="txtFaxManutentore" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFaxManutentore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revFaxManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFaxManutentore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailManutentore" AssociatedControlID="txtEmailManutentore" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailManutentore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailManutentore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailManutentore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailPecManutentore" AssociatedControlID="txtEmailPecManutentore" Text="Email pec" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecManutentore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revEmailPecManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecManutentore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoManutentore" AssociatedControlID="ddlPaeseManutentore" Text="Stato (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseManutentore" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseManutentore_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseManutentore" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese: campo obbligatorio"
                            ControlToValidate="ddlPaeseManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoManutentore" AssociatedControlID="txtIndirizzoManutentore" Text="Indirizzo/numero civico (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoManutentore" placeholder="" ClientIDMode="Static" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
							<asp:TextBox runat="server" ID="txtNumeroCivicoManutentore" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo: campo obbligatorio"
                            ControlToValidate="txtIndirizzoManutentore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapManutentore" AssociatedControlID="txtCapManutentore" Text="Cap (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapManutentore" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap: campo obbligatorio"
                            ControlToValidate="txtCapManutentore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap: non valido" ControlToValidate="txtCapManutentore"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaManutentore" AssociatedControlID="txtCittaManutentore" Text="Città (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaManutentore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaManutentore" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città: campo obbligatorio"
                            ControlToValidate="txtCittaManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaManutentore" AssociatedControlID="ddlProvinciaManutentore" Text="Provincia (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaManutentore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaManutentore" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia: campo obbligatorio"
                            ControlToValidate="ddlProvinciaManutentore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleManutentore" AssociatedControlID="txtCodiceFiscaleManutentore" Text="Codice fiscale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleManutentore" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowAttivazioneUtenzaManutentore" runat="server" Visible="false">
					<asp:TableCell Width="225" CssClass="riempimento2">
						<asp:Label runat="server" ID="lblAttivazioneUtenzaManutentore" AssociatedControlID="chkAttivazioneUtenzaManutentore" Text="Attivazione credenziali di accesso autonomo al Criter" />
					</asp:TableCell>
					<asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
						<asp:CheckBox runat="server" ID="chkAttivazioneUtenzaManutentore" OnCheckedChanged="chkAttivazioneUtenzaManutentore_CheckedChanged" AutoPostBack="true" TabIndex="1" />
					</asp:TableCell>
				</asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
						    &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessage" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnullaManutentore" Visible="false" Text="ANNULLA" CausesValidation="false" TabIndex="1" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button ID="btnProcessManutentore" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessManutentore_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaManutentore" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvEmailPresenteManutentore" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPresenteManutentore"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                         <asp:CustomValidator ID="cvCodiceFiscalePresenteManutentore" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaCodiceFiscalePresenteManutentore"
                            ErrorMessage="Codice fiscale già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>

            <asp:Table Width="900" ID="tblInfoSoftwareHouse" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneSoftwareHouse" /></h2>
                    </asp:TableCell>
                </asp:TableRow>
               
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblSoftwareHouse" AssociatedControlID="txtSoftwareHouse" Text="Ragione sociale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSoftwareHouse" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Ragione sociale: campo obbligatorio"
                            ControlToValidate="txtSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlFormaGiuridicaSoftwareHouse" AssociatedControlID="ddlFormaGiuridicaSoftwareHouse" Text="Forma giuridica (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFormaGiuridicaSoftwareHouse" Width="215" TabIndex="1" ValidationGroup="vgAnagrafica" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFormaGiuridicaSoftwareHouse" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Forma giuridica: campo obbligatorio"
                            ControlToValidate="ddlFormaGiuridicaSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlPaeseSedeLegaleSoftwareHouse" AssociatedControlID="ddlPaeseSedeLegaleSoftwareHouse" Text="Stato sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseSedeLegaleSoftwareHouse" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseSedeLegale_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseSedeLegaleSoftwareHouse" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese sede legale: campo obbligatorio"
                            ControlToValidate="ddlPaeseSedeLegaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzoSoftwareHouse" AssociatedControlID="txtIndirizzoSedeLegaleSoftwareHouse" Text="Indirizzo/numero civico sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoSedeLegaleSoftwareHouse" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
						<asp:TextBox runat="server" ID="txtCivicoSedeLegaleSoftwareHouse" Width="30" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoSedeLegaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo sede legale: campo obbligatorio"
                            ControlToValidate="txtIndirizzoSedeLegaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCivicoSedeLegaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico sede legale: campo obbligatorio"
                            ControlToValidate="txtCivicoSedeLegaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCapSedeLegaleSoftwareHouse" AssociatedControlID="txtCapSedeLegaleSoftwareHouse" Text="Cap sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapSedeLegaleSoftwareHouse" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapSedeLegaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: campo obbligatorio"
                            ControlToValidate="txtCapSedeLegaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapSedeLegaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: non valido" 
                            ControlToValidate="txtCapSedeLegaleSoftwareHouse"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCittaSedeLegaleSoftwareHouse" AssociatedControlID="txtCittaSedeLegaleSoftwareHouse" Text="Città sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaSedeLegaleSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaSedeLegaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città sede legale: campo obbligatorio"
                            ControlToValidate="txtCittaSedeLegaleSoftwareHouse">&nbsp;*
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlProvinciaSedeLegaleSoftwareHouse" AssociatedControlID="ddlProvinciaSedeLegaleSoftwareHouse" Text="Provincia sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaSedeLegaleSoftwareHouse" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaSedeLegaleSoftwareHouse" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia sede legale: campo obbligatorio"
                            ControlToValidate="ddlProvinciaSedeLegaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtPartitaIvaSoftwareHouse" AssociatedControlID="txtPartitaIvaSoftwareHouse" Text="Partita Iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaSoftwareHouse" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtPartitaIvaSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleSoftwareHouse" AssociatedControlID="txtCodiceFiscaleSoftwareHouse" Text="Codice fiscale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleSoftwareHouse" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Codice fiscale: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtNome" AssociatedControlID="txtNomeSoftwareHouse" Text="Nome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Nome: campo obbligatorio"
                            ControlToValidate="txtNomeSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCognomeSoftwareHouse" AssociatedControlID="txtCognomeSoftwareHouse" Text="Cognome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cognome: campo obbligatorio"
                            ControlToValidate="txtCognomeSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtTelefonoSoftwareHouse" AssociatedControlID="txtTelefonoSoftwareHouse" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoSoftwareHouse" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtTelefonoSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoSoftwareHouse" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtFaxSoftwareHouse" AssociatedControlID="txtFaxSoftwareHouse" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFaxSoftwareHouse" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="rfvtxtFaxSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFaxSoftwareHouse" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtEmailSoftwareHouse" AssociatedControlID="txtEmailSoftwareHouse" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailSoftwareHouse">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtEmailSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailSoftwareHouse" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtEmailPecSoftwareHouse" AssociatedControlID="txtEmailPecSoftwareHouse" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revtxtEmailPecSoftwareHouse" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecSoftwareHouse" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtSitoWebSoftwareHouse" AssociatedControlID="txtSitoWebSoftwareHouse" Text="Sito web" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSitoWebSoftwareHouse" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
							   &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessageSoftwareHouse" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnullaSoftwareHouse" Visible="false" Text="ANNULLA" CausesValidation="false" TabIndex="1" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button ID="btnProcessSoftwareHouse" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessSoftwareHouse_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaSoftwareHouse" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPresente"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPecPresente"
                            ErrorMessage="Email pec già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaPartitaIvaPresente"
                            ErrorMessage="Partita Iva già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>

            <asp:Table Width="900" ID="tblInfoIspettore" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneIspettore" /></h2>
                        <asp:Label runat="server" ID="lblfIscrizioneIspettore" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceSoggettoIspettore" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceSoggettoIspettore" Text="Codice soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label ID="lblCodiceSoggettoIspettore" runat="server" ForeColor="Green" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAccreditamentoIspettore" AssociatedControlID="ImgAccreditamentoIspettore" Text="Accreditamento Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:ImageButton runat="server" ID="ImgAccreditamentoIspettore" BorderStyle="None" AlternateText="Visualizza accreditamento Ispettore" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="ddlTitoloIspettore" Text="Titolo (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlTitoloIspettore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlTitoloIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Titolo: campo obbligatorio"
                            ControlToValidate="ddlTitoloIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNomeIspettore" AssociatedControlID="txtNomeIspettore" Text="Nome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Nome: campo obbligatorio"
                            ControlToValidate="txtNomeIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCognomeIspettore" AssociatedControlID="txtCognomeIspettore" Text="Cognome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cognome: campo obbligatorio"
                            ControlToValidate="txtCognomeIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPaeseNascitaIspettore" AssociatedControlID="ddlPaeseNascitaIspettore" Text="Stato di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseNascitaIspettore" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseNascitaIspettore_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseNascitaIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese di nascita: campo obbligatorio"
                            ControlToValidate="ddlPaeseNascitaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDataNascitaIspettore" AssociatedControlID="txtDataNascitaIspettore" Text="Data di nascita (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataNascitaIspettore" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataNascitaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Data di nascita: campo obbligatorio"
                            ControlToValidate="txtDataNascitaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="rfvDataNascitaIspettore" ValidationGroup="vgAnagraficaIspettore" ControlToValidate="txtDataNascitaIspettore" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di nascita: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaNascitaIspettore" AssociatedControlID="txtCittaNascitaIspettore" Text="Città di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaNascitaIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaNascitaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Città di nascita: campo obbligatorio"
                            ControlToValidate="txtCittaNascitaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaNascitaIspettore" AssociatedControlID="ddlProvinciaNascitaIspettore" Text="Provincia di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaNascitaIspettore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaNascitaIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di nascita: campo obbligatorio"
                            ControlToValidate="ddlProvinciaNascitaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleIspettore" AssociatedControlID="txtCodiceFiscaleIspettore" Text="Codice fiscale  (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleIspettore" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Codice fiscale: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblPartitaIvaIspettore" AssociatedControlID="txtPartitaIvaIspettore" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaIspettore" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvPartitaIvaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelefonoIspettore" AssociatedControlID="txtTelefonoIspettore" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoIspettore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTelefonoIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoIspettore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCellulareIspettore" AssociatedControlID="txtCellulareIspettore" Text="Cellulare (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCellulareIspettore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                                ID="rfvtxtCellulareIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cellulare: campo obbligatorio"
                                ControlToValidate="txtCellulareIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtCellulareIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Cellulare: campo non valido"
                            ControlToValidate="txtCellulareIspettore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailIspettore" AssociatedControlID="txtEmailIspettore" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailIspettore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailPecIspettore" AssociatedControlID="txtEmailPecIspettore" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPecIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Email pec: campo obbligatorio"
                            ControlToValidate="txtEmailPecIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailPecIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecIspettore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoResidenzaIspettore" AssociatedControlID="ddlPaeseResidenzaIspettore" Text="Stato residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseResidenzaIspettore" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseResidenzaIspettore_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseResidenzaIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese residenza: campo obbligatorio"
                            ControlToValidate="ddlPaeseResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoResidenzaIspettore" AssociatedControlID="txtIndirizzoResidenzaIspettore" Text="Indirizzo/numero civico Residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoResidenzaIspettore" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />&nbsp;
                        <asp:TextBox runat="server" ID="txtNumeroCivicoResidenzaIspettore" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" ToolTip="Numero Civico residenza" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoResidenzaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Indirizzo residenza: campo obbligatorio"
                            ControlToValidate="txtIndirizzoResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoResidenzaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Numero civico residenza: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapResidenzaIspettore" AssociatedControlID="txtCapResidenzaIspettore" Text="Cap residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapResidenzaIspettore" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapResidenzaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap residenza: campo obbligatorio"
                            ControlToValidate="txtCapResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapResidenzaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap residenza: non valido" ControlToValidate="txtCapResidenzaIspettore"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaResidenzaIspettore" AssociatedControlID="txtCittaResidenzaIspettore" Text="Città di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaResidenzaIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaResidenzaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Città di residenza: campo obbligatorio"
                            ControlToValidate="txtCittaResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaResidenzaIspettore" AssociatedControlID="ddlProvinciaResidenzaIspettore" Text="Provincia di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaResidenzaIspettore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaResidenzaIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di residenza: campo obbligatorio"
                            ControlToValidate="ddlProvinciaResidenzaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDomicilio" AssociatedControlID="txtSitoWeb" Text="Domicilio diverso dalla residenza?" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBox ID="chkDomicilio" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="chkDomicilio_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowDomicilio00">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoDomicilioIspettore" AssociatedControlID="ddlPaeseDomicilioIspettore" Text="Stato domicilio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseDomicilioIspettore" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseDomicilioIspettore_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseDomicilioIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese domicilio: campo obbligatorio"
                            ControlToValidate="ddlPaeseDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDomicilio0">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoDomicilioIspettore" AssociatedControlID="txtIndirizzoDomicilioIspettore" Text="Indirizzo/numero civico Domicilio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoDomicilioIspettore" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />&nbsp;
                        <asp:TextBox runat="server" ID="txtNumeroCivicoDomicilioIspettore" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" ToolTip="Numero Civico Domicilio" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoDomicilioIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Indirizzo Domicilio: campo obbligatorio"
                            ControlToValidate="txtIndirizzoDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoDomicilioIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Numero civico Domicilio: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapDomicilioIspettore" AssociatedControlID="txtCapDomicilioIspettore" Text="Cap Domicilio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapDomicilioIspettore" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapDomicilioIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap Domicilio: campo obbligatorio"
                            ControlToValidate="txtCapDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapDomicilioIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap Domicilio: non valido" ControlToValidate="txtCapDomicilioIspettore"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDomicilio1">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaDomicilioIspettore" AssociatedControlID="txtCittaDomicilioIspettore" Text="Città di Domicilio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaDomicilioIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaDomicilioIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Città di Domicilio: campo obbligatorio"
                            ControlToValidate="txtCittaDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaDomicilioIspettore" AssociatedControlID="ddlProvinciaDomicilioIspettore" Text="Provincia di Domicilio (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaDomicilioIspettore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaDomicilioIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di Domicilio: campo obbligatorio"
                            ControlToValidate="ddlProvinciaDomicilioIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>RICONOSCIMENTO DELLA QUALIFICA</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipoQualificaIspettore" AutoPostBack="true" OnSelectedIndexChanged="rblTipoQualificaIspettore_SelectedIndexChanged" TabIndex="1" runat="server" 
                            RepeatDirection="Horizontal" CssClass="selectClass_o">
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>REQUISITI PER IL RICONOSCIMENTO DELLA QUALIFICA</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblTitoloRequisitiIspettore" Text="A tal fine dichiara di essere in possesso dei requisiti previsti per l’accreditamento, ed in particolare di:" />
                        <br />
                        <asp:Table runat="server" ID="tblRequisitiIspettore" Width="890">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                    <asp:CheckBox runat="server" ID="chkIdoneoIspezione" AutoPostBack="true" OnCheckedChanged="chkIdoneoIspezione_CheckedChanged" Text="essere idoneo all'esercizio delle attività di ispezione sugli impianti termici in quanto già operante in questa veste antecedentemente alla entrata in vigore del Regolamento" TextAlign="Right" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                    <asp:CheckBox runat="server" ID="chkNoCondanna" Text="a) non avere subito condanna né essere stato sottoposto a misure di prevenzione" TextAlign="Right" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowTitoloStudio" runat="server">
                                <asp:TableCell>
                                    b) essere in possesso del titolo di studio
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="ddlTipologiaTitoloStudio" AutoPostBack="true" OnSelectedIndexChanged="ddlTipologiaTitoloStudio_SelectedIndexChanged" TabIndex="1" Width="200" runat="server" CssClass="selectClass_o" />
                                    <asp:RequiredFieldValidator ID="rfvddlTipologiaTitoloStudio" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Tipologia titolo di studio: campo obbligatorio"
                                        ControlToValidate="ddlTipologiaTitoloStudio">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    conseguito presso
                                </asp:TableCell>
                                 <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtTitoloStudioConseguitoPresso" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtTitoloStudioConseguitoPresso" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Conseguimento titolo di studio: campo obbligatorio"
                                        ControlToValidate="txtTitoloStudioConseguitoPresso">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                 <asp:TableCell>
                                     data (gg/mm/aaaa)
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtDataTitoloStudio" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtDataTitoloStudio" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Data conseguimento titolo di studio: campo obbligatorio"
                                         ControlToValidate="txtDataTitoloStudio">&nbsp;*</asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator
                                         ID="rfvDataTitoloStudio" ValidationGroup="vgAnagraficaIspettore" ControlToValidate="txtDataTitoloStudio" Display="Dynamic" ForeColor="Red" ErrorMessage="Data conseguimento titolo di studio: inserire la data nel formato gg/mm/aaaa"
                                         runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                         EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            
                            <asp:TableRow ID="rowInserimentoAziendaOrdineCollegio" Visible="false" runat="server">
                                <asp:TableCell ColumnSpan="6">
                                    <asp:TableCell>
                                        <asp:RadioButtonList runat="server" ID="rblTipoInserimentoAziendaOrdineCollegio" 
                                            AutoPostBack="true" Width="890" CssClass="selectClass_o" OnSelectedIndexChanged="rblTipoInserimentoAziendaOrdineCollegio_SelectedIndexChanged" />
                                    </asp:TableCell>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowOrdineCollegio0" Visible="false" runat="server">
                                <asp:TableCell ColumnSpan="2">
                                    iscrizione al Collegio dei Periti Industriali Laureati della Provincia di
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="4">
                                    <asp:DropDownList ID="ddlOrdineCollegioProvincia" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                                    <asp:RequiredFieldValidator ID="rfvddlOrdineCollegioProvincia" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Ordine Collegio Provincia: campo obbligatorio"
                                        ControlToValidate="ddlOrdineCollegioProvincia">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowOrdineCollegio1" Visible="false" runat="server">
                                <asp:TableCell>
                                    sezione
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtOrdineCollegioSezione" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtOrdineCollegioSezione" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Sezione Ordine collegio: campo obbligatorio"
                                         ControlToValidate="txtOrdineCollegioSezione">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
				                <asp:TableCell>
                                    numero
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtOrdineCollegioNumero" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtOrdineCollegioNumero" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Numero Ordine collegio: campo obbligatorio"
                                         ControlToValidate="txtOrdineCollegioNumero">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    dal (gg/mm/aaaa)
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtOrdineCollegioData" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtOrdineCollegioData" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Data Ordine Collegio: campo obbligatorio"
                                         ControlToValidate="txtOrdineCollegioData">&nbsp;*</asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator
                                         ID="rectxtOrdineCollegioData" ValidationGroup="vgAnagraficaIspettore" ControlToValidate="txtOrdineCollegioData" Display="Dynamic" ForeColor="Red" ErrorMessage="Data Ordine Collegio: inserire la data nel formato gg/mm/aaaa"
                                         runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                         EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowInserimentoPressoAzienda" runat="server">
                                <asp:TableCell>
                                    inserimento presso l'azienda
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtStageAzienda" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtStageAzienda" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Nome inserimento azienda: campo obbligatorio"
                                        ControlToValidate="txtStageAzienda">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    dal (gg/mm/aaaa)
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtDataStageAziendaDa" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtDataStageAziendaDa" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Data inserimento in azienda dal: campo obbligatorio"
                                         ControlToValidate="txtDataStageAziendaDa">&nbsp;*</asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator
                                         ID="revtxtDataStageAziendaDa" ValidationGroup="vgAnagraficaIspettore" ControlToValidate="txtDataStageAziendaDa" Display="Dynamic" ForeColor="Red" ErrorMessage="Data inserimento in azienda dal: inserire la data nel formato gg/mm/aaaa"
                                         runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                         EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    al (gg/mm/aaaa)
                                </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:TextBox runat="server" ID="txtDataStageAziendaAl" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                     <asp:RequiredFieldValidator
                                         ID="rfvtxtDataStageAziendaAl" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Data inserimento in azienda al: campo obbligatorio"
                                         ControlToValidate="txtDataStageAziendaAl">&nbsp;*</asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator
                                         ID="rectxtDataStageAziendaAl" ValidationGroup="vgAnagraficaIspettore" ControlToValidate="txtDataStageAziendaAl" Display="Dynamic" ForeColor="Red" ErrorMessage="Data inserimento in azienda al: inserire la data nel formato gg/mm/aaaa"
                                         runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                         EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            
                            <asp:TableRow ID="rowCorsoFormazione" runat="server">
                                <asp:TableCell>
                                    c) aver frequentato il corso di formazione
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtCorsoFormazione" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtCorsoFormazione" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Corso di formazione: campo obbligatorio"
                                        ControlToValidate="txtCorsoFormazione">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                <asp:TableCell>
                                    organizzato da
                                </asp:TableCell>
                                 <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtCorsoFormazioneOrganizzatore" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtCorsoFormazioneOrganizzatore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Corso di formazione organizzato da: campo obbligatorio"
                                        ControlToValidate="txtCorsoFormazioneOrganizzatore">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                                 <asp:TableCell>

                                </asp:TableCell>
                                 <asp:TableCell>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="rowIscrizioneRegistroGasFluoruratiIspettore" runat="server" Visible="false">
                                <asp:TableCell ColumnSpan="4">
                                    <asp:CheckBox ID="chkIscrizioneRegistroGasFluoruratiIspettore" TabIndex="1" runat="server" Text="d) essere iscritto alla sezione del Registro Nazionale FGas di cui all'articolo 9, commi 1 e 5 del DPR 43/2012" TextAlign="Right" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    numero iscrizione
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox runat="server" ID="txtNumeroIscrizioneRegistroGasFluoruratiIspettore" Width="100" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvtxtNumeroIscrizioneRegistroGasFluoruratiIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Numero di iscrizione al registro dei gas fluorurati: campo obbligatorio"
                                        ControlToValidate="txtNumeroIscrizioneRegistroGasFluoruratiIspettore">&nbsp;*</asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                    <asp:CheckBox runat="server" ID="chkRequisitoOrganizzativo" Text="e) il possesso dei requisiti organizzativi e gestionali richiesti dal disciplinare" TextAlign="Right" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                    <asp:CheckBox runat="server" ID="chkDisponibilitaApparecchiature" Text="f) il possesso o la completa disponibilità delle apparecchiature per la esecuzione delle prove in sito" TextAlign="Right" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="6">
                                    <asp:CheckBox runat="server" ID="chkDisponibilitaControlli" Text="g) la disponibilità a consentire i controlli sulla propria attività esercitati dall’Organismo Regionale di Accreditamento ed Ispezione" TextAlign="Right" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblOrganizzazioneIspettore" AssociatedControlID="rblOrganizzazioneIspettore" Text="Il sottoscritto dichiara di operare esclusivamente per conto di organizzazione esterna " />
                        <br />
                        <asp:RadioButtonList ID="rblOrganizzazioneIspettore" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rblOrganizzazioneIspettore_SelectedIndexChanged" ValidationGroup="vgAnagraficaIspettore" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>INFORMAZIONI ORGANIZZAZIONE ESTERNA</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore0" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblAziendaIspettore" AssociatedControlID="txtAziendaIspettore" Text="Denominazione (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtAziendaIspettore" Width="400" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtAziendaIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Denominazione: campo obbligatorio"
                            ControlToValidate="txtAziendaIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore1" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoOrganizzazioneIspettore" AssociatedControlID="ddlPaeseOrganizzazioneIspettore" Text="Stato sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseOrganizzazioneIspettore" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseOrganizzazioneIspettore_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseOrganizzazioneIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese sede legale: campo obbligatorio"
                            ControlToValidate="ddlPaeseOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore2" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoOrganizzazioneIspettore" AssociatedControlID="txtIndirizzoOrganizzazioneIspettore" Text="Indirizzo/numero civico (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoOrganizzazioneIspettore" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />&nbsp;
                        <asp:TextBox runat="server" ID="txtNumeroCivicoOrganizzazioneIspettore" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" ToolTip="Numero Civico" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Indirizzo: campo obbligatorio"
                            ControlToValidate="txtIndirizzoOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Numero civico: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapOrganizzazioneIspettore" AssociatedControlID="txtCapOrganizzazioneIspettore" Text="Cap (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapOrganizzazioneIspettore" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap: campo obbligatorio"
                            ControlToValidate="txtCapOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Cap: non valido" ControlToValidate="txtCapOrganizzazioneIspettore"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore3" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaOrganizzazioneIspettore" AssociatedControlID="txtCittaOrganizzazioneIspettore" Text="Città (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Città: campo obbligatorio"
                            ControlToValidate="txtCittaOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaOrganizzazioneIspettore" AssociatedControlID="ddlProvinciaOrganizzazioneIspettore" Text="Provincia (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaOrganizzazioneIspettore" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaOrganizzazioneIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia: campo obbligatorio"
                            ControlToValidate="ddlProvinciaOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore4" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblPartitaIvaOrganizzazioneIspettore" AssociatedControlID="txtPartitaIvaOrganizzazioneIspettore" Text="Partita Iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvPartitaIvaOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleOrganizzazioneIspettore" AssociatedControlID="txtCodiceFiscaleOrganizzazioneIspettore" Text="Codice fiscale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Codice fiscale: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore5" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelefonoOrganizzazioneIspettore" AssociatedControlID="txtTelefonoOrganizzazioneIspettore" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTelefonoOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoOrganizzazioneIspettore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFaxOrganizzazioneIspettore" AssociatedControlID="txtFaxOrganizzazioneIspettore" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFaxOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revFaxOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFaxOrganizzazioneIspettore" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore6" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailOrganizzazioneIspettore" AssociatedControlID="txtEmailOrganizzazioneIspettore" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailOrganizzazioneIspettore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailPecOrganizzazioneIspettore" AssociatedControlID="txtEmailPecOrganizzazioneIspettore" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecOrganizzazioneIspettore" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPecOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Email pec: campo obbligatorio"
                            ControlToValidate="txtEmailPecOrganizzazioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailPecOrganizzazioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecOrganizzazioneIspettore" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowOrganizzazioneIspettore7" Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblOrganismoIspezioneNumeroIspettore" AssociatedControlID="txtOrganismoIspezioneNumeroIspettore" Text="Accreditamento/Certificazione (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtOrganismoIspezioneNumeroIspettore" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtOrganismoIspezioneNumeroIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Accreditamento/Certificazione: campo obbligatorio"
                            ControlToValidate="txtOrganismoIspezioneNumeroIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloOrganismoIspezioneIspettore" AssociatedControlID="txtOrganismoIspezioneIspettore" Text="Rilasciato da (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtOrganismoIspezioneIspettore" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaIspettore" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtOrganismoIspezioneIspettore" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" ErrorMessage="Rilasciato da: campo obbligatorio"
                            ControlToValidate="txtOrganismoIspezioneIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>ALLEGATI</h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCurriculumVitae" AssociatedControlID="UploadCurriculumVitae" Text="Curriculum vitae (.pdf) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:FileUpload ID="UploadCurriculumVitae" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:HyperLink ID="lnkCurriculumVitae" runat="server" CssClass="lnkLink" Font-Bold="true" 
                            Font-Underline="true" Text="Curriculum Vitae" ToolTip="Curriculum Vitae" Target="_blank" />
                        <asp:RegularExpressionValidator ID="revUploadCurriculumVitae" runat="server"
                            ErrorMessage="Curriculum vitae .pdf: file non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.pdf|.Pdf|.pdF|.pDf|.PDF)$" ValidationGroup="vgAnagraficaIspettore"
                            ControlToValidate="UploadCurriculumVitae"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblAttestatoCorso" AssociatedControlID="UploadAttestatoCorso" Text="Attestato di superamento esame del corso di formazione / aggiornamento (.pdf) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:FileUpload ID="UploadAttestatoCorso" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:HyperLink ID="lnkAttestatoCorso" runat="server" CssClass="lnkLink" Font-Bold="true" 
                            Font-Underline="true" Text="Attestato Partecipazione al Corso" ToolTip="Attestato Partecipazione al Corso" Target="_blank" />
                        <asp:RegularExpressionValidator ID="revUploadAttestatoCorso" runat="server"
                            ErrorMessage="Attestato di partecipazione al corso .pdf: file non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.pdf|.Pdf|.pdF|.pDf|.PDF)$" ValidationGroup="vgAnagraficaIspettore"
                            ControlToValidate="UploadAttestatoCorso"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblConsensoRequisitiDichiarati" AssociatedControlID="rblConsensoRequisitiDichiarati" Text="Il sottoscritto dichiara sotto la propria responsabilità – a norma degli artt. 46 e 47 del d.p.r. 28/12/2000 n. 445 e nella consapevolezza che le dichiarazioni mendaci e la falsità in atti sono punite ai sensi del codice penale e delle leggi speciali in materia (art. 76 d.p.r. 445/200)" />
                        <br />
                        <asp:RadioButtonList ID="rblConsensoRequisitiDichiarati" TabIndex="1" ValidationGroup="vgAnagraficaIspettore" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblConsensoRequisitiDichiarati" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Consenso alla privacy: campo obbligatorio"
                            ControlToValidate="rblConsensoRequisitiDichiarati">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:TextBox ID="txtPrivacyIspettore" runat="server" TabIndex="1" Height="100" Rows="100" ReadOnly="true" TextMode="MultiLine" ToolTip="Privacy">
Il possesso dei requisiti di cui al regolamento regionale n. 1 del 3 aprile 2017, necessari per la registrazione nell'Elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER, è dichiarato dal legale rappresentante dell’azienda ai sensi dell’art. 46 del DPR 445/2000 sotto propria responsabilità, anche agli effetti delle sanzioni penali previste dall’art. 76 del medesimo DPR 445/2000.

INFORMATIVA AL TRATTAMENTO DEI DATI PERSONALI DEGLI OPERATORI AI SENSI DELL’ART. 13 REG.UE 2016/679

Premessa
La Regione Emilia-Romagna ha conferito alla società “in house” ART-ER S. cons. p. a. le funzioni di Organismo Regionale di Accreditamento ed Ispezione (nel seguito “ART-ER“ o “Organismo“) di cui all’art. 25-quater della L.R. 26/2004, con il compito di gestire ed assicurare il pieno ed efficace funzionamento del CRITER, ovvero del catasto e del sistema di controllo degli impianti termici in conformità alle disposizioni del Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”.
Nell’ambito delle funzioni conferite dalla Regione Emilia-Romagna ad ART-ER è ricompreso il ruolo di Responsabile del Trattamento dei dati ai sensi e per gli effetti dell’art.28 Reg. Ue 2016/679. In tale qualità, pertanto, ART-ER tratta i dati personali registrati tramite l’applicativo informatico CRITER – Catasto Regionale degli Impianti Termici, accessibile per via telematica all’indirizzo http://energia.regione.emilia-romagna.it/criter/catasto-impianti. La società “in house” ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Titolare del trattamento dei dati
Il Titolare del trattamento dei dati personali acquisiti tramite CRITER è la Giunta della Regione Emilia-Romagna, con sede legale in Viale Aldo Moro, 52 – 40127 Bologna. Il Titolare del Trattamento con Delibera n. 2169 del 20/12/2017 ha designato il Responsabile della protezione dei dati (Data Protection Officer) contattabile ai seguenti indirizzi mail dpo@regione.emilia-romagna.it, dpo@postacert.regione.emilia-romagna.it.
Responsabile del trattamento
Il Titolare del Trattamento, ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679, previa verifica della capacità di garantire il rispetto delle vigenti disposizioni in materia di protezione dei dati personali, ivi compreso il profilo della sicurezza dei dati, ha designato la società “in house” ART-ER - Organismo Regionale di Accreditamento ed Ispezione, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, quale Responsabile del trattamento finalizzato alla gestione del sistema di controllo degli impianti termici. ART-ER ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Base giuridica del trattamento
Il trattamento dei dati personali forniti dagli Utenti, ai sensi e per gli effetti dell’art.6 c.1 lett. e) Reg. UE 2016/679, è posto in essere per l’esecuzione di un compito di interesse pubblico o connesso all’esercizio dei pubblici poteri di cui è investito il Titolare del Trattamento (Legge Regionale 23 dicembre 2004, n. 26 “Disciplina della programmazione energetica territoriale ed altre disposizioni in materia di energia”; Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”), pertanto, il relativo trattamento non necessita del consenso dell’interessato.


Tipologia di dati trattati e finalità del trattamento

Dati acquisiti durante la navigazione
I sistemi informatici e le procedure software preposte al funzionamento del presente Sito acquisiscono, nel corso del loro normale esercizio, alcuni dati personali la cui trasmissione è implicita nell’uso dei protocolli di comunicazione di Internet. Si tratta di informazioni che non sono raccolte per essere associate a interessi identificati, ma che per loro stessa natura potrebbero, attraverso elaborazioni ed associazioni con dati detenuti da terzi, permettere di identificare gli utenti.
In questa categoria di dati rientrano gli indirizzi IP o i nomi a dominio dei computer utilizzati dagli utenti che si connettono al Siti, gli indirizzi in notazione URI (Uniform Resource Identifier) delle risorse richieste, l’orario della richiesta, il metodo utilizzato nel sottoporre la richiesta al server, la dimensione del file ottenuto in risposta, il codice numerico indicante lo stato della risposta data dal server (buon fine, errore, ecc.) ed altri parametri relativi al sistema operativo e all’ambiente informatico dell’utente.
Questi dati vengono normalmente salvati nei cosiddetti “logfile” e utilizzati al solo fine di ricavare informazioni statistiche anonime sull’uso dei siti e per controllarne il corretto funzionamento. I dati potrebbero essere utilizzati per l’accertamento di responsabilità in caso di reati informatici ai danni del Sito. Fatta salva questa eventualità, i dati vengono conservati per un tempo non superiore a quello necessario agli scopi per i quali sono raccolti e trattati.

Dati forniti dall’Utente / Operatore in fase di “iscrizione al sistema CRITER”
La registrazione effettuata dall’Utente al fine di poter operare le funzioni previste dalla normativa vigente nell’ambito del sistema CRITER avviene tramite gli appositi moduli web (form) presenti sul Sito, all’interno delle pagine e sezioni predisposte per l’iscrizione https://criter.regione.emilia-romagna.it/IscrizioneCriter.aspx . La registrazione comporta l’acquisizione di tutti i dati riportati nei campi ivi previsti, compilati dai richiedenti, ivi incluse le credenziali che consentono all’Utente / Operatore di accedere a contenuti riservati. Il trattamento dei dati personali forniti dagli Utenti / Operatori in fase di iscrizione a CRITER è posto in essere per le seguenti finalità: 
-	operatore “impresa di manutenzione”: registrazione nell’elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER;
-	operatore “ispettore CRITER”: registrazione ed accreditamento degli ispettori qualificati CRITER cui vengono affidate le funzioni di ispezione sugli impianti termici;
-	operatore “distributore di energia”: registrazione ed accreditamento dei distributori di energia per gli impianti termici degli edifici della Regione Emilia-Romagna per inserimento dati relativi alle utenze servite;
-	operatore “cittadino o amministratore condominiale responsabile di impianto”: registrazione del responsabile di impianto al fine di consentire la consultazione dei documenti relativi al proprio impianto;
-	operatore “ente locale”: registrazione ed accreditamento degli enti locali per la consultazione del catasto CRITER ai fini dell’espletamento delle funzioni di propria competenza.
Il conferimento dei dati personali richiesti nel form di registrazione è necessario ai fini del completamento dell’iter di registrazione/accreditamento. Il mancato od inesatto conferimento dei dati personali potrebbe determinare l’impossibilità di portare a termine la relativa procedura.

Dati inseriti dagli Utenti / Operatori in fase di Registrazione di documenti
Gli operatori sopra indicati, accreditati per l’esercizio delle relative funzioni, sono autorizzati al trattamento dei dati personali degli utenti finali del servizio. Tali dati, inseriti dagli operatori nell’applicativo CRITER, verranno trattati dal Titolare, previo rilascio di informativa al trattamento dei dati ai sensi e per gli effetti degli artt.13 e 14 Reg. UE 2016/679.


Modalità di trattamento
Il trattamento dei dati connessi all’utilizzo di CRITER ha luogo, informaticamente o tramite supporti cartacei, presso la sede del Responsabile del Trattamento ART-ER ed è effettuato da personale autorizzato e specificatamente formato. I servizi web connessi all’utilizzo dei dati trattati da ART-ER inclusi l’hosting degli applicativi informatici ed eventuali operazioni di gestione tecnica e manutenzione – sono forniti dal Titolare del Trattamento (Regione Emilia-Romagna). Il Titolare del Trattamento ed ART-ER, conformemente a quanto prescritto dall’art. 32 Reg. UE 2016/679 ed alle “Linee guida per la governance del sistema informatico regionale” hanno adottato misure tecniche ed organizzative adeguate allo scopo di garantire la sicurezza e la riservatezza dei dati personali trattati.


Destinatari dei dati forniti dagli Utenti
I dati personali forniti dagli Utenti potranno essere comunicati ad enti territoriali ed istituzionali abilitati per legge a richiederne l’acquisizione nonché, esclusivamente per le finalità indicate nella presente informativa, a società terze o professionisti, designati quali Responsabili del Trattamento ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679 o quali autorizzati al Trattamento, di cui il Titolare ed il Responsabile si avvalgono per la gestione del sistema di controllo degli impianti termici, tra i quali, a mero titolo esemplificativo e non esaustivo: imprese di manutenzione accreditate nell’ambito del sistema CRITER per la registrazione dei libretti di impianto e dei rapporti di controllo di efficienza energetica; Ispettori/agenti accertatori della Regione Emilia-Romagna ai fini della registrazione dei rapporti di ispezione e dei verbali di accertamento di violazione delle norme vigenti; Fornitori di servizi informatici incaricati dello sviluppo e della manutenzione ordinaria e straordinaria dell’applicativo informatico CRITER.
L’Utente può richiedere l’elenco dei Responsabili del Trattamento contattando il Titolare. Si precisa espressamente come i dati personali forniti dagli Utenti non siano oggetto di trasferimento verso Paesi terzi (Extra UE).


Periodo di conservazione
I dati forniti dagli Utenti sono conservati per un periodo di tempo non superiore a quello strettamente necessario per il perseguimento delle finalità sottese al trattamento medesimo, nel rispetto dei tempi di conservazione stabiliti dalla vigente normativa di legge. In tal senso, si precisa come i dati forniti siano soggetti a controlli periodici volti a verificarne la pertinenza, la non eccedenza e l’indispensabilità avuto riguardo al rapporto, alla prestazione od all'incarico in corso, da instaurare o cessato. I dati che, a seguito di verifica, risultino eccedenti o non pertinenti saranno cancellati, fatta salva l'eventuale conservazione, a norma di legge, dell'atto o del documento che li contiene.


Diritti degli interessati
Conformemente a quanto previsto dagli artt. 15 ss del Reg. UE 2016/679, l’interessato in ogni momento potrà esercitare, nei confronti del Titolare del Trattamento o di ART-ER - Organismo Regionale di Accreditamento ed Ispezione (Responsabile del Trattamento ex art.28 Reg. UE 2016/679), i propri diritti di: accedere ai dati personali ed ottenerne copia [diritto di accesso]; ottenere la rettifica dei dati, qualora risultino inesatti, ovvero l'integrazione dei dati, qualora risultino incompleti [diritto di rettifica];  ottenere la cancellazione dei dati nei casi previsti dall’art. 17 Reg. Ue 2016/679 [diritto di cancellazione]; ottenere la limitazione del trattamento nei casi previsti dall’art. 18 Reg. Ue 2016/679; [diritto di limitazione]; opporsi, in qualsiasi momento, per motivi connessi alla propria situazione particolare, al trattamento dei dati personali che riguardano l’interessato [diritto di opposizione].
L’apposita istanza può essere presentata contattando: 
-	il Titolare del Trattamento: Regione Emilia-Romagna, Ufficio per le relazioni con il pubblico (Urp), per iscritto o recandosi direttamente presso lo sportello Urp. L’Urp è aperto dal lunedì al venerdì dalle 9 alle 13 in Viale Aldo Moro 52, 40127 Bologna (Italia): telefono 800-662200, fax 051-527.5360, e-mail urp@regione.emilia-romagna.it. 
-	Il Responsabile del Trattamento: ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, mail criter@art-er.it, pec criter.art-er@pec.it.


Diritto di reclamo
L’interessato che ritenga che il trattamento dei dati personali effettuato attraverso l’applicativo CRITER sia posto in essere in violazione di quanto previsto dal Reg. UE 2016/679 ha diritto di proporre reclamo all’Autorità Garante per la protezione dei dati personali, ai sensi dell’art. 77 Reg. UE 2016/679, ovvero di adire le competenti sedi giudiziarie ai sensi dell’art. 79 Reg. UE 2016/679.
                        </asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblPrivacyIspettore" AssociatedControlID="rblPrivacyIspettore" Text="Il dichiarante conferma di aver preso visione dell'informativa al trattamento dei dati personali" />
                        <br />
                        <asp:RadioButtonList ID="rblPrivacyIspettore" TabIndex="1" ValidationGroup="vgAnagraficaIspettore" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblPrivacyIspettore" ValidationGroup="vgAnagraficaIspettore" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Consenso alla privacy: campo obbligatorio"
                            ControlToValidate="rblPrivacyIspettore">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                          &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnProcessIspettore" runat="server" ValidationGroup="vgAnagraficaIspettore" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessIspettore_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaIspettore" ValidationGroup="vgAnagraficaIspettore" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvCodiceFiscaleIspettore" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" OnServerValidate="ControllaCodiceFiscaleIspettore"
                            ErrorMessage="Codice fiscale non valido<br/>" />
                        <asp:CustomValidator ID="cvEmailPresenteIspettore" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" OnServerValidate="ControllaEmailPresenteIspettore"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvEmailPecPresenteIspettore" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaIspettore" EnableClientScript="true" OnServerValidate="ControllaEmailPecPresenteIspettore"
                            ErrorMessage="Email pec già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblInfoDistributoriCombustibile" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneDistributoriCombustibile" /></h2>
                        <asp:Label runat="server" ID="lblfIscrizioneDistributoriCombustibile" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
               
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDistributoriCombustibile" AssociatedControlID="txtDistributoriCombustibile" Text="Ragione sociale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Ragione sociale: campo obbligatorio"
                            ControlToValidate="txtDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlFormaGiuridicaDistributoriCombustibile" AssociatedControlID="ddlFormaGiuridicaDistributoriCombustibile" Text="Forma giuridica (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFormaGiuridicaDistributoriCombustibile" Width="215" TabIndex="1" ValidationGroup="vgAnagrafica" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFormaGiuridicaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Forma giuridica: campo obbligatorio"
                            ControlToValidate="ddlFormaGiuridicaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTipiDistributoriCombustibile" AssociatedControlID="cblTipiDistributoriCombustibile" Text="Tipologia distributore combustibile" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:CheckBoxList ID="cblTipiDistributoriCombustibile" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cblTipiDistributoriCombustibile_SelectedIndexChanged" TabIndex="1" Width="670" CssClass="checkboxlistClass" RepeatColumns="2" />
                        <asp:Panel ID="pnlTipiDistributoriCombustibileAltro" runat="server" Visible="false">
                            <asp:TextBox Width="400" ID="txtTipiDistributoriCombustibileAltro" runat="server" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvTipologiaFluidoVettoreAltro" runat="server" ForeColor="Red"
                                ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Altra tipologia distributori Combustibile: campo obbligatorio"
                                ControlToValidate="txtTipiDistributoriCombustibileAltro">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlPaeseSedeLegaleDistributoriCombustibile" AssociatedControlID="ddlPaeseSedeLegaleDistributoriCombustibile" Text="Stato sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseSedeLegaleDistributoriCombustibile" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseSedeLegaleDistributoriCombustibile_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseSedeLegaleDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese sede legale: campo obbligatorio"
                            ControlToValidate="ddlPaeseSedeLegaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzoDistributoriCombustibile" AssociatedControlID="txtIndirizzoSedeLegaleDistributoriCombustibile" Text="Indirizzo/numero civico sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoSedeLegaleDistributoriCombustibile" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
						<asp:TextBox runat="server" ID="txtCivicoSedeLegaleDistributoriCombustibile" Width="30" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoSedeLegaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo sede legale: campo obbligatorio"
                            ControlToValidate="txtIndirizzoSedeLegaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCivicoSedeLegaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico sede legale: campo obbligatorio"
                            ControlToValidate="txtCivicoSedeLegaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCapSedeLegaleDistributoriCombustibile" AssociatedControlID="txtCapSedeLegaleDistributoriCombustibile" Text="Cap sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapSedeLegaleDistributoriCombustibile" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapSedeLegaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: campo obbligatorio"
                            ControlToValidate="txtCapSedeLegaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapSedeLegaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap sede legale: non valido" 
                            ControlToValidate="txtCapSedeLegaleDistributoriCombustibile"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCittaSedeLegaleDistributoriCombustibile" AssociatedControlID="txtCittaSedeLegaleDistributoriCombustibile" Text="Città sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaSedeLegaleDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaSedeLegaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città sede legale: campo obbligatorio"
                            ControlToValidate="txtCittaSedeLegaleDistributoriCombustibile">&nbsp;*
                        </asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloddlProvinciaSedeLegaleDistributoriCombustibile" AssociatedControlID="ddlProvinciaSedeLegaleDistributoriCombustibile" Text="Provincia sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaSedeLegaleDistributoriCombustibile" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaSedeLegaleDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia sede legale: campo obbligatorio"
                            ControlToValidate="ddlProvinciaSedeLegaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtPartitaIvaDistributoriCombustibile" AssociatedControlID="txtPartitaIvaDistributoriCombustibile" Text="Partita Iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtPartitaIvaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleDistributoriCombustibile" AssociatedControlID="txtCodiceFiscaleAziendaDistributoriCombustibile" Text="Codice fiscale azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleAziendaDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleAziendaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Codice fiscale: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleAziendaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtTelefonoDistributoriCombustibile" AssociatedControlID="txtTelefonoDistributoriCombustibile" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtTelefonoDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoDistributoriCombustibile" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtFaxDistributoriCombustibile" AssociatedControlID="txtFaxDistributoriCombustibile" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFaxDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="rfvtxtFaxDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFaxDistributoriCombustibile" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtEmailDistributoriCombustibile" AssociatedControlID="txtEmailDistributoriCombustibile" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtEmailDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailDistributoriCombustibile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtEmailPecDistributoriCombustibile" AssociatedControlID="txtEmailPecDistributoriCombustibile" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPecDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email Pec: campo obbligatorio"
                            ControlToValidate="txtEmailPecDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtEmailPecDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecDistributoriCombustibile" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtSitoWebDistributoriCombustibile" AssociatedControlID="txtSitoWebDistributoriCombustibile" Text="Sito web" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSitoWebDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNumeroAlboImpreseDistributoriCombustibile" AssociatedControlID="txtNumeroAlboImpreseDistributoriCombustibile" Text="Numero iscrizione registro imprese (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroAlboImpreseDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroAlboImpreseDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero iscrizione al registro imprese: campo obbligatorio"
                            ControlToValidate="txtNumeroAlboImpreseDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaAlboImpreseDistributoriCombustibile" AssociatedControlID="ddlProvinciaAlboImpreseDistributoriCombustibile" Text="Provincia iscrizione al registro imprese (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaAlboImpreseDistributoriCombustibile" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaAlboImpreseDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia iscrizione al registro imprese: campo obbligatorio"
                            ControlToValidate="ddlProvinciaAlboImpreseDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>DATI LEGALE RAPPRESENTANTE</h2>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="txtTitolotxtNomeDistributoriCombustibile" AssociatedControlID="txtNomeDistributoriCombustibile" Text="Nome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Nome: campo obbligatorio"
                            ControlToValidate="txtNomeDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCognomeDistributoriCombustibile" AssociatedControlID="txtCognomeDistributoriCombustibile" Text="Cognome (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cognome: campo obbligatorio"
                            ControlToValidate="txtCognomeDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPaeseNascitaDistributoriCombustibile" AssociatedControlID="ddlPaeseNascitaDistributoriCombustibile" Text="Stato di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseNascitaDistributoriCombustibile" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseNascitaDistributoriCombustibile_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseNascitaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese di nascita: campo obbligatorio"
                            ControlToValidate="ddlPaeseNascitaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDataNascitaDistributoriCombustibile" AssociatedControlID="txtDataNascitaDistributoriCombustibile" Text="Data di nascita (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataNascitaDistributoriCombustibile" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataNascitaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Data di nascita: campo obbligatorio"
                            ControlToValidate="txtDataNascitaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="rfvDataNascitaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ControlToValidate="txtDataNascitaDistributoriCombustibile" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di nascita: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaNascitaDistributoriCombustibile" AssociatedControlID="txtCittaNascitaDistributoriCombustibile" Text="Città di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaNascitaDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaNascitaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città di nascita: campo obbligatorio"
                            ControlToValidate="txtCittaNascitaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaNascitaDistributoriCombustibile" AssociatedControlID="ddlProvinciaNascitaDistributoriCombustibile" Text="Provincia di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaNascitaDistributoriCombustibile" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaNascitaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di nascita del legale rappresentante: campo obbligatorio"
                            ControlToValidate="ddlProvinciaNascitaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitolotxtCodiceFiscaleDistributoriCombustibile" AssociatedControlID="txtCodiceFiscaleDistributoriCombustibile" Text="Codice fiscale legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCodiceFiscaleDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Codice fiscale legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoResidenzaDistributoriCombustibile" AssociatedControlID="ddlPaeseResidenzaDistributoriCombustibile" Text="Stato residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseResidenzaDistributoriCombustibile" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseResidenzaDistributoriCombustibile_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseResidenzaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese residenza: campo obbligatorio"
                            ControlToValidate="ddlPaeseResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoResidenzaDistributoriCombustibile" AssociatedControlID="txtIndirizzoResidenzaDistributoriCombustibile" Text="Indirizzo/numero civico residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoResidenzaDistributoriCombustibile" ClientIDMode="Static" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />&nbsp;
                            <asp:TextBox runat="server" ID="txtNumeroCivicoResidenzaDistributoriCombustibile" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" ToolTip="Numero Civico Residenza" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoResidenzaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Indirizzo residenza: campo obbligatorio"
                            ControlToValidate="txtIndirizzoResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoResidenzaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Numero civico residenza: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapResidenzaDistributoriCombustibile" AssociatedControlID="txtCapResidenzaDistributoriCombustibile" Text="Cap residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapResidenzaDistributoriCombustibile" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapResidenzaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap residenza: campo obbligatorio"
                            ControlToValidate="txtCapResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapResidenzaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Cap residenza: non valido" ControlToValidate="txtCapResidenzaDistributoriCombustibile"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaResidenzaDistributoriCombustibile" AssociatedControlID="txtCittaResidenzaDistributoriCombustibile" Text="Città di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaResidenzaDistributoriCombustibile" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaResidenzaDistributoriCombustibile" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Città di residenza: campo obbligatorio"
                            ControlToValidate="txtCittaResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaResidenzaDistributoriCombustibile" AssociatedControlID="ddlProvinciaResidenzaDistributoriCombustibile" Text="Provincia di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaResidenzaDistributoriCombustibile" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaResidenzaDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di residenza: campo obbligatorio"
                            ControlToValidate="ddlProvinciaResidenzaDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:TextBox ID="txtPrivacyDistributoriCombustibile" runat="server" Height="100" TabIndex="1" Rows="100" ReadOnly="true" TextMode="MultiLine">
INFORMATIVA AL TRATTAMENTO DEI DATI PERSONALI DEGLI OPERATORI AI SENSI DELL’ART. 13 REG.UE 2016/679

Premessa
La Regione Emilia-Romagna ha conferito alla società “in house” ART-ER S. cons. p. a. le funzioni di Organismo Regionale di Accreditamento ed Ispezione (nel seguito “ART-ER“ o “Organismo“) di cui all’art. 25-quater della L.R. 26/2004, con il compito di gestire ed assicurare il pieno ed efficace funzionamento del CRITER, ovvero del catasto e del sistema di controllo degli impianti termici in conformità alle disposizioni del Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”.
Nell’ambito delle funzioni conferite dalla Regione Emilia-Romagna ad ART-ER è ricompreso il ruolo di Responsabile del Trattamento dei dati ai sensi e per gli effetti dell’art.28 Reg. Ue 2016/679. In tale qualità, pertanto, ART-ER tratta i dati personali registrati tramite l’applicativo informatico CRITER – Catasto Regionale degli Impianti Termici, accessibile per via telematica all’indirizzo http://energia.regione.emilia-romagna.it/criter/catasto-impianti. La società “in house” ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Titolare del trattamento dei dati
Il Titolare del trattamento dei dati personali acquisiti tramite CRITER è la Giunta della Regione Emilia-Romagna, con sede legale in Viale Aldo Moro, 52 – 40127 Bologna. Il Titolare del Trattamento con Delibera n. 2169 del 20/12/2017 ha designato il Responsabile della protezione dei dati (Data Protection Officer) contattabile ai seguenti indirizzi mail dpo@regione.emilia-romagna.it, dpo@postacert.regione.emilia-romagna.it.
Responsabile del trattamento
Il Titolare del Trattamento, ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679, previa verifica della capacità di garantire il rispetto delle vigenti disposizioni in materia di protezione dei dati personali, ivi compreso il profilo della sicurezza dei dati, ha designato la società “in house” ART-ER - Organismo Regionale di Accreditamento ed Ispezione, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, quale Responsabile del trattamento finalizzato alla gestione del sistema di controllo degli impianti termici. ART-ER ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Base giuridica del trattamento
Il trattamento dei dati personali forniti dagli Utenti, ai sensi e per gli effetti dell’art.6 c.1 lett. e) Reg. UE 2016/679, è posto in essere per l’esecuzione di un compito di interesse pubblico o connesso all’esercizio dei pubblici poteri di cui è investito il Titolare del Trattamento (Legge Regionale 23 dicembre 2004, n. 26 “Disciplina della programmazione energetica territoriale ed altre disposizioni in materia di energia”; Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”), pertanto, il relativo trattamento non necessita del consenso dell’interessato.


Tipologia di dati trattati e finalità del trattamento

Dati acquisiti durante la navigazione
I sistemi informatici e le procedure software preposte al funzionamento del presente Sito acquisiscono, nel corso del loro normale esercizio, alcuni dati personali la cui trasmissione è implicita nell’uso dei protocolli di comunicazione di Internet. Si tratta di informazioni che non sono raccolte per essere associate a interessi identificati, ma che per loro stessa natura potrebbero, attraverso elaborazioni ed associazioni con dati detenuti da terzi, permettere di identificare gli utenti.
In questa categoria di dati rientrano gli indirizzi IP o i nomi a dominio dei computer utilizzati dagli utenti che si connettono al Siti, gli indirizzi in notazione URI (Uniform Resource Identifier) delle risorse richieste, l’orario della richiesta, il metodo utilizzato nel sottoporre la richiesta al server, la dimensione del file ottenuto in risposta, il codice numerico indicante lo stato della risposta data dal server (buon fine, errore, ecc.) ed altri parametri relativi al sistema operativo e all’ambiente informatico dell’utente.
Questi dati vengono normalmente salvati nei cosiddetti “logfile” e utilizzati al solo fine di ricavare informazioni statistiche anonime sull’uso dei siti e per controllarne il corretto funzionamento. I dati potrebbero essere utilizzati per l’accertamento di responsabilità in caso di reati informatici ai danni del Sito. Fatta salva questa eventualità, i dati vengono conservati per un tempo non superiore a quello necessario agli scopi per i quali sono raccolti e trattati.

Dati forniti dall’Utente / Operatore in fase di “iscrizione al sistema CRITER”
La registrazione effettuata dall’Utente al fine di poter operare le funzioni previste dalla normativa vigente nell’ambito del sistema CRITER avviene tramite gli appositi moduli web (form) presenti sul Sito, all’interno delle pagine e sezioni predisposte per l’iscrizione https://criter.regione.emilia-romagna.it/IscrizioneCriter.aspx . La registrazione comporta l’acquisizione di tutti i dati riportati nei campi ivi previsti, compilati dai richiedenti, ivi incluse le credenziali che consentono all’Utente / Operatore di accedere a contenuti riservati. Il trattamento dei dati personali forniti dagli Utenti / Operatori in fase di iscrizione a CRITER è posto in essere per le seguenti finalità: 
-	operatore “impresa di manutenzione”: registrazione nell’elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER;
-	operatore “ispettore CRITER”: registrazione ed accreditamento degli ispettori qualificati CRITER cui vengono affidate le funzioni di ispezione sugli impianti termici;
-	operatore “distributore di energia”: registrazione ed accreditamento dei distributori di energia per gli impianti termici degli edifici della Regione Emilia-Romagna per inserimento dati relativi alle utenze servite;
-	operatore “cittadino o amministratore condominiale responsabile di impianto”: registrazione del responsabile di impianto al fine di consentire la consultazione dei documenti relativi al proprio impianto;
-	operatore “ente locale”: registrazione ed accreditamento degli enti locali per la consultazione del catasto CRITER ai fini dell’espletamento delle funzioni di propria competenza.
Il conferimento dei dati personali richiesti nel form di registrazione è necessario ai fini del completamento dell’iter di registrazione/accreditamento. Il mancato od inesatto conferimento dei dati personali potrebbe determinare l’impossibilità di portare a termine la relativa procedura.

Dati inseriti dagli Utenti / Operatori in fase di Registrazione di documenti
Gli operatori sopra indicati, accreditati per l’esercizio delle relative funzioni, sono autorizzati al trattamento dei dati personali degli utenti finali del servizio. Tali dati, inseriti dagli operatori nell’applicativo CRITER, verranno trattati dal Titolare, previo rilascio di informativa al trattamento dei dati ai sensi e per gli effetti degli artt.13 e 14 Reg. UE 2016/679.


Modalità di trattamento
Il trattamento dei dati connessi all’utilizzo di CRITER ha luogo, informaticamente o tramite supporti cartacei, presso la sede del Responsabile del Trattamento ART-ER ed è effettuato da personale autorizzato e specificatamente formato. I servizi web connessi all’utilizzo dei dati trattati da ART-ER inclusi l’hosting degli applicativi informatici ed eventuali operazioni di gestione tecnica e manutenzione – sono forniti dal Titolare del Trattamento (Regione Emilia-Romagna). Il Titolare del Trattamento ed ART-ER, conformemente a quanto prescritto dall’art. 32 Reg. UE 2016/679 ed alle “Linee guida per la governance del sistema informatico regionale” hanno adottato misure tecniche ed organizzative adeguate allo scopo di garantire la sicurezza e la riservatezza dei dati personali trattati.


Destinatari dei dati forniti dagli Utenti
I dati personali forniti dagli Utenti potranno essere comunicati ad enti territoriali ed istituzionali abilitati per legge a richiederne l’acquisizione nonché, esclusivamente per le finalità indicate nella presente informativa, a società terze o professionisti, designati quali Responsabili del Trattamento ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679 o quali autorizzati al Trattamento, di cui il Titolare ed il Responsabile si avvalgono per la gestione del sistema di controllo degli impianti termici, tra i quali, a mero titolo esemplificativo e non esaustivo: imprese di manutenzione accreditate nell’ambito del sistema CRITER per la registrazione dei libretti di impianto e dei rapporti di controllo di efficienza energetica; Ispettori/agenti accertatori della Regione Emilia-Romagna ai fini della registrazione dei rapporti di ispezione e dei verbali di accertamento di violazione delle norme vigenti; Fornitori di servizi informatici incaricati dello sviluppo e della manutenzione ordinaria e straordinaria dell’applicativo informatico CRITER.
L’Utente può richiedere l’elenco dei Responsabili del Trattamento contattando il Titolare. Si precisa espressamente come i dati personali forniti dagli Utenti non siano oggetto di trasferimento verso Paesi terzi (Extra UE).


Periodo di conservazione
I dati forniti dagli Utenti sono conservati per un periodo di tempo non superiore a quello strettamente necessario per il perseguimento delle finalità sottese al trattamento medesimo, nel rispetto dei tempi di conservazione stabiliti dalla vigente normativa di legge. In tal senso, si precisa come i dati forniti siano soggetti a controlli periodici volti a verificarne la pertinenza, la non eccedenza e l’indispensabilità avuto riguardo al rapporto, alla prestazione od all'incarico in corso, da instaurare o cessato. I dati che, a seguito di verifica, risultino eccedenti o non pertinenti saranno cancellati, fatta salva l'eventuale conservazione, a norma di legge, dell'atto o del documento che li contiene.


Diritti degli interessati
Conformemente a quanto previsto dagli artt. 15 ss del Reg. UE 2016/679, l’interessato in ogni momento potrà esercitare, nei confronti del Titolare del Trattamento o di ART-ER - Organismo Regionale di Accreditamento ed Ispezione (Responsabile del Trattamento ex art.28 Reg. UE 2016/679), i propri diritti di: accedere ai dati personali ed ottenerne copia [diritto di accesso]; ottenere la rettifica dei dati, qualora risultino inesatti, ovvero l'integrazione dei dati, qualora risultino incompleti [diritto di rettifica];  ottenere la cancellazione dei dati nei casi previsti dall’art. 17 Reg. Ue 2016/679 [diritto di cancellazione]; ottenere la limitazione del trattamento nei casi previsti dall’art. 18 Reg. Ue 2016/679; [diritto di limitazione]; opporsi, in qualsiasi momento, per motivi connessi alla propria situazione particolare, al trattamento dei dati personali che riguardano l’interessato [diritto di opposizione].
L’apposita istanza può essere presentata contattando: 
-	il Titolare del Trattamento: Regione Emilia-Romagna, Ufficio per le relazioni con il pubblico (Urp), per iscritto o recandosi direttamente presso lo sportello Urp. L’Urp è aperto dal lunedì al venerdì dalle 9 alle 13 in Viale Aldo Moro 52, 40127 Bologna (Italia): telefono 800-662200, fax 051-527.5360, e-mail urp@regione.emilia-romagna.it. 
-	Il Responsabile del Trattamento: ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, mail criter@art-er.it, pec criter.art-er@pec.it.


Diritto di reclamo
L’interessato che ritenga che il trattamento dei dati personali effettuato attraverso l’applicativo CRITER sia posto in essere in violazione di quanto previsto dal Reg. UE 2016/679 ha diritto di proporre reclamo all’Autorità Garante per la protezione dei dati personali, ai sensi dell’art. 77 Reg. UE 2016/679, ovvero di adire le competenti sedi giudiziarie ai sensi dell’art. 79 Reg. UE 2016/679.
                        </asp:TextBox>
                        <br /><br />
                        <asp:Label runat="server" ID="lblPrivacyDistributoriCombustibile" AssociatedControlID="rblPrivacyDistributoriCombustibile" Text="Il dichiarante conferma di aver preso visione dell'informativa al trattamento dei dati personali" />
                        <br />
                        <asp:RadioButtonList ID="rblPrivacyDistributoriCombustibile" TabIndex="1" ValidationGroup="vgAnagrafica" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblPrivacyDistributoriCombustibile" ValidationGroup="vgAnagrafica" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Consenso alla privacy: campo obbligatorio"
                            ControlToValidate="rblPrivacyDistributoriCombustibile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
				        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessageDistributoriCombustibile" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnullaDistributoriCombustibile" Visible="false" Text="ANNULLA" CausesValidation="false" TabIndex="1" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button ID="btnProcessDistributoriCombustibile" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessDistributoriCombustibile_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaDistributoriCombustibile" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                       <asp:CustomValidator ID="cvPartitaIvaDistributoriCombustibile" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaPivaDistributoriCombustibile"
                            ErrorMessage="Partita Iva non valida<br/>" />
                        <asp:CustomValidator ID="cvTipiDistributoriCombustibile" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaTipiDistributoriCombustibile"
                            ErrorMessage="Selezionare almeno un ruolo<br/>" />
                        <asp:CustomValidator ID="cvEmailDistributoriCombustibilePresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPresenteDistributoriCombustibile"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvEmailDistributoriCombustibilePecPresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaEmailPecPresenteDistributoriCombustibile"
                            ErrorMessage="Email pec già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvPartitaIvaDistributoriCombustibilePresente" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaPartitaIvaPresenteDistributoriCombustibile"
                            ErrorMessage="Partita Iva già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblInfoCittadino" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneCittadino" /></h2>
                    </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloNomeCitatdino" AssociatedControlID="lblNomeCittadino" Text="Nome" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblNomeCittadino" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCognomeCittadino" AssociatedControlID="lblCognomeCittadino" Text="Cognome" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCognomeCittadino" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPaeseNascitaCittadino" AssociatedControlID="lblPaeseNascitaCittadino" Text="Stato di nascita" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblPaeseNascitaCittadino" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataNascitaCittadino" AssociatedControlID="lblDataNascitaCittadino" Text="Data di nascita (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblDataNascitaCittadino" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCittaNascitaCittadino" AssociatedControlID="lblCittaNascitaCittadino" Text="Città di nascita" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCittaNascitaCittadino" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloProvinciaNascitaCittadino" AssociatedControlID="lblProvinciaNascitaCittadino" Text="Provincia di nascita" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblProvinciaNascitaCittadino" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceFiscaleCittadino" AssociatedControlID="lblCodiceFiscaleCittadino" Text="Codice fiscale" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCodiceFiscaleCittadino" />
                    </asp:TableCell>
                </asp:TableRow>
                
            </asp:Table>

            <asp:Table Width="900" ID="tblInfoEnteLocale" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloGestioneEnteLocale" /></h2>
                        <asp:Label runat="server" ID="lblfIscrizioneEnteLocale" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEnteLocale" AssociatedControlID="txtEnteLocale" Text="Ente (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEnteLocale" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Ente locale: campo obbligatorio"
                            ControlToValidate="txtEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFormaGiuridicaEnteLocale" AssociatedControlID="ddlFormaGiuridicaEnteLocale" Text="Forma giuridica (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFormaGiuridicaEnteLocale" Width="215" TabIndex="1" ValidationGroup="vgAnagraficaEnteLocale" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFormaGiuridicaEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Forma giuridica: campo obbligatorio"
                            ControlToValidate="ddlFormaGiuridicaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblAreaTerritorialeEnteLocale" AssociatedControlID="gridAreaTerritorialeEnteLocale" Text="Area territoriale di competenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <dx:ASPxGridView ID="gridAreaTerritorialeEnteLocale" DataSourceID="dsGridPrincipale" ClientInstanceName="grid" runat="server"
                            OnSelectionChanged="gridAreaTerritorialeEnteLocale_SelectionChanged"
                            KeyFieldName="IDCodiceCatastale"
                            Width="100%"
                            EnableCallBacks="False" EnableCallbackCompression="False">
                            <Columns>
                                <dx:GridViewDataColumn FieldName="IDCodiceCatastale" VisibleIndex="1" Settings-AllowAutoFilterTextInputTimer="True" Visible="false" />
                                <dx:GridViewDataColumn FieldName="CodiceCatastale" VisibleIndex="2" Settings-AllowAutoFilterTextInputTimer="True" />
                                <dx:GridViewDataColumn FieldName="Comune" VisibleIndex="3" Settings-AllowAutoFilterTextInputTimer="True" />
                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" CellStyle-HorizontalAlign="Left" VisibleIndex="0" Caption="" />
                            </Columns>
                            <SettingsSearchPanel Visible="true" ColumnNames="Comune" AllowTextInputTimer="True" HighlightResults="true" Delay="500" />
                            <SettingsText SearchPanelEditorNullText="Comune da cercare..." />
                            <SettingsBehavior ProcessSelectionChangedOnServer="True" />
                            <SettingsPager EnableAdaptivity="true" PageSize="5" Visible="false" />
                        </dx:ASPxGridView>
                        <ef:EntityDataSource ID="dsGridPrincipale" runat="server"
                            ConnectionString="name=CriterDataModel"
                            ContextTypeName="DataLayer.CriterDataModel"
                            DefaultContainerName="CriterDataModel" EnableFlattening="False"
                            EntitySetName="SYS_CodiciCatastali"
                            AutoGenerateWhereClause="false"
                            Where="it.fAttivo=true and it.IDProvincia IN {8,20,23,37,46,40,50,51,53}"
                            OrderBy="it.Comune">
                        </ef:EntityDataSource>
                        <asp:Label runat="server" ID="lblComuniSelezionati" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoSedeLegaleEnteLocale" AssociatedControlID="txtIndirizzoSedeLegaleEnteLocale" Text="Indirizzo/numero civico sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoSedeLegaleEnteLocale" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />&nbsp;
                        <asp:TextBox runat="server" ID="txtNumeroCivicoSedeLegaleEnteLocale" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" ToolTip="Numero Civico Sede Legale" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoSedeLegaleEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Indirizzo sede legale: campo obbligatorio"
                            ControlToValidate="txtIndirizzoSedeLegaleEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoSedeLegaleEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Numero civico sede legale: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoSedeLegaleEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapSedeLegaleEnteLocale" AssociatedControlID="txtCapSedeLegaleEnteLocale" Text="Cap sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapSedeLegaleEnteLocale" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapSedeLegaleEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Cap sede legale: campo obbligatorio"
                            ControlToValidate="txtCapSedeLegaleEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapSedeLegaleEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Cap sede legale: non valido" ControlToValidate="txtCapSedeLegaleEnteLocale"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaSedeLegaleEnteLocale" AssociatedControlID="txtCittaSedeLegaleEnteLocale" Text="Città sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaSedeLegaleEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaSedeLegaleEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Città sede legale: campo obbligatorio"
                            ControlToValidate="txtCittaSedeLegaleEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaSedeLegaleEnteLocale" AssociatedControlID="ddlProvinciaSedeLegaleEnteLocale" Text="Provincia sede legale (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaSedeLegaleEnteLocale" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaSedeLegaleEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia sede legale: campo obbligatorio"
                            ControlToValidate="ddlProvinciaSedeLegaleEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblPartitaIvaEnteLocale" AssociatedControlID="txtPartitaIvaEnteLocale" Text="Partita Iva (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIvaEnteLocale" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvPartitaIvaEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Partita Iva: campo obbligatorio"
                            ControlToValidate="txtPartitaIvaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleEnteLocale" AssociatedControlID="txtCodiceFiscaleEnteLocale" Text="Codice fiscale" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleEnteLocale" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTelefonoEnteLocale" AssociatedControlID="txtTelefonoEnteLocale" Text="Telefono (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtTelefonoEnteLocale" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtTelefonoEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Telefono: campo obbligatorio"
                            ControlToValidate="txtTelefonoEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTelefonoEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true"
                            ErrorMessage="Telefono: campo non valido"
                            ControlToValidate="txtTelefonoEnteLocale" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFaxEnteLocale" AssociatedControlID="txtFaxEnteLocale" Text="Fax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFaxEnteLocale" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass" />
                        <asp:RegularExpressionValidator ID="revFaxEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFaxEnteLocale" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailEnteLocale" AssociatedControlID="txtEmailEnteLocale" Text="Email (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmailEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmailEnteLocale" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblEmailPecEnteLocale" AssociatedControlID="txtEmailPecEnteLocale" Text="Email pec (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPecEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPecEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Email pec: campo obbligatorio"
                            ControlToValidate="txtEmailPecEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailPecEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPecEnteLocale" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblSitoWebEnteLocale" AssociatedControlID="txtSitoWebEnteLocale" Text="Sito web" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSitoWebEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>DATI LEGALE RAPPRESENTANTE (O SUO DELEGATO) - <i>il nominativo deve corrispondere a quello sulla firma digitale</i></h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblFunzioneLegaleRappresentanteEnteLocale" AssociatedControlID="ddlFunzioneLegaleRappresentanteEnteLocale" Text="Qualifica (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlFunzioneLegaleRappresentanteEnteLocale" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlFunzioneLegaleRappresentanteEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Qualifica: campo obbligatorio"
                            ControlToValidate="ddlFunzioneLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNomeEnteLocale" AssociatedControlID="txtNomeEnteLocale" Text="Nome legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNomeEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNomeEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Nome legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtNomeEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCognomeEnteLocale" AssociatedControlID="txtCognomeEnteLocale" Text="Cognome legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCognomeEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCognomeEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Cognome legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtCognomeEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPaeseNascitaEnteLocale" AssociatedControlID="ddlPaeseNascitaEnteLocale" Text="Stato di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseNascitaEnteLocale" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseNascitaEnteLocale_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseNascitaEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese di nascita: campo obbligatorio"
                            ControlToValidate="ddlPaeseNascitaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>

                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDataNascitaEnteLocale" AssociatedControlID="txtDataNascitaEnteLocale" Text="Data di nascita (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataNascitaEnteLocale" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataNascitaEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Data di nascita: campo obbligatorio"
                            ControlToValidate="txtDataNascitaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="rfvDataNascitaEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ControlToValidate="txtDataNascitaEnteLocale" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di nascita: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaNascitaEnteLocale" AssociatedControlID="txtCittaNascitaEnteLocale" Text="Città di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaNascitaEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaNascitaEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Città di nascita: campo obbligatorio"
                            ControlToValidate="txtCittaNascitaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaNascitaEnteLocale" AssociatedControlID="ddlProvinciaNascitaEnteLocale" Text="Provincia di nascita (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaNascitaEnteLocale" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaNascitaEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di nascita del legale rappresentante: campo obbligatorio"
                            ControlToValidate="ddlProvinciaNascitaEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCodiceFiscaleLegaleRappresentanteEnteLocale" AssociatedControlID="txtCodiceFiscaleLegaleRappresentanteEnteLocale" Text="Codice fiscale legale rappresentante (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceFiscaleLegaleRappresentanteEnteLocale" Width="200" TabIndex="1" MaxLength="16" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvCodiceFiscaleLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Codice fiscale legale rappresentante: campo obbligatorio"
                            ControlToValidate="txtCodiceFiscaleLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoResidenzaLegaleRappresentanteEnteLocale" AssociatedControlID="ddlPaeseResidenzaLegaleRappresentanteEnteLocale" Text="Stato residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                        <asp:DropDownList ID="ddlPaeseResidenzaLegaleRappresentanteEnteLocale" TabIndex="1" Width="215" AutoPostBack="true" OnSelectedIndexChanged="ddlPaeseResidenzaLegaleRappresentanteEnteLocale_SelectedIndexChanged" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlPaeseResidenzaLegaleRappresentanteEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Paese residenza: campo obbligatorio"
                            ControlToValidate="ddlPaeseResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblIndirizzoResidenzaEnteLocale" AssociatedControlID="txtIndirizzoResidenza" Text="Indirizzo/numero civico residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoResidenzaLegaleRappresentanteEnteLocale" placeholder="" ClientIDMode="Static" Width="160" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />&nbsp;
                        <asp:TextBox runat="server" ID="txtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale" Width="30" TabIndex="1" MaxLength="10" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" ToolTip="Numero Civico Residenza" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtIndirizzoResidenzaLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Indirizzo residenza: campo obbligatorio"
                            ControlToValidate="txtIndirizzoResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Numero civico residenza: campo obbligatorio"
                            ControlToValidate="txtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCapResidenzaLegaleRappresentanteEnteLocale" AssociatedControlID="txtCapResidenzaLegaleRappresentanteEnteLocale" Text="Cap residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCapResidenzaLegaleRappresentanteEnteLocale" ClientIDMode="Static" Width="60" TabIndex="1" MaxLength="5" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCapResidenzaLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Cap residenza: campo obbligatorio"
                            ControlToValidate="txtCapResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtCapResidenzaLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Cap residenza: non valido" ControlToValidate="txtCapResidenzaLegaleRappresentanteEnteLocale"
                            ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCittaResidenzaLegaleRappresentanteEnteLocale" AssociatedControlID="txtCittaResidenzaLegaleRappresentanteEnteLocale" Text="Città di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCittaResidenzaLegaleRappresentanteEnteLocale" Width="200" TabIndex="1" MaxLength="100" ValidationGroup="vgAnagraficaEnteLocale" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtCittaResidenzaLegaleRappresentanteEnteLocale" ForeColor="Red" runat="server" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" ErrorMessage="Città di residenza: campo obbligatorio"
                            ControlToValidate="txtCittaResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblProvinciaResidenzaLegaleRappresentanteEnteLocale" AssociatedControlID="ddlProvinciaResidenzaLegaleRappresentanteEnteLocale" Text="Provincia di residenza (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:DropDownList ID="ddlProvinciaResidenzaLegaleRappresentanteEnteLocale" Width="215" TabIndex="1" runat="server" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlProvinciaResidenzaLegaleRappresentanteEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Provincia di residenza: campo obbligatorio"
                            ControlToValidate="ddlProvinciaResidenzaLegaleRappresentanteEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento">
                          Dichiaro che l’Ente accede ai dati per i Comuni indicati per compiti istituzionali.
Impegno a comunicare tempestivamente all’Organismo Regionale di Accreditamento ed Ispezione eventuali variazioni dei compiti istituzionali dell’Ente che vengono a fare cadere le motivazioni per l’accesso ai dati.
Impegno a trasferire nella propria organizzazione, e in particolare al soggetto incaricato, tutti gli impegni e i vincoli qui indicati, ferma restando la responsabilità in capo a chi firma.
L’Organismo Regionale di Accreditamento ed Ispezione si riserva di verificare la veridicità delle dichiarazioni con controlli periodici sulle utenze a cui viene autorizzato l’accesso.

                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:TextBox ID="txtPrivacyEnteLocale" runat="server" TabIndex="1" Height="100" Rows="100" ReadOnly="true" TextMode="MultiLine" ToolTip="Privacy">
INFORMATIVA AL TRATTAMENTO DEI DATI PERSONALI DEGLI OPERATORI AI SENSI DELL’ART. 13 REG.UE 2016/679

Premessa
La Regione Emilia-Romagna ha conferito alla società “in house” ART-ER S. cons. p. a. le funzioni di Organismo Regionale di Accreditamento ed Ispezione (nel seguito “ART-ER“ o “Organismo“) di cui all’art. 25-quater della L.R. 26/2004, con il compito di gestire ed assicurare il pieno ed efficace funzionamento del CRITER, ovvero del catasto e del sistema di controllo degli impianti termici in conformità alle disposizioni del Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”.
Nell’ambito delle funzioni conferite dalla Regione Emilia-Romagna ad ART-ER è ricompreso il ruolo di Responsabile del Trattamento dei dati ai sensi e per gli effetti dell’art.28 Reg. Ue 2016/679. In tale qualità, pertanto, ART-ER tratta i dati personali registrati tramite l’applicativo informatico CRITER – Catasto Regionale degli Impianti Termici, accessibile per via telematica all’indirizzo http://energia.regione.emilia-romagna.it/criter/catasto-impianti. La società “in house” ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Titolare del trattamento dei dati
Il Titolare del trattamento dei dati personali acquisiti tramite CRITER è la Giunta della Regione Emilia-Romagna, con sede legale in Viale Aldo Moro, 52 – 40127 Bologna. Il Titolare del Trattamento con Delibera n. 2169 del 20/12/2017 ha designato il Responsabile della protezione dei dati (Data Protection Officer) contattabile ai seguenti indirizzi mail dpo@regione.emilia-romagna.it, dpo@postacert.regione.emilia-romagna.it.
Responsabile del trattamento
Il Titolare del Trattamento, ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679, previa verifica della capacità di garantire il rispetto delle vigenti disposizioni in materia di protezione dei dati personali, ivi compreso il profilo della sicurezza dei dati, ha designato la società “in house” ART-ER - Organismo Regionale di Accreditamento ed Ispezione, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, quale Responsabile del trattamento finalizzato alla gestione del sistema di controllo degli impianti termici. ART-ER ha designato un proprio Responsabile della protezione dei dati contattabile al seguente indirizzo mail dpo-team@lepida.it.


Base giuridica del trattamento
Il trattamento dei dati personali forniti dagli Utenti, ai sensi e per gli effetti dell’art.6 c.1 lett. e) Reg. UE 2016/679, è posto in essere per l’esecuzione di un compito di interesse pubblico o connesso all’esercizio dei pubblici poteri di cui è investito il Titolare del Trattamento (Legge Regionale 23 dicembre 2004, n. 26 “Disciplina della programmazione energetica territoriale ed altre disposizioni in materia di energia”; Regolamento regionale 3 aprile 2017, n. 1 “Regolamento di attuazione delle disposizioni in materia di esercizio, conduzione, controllo, manutenzione e ispezione degli impianti termici per la climatizzazione invernale ed estiva degli edifici e per la preparazione dell'acqua calda per usi igienici sanitari, a norma dell'articolo 25-quater della Legge regionale 23 dicembre 2004, n. 26 e s.m.”), pertanto, il relativo trattamento non necessita del consenso dell’interessato.


Tipologia di dati trattati e finalità del trattamento

Dati acquisiti durante la navigazione
I sistemi informatici e le procedure software preposte al funzionamento del presente Sito acquisiscono, nel corso del loro normale esercizio, alcuni dati personali la cui trasmissione è implicita nell’uso dei protocolli di comunicazione di Internet. Si tratta di informazioni che non sono raccolte per essere associate a interessi identificati, ma che per loro stessa natura potrebbero, attraverso elaborazioni ed associazioni con dati detenuti da terzi, permettere di identificare gli utenti.
In questa categoria di dati rientrano gli indirizzi IP o i nomi a dominio dei computer utilizzati dagli utenti che si connettono al Siti, gli indirizzi in notazione URI (Uniform Resource Identifier) delle risorse richieste, l’orario della richiesta, il metodo utilizzato nel sottoporre la richiesta al server, la dimensione del file ottenuto in risposta, il codice numerico indicante lo stato della risposta data dal server (buon fine, errore, ecc.) ed altri parametri relativi al sistema operativo e all’ambiente informatico dell’utente.
Questi dati vengono normalmente salvati nei cosiddetti “logfile” e utilizzati al solo fine di ricavare informazioni statistiche anonime sull’uso dei siti e per controllarne il corretto funzionamento. I dati potrebbero essere utilizzati per l’accertamento di responsabilità in caso di reati informatici ai danni del Sito. Fatta salva questa eventualità, i dati vengono conservati per un tempo non superiore a quello necessario agli scopi per i quali sono raccolti e trattati.

Dati forniti dall’Utente / Operatore in fase di “iscrizione al sistema CRITER”
La registrazione effettuata dall’Utente al fine di poter operare le funzioni previste dalla normativa vigente nell’ambito del sistema CRITER avviene tramite gli appositi moduli web (form) presenti sul Sito, all’interno delle pagine e sezioni predisposte per l’iscrizione https://criter.regione.emilia-romagna.it/IscrizioneCriter.aspx . La registrazione comporta l’acquisizione di tutti i dati riportati nei campi ivi previsti, compilati dai richiedenti, ivi incluse le credenziali che consentono all’Utente / Operatore di accedere a contenuti riservati. Il trattamento dei dati personali forniti dagli Utenti / Operatori in fase di iscrizione a CRITER è posto in essere per le seguenti finalità: 
-	operatore “impresa di manutenzione”: registrazione nell’elenco delle imprese di installazione e/o manutenzione degli impianti termici della Regione Emilia-Romagna qualificate ad operare servizi di controllo dell’efficienza energetica degli impianti termici sul territorio regionale e nell’ambito del sistema CRITER;
-	operatore “ispettore CRITER”: registrazione ed accreditamento degli ispettori qualificati CRITER cui vengono affidate le funzioni di ispezione sugli impianti termici;
-	operatore “distributore di energia”: registrazione ed accreditamento dei distributori di energia per gli impianti termici degli edifici della Regione Emilia-Romagna per inserimento dati relativi alle utenze servite;
-	operatore “cittadino o amministratore condominiale responsabile di impianto”: registrazione del responsabile di impianto al fine di consentire la consultazione dei documenti relativi al proprio impianto;
-	operatore “ente locale”: registrazione ed accreditamento degli enti locali per la consultazione del catasto CRITER ai fini dell’espletamento delle funzioni di propria competenza.
Il conferimento dei dati personali richiesti nel form di registrazione è necessario ai fini del completamento dell’iter di registrazione/accreditamento. Il mancato od inesatto conferimento dei dati personali potrebbe determinare l’impossibilità di portare a termine la relativa procedura.

Dati inseriti dagli Utenti / Operatori in fase di Registrazione di documenti
Gli operatori sopra indicati, accreditati per l’esercizio delle relative funzioni, sono autorizzati al trattamento dei dati personali degli utenti finali del servizio. Tali dati, inseriti dagli operatori nell’applicativo CRITER, verranno trattati dal Titolare, previo rilascio di informativa al trattamento dei dati ai sensi e per gli effetti degli artt.13 e 14 Reg. UE 2016/679.


Modalità di trattamento
Il trattamento dei dati connessi all’utilizzo di CRITER ha luogo, informaticamente o tramite supporti cartacei, presso la sede del Responsabile del Trattamento ART-ER ed è effettuato da personale autorizzato e specificatamente formato. I servizi web connessi all’utilizzo dei dati trattati da ART-ER inclusi l’hosting degli applicativi informatici ed eventuali operazioni di gestione tecnica e manutenzione – sono forniti dal Titolare del Trattamento (Regione Emilia-Romagna). Il Titolare del Trattamento ed ART-ER, conformemente a quanto prescritto dall’art. 32 Reg. UE 2016/679 ed alle “Linee guida per la governance del sistema informatico regionale” hanno adottato misure tecniche ed organizzative adeguate allo scopo di garantire la sicurezza e la riservatezza dei dati personali trattati.


Destinatari dei dati forniti dagli Utenti
I dati personali forniti dagli Utenti potranno essere comunicati ad enti territoriali ed istituzionali abilitati per legge a richiederne l’acquisizione nonché, esclusivamente per le finalità indicate nella presente informativa, a società terze o professionisti, designati quali Responsabili del Trattamento ai sensi e per gli effetti dell’art. 28 Reg. UE 2016/679 o quali autorizzati al Trattamento, di cui il Titolare ed il Responsabile si avvalgono per la gestione del sistema di controllo degli impianti termici, tra i quali, a mero titolo esemplificativo e non esaustivo: imprese di manutenzione accreditate nell’ambito del sistema CRITER per la registrazione dei libretti di impianto e dei rapporti di controllo di efficienza energetica; Ispettori/agenti accertatori della Regione Emilia-Romagna ai fini della registrazione dei rapporti di ispezione e dei verbali di accertamento di violazione delle norme vigenti; Fornitori di servizi informatici incaricati dello sviluppo e della manutenzione ordinaria e straordinaria dell’applicativo informatico CRITER.
L’Utente può richiedere l’elenco dei Responsabili del Trattamento contattando il Titolare. Si precisa espressamente come i dati personali forniti dagli Utenti non siano oggetto di trasferimento verso Paesi terzi (Extra UE).


Periodo di conservazione
I dati forniti dagli Utenti sono conservati per un periodo di tempo non superiore a quello strettamente necessario per il perseguimento delle finalità sottese al trattamento medesimo, nel rispetto dei tempi di conservazione stabiliti dalla vigente normativa di legge. In tal senso, si precisa come i dati forniti siano soggetti a controlli periodici volti a verificarne la pertinenza, la non eccedenza e l’indispensabilità avuto riguardo al rapporto, alla prestazione od all'incarico in corso, da instaurare o cessato. I dati che, a seguito di verifica, risultino eccedenti o non pertinenti saranno cancellati, fatta salva l'eventuale conservazione, a norma di legge, dell'atto o del documento che li contiene.


Diritti degli interessati
Conformemente a quanto previsto dagli artt. 15 ss del Reg. UE 2016/679, l’interessato in ogni momento potrà esercitare, nei confronti del Titolare del Trattamento o di ART-ER - Organismo Regionale di Accreditamento ed Ispezione (Responsabile del Trattamento ex art.28 Reg. UE 2016/679), i propri diritti di: accedere ai dati personali ed ottenerne copia [diritto di accesso]; ottenere la rettifica dei dati, qualora risultino inesatti, ovvero l'integrazione dei dati, qualora risultino incompleti [diritto di rettifica];  ottenere la cancellazione dei dati nei casi previsti dall’art. 17 Reg. Ue 2016/679 [diritto di cancellazione]; ottenere la limitazione del trattamento nei casi previsti dall’art. 18 Reg. Ue 2016/679; [diritto di limitazione]; opporsi, in qualsiasi momento, per motivi connessi alla propria situazione particolare, al trattamento dei dati personali che riguardano l’interessato [diritto di opposizione].
L’apposita istanza può essere presentata contattando: 
-	il Titolare del Trattamento: Regione Emilia-Romagna, Ufficio per le relazioni con il pubblico (Urp), per iscritto o recandosi direttamente presso lo sportello Urp. L’Urp è aperto dal lunedì al venerdì dalle 9 alle 13 in Viale Aldo Moro 52, 40127 Bologna (Italia): telefono 800-662200, fax 051-527.5360, e-mail urp@regione.emilia-romagna.it. 
-	Il Responsabile del Trattamento: ART-ER S. cons. p. a. - Organismo Regionale di Accreditamento, con sede legale c/o CNR – Area della Ricerca di Bologna Via P. Gobetti, 101 – 40129 Bologna, mail criter@art-er.it, pec criter.art-er@pec.it.


Diritto di reclamo
L’interessato che ritenga che il trattamento dei dati personali effettuato attraverso l’applicativo CRITER sia posto in essere in violazione di quanto previsto dal Reg. UE 2016/679 ha diritto di proporre reclamo all’Autorità Garante per la protezione dei dati personali, ai sensi dell’art. 77 Reg. UE 2016/679, ovvero di adire le competenti sedi giudiziarie ai sensi dell’art. 79 Reg. UE 2016/679.
                        </asp:TextBox>
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblPrivacyEnteLocale" AssociatedControlID="rblPrivacyEnteLocale" Text="Il dichiarante conferma di aver preso visione dell'informativa al trattamento dei dati personali" />
                        <br />
                        <asp:RadioButtonList ID="rblPrivacyEnteLocale" TabIndex="1" ValidationGroup="vgAnagraficaEnteLocale" runat="server" RepeatDirection="Horizontal" CssClass="selectClass_o">
                            <asp:ListItem Text="Si" Value="1" />
                            <asp:ListItem Text="No" Selected="True" Value="0" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvrblPrivacyEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Consenso alla privacy: campo obbligatorio"
                            ControlToValidate="rblPrivacyEnteLocale">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                               &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessageEnteLocale" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnullaEnteLocale" Visible="false" Text="ANNULLA" CausesValidation="false" TabIndex="1" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button ID="btnProcessEnteLocale" runat="server" ValidationGroup="vgAnagraficaEnteLocale" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessEnteLocale_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagraficaEnteLocale" ValidationGroup="vgAnagraficaEnteLocale" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvPartitaIvaEnteLocale" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" OnServerValidate="ControllaPiva"
                            ErrorMessage="Partita Iva non valida<br/>" />
                        <asp:CustomValidator ID="cvComuneCompetenza" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" OnServerValidate="ControllaComuneCompetenza"
                            ErrorMessage="Selezionare almeno un'area territoriale di competenza<br/>" />
                        <asp:CustomValidator ID="cvEmailEnteLocalePresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" OnServerValidate="ControllaEmailEnteLocalePresente"
                            ErrorMessage="Email già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvEmailPecEnteLocalePresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" OnServerValidate="ControllaEmailPecEnteLocalePresente"
                            ErrorMessage="Email pec già presente nel sistema CRITER<br/>" />
                        <asp:CustomValidator ID="cvPartitaIvaEnteLocalePresente" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="vgAnagraficaEnteLocale" EnableClientScript="true" OnServerValidate="ControllaPartitaIvaEnteLocalePresente"
                            ErrorMessage="Partita Iva già presente nel sistema CRITER<br/>" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcessIspettore" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>