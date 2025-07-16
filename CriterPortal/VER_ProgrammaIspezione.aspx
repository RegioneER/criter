<%@ Page Title="Programma Ispezione" Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="VER_ProgrammaIspezione.aspx.cs" Inherits="VER_ProgrammaIspezione" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=cbAttivo.ClientID %>").checked == true)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- -->
            <asp:Table ID="tblProgrammaIspezione" Width="900" runat="server" CssClass="TableClass">
                <asp:TableRow ID="rowHeader">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                         <h2>GESTIONE PROGRAMMA ISPEZIONI</h2>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow ID="rowBody">
                    <asp:TableCell>
                        <br />
                        <asp:Table ID="tblPanel1" runat="server" Width="890">
                            <asp:TableRow ID="rowGrid">
                                <asp:TableCell ColumnSpan="2">
                                    <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="false" KeyFieldName="IDProgrammaIspezione" Width="100%" Styles-Cell-HorizontalAlign="Center"
                                        OnHtmlRowCreated="Grid_HtmlRowCreated" ClientInstanceName="Grid">
                                        <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                            Row-CssClass="GridItem"
                                            Header-CssClass="GridHeader" />
                                        <SettingsBehavior AllowFocusedRow="true"
                                            ProcessFocusedRowChangedOnServer="false"
                                            AllowSort="false" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="IDProgrammaIspezione" Caption="Identificativo" />
                                            <dx:GridViewDataColumn FieldName="Descrizione" />
                                            <dx:GridViewDataColumn FieldName="DataInizio" />
                                            <dx:GridViewDataColumn FieldName="DataFine" />
                                            <dx:GridViewDataCheckColumn FieldName="fAttivo" Caption="Attivo">
                                                <DataItemTemplate>
                                                    <dx:ASPxImage runat="server" ID="imgfAttivo" OnInit="imgfAttivo_Init" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataColumn Caption="Generatori">
                                                <DataItemTemplate>
                                                    <asp:Label ID="lblCountLibretti" runat="server" ForeColor="#000000" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn>
                                                <DataItemTemplate>
                                                    <asp:ImageButton ID="btnModificaProgrammaIspezione" runat="server" ImageUrl="~/images/buttons/editSmall.png" OnClick="btnModificaProgrammaIspezione_Click" ToolTip="MODIFICA PROGRAMMA ISPEZIONE" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn>
                                                <DataItemTemplate>
                                                    <asp:ImageButton ID="btnPrintProgrammaIspezione" runat="server" ImageUrl="~/images/buttons/pdfSmall.png" ToolTip="STAMPA PDF PROGRAMMA ISPEZIONE" />                                  
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn>
                                                <DataItemTemplate>
                                                    <asp:ImageButton ID="btnPrintVisiteIspettiveProgrammaIspezione" runat="server" ImageUrl="~/images/buttons/excelSmall.png" ToolTip="STAMPA EXCEL VISITE ISPETTIVE NEL PROGRAMMA ISPEZIONE" />                                  
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <dx:ASPxPopupControl ID="WindowProgrammaIspezione" runat="server" CloseAction="OuterMouseClick"
                                        Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ClientInstanceName="WindowProgrammaIspezione" HeaderText="Programma Ispezione"
                                        AllowDragging="True" PopupAnimationType="Fade" Width="905px" Height="600px" AllowResize="True">
                                    </dx:ASPxPopupControl>
                                    <dx:ASPxPopupControl ID="WindowProgrammaIspezioneVisiteIspettive" runat="server" CloseAction="OuterMouseClick"
                                        Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ClientInstanceName="WindowProgrammaIspezioneVisiteIspettive" HeaderText="Visite Ispettive nel Programma Ispezione"
                                        AllowDragging="True" PopupAnimationType="Fade" Width="905px" Height="600px" AllowResize="True">
                                    </dx:ASPxPopupControl>
                                    <script type="text/javascript">
                                        function OpenPopupWindows(element, key)
                                        {
                                            var url = 'VER_ProgrammaIspezioneViewer.aspx?IDProgrammaIspezione=' + key;
                                            WindowProgrammaIspezione.SetContentUrl(url)
                                            WindowProgrammaIspezione.Show();
                                        }

                                        function OpenPopupWindowsVisiteIspettive(element, key) {
                                            var url = 'VER_ProgrammaIspezioneVisiteIspettiveViewer.aspx?IDProgrammaIspezione=' + key;
                                            WindowProgrammaIspezioneVisiteIspettive.SetContentUrl(url)
                                            WindowProgrammaIspezioneVisiteIspettive.Show();
                                        }
                                    </script>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="rowButtonNuovoProgrammaIspezione">
                                <asp:TableCell HorizontalAlign="Center">
                                    <br />
                                    <asp:Button ID="btnNuovoProgrammaIspezione" runat="server" TabIndex="1" CssClass="buttonClass" Width="250"
                                        OnClick="btnNuovoProgrammaIspezione_Click" Text="NUOVO PROGRAMMA ISPEZIONE" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <br />
                        <dx:ASPxPageControl ID="pageControl" runat="server" AutoPostBack="false" ActiveTabIndex="0" EnableCallBacks="false" Width="880" Border-BorderColor="Black">
                            <TabPages>
                                <dx:TabPage Text="RICERCA GENERATORI / ACCERTAMENTI DA INSERIRE NEL PROGRAMMA ISPEZIONE" TabStyle-Width="50%" TabStyle-Font-Size="Smaller">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <asp:Table ID="tblLibrettiAccertamenti" runat="server" CssClass="TableClass" HorizontalAlign="Center">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2">
                                                        <asp:Table ID="tblRicercaDaInserireNelProgrammaIspezione" runat="server" Width="100%" CssClass="TableClass" Visible="true">
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                                                    <asp:Label ID="lbltitoloRicerca" runat="server" Text="RICERCA GENERATORI NEL CATASTO/ACCERTAMENTI DA INSERIRE NEL PROGRAMMA ISPEZIONE" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Tipo Ricerca" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    <dx:ASPxRadioButtonList ID="rblTipoRicerca" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="rblTipoRicerca_SelectedIndexChanged" runat="server" >
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Accertamenti in attesa di ispezione" Selected="true" Value="0" />
                                                                            <dx:ListEditItem Text="Generatori presenti nel catasto" Value="1" />
                                                                        </Items>
                                                                    </dx:ASPxRadioButtonList>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowCodiceTargatura">
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" ID="lblCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice Targatura" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" CssClass="txtClass" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowPOD">
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" ID="lblTitoloCodicePod" AssociatedControlID="txtCodicePod" Text="Codice POD" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    <asp:TextBox runat="server" ID="txtCodicePod" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowPDR">
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" ID="lblTitoloCodicePdr" AssociatedControlID="txtCodicePdr" Text="Codice PDR" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    <asp:TextBox runat="server" ID="txtCodicePdr" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowDatiCatastali">
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" ID="lblTitoloDatiCatastali" AssociatedControlID="cmbDatiCatastali" Text="Dati Catastali" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    <asp:Table ID="tblCodiceCatastale" Width="500" runat="server">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="150" HorizontalAlign="Left">
                                                                                 Comune
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="350">
                                                                                <dx:ASPxComboBox ID="cmbDatiCatastali" runat="server" Theme="Default" TabIndex="1"
                                                                                    EnableCallbackMode="true"
                                                                                    CallbackPageSize="30"
                                                                                    IncrementalFilteringMode="Contains"
                                                                                    ValueType="System.String"
                                                                                    ValueField="IDCodiceCatastale"
                                                                                    OnItemsRequestedByFilterCondition="cmbDatiCatastali_OnItemsRequestedByFilterCondition"
                                                                                    OnItemRequestedByValue="cmbDatiCatastali_OnItemRequestedByValue"
                                                                                    OnButtonClick="cmbDatiCatastali_ButtonClick"
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
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="150" HorizontalAlign="Left">
                                                                                Foglio
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="350">
                                                                                <asp:TextBox ID="txtFoglio" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="150">
                                                                                Mappale
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="350">
                                                                                <asp:TextBox ID="txtMappale" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="150">
                                                                                Subalterno
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="350">
                                                                                <asp:TextBox ID="txtSubalterno" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="150">
                                                                                Identificativo
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="350">
                                                                                <asp:TextBox ID="txtIdentificativo" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                   <asp:Label runat="server" Text="Potenza" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtPotenzaDa" />
                                                                    &nbsp;&nbsp;
					                                            	a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtPotenzaA" />
                                                                    <asp:RegularExpressionValidator ID="revPotenzaDa" ControlToValidate="txtPotenzaDa"
                                                                        ErrorMessage="Potenza da: inserire un valore numerico eventualmente con separatore decimale il punto" runat="server"
                                                                        ValidationExpression="^([0-9]*\.?[0-9]+|[0-9]+\.?[0-9]*)?$" ForeColor="Red" ValidationGroup="vsFiltriRicerca"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                    <asp:RegularExpressionValidator ID="revPotenzaA" ControlToValidate="txtPotenzaA"
                                                                        ErrorMessage="Potenza a: inserire un valore numerico eventualmente con separatore decimale il punto" runat="server"
                                                                        ValidationExpression="^([0-9]*\.?[0-9]+|[0-9]+\.?[0-9]*)?$" ForeColor="Red" ValidationGroup="vsFiltriRicerca"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Data Installazione (gg/mm/aaaa)" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInstallazioneDa" />
                                                                    &nbsp;&nbsp;
						                                            a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInstallazioneA" />
                                                                    <asp:RegularExpressionValidator
                                                                        ID="revDataInstallazioneDa" ControlToValidate="txtDataInstallazioneDa" ForeColor="Red" ErrorMessage=""
                                                                        runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                    <asp:RegularExpressionValidator
                                                                        ID="revDataInstallazioneA" ControlToValidate="txtDataInstallazioneA" ForeColor="Red" ErrorMessage=""
                                                                        runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell Width="300" CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Data Inserimento (gg/mm/aaaa)" />
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="600" CssClass="riempimento">
                                                                    da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInserimentoDa" />
                                                                    &nbsp;&nbsp;
						                                            a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInserimentoA" />
                                                                    <asp:RegularExpressionValidator
                                                                        ID="revDataInserimentoDa" ControlToValidate="txtDataInserimentoDa" ForeColor="Red" ErrorMessage=""
                                                                        runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                    <asp:RegularExpressionValidator
                                                                        ID="revDataInserimentoA" ControlToValidate="txtDataInserimentoA" ForeColor="Red" ErrorMessage=""
                                                                        runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                                                        EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Combustibile" />
                                                                </asp:TableCell>
                                                                <asp:TableCell CssClass="riempimento">
                                                                    <asp:RadioButtonList ID="rblCombustibile" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="radiobuttonlistClass" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Tipologia gruppo termico" />
                                                                </asp:TableCell>
                                                                <asp:TableCell CssClass="riempimento">
                                                                    <asp:RadioButtonList ID="rbTipologiaGruppoTermico" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                                                    <br />
                                                                    <asp:Button ID="btnRicercaAccertamentiDaInserireNelProgrammaIspezione" runat="server" TabIndex="1" CssClass="buttonClass" Width="300"
                                                                        OnClick="btnRicercaAccertamentiDaInserireNelProgrammaIspezione_Click" Text="RICERCA ACCERTAMENTI" ValidationGroup="vsFiltriRicerca" 
                                                                        UseSubmitBehavior="false"
                                                                        OnClientClick="disableBtn(this.id, 'Attendere, ricerca in corso...')"/>&nbsp;
                                                                    <asp:Button ID="btnRicercaLibrettiDaInserireNelProgrammaIspezione" runat="server" TabIndex="1" CssClass="buttonClass" Width="300"
                                                                        OnClick="btnRicercaLibrettiDaInserireNelProgrammaIspezione_Click" Text="RICERCA GENERATORI" Visible="false" ValidationGroup="vsFiltriRicerca"
                                                                        UseSubmitBehavior="false" 
                                                                        OnClientClick="disableBtn(this.id, 'Attendere, ricerca in corso...')"/>&nbsp;

                                                                    <asp:Button ID="btnRicercaLibrettiDaInserireNelProgrammaIspezioneRegoleControlli" runat="server" TabIndex="1" 
                                                                        CssClass="buttonClass" Width="300" 
                                                                        CausesValidation="false" Text="RICERCA GENERATORI CON REGOLE CONTROLLI" 
                                                                        OnClick="btnRicercaLibrettiDaInserireNelProgrammaIspezioneRegoleControlli_Click"
                                                                         /><br /><br />
                                                                    <%--OnClientClick="return WindowHelperProgrammaIspezioneRegoleControlli.Show();"
                                                                        <dx:ASPxPopupControl ID="ASPxPopupControlHelper" runat="server" Modal="true" AllowDragging="True" AllowResize="True"
                                                                        CloseAction="CloseButton" CloseAnimationType="Fade" 
                                                                        EnableViewState="False" PopupElementID="popupAreaHelper" 
                                                                        PopupHorizontalAlign="WindowCenter" ContentUrl="~/VER_ProgrammaIspezioneRegoleControlli.aspx"
                                                                        PopupVerticalAlign="WindowCenter" HeaderText="" ShowFooter="True"  
                                                                        Width="905px" Height="600px" MinWidth="905px" MinHeight="600px" 
                                                                        ClientInstanceName="WindowHelperProgrammaIspezioneRegoleControlli" EnableHierarchyRecreation="True" FooterStyle-Wrap="True">
                                                                        <ContentStyle Paddings-Padding="0" />
                                                                        <ClientSideEvents Shown="WindowHelperProgrammaIspezioneRegoleControlli" />
                                                                    </dx:ASPxPopupControl>--%>

                                                                    <asp:Button ID="btnInserireLibretti" runat="server" TabIndex="1" CssClass="buttonClass" Width="250" 
                                                                        OnClick="btnInserisciLibrettiNelProgrammaIspezione_Click" Text="INSERISCI NEL PROGRAMMA ISPEZIONE" Visible="false" UseSubmitBehavior="false"
                                                                        OnClientClick="if (confirm('Confermi inserire generatore/i nel programma ispezione')) {disableBtn(this.id, 'Attendere, inserimento in corso...')} else {return false};" />

                                                                    <asp:ValidationSummary ID="vsFiltriRicerca" ValidationGroup="vsFiltriRicerca" runat="server" EnableClientScript="true"
                                                                        ShowMessageBox="true" ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />

                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowDataGridAccertamenti">
                                                                <asp:TableCell Width="880px" ColumnSpan="2" CssClass="riempimento5">
                                                                    <asp:Label runat="server" ID="lblCountAccertamentiInAttesaIspezione" Visible="false" />
                                                                    <asp:DataGrid ID="DataGridAccertamentiInAttesaIspezione" CssClass="Grid" Width="865px" GridLines="None" HorizontalAlign="Center"
                                                                        CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="50" AllowSorting="True" AllowPaging="True"
                                                                        AutoGenerateColumns="False" runat="server" DataKeyField="IDAccertamento"
                                                                        OnPageIndexChanged="DataGridAccertamentiInAttesaIspezione_PageChanger">
                                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                        <ItemStyle CssClass="GridItem" />
                                                                        <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="true" />
                                                                            <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="true" />
                                                                            <asp:BoundColumn DataField="IDLIM_LibrettiImpiantiGruppitermici" Visible="false" ReadOnly="true" />
                                                                            <asp:BoundColumn DataField="IDRapportoDiControlloTecnicoBase" Visible="false" ReadOnly="True" />
                                                                            <asp:BoundColumn DataField="IDStatoAccertamento" Visible="false" ReadOnly="True" />
                                                                            <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                                                            <asp:BoundColumn DataField="CodiceAccertamento" Visible="false" ReadOnly="True" />
                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="700px" HeaderText="Lista Accertamenti">
                                                                                <ItemTemplate>
                                                                                    <asp:Table ID="tblInfoInterventi" Width="650px" runat="server">
                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                               <b>Codice targatura:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" Width="330">
                                                                                                <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
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

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                <b>Codice&nbsp;accertamento:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="330">
                                                                                                <asp:Label ID="lblCodiceAccertamento" runat="server" Text='<%# Eval("CodiceAccertamento") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                <b>Data&nbsp;scadenza&nbsp;intervento:</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblDataScadenzaIntervento" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataScadenzaIntervento")) %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                 <b>Data Installazione:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblInstallazione" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                <b>Data registrazione libretto:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblDataInserimento" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataInserimento")) %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                 <b>Potenza:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblPotenza" runat="server" Text='<%# Eval("PotenzaTermicaNominale") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                <b>Numero PDR:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblNumeroPDR" runat="server" Text='<%# Eval("NumeroPDR") %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                 <b>Numero POD:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblNumeroPOD" runat="server" Text='<%# Eval("NumeroPOD") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="640px">
                                                                                            <asp:TableCell Width="100" ColumnSpan="2">
                                                                                                <b>Combustibile:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblCombustibile" runat="server" Text='<%# Eval("TipologiaCombustibile") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>
                                                                                    </asp:Table>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <%--<asp:TemplateColumn ItemStyle-Width="30px">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgInsertAccertamentoNelProgrammaIspezione" TabIndex="1" AlternateText="INSERISCI ACCERTAMENTI NEL PROGRAMMA ISPEZIONE ATTIVO" ToolTip="INSERISCI ACCERTAMENTI NEL PROGRAMMA ISPEZIONE ATTIVO" ImageUrl="~/images/buttons/newSmall.png" OnClientClick="javascript:return confirm('Confermi inserimento nel programma ispezione?')"
                                                                                        OnCommand="RowCommandAccertamento" CommandName="InsertAccertamentoNelProgrammaIspezione" 
                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamento") +","+ DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") +","+ DataBinder.Eval(Container.DataItem,"IDLIM_LibrettiImpiantiGruppitermici")+","+ DataBinder.Eval(Container.DataItem,"IDTargaturaImpianto") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>--%>
                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkSelezioneAllAccertamenti" OnCheckedChanged="chkSelezioneAllAccertamenti_CheckedChanged" AutoPostBack="true" ToolTip="Seleziona tutti i libretti" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkSelezioneAccertamenti" OnCheckedChanged="chkSelezioneAccertamenti_CheckedChanged" AutoPostBack="true" TabIndex="1" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>


                                                                        </Columns>
                                                                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow ID="rowDataGridLibretti" Visible="false">
                                                                <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                                                                    <asp:Label runat="server" ID="lblCountLibretti" Visible="false" />
                                                                    <asp:DataGrid ID="DataGridLibretti" CssClass="Grid" Width="100%" GridLines="None" HorizontalAlign="Center"
                                                                        CellSpacing="1" CellPadding="3" UseAccessibleHeader="false" PageSize="10" AllowSorting="True" AllowPaging="True"
                                                                        AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoGruppoTermico"
                                                                        OnPageIndexChanged="DataGridLibretti_PageChanger" OnItemDataBound="DataGridLibretti_ItemDataBound">
                                                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                                                        <ItemStyle CssClass="GridItem" />
                                                                        <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="IDLibrettoImpiantoGruppoTermico" Visible="false" ReadOnly="true" />
                                                                            <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                                                            <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                                                            <asp:BoundColumn DataField="CodiceTargatura" Visible="false" ReadOnly="True" />
                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Generatori" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Table ID="tblInfoLibretti" Width="700" runat="server">
                                                                                        <asp:TableRow Width="700">
                                                                                            <asp:TableCell Width="130">
                                                                                                <b>Codice targatura:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="350" ColumnSpan="4">
                                                                                                <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
                                                                                                <%# Eval("NumeroRevisione") is System.DBNull  ? "" : " - Rev " + Eval("NumeroRevisione")  %>
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="700">
                                                                                            <asp:TableCell Width="100">
                                                                                                <b>Comune:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                                                                <asp:Label ID="lblCodiceCatastale" runat="server" Text='<%#Eval("CodiceCatastale") %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100">
                                                                                                <b>Indirizzo:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblIndirizzo" runat="server" Text='<%#Eval("Indirizzo") %>' />&nbsp;
                                                                                                <%#Eval("Civico") %>
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="700">
                                                                                         <asp:TableCell Width="100">
                                                                                                <b>Potenza:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblPotenza" runat="server" Text='<%#Eval("PotenzaTermicaUtileNominaleKw") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="700">
                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Numero PDR:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                                                                <asp:Label ID="lblPdr" runat="server" Text='<%#Eval("NumeroPDR") %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Numero POD:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblPod" runat="server" Text='<%#Eval("NumeroPOD") %>' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="700">
                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Data Installazione:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                                                                <asp:Label ID="lblDataInstallazione" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>' />
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Generatore:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblGeneratore" runat="server" Text='<%# Eval("Prefisso") +  Eval("CodiceProgressivo").ToString() %> ' />
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                        <asp:TableRow Width="700">
                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Data Inserimento:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                                                                <asp:Label ID="lblDataInserimento" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataInserimento")) %>' />
                                                                                            </asp:TableCell>

                                                                                            <asp:TableCell Width="100">
                                                                                               <b>Combustibile:&nbsp;</b>
                                                                                            </asp:TableCell>
                                                                                            <asp:TableCell HorizontalAlign="Left" Width="130">
                                                                                                <asp:Label ID="lblCombustibile" runat="server" Text='<%#Eval("TipologiaCombustibile") %>' />&nbsp;
                                                                                                <%# Eval("CombustibileAltro") is System.DBNull  ? "" : " - " + Eval("CombustibileAltro")  %>
                                                                                            </asp:TableCell>
                                                                                        </asp:TableRow>

                                                                                    </asp:Table>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf libretto impianto" AlternateText="Visualizza pdf libretto impianto"
                                                                                        ImageUrl="~/images/Buttons/pdf.png" TabIndex="1" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>

                                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkSelezioneAllLibretti" OnCheckedChanged="chkSelezioneAllLibretti_CheckedChanged" AutoPostBack="true" ToolTip="Seleziona tutti i libretti" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkSelezioneLibretti" OnCheckedChanged="chkSelezioneLibretti_CheckedChanged" AutoPostBack="true" TabIndex="1" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                                                                    </asp:DataGrid>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                                <dx:TabPage Text="CREA VISITE ISPETTIVE / ISPEZIONI" TabStyle-Width="50%" TabStyle-Font-Size="Smaller">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <br />
                                            <asp:Table ID="tblVititeIspezione" runat="server" Width="100%" CssClass="">
                                                <asp:TableRow>
                                                    <asp:TableCell Width="880px" ColumnSpan="2" CssClass="riempimento5">
                                                        
                                                        <asp:Table ID="tblListaGridVisiteIspettivo" runat="server" Width="100%" CssClass="">
                                                            <asp:TableRow>
                                                                <asp:TableCell CssClass="riempimento2">
                                                                    <asp:Label runat="server" Text="Ricerca generatore" />
                                                                </asp:TableCell>
                                                                <asp:TableCell CssClass="riempimento">
                                                                    <asp:TextBox CssClass="txtClass" Width="300" TabIndex="1" runat="server" ID="txtCodiceTargaturaInVisite" />&nbsp;
                                                                    <dx:ASPxButton runat="server" ID="btnRicercaCodiceTargaturaInVisite" OnClick="btnRicercaCodiceTargaturaInVisite_Click"
                                                                         Text="RICERCA" />
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lblMessageSearchCodiceTargatura" Visible="false" ForeColor="Red" Text="Nessun generatore presente nelle visite ispettive con il codice targatura inserito!" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                                                                                        
                                                            <asp:TableRow>
                                                                <asp:TableCell ColumnSpan="2">
                                                                    <dx:ASPxGridView ID="GridViewVisita" runat="server" ClientInstanceName="GridViewVisita" KeyFieldName="IDIspezioneVisita" EnableRowsCache="False" 
                                                                        OnHtmlRowCreated="GridViewVisita_HtmlRowCreated" 
                                                                        OnHtmlRowPrepared="GridViewVisita_HtmlRowPrepared"
                                                                        OnFocusedRowChanged="GridViewVisita_FocusedRowChanged"
                                                                        OnBeforePerformDataSelect="GridViewVisita_BeforePerformDataSelect"
                                                                        Width="100%" DataSourceID="dsVisiteIspettive" EnableCallBacks="false" >
                                                                        <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                                                            Row-CssClass="GridItem" Cell-Font-Size="7"
                                                                            Header-CssClass="GridHeader"
                                                                            DetailRow-BackColor="WhiteSmoke" 
                                                                            FilterRow-CssClass="GridAlternativeItem" />
                                                                        <SettingsBehavior AllowFocusedRow="true"
                                                                            ProcessFocusedRowChangedOnServer="true"
                                                                            AllowSelectSingleRowOnly="true"
                                                                            AllowSelectByRowClick="true"
                                                                            AllowSort="true" SortMode="Custom" />
                                                                        <SettingsPager PageSize="10" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />  
                                                                        <Settings ShowFilterRow="true" AutoFilterCondition="Contains" />
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn FieldName="IDIspezioneVisita" Width="40px" CellStyle-HorizontalAlign="Center" Caption="Codice" VisibleIndex="0" />
                                                                            <dx:GridViewDataColumn FieldName="IDProgrammaIspezione" Visible="false" VisibleIndex="1" />
                                                                            <dx:GridViewDataColumn FieldName="DescrizioneVisita" Width="60px" Caption="Visita" VisibleIndex="2" />
                                                                            
                                                                            <dx:GridViewDataColumn Caption="Generatori" Width="50px" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                                                                                <DataItemTemplate>
                                                                                    <asp:Label ID="lblCountLibrettiNellaVisita" runat="server" ForeColor="#000000" Width="50px" />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataColumn Caption="Accertamenti" Width="50px" CellStyle-HorizontalAlign="Center" VisibleIndex="4">
                                                                                <DataItemTemplate>
                                                                                    <asp:Label ID="lblCountAccertamentiNellaVisita" runat="server" ForeColor="#000000" Width="50px" />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataColumn Caption="Importo visita" Width="50px" CellStyle-HorizontalAlign="Center" VisibleIndex="5">
                                                                                <DataItemTemplate>
                                                                                    <asp:Label ID="lblImportoVisita" runat="server" ForeColor="#000000" Width="50px" />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataColumn Caption="Ubicazioni" Width="140px" CellStyle-HorizontalAlign="Center" VisibleIndex="6">
                                                                                <DataItemTemplate>
                                                                                    <asp:Label ID="lblUbicazioniLibretti" runat="server" ForeColor="#000000" Width="140px" />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataCheckColumn  Caption="In Ispezione" FieldName="fInIspezione" Width="60px" CellStyle-HorizontalAlign="Center" VisibleIndex="7">
                                                                                <PropertiesCheckEdit DisplayTextChecked="Si" DisplayTextUnchecked="No" />
                                                                                <DataItemTemplate>
                                                                                    <asp:Image runat="server" ID="imgfInIspezione" />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataCheckColumn>

                                                                            <dx:GridViewCommandColumn Caption="" ShowSelectCheckbox="true" Width="30px" VisibleIndex="8" />
                                                                            
                                                                            <dx:GridViewDataColumn Width="25px" VisibleIndex="9">
                                                                                <DataItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgCreaIspezioni" Visible="false" TabIndex="1" AlternateText="CREA ISPEZIONI" ToolTip="CREA ISPEZIONI" ImageUrl="~/images/buttons/ispezione.png" OnClientClick="if (!confirm('Confermi di inviare in ispezione la visita ispettiva selezionata? Confermando tale operazione verrà automaticamente cercato l\'ispettore accreditato e con contratto attivo più geograficamente vicino all\'ubicazione dell\'impianto. Sulla base della classificazione del parco ispettori verranno inviate emails PEC in modo da consentire l\'accettazione/rifiuto della visita ispettiva. Le emails verranno inviate nell\'arco orario tipicamente lavorativo 8-18 escludendo i giorni festivi e i giorni feriali. Confermi?')) return false;"
                                                                                        OnCommand="RowCommandVisiteIspettive" CommandName="CreaIspezioni" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>' />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn Width="25px" VisibleIndex="10">
                                                                                <DataItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgDeleteVisita" Visible="false" TabIndex="1" AlternateText="ELIMINA VISITA ISPETTIVA" ToolTip="ELIMINA VISITA ISPETTIVA" ImageUrl="~/images/buttons/deleteSmall.png" OnClientClick="if (!confirm('Confermi eliminazione di visita ispettiva?')) return false;"
                                                                                        OnCommand="RowCommandVisiteIspettive" CommandName="DeleteVisita" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>' />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                        <Templates>
                                                                            <DetailRow>
                                                                                <dx:ASPxGridView ID="grdLibrettiNellaVista" runat="server" DataSourceID="dsLibrettiNellaVista" 
                                                                                    Width="100%" EnableRowsCache="False"
                                                                                    KeyFieldName="IDIspezioneVisitaInfo" 
                                                                                    OnBeforePerformDataSelect="grdLibrettiNellaVista_BeforePerformDataSelect"
                                                                                    OnRowUpdating="grdLibrettiNellaVista_RowUpdating"
                                                                                    >
                                                                                    <Styles Row-CssClass="GridItem" AlternatingRow-CssClass="GridAlternativeItem" />
                                                                                    <Settings ShowColumnHeaders="false" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn VisibleIndex="0">
                                                                                            <EditFormSettings Visible="False" />
                                                                                            <DataItemTemplate>
                                                                                                <asp:Table runat="server" Font-Size="8" Width="100%" CssClass="TableClass">
                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell Width="115px">
                                                                                                            <asp:Label runat="server" Text="Combustibile:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell Width="390px">
                                                                                                             <%# Eval("TipologiaCombustibile").ToString() %>
                                                                                                             <%# Eval("CombustibileAltro") == null ? "" : " - " + Eval("CombustibileAltro") %>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell Width="105px">
                                                                                                            <asp:Label runat="server" Text="Responsabile:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell Width="320px">
                                                                                                             <%# Eval("NomeResponsabile").ToString() %>&nbsp;<%# Eval("CognomeResponsabile").ToString() %>
                                                                                                        </asp:TableCell>
                                                                                                    </asp:TableRow>

                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Codice targatura:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# Eval("CodiceTargatura").ToString() %>
                                                                                                            <%# Eval("NumeroRevisione") == null  ? "" : " - Rev " + Eval("NumeroRevisione").ToString() %>
                                                                                                            <br />
                                                                                                            <asp:Label runat="server" ID="lblGeneratoreModificato" Font-Bold="true" ForeColor="Red" Text="Generatore modificato"
                                                                                                              Visible='<%# (bool)Eval("fGeneratoreModificato") == true %>' ToolTip='<%# DataUtilityCore.UtilityVerifiche.GetDescriptionOfLibrettoModificatoInVisiteIspettive(Eval("GeneratoreModificatoDettagli")) %>' /> <%----%>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Comune:" Font-Bold="true"/>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# Eval("CodiceCatastale").ToString() %>
                                                                                                        </asp:TableCell>                     
                                                                                                    </asp:TableRow>

                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Indirizzo:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# Eval("Indirizzo").ToString() %>&nbsp;<%#Eval("Civico") %>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Tipo ispezione:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# Eval("TipoIspezione").ToString() %>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell Width="25px" HorizontalAlign="Right">
                                                                                                            &nbsp;
                                                                                                            <asp:ImageButton runat="server" CssClass="riempimento5" ImageUrl="~/images/buttons/deleteSmall.png" OnCommand="CommandButtonGeneratori" CommandName="DeleteGeneratore" ToolTip="ELIMINA GENERATORE DA VISITA ISPETTIVA"
                                                                                                                 CommandArgument='<%# Eval("IDLibrettoImpianto") +","+ Eval("IDIspezioneVisita") +","+ Eval("IDLibrettoImpiantoGruppoTermico")+","+ Eval("IDIspezioneVisitaInfo") %>' OnClientClick="javascript:return confirm('Confermi eliminazione Generatore/Accertamento da visita ispettiva?')"
                                                                                                                 Visible='<%# !bool.Parse(Eval("fInIspezione").ToString()) %>' />             
                                                                                                        </asp:TableCell>
                                                                                                    </asp:TableRow>

                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Potenza:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                             <%# Eval("PotenzaTermicaUtileNominaleKw").ToString() %>
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
                                                                                                            <asp:Label runat="server" Text="Data Installazione:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Data Inserimento:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell>
                                                                                                            <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInserimento")) %>
                                                                                                        </asp:TableCell>
                                                                                                    </asp:TableRow>

                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Note ad ispettore:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell ColumnSpan="3">
                                                                                                            <asp:Label runat="server" ID="lblNoteIspezioneVisita" Text='<%# Eval("NoteIspezioneVisita") == null ? string.Empty : Eval("NoteIspezioneVisita") %>' />
                                                                                                        </asp:TableCell>
                                                                                                    </asp:TableRow>
                                                                                                    
                                                                                                    <asp:TableRow>
                                                                                                        <asp:TableCell>
                                                                                                            <asp:Label runat="server" Text="Note coordinatore:" Font-Bold="true" />
                                                                                                        </asp:TableCell>
                                                                                                        <asp:TableCell ColumnSpan="3">
                                                                                                            <asp:Label runat="server" ID="lblNoteProgrammaIspezione" Text='<%# Eval("NoteProgrammaIspezione") == null ? string.Empty : Eval("NoteProgrammaIspezione") %>' />
                                                                                                        </asp:TableCell>
                                                                                                    </asp:TableRow>
                                                                                                </asp:Table>
                                                                                            </DataItemTemplate>
                                                                                        </dx:GridViewDataTextColumn>

                                                                                        <dx:GridViewDataTextColumn FieldName="NoteIspezioneVisita" VisibleIndex="1" Visible="false">
                                                                                            <EditFormSettings Visible="True" ColumnSpan="4" CaptionLocation="None" />
                                                                                            <PropertiesTextEdit Width="100%" Height="100" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="2" Width="40px" />
                                                                                         
                                                                                    </Columns>
                                                                                </dx:ASPxGridView>
                                                                            </DetailRow>
                                                                        </Templates>
                                                                        <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" />
                                                                        <SettingsEditing EditFormColumnCount="2" />
                                                                    </dx:ASPxGridView>
                                                                    <%--<dx:ASPxLoadingPanel ID="lp" ClientInstanceName="lp" Modal="true" 
                                                                        Text="La pazienza è la virtù dei forti!" Font-Bold="true" runat="server" />--%>
                                                                    <br />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                                                                    <asp:Button ID="CreaVisitaIspettiva" runat="server" TabIndex="1" CssClass="buttonClass" Width="280" Text="NUOVA VISITA ISPETTIVA" OnClick="CreaVisitaIspettiva_Click" OnClientClick="javascript:return confirm('Confermi creazione nuova visita ispettiva?')" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnCreaVisiteIspettiveAutomatiche" runat="server" TabIndex="1" CssClass="buttonClass" Width="280" Text="CREAZIONE AUTOMATICA VISITE ISPETTIVE" OnClick="btnCreaVisiteIspettiveAutomatiche_Click" />
                                                                    
                                                                    <br />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                            <asp:TableRow>
                                                                <asp:TableCell VerticalAlign="Top" ColumnSpan="2">
                                                                    <br />
                                                                    <dx:ASPxGridView ID="GridViewListaNelProgramma" ClientInstanceName="GridViewListaNelProgramma" runat="server" AutoGenerateColumns="false"
                                                                        KeyFieldName="IDLibrettoImpianto" Width="100%" Font-Size="7" EnableCallBacks="false" 
                                                                        OnHtmlRowPrepared="GridViewListaNelProgramma_HtmlRowPrepared">
                                                                        <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                                                                            Row-CssClass="GridItem"
                                                                            Header-CssClass="GridHeader"
                                                                            FilterCell-CssClass="GridAlternativeItem" />
                                                                        <SettingsPager PageSize="15" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                                                                        <Settings ShowFilterRow="true" AutoFilterCondition="Contains" />
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="IDLibrettoImpianto" Visible="false" />
                                                                            <dx:GridViewDataTextColumn FieldName="IDAccertamento" Visible="false" />
                                                                            <dx:GridViewDataTextColumn FieldName="Impresa" Caption="Impresa" VisibleIndex="0" />
                                                                            <dx:GridViewDataTextColumn FieldName="TipologiaCombustibile" Caption="Combustibile" VisibleIndex="1" />
                                                                            <dx:GridViewDataTextColumn FieldName="CodiceCatastale" Caption="Comune" VisibleIndex="2" />
                                                                            <dx:GridViewDataTextColumn FieldName="indirizzo" VisibleIndex="3" />
                                                                            <dx:GridViewDataTextColumn FieldName="CodiceTargatura" Width="235px" VisibleIndex="4" />
                                                                            <dx:GridViewDataTextColumn FieldName="Responsabile" Caption="Responsabile" VisibleIndex="5" />
                                                                            <dx:GridViewDataTextColumn FieldName="TerzoResponsabile" Caption="Terzo Responsabile" VisibleIndex="6" />
                                                                            <dx:GridViewDataTextColumn FieldName="km" UnboundType="Decimal" Width="40px" Visible="false" VisibleIndex="10" Settings-AutoFilterCondition="LessOrEqual" SortOrder="Ascending">
                                                                                <PropertiesTextEdit DisplayFormatString="N1" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            
                                                                            <dx:GridViewDataTextColumn FieldName="ImportoIspezione" Caption="Importo" Width="20px" PropertiesTextEdit-DisplayFormatString="N0" VisibleIndex="11" />
                                                                            <dx:GridViewDataTextColumn FieldName="NoteProgrammaIspezione" Caption="Note" VisibleIndex="12" />

                                                                            <dx:GridViewDataColumn Width="25px" VisibleIndex="13">
                                                                                <DataItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="ELIMINA DA PROGRAMMA ISPEZIONE" AlternateText="ELIMINA DA PROGRAMMA ISPEZIONE" OnClientClick="javascript:return confirm('Confermi eliminazione generatore/accertamento da programma ispezione?')"
                                                                                        ImageUrl="~/images/buttons/deleteSmall.png" TabIndex="1" OnCommand="RowCommandListaPerVisiteIspettive" CommandName="DeleteLibretto" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") + "," + DataBinder.Eval(Container.DataItem,"IDLibrettoImpiantoGruppoTermico") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento") + "," + DataBinder.Eval(Container.DataItem,"IDTargaturaImpianto")%>' />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataColumn Width="25px" Visible="false" VisibleIndex="14">
                                                                                <DataItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgInsertNellaVisitaIspettiva" TabIndex="1" AlternateText="INSERIRE NELLA VISITA ISPETTIVA" ToolTip="INSERIRE NELLA VISITA ISPETTIVA" ImageUrl="~/images/buttons/newSmall.png" OnClientClick="javascript:return confirm('Confermi inserimento nella visita ispettiva?')"
                                                                                        OnCommand="RowCommandListaPerVisiteIspettive" CommandName="InsertNellaVisitaIspettiva" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") + "," + DataBinder.Eval(Container.DataItem,"IDLibrettoImpiantoGruppoTermico") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento") + "," + DataBinder.Eval(Container.DataItem,"IDTargaturaImpianto")%>' />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                            <dx:GridViewDataColumn VisibleIndex="7" FieldName="TipoIspezione" />
                                                                            <dx:GridViewDataColumn VisibleIndex="8" FieldName="PotenzaTermicaUtileNominaleKw" Settings-AutoFilterCondition="LessOrEqual" Caption="Potenza" Width="25px" CellStyle-HorizontalAlign="Center" />
                                                                            <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="Generatore" CellStyle-HorizontalAlign="Center" />
                                                                            <dx:GridViewDataTextColumn FieldName="IDLibrettoImpiantoGruppoTermico" Visible="false" />

                                                                            <dx:GridViewDataColumn Width="25px" VisibleIndex="15">
                                                                                <DataItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgInsertNota" TabIndex="1" AlternateText="INSERISCI NOTA" ToolTip="INSERISCI NOTA" ImageUrl="~/images/buttons/editSmall.png" 
                                                                                        OnCommand="RowCommandListaPerVisiteIspettive" CommandName="InsertNellaVisitaIspettivaNota" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") + "," + DataBinder.Eval(Container.DataItem,"IDLibrettoImpiantoGruppoTermico") + "," + DataBinder.Eval(Container.DataItem,"IDAccertamento") + "," + DataBinder.Eval(Container.DataItem,"IDTargaturaImpianto")%>' />
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataColumn>

                                                                        </Columns>
                                                                    </dx:ASPxGridView>

                                                                    
                                                                    <asp:Table ID="tblAddNotaGridViewListaNelProgramma" Visible="false" runat="server" Width="100%" CssClass="">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell CssClass="riempimento">
                                                                                <asp:Label runat="server" ID="lblHiddenIDLibrettoImpiantoGruppoTermico" Visible="false" />
                                                                                <asp:TextBox Width="600" Height="100" TextMode="MultiLine" ID="txtNoteListaNelProgramma" runat="server" CssClass="txtClass" />
                                                                            </asp:TableCell>
                                                                            <asp:TableCell CssClass="riempimento" VerticalAlign="Middle">
                                                                                <asp:Button ID="btnSaveNoteListaNelProgramma" runat="server" CssClass="buttonSmallClass" Text="Salva nota"
                                                                                    OnClick="btnSaveNoteListaNelProgramma_Click"
                                                                                        />&nbsp;
                                                                                <asp:Button ID="btnAnnullaNoteListaNelProgramma" runat="server" CssClass="buttonSmallClass" Text="Annulla"
                                                                                    OnClick="btnAnnullaNoteListaNelProgramma_Click"
                                                                                        />
                                                                            </asp:TableCell>
                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                        
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>

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
                                    <%--<asp:RequiredFieldValidator
                                        ID="rfvDataFine" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true" ErrorMessage="Data Fine: campo obbligatorio"
                                        ControlToValidate="txtDataInizio">&nbsp;*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDataFine" ForeColor="Red" runat="server" ValidationGroup="vgtbPanel2" EnableClientScript="true"
                                        ErrorMessage="Data Fine: campo non valido"
                                        ControlToValidate="txtDataInizio" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))">&nbsp;*</asp:RegularExpressionValidator>      --%>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="rowAttivo" CssClass="riempimento">
                                <asp:TableCell CssClass="riempimento2">
                                    <asp:Label ID="lblAttivo" Text="Attivo ( SI / NO )" runat="server" />
                                </asp:TableCell>

                                <asp:TableCell>
                                    <asp:CheckBox ID="cbAttivo" runat="server" Checked="true" />
                                    <asp:CustomValidator ID="cvAttivo" runat="server" ClientValidationFunction="ValidateCheckBox" EnableClientScript="true" ErrorMessage="Attivo: Può essere attiva solo una programma ispezione"
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
            </asp:Table>

            <ef:EntityDataSource ID="dsVisiteIspettive" runat="server"
                ConnectionString="name=CriterDataModel"
                ContextTypeName="DataLayer.CriterDataModel"
                DefaultContainerName="CriterDataModel" EnableFlattening="false"
                 CommandText="SELECT VER_IspezioneVisita.IDIspezioneVisita, VER_IspezioneVisita.IDProgrammaIspezione, VER_IspezioneVisita.DescrizioneVisita, VER_IspezioneVisitaInfo.fInIspezione
                              FROM VER_IspezioneVisita LEFT OUTER JOIN
                              VER_IspezioneVisitaInfo ON VER_IspezioneVisita.IDIspezioneVisita = VER_IspezioneVisitaInfo.IDIspezioneVisita
                              GROUP BY VER_IspezioneVisita.IDIspezioneVisita, VER_IspezioneVisita.IDProgrammaIspezione, VER_IspezioneVisita.DescrizioneVisita, VER_IspezioneVisitaInfo.fInIspezione
                              HAVING (VER_IspezioneVisita.IDProgrammaIspezione=@IDProgrammaIspezione)"
                AutoGenerateWhereClause="false"
                Where="it.IDProgrammaIspezione=@IDProgrammaIspezione">
                <WhereParameters>
                    <asp:Parameter Name="IDProgrammaIspezione" Type="Int32" DefaultValue="0" />
                </WhereParameters>
            </ef:EntityDataSource>
            
            <ef:EntityDataSource ID="dsLibrettiNellaVista" runat="server"
                ConnectionString="name=CriterDataModel"
                ContextTypeName="DataLayer.CriterDataModel"
                DefaultContainerName="CriterDataModel" EnableFlattening="false"
                EntitySetName="V_VER_IspezioniVisite" EnableUpdate="True"
                AutoGenerateWhereClause="false"
                Where="it.IDIspezioneVisita=@IDIspezioneVisita ">
                <WhereParameters>
                    <asp:Parameter Name="IDIspezioneVisita" Type="Int32" DefaultValue="0" />
                </WhereParameters>
            </ef:EntityDataSource>
            <!-- -->
        </ContentTemplate>
    </asp:UpdatePanel>

    <uc1:WebUSUpdateProgress runat="server" id="WebUSUpdateProgress" />
    <%--<asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>

