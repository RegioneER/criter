<%@ Page Title="Nuovo Rapporto di Controllo Tecnico" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_RapportoDiControlloTecnicoNuovo.aspx.cs" Inherits="RCT_RapportoDiControlloTecnicoNuovo" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCBolliniSelector.ascx" TagPrefix="uc1" TagName="UCBolliniSelector" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="head">
    <script type = "text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentDisplay">
    <%-- HO DOVUTO DISATTIVARE L'UPDATEPANEL PERCHE' ALLA SELEZIONE DEL BOLLINO NON FUNZIOVA PER IL RUOLO OPERATORE: MISTERO! --%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <asp:Table Width="100%" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell ColumnSpan="2" CssClass="riempimento1">
                        INSERIMENTO NUOVO RAPPORTO DI CONTROLLO TECNICO
                    </asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" Width="100%" CssClass="riempimento">
                            Inserire il Codice targatura impianto per il quale si vuole creare un rapporto di controllo. Il libretto deve essere in stato definitivo per poter essere accettato.<br/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                        Azienda (*)
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAziende"
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
                            ValidationGroup="vgRapportoControllo" 
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
                        <dx:ASPxComboBox runat="server" ID="cmbAddetti" Theme="Default"
                            TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32" AutoPostBack="true"
                            Width="350px" OnSelectedIndexChanged="ASPxComboBox2_OnSelectedIndexChanged"
                            DropDownWidth="350px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator 
                            ID="rfvASPxComboBox2" 
                            ValidationGroup="vgRapportoControllo" 
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
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" ValidationGroup="vgRapportoControllo" TabIndex="1" MaxLength="36" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvtxtCodiceTargatura" runat="server" ForeColor="Red"
							ValidationGroup="vgRapportoControllo" EnableClientScript="true" ErrorMessage="Codice targatura impianto: campo obbligatorio"
							ControlToValidate="txtCodiceTargatura">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA GENERATORI SUI QUALI CREARE I RAPPORTI DI CONTROLLO" OnClick="btnRicerca_Click" ValidationGroup="vgRapportoControllo" CssClass="buttonClass" Width="450px" />
                        <asp:Label runat="server" ID="lblPrivacyBloccoRct" ForeColor="Red" Visible="false" />
                        <asp:Label runat="server" ID="lblLottoDuplicatoBloccoRct" ForeColor="Red" Visible="false" />
                        <asp:HyperLink ID="lnkPrivacyLink" runat="server" NavigateUrl="~/default.aspx" Visible="false" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="qui" />
                        <asp:ValidationSummary ID="vsRapportoControllo" ValidationGroup="vgRapportoControllo" runat="server" ShowMessageBox="True"
							ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
						</asp:ValidationSummary>

                        <cc1:ModalPopupExtender ID="dmpBloccoRct" DropShadow="true" runat="server" Enabled="false" 
                            TargetControlID="btnRicerca"
                            PopupControlID="pnlBloccoRctConfirm" BackgroundCssClass="modalBackground" />
                        <asp:Panel ID="pnlBloccoRctConfirm" runat="server" Style="display: none; width: 400px; height:200px; background-color: White; border-width: 1px; border-color: Black; border-style: solid;">
                            <asp:Label runat="server" ID="lblBloccoRctMessage" />
                            <br />
                            <br />
                            <div style="text-align: center;">
                                <asp:Button ID="btnBloccoRctConfirm" runat="server" OnClick="btnBloccoRctConfirm_Click" CssClass="buttonClass" Width="100" Text="Ok" />
                            </div>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowNoResult" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                       <asp:Label runat="server" ID="lblNoResult" Text="NESSUN GENERATORE CORRISPONDENTE AL CODICE TARGATURA INSERITO" CssClass="GridLabelNoCount" />
                    </asp:TableCell>
                </asp:TableRow>

                
                <asp:TableRow runat="server" ID="rowInteroImpianto" Visible="false">
                    <asp:TableCell ColumnSpan="2" Width="100%" CssClass="riempimento">
                        Per il calcolo del bollino “calore pulito” nel caso di impianti con più generatori, è possibile attivare l’apposita funzione selezionando l’opzione “<b>intero impianto</b>”: in questo modo, il calcolo dei bollini necessari viene effettuato in riferimento alla potenza complessiva dei generatori selezionati (la selezione è automatica), facenti parte dello stesso impianto, e non in base alla potenza del singolo generatore.
                        Tale procedura può essere applicata solo ai generatori di tipo GT (gruppi termici) facenti parte di un impianto alimentati da combustibile liquido e gassoso.<br /><br />
                        <%--ESEMPIO<br />
                        Un impianto con quattro generatori da 150 kW ciascuno, ha una potenza complessiva pari a 600 kW:<br />
                        <ul>
                            <li>•	selezionando l’opzione “<b>singolo generatore</b>” il sistema calcolerà un valore del contributo di Euro 56,00 per ogni generatore (8 bollini per ogni RTCEE), per un importo complessivo di Euro 224,00. E’ possibile in questo caso registrare separatamente i singoli RTCEE.</li>
                            <li>•	selezionando l’opzione “<b>intero impianto</b>” il sistema calcolerà per questo impianto un valore del contributo di Euro 98,00 complessivi (14 bollini complessivi: il sistema autoseleziona tutti i generatori di tipo GT presenti nell’impianto sulla base del codice targatura, effettua la somma delle potenze indicate e calcola il numero dei bollini necessari). Per poter usufruire di questa funzione, è necessario che i RTCEE relativi ai singoli generatori siano registrati contemporaneamente: prima di effettuare la registrazione, il sistema evidenzia uno specchietto riassuntivo su come devono essere associati i bollini nei vari rapporti di controllo dell’impianto.</li>
                        </ul>
                        <br />--%>
                        <b>Utilizzando l’opzione “intero impianto”, il sistema visualizzerà l’elenco dei bollini calore pulito liberi disponibili, dando la possibilità all’utente di selezionare i bollini necessari richiesti dal sistema in base alla potenza complessiva. I bollini selezionati saranno ripartiti in automatico dal sistema tra i singoli rapporti di controllo.</b>
                        <br /><br />
                        Nel caso di sola predisposizione di Rapporti di controllo funzionale e manutenzione (non viene richiesto il bollino calore pulito), procedere con l’opzione “singolo generatore” selezionando il generatore di interesse.
                        <br /><br />
                        <asp:RadioButtonList ID="rblTipoRapportoInteroImpianto" AutoPostBack="true" RepeatColumns="2"
                            OnSelectedIndexChanged="rblTipoRapportoInteroImpianto_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="Singolo Generatore" Selected="true" Value="0" />
                            <asp:ListItem Text="Intero Impianto" Value="1" />
                        </asp:RadioButtonList>
                        <br />
                        <asp:Panel runat="server" ID="pnlInteroImpianto" Visible="false">
                            <asp:Label runat="server" ID="lblTitoloDataControllo" Visible="false" Text="Data del controllo (gg/mm/aaaa) (*): " />
                            <asp:TextBox Width="100" ID="txtDataControllo" Visible="false" CssClass="txtClass_o" AutoPostBack="true" OnTextChanged="txtDataControllo_TextChanged" runat="server" ValidationGroup="vgRapportodiControllo" />
                            <asp:RequiredFieldValidator ID="rfvtxtDataControllo" Enabled="false" ValidationGroup="vgRapportoDiControllo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data del presente controllo: campo obbligatorio"
                                ControlToValidate="txtDataControllo">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Enabled="false"
                                ID="revtxtDataControllo" ValidationGroup="vgRapportoDiControllo" ControlToValidate="txtDataControllo" Display="Dynamic" ForeColor="Red" ErrorMessage="Data del presente controllo: inserire la data nel formato gg/mm/aaaa"
                                runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                            <br /><br />
                       
                            <asp:Label runat="server" ID="lblFeedbackImportoInsufficiente" CssClass="GridLabelNoCount" Font-Bold="true" />
                            <asp:Label runat="server" ID="lblFeedbackRdp" Font-Bold="false" />
                            <asp:Label runat="server" ID="lblImportoRichiesto" Visible="false" Font-Bold="false" />
                            <br />
                                                
                            <br />
                            <uc1:UCBolliniSelector runat="server" ID="UCBolliniSelector" OnSelezioneterminata="UCBolliniSelector_Selezioneterminata" />
                        </asp:Panel>
                        
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowErroriRapportiControlloTecnico">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblErrors" CssClass="GridLabelNoCount" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowGT" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:DataGrid ID="gridGT" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoGruppoTermico"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpiantoGruppoTermico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Gruppi Termici">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfo" Width="780" runat="server">
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                </asp:TableCell>

                                                <asp:TableCell Width="120">
                                                     <b>Potenza termica:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                   <%# Eval("PotenzaTermicaUtileNominaleKw") %> kw
                                                </asp:TableCell>

                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Combustibile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Combustibile") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">
                                                   <%#Eval("Tipologia") %> <%#Eval("AnalisiFumoPrevisteNr", " - Previste {0} analisi fumi") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderText="Seleziona">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IDTipologiaCombustibile" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGF" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:DataGrid ID="gridGF" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoMacchinaFrigorifera"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpiantoMacchinaFrigorifera" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Gruppi Macchine Frigo">
                                    <ItemTemplate>
                                       <asp:Table ID="tblInfo" Width="780" runat="server">
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                   <%#Eval("Tipologia") %> <%#Eval("NumCircuiti", " - {0} circuiti") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderText="Seleziona">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowSC" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:DataGrid ID="gridSC" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoScambiatoreCalore"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpiantoScambiatoreCalore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Scambiatori di Calore">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfo" Width="780" runat="server">
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderText="Seleziona">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowCG" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:DataGrid ID="gridCG" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoCogeneratore"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpiantoCogeneratore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Cogeneratori">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfo" Width="780" runat="server">
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="780">
                                                <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell ColumnSpan="5">
                                                   <%#Eval("Tipologia") %>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderText="Seleziona">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowConfermaInserimento" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnNuoviRapportiControllo" Text="CONFERMA INSERIMENTO RAPPORTI DI CONTROLLO" 
                            OnClick="btnNuoviRapportiControllo_Click" 
                            OnClientClick="javascript:return confirm('Confermando tale operazione verranno creati tanti rapporti di controllo in bozza pari al numero di generatori selezionati.Procedere?')" 
                            CssClass="buttonClass" Width="340px" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvControllaBolliniSelezionatiInteroImpianto" runat="server" 
                            ForeColor="Red" 
                            EnableClientScript="true" 
                            OnServerValidate="ControllaBolliniSelezionatiInteroImpianto"
                            ErrorMessage="Attenzione: i bollini selezionati non corrispondono ai bollini richiesti per l'intero Impianto" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvControllaGeneratoriSelezionati" runat="server" 
                            ForeColor="Red" 
                            EnableClientScript="true" 
                            OnServerValidate="ControllaGeneratoriSelezionati"
                            ErrorMessage="Attenzione: per inserire i rapporti di controllo è necessario selezionare almeno un generatore" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>