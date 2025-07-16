<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ANAG_AnagraficaAlbo.aspx.cs" Inherits="ANAG_AnagraficaAlbo" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoAzienda" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblSoggetto" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloImpresa" AssociatedControlID="lblImpresa" Text="Impresa di Installazione/Manutenzione" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblImpresa" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzoImpresa" AssociatedControlID="lblIndirizzo" Text="Indirizzo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblIndirizzo" />
                        <asp:Label runat="server" ID="lbliDProvincia" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCap" AssociatedControlID="lblCap" Text="Cap" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCap" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow Visible="false">
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCitta" AssociatedControlID="lblCitta" Text="Città" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" ColumnSpan="3" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCitta" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAmministratoreDelegato" AssociatedControlID="txtAmministratoreDelegato" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtAmministratoreDelegato" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtAmministratoreDelegato" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Amministratore delegato: campo obbligatorio"
                            ControlToValidate="txtAmministratoreDelegato">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfAmministratoreDelegato" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxAmministratoreDelegato" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxAmministratoreDelegato_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTelefono" AssociatedControlID="txtTelefono" />
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
                        <asp:Label runat="server" ID="lblfTelefono" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxTelefono" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxTelefono_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloEmail" AssociatedControlID="txtEmail" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmail" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmail" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Email: campo obbligatorio"
                            ControlToValidate="txtEmail">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email: campo non valido"
                            ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfEmail" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxEmail" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxEmail_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloEmailPec" AssociatedControlID="txtEmailPec" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtEmailPec" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtEmailPec" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="EmailPec: campo obbligatorio"
                            ControlToValidate="txtEmailPec">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailPec" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Email pec: campo non valido"
                            ControlToValidate="txtEmailPec" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfEmailPec" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxEmailPec" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxEmailPec_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloFax" AssociatedControlID="txtFax" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtFax" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtFax" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Fax: campo obbligatorio"
                            ControlToValidate="txtFaX">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revFax" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true"
                            ErrorMessage="Fax: campo non valido"
                            ControlToValidate="txtFax" ValidationExpression="\d{0,50}">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfFax" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxFax" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxFax_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloSitoWeb" AssociatedControlID="txtSitoWeb" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSitoWeb" Width="200" TabIndex="1" MaxLength="50" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtSitoWeb" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Sito Web: campo obbligatorio"
                            ControlToValidate="txtSitoWeb">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfSitoWeb" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxSitoWeb" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxSitoWeb_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPartitaIVA" AssociatedControlID="txtPartitaIVA" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtPartitaIVA" Width="200" TabIndex="1" MaxLength="20" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtPartitaIVA" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="PartitaIVA: campo obbligatorio"
                            ControlToValidate="txtPartitaIVA">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblfPartitaIVA" Text="Visualizza su albo" />
                    </asp:TableCell>
                    <asp:TableCell Width="225" CssClass="riempimento" HorizontalAlign="Center">
                        <dx:ASPxCheckBox runat="server" ID="ASPxCheckBoxPartitaIVA" AutoPostBack="true" OnCheckedChanged="ASPxCheckBoxPartitaIVA_CheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnProcessSave" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnProcessSave_Click" Text="SALVA DATI ANAGRAFICI" />
                        <asp:ValidationSummary ID="vsAnagrafica" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>