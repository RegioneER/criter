<%@ Page Title="Criter - Nuovo Libretto di Impianto" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="LIM_LibrettiImpiantiNuovo.aspx.cs" Inherits="LIM_LibrettiImpiantiNuovo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content runat="server" ContentPlaceHolderID="contentDisplay">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table Width="100%" ID="tblTypeSearch1" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="2" CssClass="riempimento1">
                         NUOVO LIBRETTO DI IMPIANTO
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow runat="server" ID="rowIntestazioneDatiCatastali">
                    <asp:TableCell ColumnSpan="2" Width="100%" CssClass="riempimento">
                        Per inserire un nuovo libretto di impianto, dichiarare di seguito i dati catastali dell'edificio. Il sistema controllerà se i dati sono già presenti nel catasto regionale degli impianti termici.<br/><br />
                        Ai fini della registrazione del libretto di impianto nel sistema CRITER i dati relativi ai riferimenti catastali dell’immobile (dati catastali) e i codici  POD (Punto riconsegna energia elettrica) e PDR (Punto di riconsegna del combustibile) sono obbligatori.<br /> 
                        Ai sensi della normativa vigente questi dati devono essere resi disponibili dal Responsabile di impianto all’impresa di installazione/manutenzione di impianti termici incaricata della registrazione del libretto di impianto.<br /><br /> 
                        Qualora tali non siano resi disponibili inserire i seguenti codici nei rispettivi campi:<br /> 
                        <b>Dati Catastali</b>: Foglio 0, Mappale 0, Subalterno 0<br />
                        <b>Numero punto riconsegna combustibile (PDR)</b>: 00000000000000 (14 cifre=0)<br />
                        <b>Numero punto riconsegna energia elettrica (POD)</b>: 00000000000000 (14 cifre=0)
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                        Azienda (*)
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAziende" TabIndex="1"
                            Theme="Default"
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
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox1"
                            ValidationGroup="vgInfoGeneraliLibrettoImpianto"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Azienda: campo obbligatorio"
                            ControlToValidate="cmbAziende">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                        Operatore/Addetto (*)
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAddetti" Theme="Default" TabIndex="1"
                            TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32" AutoPostBack="true"
                            Width="350px" OnSelectedIndexChanged="ASPxComboBox2_OnSelectedIndexChanged"
                            DropDownWidth="350px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox2"
                            ValidationGroup="vgInfoGeneraliLibrettoImpianto"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Operatore/Addetto: campo obbligatorio"
                            ControlToValidate="cmbAddetti">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                        Codice Targatura Impianto (*)
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbTargature" Theme="Default" TabIndex="1"
                            TextField="CodiceTargatura" ValueField="IDTargaturaImpianto" ValueType="System.String"
                            Width="350px" EnableCallbackMode="true"
                            DropDownWidth="350px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxcmbTargature"
                            ValidationGroup="vgInfoGeneraliLibrettoImpianto"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Codice targatura impianto: campo obbligatorio"
                            ControlToValidate="cmbTargature">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDTargaturaImpianto" runat="server" Visible="false" />
                        <asp:Label ID="lblCodiceTargatura" runat="server" ForeColor="Green" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblComune" Text="Comune (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Panel ID="pnlComuni" runat="server">
                            <asp:Table ID="tblCodiceCatastale" Width="500" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="250">
                                        <dx:ASPxComboBox AutoPostBack="true" ID="RadComboBoxCodiciCatastali" runat="server" TabIndex="1"
                                            Theme="Default" EnableViewState="False"
                                            EnableCallbackMode="true"
                                            CallbackPageSize="30"
                                            IncrementalFilteringMode="Contains"
                                            ValueType="System.String"
                                            ValueField="IDCodiceCatastale"
                                            OnItemsRequestedByFilterCondition="comboComune_OnItemsRequestedByFilterCondition"
                                            OnItemRequestedByValue="comboComune_OnItemRequestedByValue"
                                            OnButtonClick="comboComune_ButtonClick"
                                            OnSelectedIndexChanged="RadComboBoxCodiciCatastali_OnSelectedIndexChanged"
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
                                        <asp:RequiredFieldValidator ID="rfvRadComboBoxCodiciCatastali" ValidationGroup="vgInfoGeneraliLibrettoImpianto" 
                                            ForeColor="Red" Display="Dynamic"
                                            runat="server" 
                                            InitialValue="" 
                                            ErrorMessage="Comune: campo obbligatorio"
                                            ControlToValidate="RadComboBoxCodiciCatastali">&nbsp;*</asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Label ID="lblIDCodiceCatastale" runat="server" Visible="false" />
                        <asp:Label ID="lblCodiceCatastale" runat="server" />
                        <asp:Label ID="lblComuneCodiceCatastale" runat="server" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="250" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDatiCatastali" Text="Dati catastali (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="700" CssClass="riempimento">
                        <asp:Panel runat="server" ID="pnlInsertDatiCatastali" CssClass="testo0" HorizontalAlign="Right">
                            <asp:LinkButton ID="lnkInsertDatiCatastali" runat="server" Text="Aggiungi dati catastali"
                                OnClick="lnkInsertDatiCatastali_Click"
                                Visible="true" Enabled="false" CausesValidation="false" TabIndex="1" />
                        </asp:Panel>
                        <asp:Panel ID="pnlDatiCatastaliInsert" Visible="false" runat="server">
                            <br />
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
                                        <asp:TextBox ID="txtFoglio" runat="server" CssClass="txtClass" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
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
                                        <asp:TextBox ID="txtMappale" runat="server" CssClass="txtClass" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
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
                                        <asp:TextBox ID="txtSubalterno" runat="server" CssClass="txtClass" ValidationGroup="vgDatiCatastali" Width="100" MaxLength="5" />&nbsp;
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
                                        <asp:CustomValidator ID="cvDatiCatastali" runat="server" EnableClientScript="true"
                                            OnServerValidate="ControllaDatiCatastaliCoerenza" CssClass="testoerr" ValidationGroup="vgDatiCatastali"
                                            ErrorMessage="<br />Attenzione: i dati catastali non sono coerenti!<br />" />

                                        <asp:CustomValidator ID="cvDatiCatastaliSigmater" runat="server" CssClass="testoerr" ErrorMessage="Dati catastali non presenti nel catasto Sigmater"
                                            Display="Dynamic" ValidationGroup="vgDatiCatastali" EnableClientScript="true"
                                            OnServerValidate="ControllaDatiCatastaliSigmater">Dati catastali non presenti nel catasto Sigmater</asp:CustomValidator>

                                        <asp:CustomValidator ID="cvDatiCatastaliPresenti" Enabled="false" runat="server" ErrorMessage=""
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
                                            <cc1:ModalPopupExtender ID="dmpDatiCatastali" DropShadow="true" runat="server" Enabled="false" 
                                                TargetControlID="btnSaveDatiCatastali"
                                                PopupControlID="pnlDatiCatastaliConfirm" BackgroundCssClass="modalBackground" />
                                            <asp:Panel ID="pnlDatiCatastaliConfirm" runat="server" Style="display: none; width: 400px; height:200px; background-color: White; border-width: 1px; border-color: Black; border-style: solid;">
                                                <asp:Label runat="server" ID="lblDatiCatastaliMessage" />
                                                <br />
                                                <br />
                                                <div style="text-align: center;">
                                                    <asp:Button ID="btnSaveDatiCatastaliConfirm" runat="server" CommandName="ConfirmSaveDatiCatastali" OnClick="btnSaveDatiCatastali_Click" CssClass="buttonClass" Width="100" Text="Ok" />
                                                    <asp:Button ID="btnSaveDatiCatastaliCancel" runat="server" OnClick="btnSaveDatiCatastaliCancel_Click" CssClass="buttonClass" Width="100" Text="Annulla" />
                                                </div>
                                            </asp:Panel>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlDatiCatastaliView" Visible="false" runat="server">
                            <br />
                            <asp:Label runat="server" ID="lblCount" Visible="false" />
                            <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="530px" GridLines="None"
                                CellSpacing="1" CellPadding="3" PageSize="10" UseAccessibleHeader="true"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDDatiCatastali">
                                <HeaderStyle CssClass="riempimento2" HorizontalAlign="Center" />
                                <ItemStyle CssClass="GridItem" />
                                <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDDatiCatastali" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDSezione" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" DataField="Sezione" HeaderText="Sezione" />
                                    <asp:BoundColumn ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="Foglio" HeaderText="Foglio" />
                                    <asp:BoundColumn ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="Mappatura" HeaderText="Mappale" />
                                    <asp:BoundColumn ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataField="SubMappatura" HeaderText="Subalterno" />
                                    <asp:BoundColumn ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center" DataField="Identificativo" HeaderText="Identificativo" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="Cancella dato Catastale" AlternateText="Cancella dato Catastale" ImageUrl="~/images/Buttons/delete.png" OnClientClick="javascript:return confirm('Confermi la cancellazione del dato catastale?')"
                                                OnCommand="RowCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDDatiCatastali") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="950" HorizontalAlign="Center" ColumnSpan="2">
                        <div>
                            <asp:CustomValidator ID="cvControllaPresenzaAlmenoUnDatoCatastale" runat="server" EnableClientScript="true"
                                OnServerValidate="ControllaPresenzaAlmenoUnDatoCatastale" CssClass="testoerr" ValidationGroup="vgInfoGeneraliLibrettoImpianto"
                                ErrorMessage="Attenzione: inserire almeno un dato catastale." />
                        </div>
                        <%--                         <div><asp:CustomValidator ID="cvControllaDatiCatastaliPresentiAllInserimento" runat="server" EnableClientScript="true"
                                                OnServerValidate="ControllaDatiCatastaliPresentiAllInserimento" CssClass="testoerr" ValidationGroup="vgInserimento" 
                                                ErrorMessage="Attenzione: è presente un libretto di impianto avente lo stesso Comune, Foglio, Subalterno!" />
                        </div>--%>
                        <div>
                            <asp:Button TabIndex="1" CssClass="buttonClass" OnClick="btnConfermaInserimento_Click" Enabled="true" CausesValidation="True"
                                Text="CONFERMA INSERIMENTO LIBRETTO IMPIANTO" runat="server" ValidationGroup="vgInfoGeneraliLibrettoImpianto" ID="btnConfermaInserimento" Width="300px" />
                        </div>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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
