<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_LibrettiImpiantiSearchCatasto.aspx.cs" Inherits="LIM_LibrettiImpiantiSearchCatasto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow runat="server" ID="rowRicerca1">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA LIBRETTO IMPIANTO PRESENTE SUL CATASTO
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowIntestazione">
                    <asp:TableCell ColumnSpan="2" Width="100%" CssClass="riempimento">
                        La ricerca può essere effettuata:<br />
                        <ol type="1">
                            <li><b>per Codice targatura impianto</b>, inserendo il codice. In questo caso assicurarsi di verificare la correttezza del dato inserendo un codice nel seguente formato XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX (8-4-4-4-12)</li>
                            <li><b>per altri dati.</b>In questo caso la ricerca deve essere effettuata compilando obbligatoriamente i <b>due</b> campi contrassegnati con l’asterisco (“Tipologia generatori” e “Comune”) e almeno <b>tre</b> dei restanti campi.</li> 
                            Si riportano alcune specifiche come supporto alla ricerca:
                            <ul type="circle">
                                <li>- per i campi “Indirizzo” e “Matricola”, la ricerca può essere effettuata anche inserendo parzialmente il dato;</li>
                                <li>- il campo “Civico” viene considerato come parametro di ricerca solo se il campo indirizzo non è vuoto;</li>
                                <li>- per i campi di ricerca “Codice POD” e “Codice PDR”, se vengono inseriti degli zeri “00000000000000”, la ricerca non produrrà alcun risultato.</li>
                            </ul>
                        </ol><br />
                        Per eventuali verifiche inerenti la registrazioni di impianti che hanno avuto esito negativo con la maschera inviare esclusivamente una mail all’indirizzo <b><i>criter@art-er.it</i></b> indicando i seguenti dati: Comune - Dati catastali - Indirizzo/Civico -  Fabbricante/Modello/Matricola generatore- Codice POD/PDR - C.F./P.IVA Responsabile. 
                        Si specifica che non saranno prese in carico richieste a mezzo telefonico.<br /><br />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicercaType">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipoRicerca" AssociatedControlID="rblTipoRicercaLibretti" Text="Tipo ricerca libretto di impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipoRicercaLibretti" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblTipoRicercaLibretti_SelectedIndexChanged" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Text="Ricerca libretto impianto per Codice targatura impianto" Value="0" Selected="True" />
                            <asp:ListItem Text="Ricerca libretto impianto per altri dati" Value="1" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceTargatura" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvtxtCodiceTargatura" runat="server" ForeColor="Red" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true"
                            ErrorMessage="Codice targatura: campo obbligatorio"
                            ControlToValidate="txtCodiceTargatura">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTipologieGeneratori" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_TipologieGeneratori" AssociatedControlID="rblTipologieGeneratori" Text="Tipologie generatori (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieGeneratori" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowComune" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_Comune" AssociatedControlID="RadComboBoxCodiciCatastali" Text="Comune (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox AutoPostBack="false" ID="RadComboBoxCodiciCatastali" runat="server" TabIndex="1"
                            Theme="Default" EnableViewState="False"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDCodiceCatastale"
                            OnItemsRequestedByFilterCondition="comboComune_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="comboComune_OnItemRequestedByValue"
                            OnButtonClick="comboComune_ButtonClick"
                            TextFormatString="{0}"
                            Width="400px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvRadComboBoxCodiciCatastali" ValidationGroup="vgLibrettoImpianto"
                            ForeColor="Red" Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Comune: campo obbligatorio"
                            ControlToValidate="RadComboBoxCodiciCatastali">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowIndirizzoCivico" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzoImpianto" AssociatedControlID="txtIndirizzoImpianto" Text="Indirizzo/Civico" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoImpianto" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />&nbsp;
                        <asp:TextBox runat="server" ID="txtCivicoImpianto" Width="100" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowMatricolaGeneratore" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblMatricolaGeneratore" AssociatedControlID="txtMatricolaGeneratore" Text="Matricola generatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtMatricolaGeneratore" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodicePod" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_CodicePod" AssociatedControlID="txtCodicePod" Text="Codice POD" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtCodicePod" runat="server" Width="200" MaxLength="14" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodicePdr" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_txtCodicePdr" AssociatedControlID="txtCodicePdr" Text="Codice PDR" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtCodicePdr" runat="server" Width="200" MaxLength="14" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCfPIvaResponsabile" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCfPIvaResponsabile" AssociatedControlID="txtCfPIvaResponsabile" Text="C.F./P.IVA Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCfPIvaResponsabile" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                       <asp:Button ID="LIM_LibrettiImpiantiSearch_btnProcess" TabIndex="1" runat="server" CssClass="buttonClass" Width="370"
                            OnClick="LIM_LibrettiImpiantiSearch_btnProcess_Click" ValidationGroup="vgLibrettoImpianto" Text="RICERCA LIBRETTO IMPIANTO" />
                        <asp:ValidationSummary ID="vsSaveLibrettoImpianto" ValidationGroup="vgLibrettoImpianto" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" /><br /><br />
                         <asp:CustomValidator ID="cvControllaPresenzaMinimiFieldSearch" runat="server" EnableClientScript="true"
                            OnServerValidate="ControllaPresenzaMinimiFieldSearch"  ValidationGroup="vgLibrettoImpianto"
                            ErrorMessage="Attenzione: inserire almeno tre campi di ricerca oltre ai campi obbligatori contrassegnati con l’asterisco (*)." Font-Bold="true" ForeColor="Red" /><br />
                        <asp:CustomValidator ID="cvControllaPodPdrFieldSearch" runat="server" EnableClientScript="true"
                           OnServerValidate="ControllaPodPdrFieldSearch"  ValidationGroup="vgLibrettoImpianto"
                           ErrorMessage="Attenzione: Non è possibile effettuare la ricerca impostando i valori di Pdr e/o Pod con 0000000000000." Font-Bold="true" ForeColor="Red" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowMessage" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                          <asp:Label runat="server" ID="lblMessage" Font-Bold="true" ForeColor="Red" />                      
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowGridHeaderLibretti" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        LIBRETTO IMPIANTO PRESENTE SUL CATASTO
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowGridLibretti" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" ShowHeader="false" ShowFooter="false" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpianto"
                            OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoManutentore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoAzienda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Libretti di Impianti">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoLibretti" Width="700" runat="server">
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Azienda:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Width="600">
                                                    <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("SoggettoAzienda") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Operatore/Addetto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Width="600">
                                                    <asp:Label ID="lblSoggettoManutentore" runat="server" Text='<%#Eval("SoggettoManutentore") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Codice targatura:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
                                                    <%# Eval("NumeroRevisione") is System.DBNull  ? "" : " - Rev " + Eval("NumeroRevisione")  %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Stato libretto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblStatoLibrettoImpianto" runat="server" Text='<%# Eval("StatoLibrettoImpianto") %>' />
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
                                                    <asp:Label ID="lblIndirizzo" runat="server" Text='<%#Eval("Indirizzo") %>' />
                                                    <%#Eval("Civico") %>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblResponsabile" runat="server" Text='<%#Eval("NomeResponsabile") %>' />
                                                    <%#Eval("CognomeResponsabile") %> <%#Eval("RagioneSocialeResponsabile") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>C.F./ P.Iva:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblCfPIvaResponsabile" runat="server" Text='<%#Eval("CodiceFiscaleResponsabile") %>' />
                                                    <%#Eval("PartitaIvaResponsabile") %>
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
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf libretto impianto" AlternateText="Visualizza pdf libretto impianto" ImageUrl="~/images/Buttons/pdf.png"
                                            OnCommand="RowCommand" CommandName="Pdf" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="DataRevisione" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowAzienda" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="cmbAziende" Text="Azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
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
                            ValidationGroup="vgIncaricoImpianto"
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

                <asp:TableRow runat="server" ID="rowOperatore" Visible="false">
                    <asp:TableCell runat="server" Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloOperatore" AssociatedControlID="cmbAddetti" Text="Operatore/Addetto (*)" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="65%" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAddetti" Theme="Default"
                            TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32"
                            Width="350px" DropDownWidth="350px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox2"
                            ValidationGroup="vgIncaricoImpianto"
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
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowPresaInCarico" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpiantiSearch_btnPresaInCarico" TabIndex="1" ValidationGroup="vgIncaricoImpianto" runat="server" OnClientClick="javascript:return confirm('Confermi di prendere in carico questo Libretto di Impianto?');" CssClass="buttonClass" Width="370"
                            OnClick="LIM_LibrettiImpiantiSearch_btnPresaInCarico_Click" Text="PRESA IN CARICO LIBRETTO IMPIANTO" />
                        <asp:ValidationSummary ID="vsIncaricoImpianto" ValidationGroup="vgIncaricoImpianto" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
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
