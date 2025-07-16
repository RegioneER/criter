<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_ProgrammaAccertamento.aspx.cs" Inherits="VER_ProgrammaAccertamento" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v16.2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=cbAttivo.ClientID %>").checked == true) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:Table ID="tblProgrammaIspezione" Width="895px" runat="server" CssClass="TableClass">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1" Width="890px">
                 <asp:Label runat="server" ID="lblTitoloPagina" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Table ID="tblPanel1" runat="server" Width="890">
                    <asp:TableRow ID="rowGrid">
                        <asp:TableCell ColumnSpan="2">
                            <dx:ASPxGridView ID="GridProgrammaAccertamento" runat="server" AutoGenerateColumns="false" KeyFieldName="IDProgrammaAccertamento" 
                                Width="100%" Styles-Cell-HorizontalAlign="Center"
                                OnHtmlRowCreated="GridProgrammaAccertamento_HtmlRowCreated"
                                OnPageIndexChanged="GridProgrammaAccertamento_PageIndexChanged"
                                ClientInstanceName="GridProgrammaAccertamento">
                                <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                    Row-CssClass="GridItem"
                                    Header-CssClass="GridHeader" />
                                <SettingsBehavior AllowFocusedRow="true"
                                    ProcessFocusedRowChangedOnServer="false"
                                    AllowSort="false" />
                                <SettingsPager PageSize="5" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="IDProgrammaAccertamento" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="Descrizione" />
                                    <dx:GridViewDataColumn FieldName="DataInizio" />
                                    <dx:GridViewDataColumn FieldName="DataFine" />
                                    <dx:GridViewDataCheckColumn FieldName="fAttivo" Caption="Attivo">
                                        <DataItemTemplate>
                                            <dx:ASPxImage runat="server" ID="imgfAttivo" OnInit="imgfAttivo_Init" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataCheckColumn>
                                    <dx:GridViewDataColumn Caption="Accertamenti">
                                        <DataItemTemplate>
                                            <asp:Label ID="lblCountAccertamenti" runat="server" ForeColor="#000000" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn>
                                        <DataItemTemplate>
                                            <asp:ImageButton ID="btnModificaProgrammaAccertamento" runat="server" ImageUrl="~/images/buttons/editSmall.png" OnClick="btnModificaProgrammaAccertamento_Click" ToolTip="Modifica Programma Accertamento" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn>
                                        <DataItemTemplate>
                                            <asp:ImageButton ID="btnProgrammaAccertamentoDetails" runat="server" ImageUrl="~/images/buttons/detailsSmall.png" ToolTip="Visualizza dettaglio Accertamenti nel programma" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn>
                                        <DataItemTemplate>
                                            <asp:ImageButton ID="btnProgrammaAccertamentoDelete" runat="server" ImageUrl="~/images/buttons/deleteSmall.png" ToolTip="Cancella Programma Accertamento"
                                                OnClientClick="if (!confirm('Confermi eliminazione del programma accertamento?')) return false;"
                                                OnCommand="RowCommandProgrammaAccertamento" CommandName="DeleteProgrammaAccertamento" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDProgrammaAccertamento") %>'/>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                            </dx:ASPxGridView>
                            <dx:ASPxPopupControl ID="WindowProgrammaAccertamento" runat="server" CloseAction="OuterMouseClick"
                                        Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ClientInstanceName="WindowProgrammaAccertamento" HeaderText="Accertamenti nel Programma"
                                        AllowDragging="True" PopupAnimationType="Fade" Width="905px" Height="600px" AllowResize="True">
                                    </dx:ASPxPopupControl>
                                    <script type="text/javascript">
                                        function OpenDetailsProgrammaAccertamento(element, key)
                                        {
                                            var url = 'VER_ProgrammaAccertamentoDetails.aspx?IDProgrammaAccertamento=' + key;
                                            WindowProgrammaAccertamento.SetContentUrl(url)
                                            WindowProgrammaAccertamento.Show();
                                        }
                                    </script>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowButtonNuovoProgrammaAccertamento">
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:Button ID="btnNuovoProgrammaAccertamento" runat="server" TabIndex="1" CssClass="buttonClass" Width="280"
                                OnClick="btnNuovoProgrammaAccertamento_Click" Text="NUOVO PROGRAMMA ACCERTAMENTO" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="tblPanel2" runat="server" Width="890" CssClass="TableClass" HorizontalAlign="Center" Visible="false">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                            <asp:Label ID="lbltitoloPanel2" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDescrizione" CssClass="riempimento" Width="500">
                        <asp:TableCell CssClass="riempimento2" Width="150">
                            <asp:Label ID="lblDescrizione" Text="Descrizione" runat="server" Width="100" />
                        </asp:TableCell>
                        <asp:TableCell Width="350">
                            <asp:TextBox ID="txtDescrizione" Width="350" runat="server" CssClass="txtClass" TextMode="MultiLine" Height="50" />
                            <asp:RequiredFieldValidator
                                ID="rfvDescrizione" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true"
                                ErrorMessage="Descrizione: campo obbligatorio"
                                ControlToValidate="txtDescrizione">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDataInizio" CssClass="riempimento">
                        <asp:TableCell CssClass="riempimento2">
                            <asp:Label ID="lblDataInizio" Text="Data inizio  (gg/mm/aaaa)" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDataInizio" Width="150" runat="server" CssClass="txtClass" />
                            <asp:RequiredFieldValidator
                                ID="rfvDataInizio" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true" ErrorMessage="Data Inizio: campo obbligatorio"
                                ControlToValidate="txtDataInizio">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDataInizio" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true"
                                ErrorMessage="Data Inizio: campo non valido"
                                ControlToValidate="txtDataInizio" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))">&nbsp;*</asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDataFine" CssClass="riempimento">
                        <asp:TableCell CssClass="riempimento2">
                            <asp:Label ID="lblDataFine" Text="Data fine  (gg/mm/aaaa)" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDataFine" Width="150" runat="server" CssClass="txtClass" />
                            <asp:RequiredFieldValidator
                                ID="rfvDataFine" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true" ErrorMessage="Data Fine: campo obbligatorio"
                                ControlToValidate="txtDataFine">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDataFine" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true"
                                ErrorMessage="Data Fine: campo non valido"
                                ControlToValidate="txtDataFine" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))">&nbsp;*</asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowAttivo" CssClass="riempimento">
                        <asp:TableCell CssClass="riempimento2">
                            <asp:Label ID="lblAttivo" Text="Attivo ( SI / NO )" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="cbAttivo" runat="server" Checked="true" />
                            <asp:CustomValidator ID="cvAttivo" runat="server" ClientValidationFunction="ValidateCheckBox" EnableClientScript="true" ErrorMessage="Attivo: Può essere attiva solo una programma accertamento"
                                ValidationGroup="vgtbPanel2">&nbsp;*</asp:CustomValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowButtonsP2" HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="2">
                            <br />
                            <asp:Button ID="btnSalvaDati" TabIndex="1" runat="server" CssClass="buttonClass" Width="200"
                                OnClick="btnSalvaDati_Click" Text="SALVA DATI" CausesValidation="True" ValidationGroup="vgtbPanel2" />&nbsp;                                                          
                            <asp:Button ID="btnAnnullaDati" runat="server" TabIndex="1" CssClass="buttonClass" Width="200"
                                OnClick="btnAnnullaDati_Click" Text="ANNULLA" />&nbsp;
                            <asp:ValidationSummary ID="vgtbPanel2" ValidationGroup="vgtbPanel2" runat="server" CssClass="testoerr" ShowMessageBox="True"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="890px">
                <dx:ASPxPageControl ID="pageControl" runat="server" AutoPostBack="false" ActiveTabIndex="0" EnableCallBacks="false" Width="880px" Border-BorderColor="Black">
                    <TabPages>
                        <dx:TabPage Text="RICERCA ACCERTAMENTI" TabStyle-Width="50%" TabStyle-Font-Size="Smaller">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <asp:Table runat="server" Width="870">
                                        <asp:TableRow>
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                                                    AutoPostBack="false"
                                                    EnableCallbackMode="true"
                                                    CallbackPageSize="30"
                                                    IncrementalFilteringMode="Contains"
                                                    ValueType="System.Int32"
                                                    ValueField="IDSoggetto"
                                                    OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                                                    OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
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
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow runat="server" ID="rowCodiceIspezione">
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceIspezione" AssociatedControlID="txtCodiceIspezione" Text="Codice ispezione" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <asp:TextBox runat="server" ID="txtCodiceIspezione" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" ID="rowDataIspezione">
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="lblTitoloDataIspezione" Text="Data ispezione (gg/mm/aaaa)" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIspezioneDa" />
                                                <asp:RegularExpressionValidator
                                                    ID="revDataIspezioneDa" ControlToValidate="txtDataIspezioneDa" ForeColor="Red" ErrorMessage=""
                                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                &nbsp;&nbsp;
						                        a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIspezioneAl" />
                                                <asp:RegularExpressionValidator
                                                    ID="revDataIspezioneAl" ControlToValidate="txtDataIspezioneAl" ForeColor="Red" ErrorMessage=""
                                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceAccertamento" AssociatedControlID="txtCodiceTargatura" Text="Codice accertamento" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <asp:TextBox runat="server" ID="txtCodiceAccertamento" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="lblTitoloDataRilevazione" Text="Data accertamento (gg/mm/aaaa)" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRilevazioneDa" />
                                                <asp:RegularExpressionValidator
                                                    ID="revDataRilevazioneDa" ControlToValidate="txtDataRilevazioneDa" ForeColor="Red" ErrorMessage=""
                                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                &nbsp;&nbsp;
						                        a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRilevazioneAl" />
                                                <asp:RegularExpressionValidator
                                                    ID="revDataRilevazioneAl" ControlToValidate="txtDataRilevazioneAl" ForeColor="Red" ErrorMessage=""
                                                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" ID="rowCriticita">
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="lblTitoloCriticita" AssociatedControlID="chkCriticita" Text="Non conformità gravi" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <asp:CheckBox runat="server" ID="chkCriticita" AutoPostBack="true" OnCheckedChanged="chkCriticita_CheckedChanged" Text="&nbsp;SI" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" ID="rowLivelloGravitaNC" Visible="false">
                                            <asp:TableCell Width="300" CssClass="riempimento2">
                                                <asp:Label runat="server" ID="lblTitoloLivelloGravitaNC" AssociatedControlID="chkCriticita" Text="Livello gravità NC" />
                                            </asp:TableCell>
                                            <asp:TableCell Width="570" CssClass="riempimento">
                                                <asp:CheckBoxList runat="server" ID="cblLivelloGravitaNC" CssClass="checkboxlistClass" RepeatColumns="2" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="880" ColumnSpan="2" CssClass="riempimento5">
                                                 &nbsp;
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5" Width="880px">
                                                <asp:Button runat="server" ID="btnRicerca" Text="RICERCA ACCERTAMENTI" OnClick="btnRicerca_Click" CssClass="buttonClass" Width="300px" TabIndex="1" />
                                                &nbsp;
                                                <asp:Button ID="btnInserireAccertamenti" runat="server" TabIndex="1" CssClass="buttonClass" Width="300px"
                                                    OnClick="btnInserireAccertamenti_Click" Text="INSERISCI NEL PROGRAMMA ACCERTAMENTO" Visible="false" UseSubmitBehavior="false"
                                                    OnClientClick="if (confirm('Confermi inserire accertamento/i nel programma accertamento')) {disableBtn(this.id, 'Attendere, inserimento in corso...')} else {return false};" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="880" ColumnSpan="2" CssClass="riempimento5">
                                                 &nbsp;
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="880" ColumnSpan="2" CssClass="riempimento5">
                                                <div class="Divfixed">
                                                        <asp:Label runat="server" ID="lblCountSelezionati" Font-Bold="true" Text="0 - ACCERTAMENTI SELEZIONATI" />
                                                </div>
                                                <asp:Label runat="server" ID="lblCount" Visible="false" />&nbsp;
                                                <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="865px" GridLines="None" HorizontalAlign="Center"
                                                    CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="20" AllowSorting="True" AllowPaging="True"
                                                    AutoGenerateColumns="False" runat="server" DataKeyField="IDAccertamento"
                                                    OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridItem" />
                                                    <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDRapportoDiControlloTecnicoBase" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDSoggetto" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDStatoAccertamento" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDUtenteAccertatore" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDUtenteCoordinatore" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="CodiceAccertamento" Visible="false" ReadOnly="True" />
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Accertamenti">
                                                            <ItemTemplate>
                                                                <asp:Table ID="tblInfoAccertamenti" Width="700" runat="server">
                                                                    <asp:TableRow Width="700" Visible='<%# (int)Eval("IDTipoAccertamento") == 1 %>'>
                                                                        <asp:TableCell Width="100">
                                                                               <b>Azienda:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" Width="230">
                                                                            <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("NomeAzienda") %>' />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                              &nbsp;
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" Width="230">
                                                                           &nbsp;
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Width="700">
                                                                        <asp:TableCell Width="100">
                                                                               <b>Codice targatura:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Width="330">
                                                                            <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Width="700">
                                                                        <asp:TableCell Width="100">
                                                                              <b>Dati&nbsp;impianto:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="230">
                                                                             <%# Eval("Indirizzo") %>&nbsp;<%# Eval("Civico") %>&nbsp;<%# Eval("Comune") %>&nbsp;(<%# Eval("SiglaProvincia") %>)
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                                 <b>Generatore:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                            <asp:Label ID="lblGeneratorePrefisso" runat="server" Text='<%#Eval("Prefisso") %>' />
                                                                            <asp:Label ID="lblGeneratoreCodiceProgressivo" runat="server" Text='<%#Eval("CodiceProgressivo") %>' />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Width="700">
                                                                        <asp:TableCell Width="100">
                                                                                <b>Codice&nbsp;accertamento:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" Width="330">
                                                                            <asp:HyperLink runat="server" ID="lnkAccertamento" Text='<%# Eval("CodiceAccertamento") %>' />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                                  <b>Data&nbsp;accertamento:&nbsp;</b>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" Width="330">
                                                                               <asp:Label ID="lblDataAccertamento" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataRilevazione")) %>' />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                            <asp:Label runat="server" ID="lblDescrizioneDataControllo" Font-Bold="true" Text="Data&nbsp;controllo:&nbsp;" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                            <asp:Label ID="lblDataControllo" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataControllo")) %>' />                                                                 
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Width="700">
                                                                        <asp:TableCell Width="100">
                                                                            <asp:Label runat="server" ID="lblDescrizioneRctIspezione" Font-Bold="true" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                                            <asp:HyperLink runat="server" ID="lnkRctIspezione" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                            <asp:Label runat="server" ID="lblDescrizionePunteggioAccertamento" Font-Bold="true" Text="Livello gravità NC:&nbsp;" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                                             <asp:Label runat="server" Text='<%# Eval("PunteggioNCAccertamento") %>' />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    
                                                                    <asp:TableRow Width="700" runat="server" ID="rowDatiIspezioneResult">
                                                                        <asp:TableCell Width="100">
                                                                            <asp:Label runat="server" ID="lblDescrizioneIspettore" Font-Bold="true" Text="Ispettore:&nbsp;" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                                            <asp:Label runat="server" ID="lblIspettore" Text='<%# Eval("Ispettore") %>' />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="100">
                                                                            <asp:Label runat="server" ID="lblDataIspezione" Font-Bold="true" Text="Data Ispezione:&nbsp;" />
                                                                        </asp:TableCell>
                                                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                                             <asp:Label runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataIspezione")) %>' />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>

                                                                    <asp:TableRow>
                                                                        <asp:TableCell ColumnSpan="6">
                                                                            <cc1:Accordion ID="Accordion4"
                                                                                runat="server"
                                                                                SelectedIndex="1"
                                                                                AutoSize="None"
                                                                                FadeTransitions="true"
                                                                                TransitionDuration="250"
                                                                                FramesPerSecond="40"
                                                                                RequireOpenedPane="false"
                                                                                SuppressHeaderPostbacks="true">
                                                                                <Panes>
                                                                                    <cc1:AccordionPane ID="AccordionPane4" HeaderCssClass="riempimento1" runat="server">
                                                                                        <Header>
                                                                                            <div style="cursor: pointer; border: solid; border-width: 1px; width: 99%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                                                                               Osservazioni
                                                                                            </div>
                                                                                        </Header>
                                                                                        <Content>
                                                                                           <asp:TextBox ID="txtOsservazioni" runat="server" Enabled="false" TextMode="MultiLine" Width="99%" CssClass="txtClass" Height="80px" />
                                                                                        </Content>
                                                                                    </cc1:AccordionPane>
                                                                                </Panes>
                                                                            </cc1:Accordion>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>

                                                                    <asp:TableRow>
                                                                        <asp:TableCell ColumnSpan="6">
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
                                                                                            <div style="cursor: pointer; border: solid; border-width: 1px; width: 99%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                                                                               Raccomandazioni
                                                                                            </div>
                                                                                        </Header>
                                                                                        <Content>
                                                                                           <asp:TextBox ID="txtRaccomandazioni" runat="server" Enabled="false" TextMode="MultiLine" Width="99%" CssClass="txtClass" Height="80px" />
                                                                                        </Content>
                                                                                    </cc1:AccordionPane>
                                                                                </Panes>
                                                                            </cc1:Accordion>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow>
                                                                        <asp:TableCell ColumnSpan="6">
                                                                            <cc1:Accordion ID="Accordion3"
                                                                                runat="server"
                                                                                SelectedIndex="1"
                                                                                AutoSize="None"
                                                                                FadeTransitions="true"
                                                                                TransitionDuration="250"
                                                                                FramesPerSecond="40"
                                                                                RequireOpenedPane="false"
                                                                                SuppressHeaderPostbacks="true">
                                                                                <Panes>
                                                                                    <cc1:AccordionPane ID="AccordionPane3" HeaderCssClass="riempimento1" runat="server">
                                                                                        <Header>
                                                                                             <div style="cursor: pointer; border: solid; border-width: 1px; width: 99%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                                                                                Prescrizioni
                                                                                            </div>
                                                                                        </Header>
                                                                                        <Content>
                                                                                            <asp:TextBox ID="txtPrescrizioni" runat="server" Enabled="false" TextMode="MultiLine" Width="99%" CssClass="txtClass" Height="80px" />
                                                                                        </Content>
                                                                                    </cc1:AccordionPane>
                                                                                </Panes>
                                                                            </cc1:Accordion>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow runat="server" ID="rowNonConformitaResult">
                                                                        <asp:TableCell ColumnSpan="6">
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
                                                                                            <div style="cursor: pointer; border: solid; border-width: 1px; width: 99%; border-color: #000000; display: flex; justify-content: center; align-items: center; height: 26px; margin-bottom: 5px; font-weight: bold !important; font-size: 0.92em !important; color: Black !important; background-color: #ffcc3d !important;">
                                                                                                Non Conformità
                                                                                            </div>
                                                                                        </Header>
                                                                                        <Content>
                                                                                            <asp:GridView ID="GridNC" runat="server" Width="99%" AllowPaging="false" AutoGenerateColumns="false">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="Punteggio" HeaderText="Punteggio" ItemStyle-HorizontalAlign="Center" />
                                                                                                    <asp:BoundField DataField="NonConformita" HeaderText="Non Conformità" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </Content>
                                                                                    </cc1:AccordionPane>
                                                                                </Panes>
                                                                            </cc1:Accordion>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                    <asp:TableRow Width="700" runat="server" ID="rowAccertamentoInCorso" Visible="false">
                                                                        <asp:TableCell Width="100" ColumnSpan="4">
                                                                            <asp:Label runat="server" ID="lblAccertamentoInCorso" Text="Attenzione esiste un accertamento in corso" ForeColor="Red" Font-Bold="true" />
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox runat="server" ID="chkSelezioneAllAccertamenti" OnCheckedChanged="chkSelezioneAllAccertamenti_CheckedChanged" AutoPostBack="true" ToolTip="Seleziona tutti gli Accertamenti" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkSelezioneAccertamenti" OnCheckedChanged="chkSelezioneAccertamenti_CheckedChanged" AutoPostBack="true" TabIndex="1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="Raccomandazioni" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="Prescrizioni" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="Osservazioni" Visible="false" ReadOnly="True" />

                                                        <asp:BoundColumn DataField="IDTipologiaRCT" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDSoggettoDerived" Visible="false" ReadOnly="True" />
                                                        <asp:BoundColumn DataField="IDIspezione" Visible="false" ReadOnly="True" />
                                                    </Columns>
                                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="CREA E ASSEGNA PACCHETTI DEGLI ACCERTAMENTI" TabStyle-Width="50%" TabStyle-Font-Size="Smaller">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <br />
                                    <asp:Table ID="tblVititeIspezione" runat="server" Width="100%" CssClass="">

                                        <asp:TableRow>
                                            <asp:TableCell Width="880px" ColumnSpan="2" CssClass="riempimento5">
                                                <dx:ASPxGridView ID="GridViewVisita" runat="server" ClientInstanceName="GridViewVisita" 
                                                    KeyFieldName="IDAccertamentoVisita" EnableRowsCache="False"
                                                    OnHtmlRowCreated="GridViewVisita_HtmlRowCreated"
                                                    OnHtmlRowPrepared="GridViewVisita_HtmlRowPrepared"
                                                    OnFocusedRowChanged="GridViewVisita_FocusedRowChanged"
                                                    OnBeforePerformDataSelect="GridViewVisita_BeforePerformDataSelect"
                                                    Width="100%" DataSourceID="dsPacchetti" EnableCallBacks="false" Font-Size="7">
                                                    <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                                        Row-CssClass="GridItem"
                                                        Header-CssClass="GridHeader"
                                                        DetailRow-BackColor="WhiteSmoke" />
                                                    <SettingsBehavior AllowFocusedRow="true"
                                                        ProcessFocusedRowChangedOnServer="true"
                                                        AllowSelectSingleRowOnly="true"
                                                        AllowSelectByRowClick="true"
                                                        AllowSort="true" />
                                                    <SettingsPager PageSize="15" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="IDAccertamentoVisita" CellStyle-HorizontalAlign="Center" Width="100" Caption="Codice Pacchetto" />
                                                        <dx:GridViewDataColumn FieldName="DataCreazione" Caption="Data Creazione" Width="100" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataColumn Caption="Accertamenti" Width="80px" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:Label ID="lblCountAccertamentiNelPacchetto" runat="server" ForeColor="#000000" Width="50px" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataTextColumn Caption="Assegnato" Width="150px" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <asp:Image runat="server" ID="imgfAssegnato" />
                                                                <asp:Label ID="lblAccertatore" runat="server" Visible="false" ForeColor="Green" Font-Bold="true" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn Caption="Seleziona Pacchetto" ShowSelectCheckbox="true" Width="50px" />
                                                        <dx:GridViewDataColumn Width="25px">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgAssegnaPacchetto" Visible="false" TabIndex="1" AlternateText="Assegnazione Pacchetto" 
                                                                    ToolTip="Assegnazione Pacchetto" ImageUrl="~/images/buttons/AssegnaSmall.png" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Width="25px">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgDeletePacchetto" Visible="false" TabIndex="1" AlternateText="Elimina Pacchetto" ToolTip="Elimina Pacchetto" ImageUrl="~/images/buttons/deleteSmall.png" OnClientClick="if (!confirm('Confermi eliminazione del pacchetto?')) return false;"
                                                                    OnCommand="RowCommandPacchetto" CommandName="DeletePacchetto" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamentoVisita") %>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <DetailRow>
                                                            <dx:ASPxGridView ID="grdAccertamentiNelPacchetto" runat="server" DataSourceID="dsAccertamentiNelPacchetto"
                                                                Width="100%" EnableRowsCache="False"
                                                                KeyFieldName="IDAccertamentoVisitaInfo"
                                                                OnBeforePerformDataSelect="grdAccertamentiNelPacchetto_BeforePerformDataSelect">
                                                                <Styles Row-CssClass="GridItem" AlternatingRow-CssClass="GridAlternativeItem" />
                                                                <Settings ShowColumnHeaders="false" />
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn VisibleIndex="0">
                                                                        <EditFormSettings Visible="False" />
                                                                        <DataItemTemplate>
                                                                            <asp:Table runat="server" Font-Size="8" Width="100%" CssClass="TableClass">
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell Width="140px">
                                                                                        <asp:Label runat="server" Text="Codice targatura:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Width="370px">
                                                                                        <%# Eval("CodiceTargatura").ToString() %>
                                                                                        <br />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Width="140px">
                                                                                        <asp:Label runat="server" Text="Codice accertamento:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Width="370px">
                                                                                        <%# Eval("CodiceAccertamento").ToString() %>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell>
                                                                                        <asp:Label runat="server" Text="Nome Azienda:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <%# Eval("NomeAzienda").ToString() %>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                        <asp:Label runat="server" Text="Livello gravità NC:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <%# Eval("PunteggioNCAccertamento") %>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell Width="25px" HorizontalAlign="Right">
                                                                                    &nbsp;
                                                                                    <asp:ImageButton runat="server" CssClass="riempimento5" ImageUrl="~/images/buttons/deleteSmall.png" OnCommand="CommandButtonAccertamenti" CommandName="DeleteAccertamento" ToolTip="ELIMINA DAL PACCHETTO"
                                                                                            CommandArgument='<%# Eval("IDAccertamentoVisita") +","+ Eval("IDAccertamentoProgramma") +","+ Eval("IDAccertamento") %>' OnClientClick="javascript:return confirm('Confermi eliminazione Accertamento dal pacchetto?')"
                                                                                            Visible='<%# !bool.Parse(Eval("fPacchettoAssegnato").ToString()) %>' />
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell>
                                                                                        <asp:Label runat="server" Text="Potenza:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <%# Eval("PotenzaTermicaNominale").ToString() %>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <asp:Label runat="server" Text="Generatore:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <%# Eval("Prefisso") + Eval("CodiceProgressivo").ToString() %>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell>
                                                                                        <asp:Label runat="server" Text="Indirizzo:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                        <%# Eval("Indirizzo").ToString() %>
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <asp:Label runat="server" Text="Comune:" Font-Bold="true" />
                                                                                    </asp:TableCell>
                                                                                    <asp:TableCell>
                                                                                         <%# Eval("CodiceCatastale") + " - " + Eval("Comune").ToString() %>
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                            </asp:Table>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </DetailRow>
                                                    </Templates>
                                                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" />
                                                </dx:ASPxGridView>
                                                
                                                <dx:ASPxPopupControl ID="WindowProgrammaAccertamentoAssegnazione" runat="server" CloseAction="CloseButton"
                                                    Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                                    ClientInstanceName="WindowProgrammaAccertamentoAssegnazione" HeaderText="Assegnazione Accertamenti"
                                                    AllowDragging="True" PopupAnimationType="Fade" Width="905px" OnWindowCallback="WindowProgrammaAccertamentoAssegnazione_WindowCallback" Height="600px" AllowResize="True">
                                                    
                                                </dx:ASPxPopupControl>
                                                <script type="text/javascript">
                                                    function OpenDetailsProgrammaAccertamentoAssegnazione(element, key) {
                                                        var url = 'VER_ProgrammaAccertamentoAssegnazione.aspx?IdAccertamentoPacchetto=' + key;
                                                        WindowProgrammaAccertamentoAssegnazione.SetContentUrl(url)
                                                        WindowProgrammaAccertamentoAssegnazione.Show();
                                                    }
                                                </script>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                                <asp:Button ID="CreaPacchetto" runat="server" TabIndex="1" CssClass="buttonClass" Width="200" Text="NUOVO PACCHETTO" OnClick="CreaPacchetto_Click" OnClientClick="javascript:return confirm('Confermi creazione nuovo pacchetto?')" />
                                                <br />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell VerticalAlign="Top" ColumnSpan="2">
                                                <br />
                                                <dx:ASPxGridView ID="GridViewListaNelProgramma" ClientInstanceName="GridViewListaNelProgramma" runat="server" AutoGenerateColumns="false"
                                                    KeyFieldName="IDAccertamento" Width="100%" Font-Size="7" EnableCallBacks="false">
                                                    <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                                        Row-CssClass="GridItem"
                                                        Header-CssClass="GridHeader"
                                                        FilterCell-CssClass="GridAlternativeItem" />
                                                    <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                                                    <Settings ShowFilterRow="true" AutoFilterCondition="Contains" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="IDAccertamentoProgramma" Visible="false" />
                                                        <dx:GridViewDataTextColumn FieldName="IDAccertamento" Visible="false" />
                                                        <dx:GridViewDataTextColumn FieldName="NomeAzienda" Width="235px" Caption="Impresa" VisibleIndex="0" />
                                                        <dx:GridViewDataTextColumn FieldName="CodiceCatastale" Width="180px" Caption="Comune" VisibleIndex="1" />
                                                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="CodiceTargatura" Width="255px"  />
                                                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="CodiceAccertamento" Caption="Cod.Accert." Width="18px"  />
                                                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="PunteggioNCAccertamento" CellStyle-HorizontalAlign="Center" Caption="Livello gravità NC"  />
                                                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="PotenzaTermicaNominale" Settings-AutoFilterCondition="LessOrEqual" Caption="Potenza" Width="25px" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="Generatore" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="CodiceIspezione" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="Ispettore" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataColumn Width="25px" VisibleIndex="10">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="ELIMINA DA PROGRAMMA ACCERTAMENTO" AlternateText="ELIMINA DA PROGRAMMA ACCERTAMENTO" OnClientClick="javascript:return confirm('Confermi eliminazione accertamento da programma accertamento?')"
                                                                    ImageUrl="~/images/buttons/deleteSmall.png" TabIndex="1" OnCommand="RowCommandListaPerPacchettoAccertamenti" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamentoProgramma") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento")%>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Width="25px" Visible="false" VisibleIndex="11">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgInsertNellaVisitaIspettiva" TabIndex="1" AlternateText="Inserisci Accertamento nel Pacchetto" 
                                                                    ToolTip="Inserisci Accertamento nel Pacchetto" ImageUrl="~/images/buttons/newSmall.png" OnClientClick="javascript:return confirm('Confermi inserimento nel pacchetto?')"
                                                                    OnCommand="RowCommandListaPerPacchettoAccertamenti" CommandName="InsertNelPacchetto" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamentoProgramma") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento")%>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>


                                                <%--<dx:ASPxGridView ID="GridViewListaNelProgramma" ClientInstanceName="GridViewListaNelProgramma" runat="server" AutoGenerateColumns="false"
                                                    KeyFieldName="IDAccertamento" Width="100%" Font-Size="7" EnableCallBacks="false">
                                                    <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                                        Row-CssClass="GridItem"
                                                        Header-CssClass="GridHeader"
                                                        FilterCell-CssClass="GridAlternativeItem" />
                                                    <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                                                    <Settings ShowFilterRow="true" AutoFilterCondition="Contains" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="IDAccertamentoProgramma" Visible="false" />
                                                        <dx:GridViewDataTextColumn FieldName="IDAccertamento" Visible="false" />
                                                        <dx:GridViewDataTextColumn FieldName="PunteggioNCAccertamento" CellStyle-HorizontalAlign="Center" Caption="Livello gravità NC" VisibleIndex="5" />
                                                        <dx:GridViewDataTextColumn FieldName="CodiceCatastale" Width="180px" Caption="Comune" VisibleIndex="1" />
                                                        <dx:GridViewDataTextColumn FieldName="CodiceTargatura" Width="255px" VisibleIndex="3" />
                                                        <dx:GridViewDataTextColumn FieldName="CodiceAccertamento" Caption="Cod.Accert." Width="18px" VisibleIndex="4" />
                                                        <dx:GridViewDataTextColumn FieldName="NomeAzienda" Width="235px" Caption="Impresa" VisibleIndex="0" />
                                                        <dx:GridViewDataColumn Width="25px" VisibleIndex="8">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="ELIMINA DA PROGRAMMA ACCERTAMENTO" AlternateText="ELIMINA DA PROGRAMMA ACCERTAMENTO" OnClientClick="javascript:return confirm('Confermi eliminazione accertamento da programma accertamento?')"
                                                                    ImageUrl="~/images/buttons/deleteSmall.png" TabIndex="1" OnCommand="RowCommandListaPerPacchettoAccertamenti" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamentoProgramma") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento")%>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Width="25px" Visible="false" VisibleIndex="9">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgInsertNellaVisitaIspettiva" TabIndex="1" AlternateText="Inserisci Accertamento nel Pacchetto" 
                                                                    ToolTip="Inserisci Accertamento nel Pacchetto" ImageUrl="~/images/buttons/newSmall.png" OnClientClick="javascript:return confirm('Confermi inserimento nel pacchetto?')"
                                                                    OnCommand="RowCommandListaPerPacchettoAccertamenti" CommandName="InsertNelPacchetto" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamentoProgramma") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento")%>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn VisibleIndex="6" FieldName="PotenzaTermicaNominale" Settings-AutoFilterCondition="LessOrEqual" Caption="Potenza" Width="25px" CellStyle-HorizontalAlign="Center" />
                                                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="Generatore" CellStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </dx:ASPxGridView>--%>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <ef:EntityDataSource ID="dsPacchetti" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="VER_AccertamentoVisita"
        AutoGenerateWhereClause="false"
        Where="it.IDProgrammaAccertamento=@IDProgrammaAccertamento ">
        <WhereParameters>
            <asp:Parameter Name="IDProgrammaAccertamento" Type="Int32" DefaultValue="0" />
        </WhereParameters>
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="dsAccertamentiNelPacchetto" runat="server"
        ConnectionString="name=CriterDataModel"
        ContextTypeName="DataLayer.CriterDataModel"
        DefaultContainerName="CriterDataModel" EnableFlattening="false"
        EntitySetName="V_VER_AccertamentoVisita" EnableUpdate="True"
        AutoGenerateWhereClause="false"
        Where="it.IDAccertamentoVisita=@IDAccertamentoVisita ">
        <WhereParameters>
            <asp:Parameter Name="IDAccertamentoVisita" Type="Int32" DefaultValue="0" />
        </WhereParameters>
    </ef:EntityDataSource>
</asp:Content>