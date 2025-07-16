<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_ResponsabiliImpianto.aspx.cs" Inherits="LIM_ResponsabiliImpianto" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoNominaOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblFirmaOk" Font-Bold="true" Text="Nomina terzo responsabile avvenuta con successo!" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow runat="server" ID="rowRicerca1">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        NOMINA TERZO RESPONSABILE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca0">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloTipoRicerca" AssociatedControlID="rblTipoRicercaLibretti" Text="Tipo ricerca libretto di impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipoRicercaLibretti" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblTipoRicercaLibretti_SelectedIndexChanged" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Text="Ricerca libretto impianto per Codice targatura impianto" Value="0" Selected="True" />
                            <asp:ListItem Text="Ricerca libretto impianto per C.F. o P.IVA Responsabile/Numero POD/PDR" Value="1" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca2">
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

                <asp:TableRow runat="server" ID="rowRicerca5">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCfPIvaResponsabile" AssociatedControlID="txtCfPIvaResponsabile" Text="C.F./P.IVA Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCfPIvaResponsabile" Width="200" TabIndex="1" ValidationGroup="vgLibrettoImpianto" MaxLength="20" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator ID="rfvtxtCfPIvaResponsabile" runat="server" ValidationGroup="vgLibrettoImpianto" ForeColor="Red" EnableClientScript="true" 
							ErrorMessage="Codice fiscale/Partita IVA Responsabile: campo obbligatorio"
							ControlToValidate="txtCfPIvaResponsabile">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca3">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNumeroPod" Text="Numero POD o numero PDR (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtNumeroPodPdr" runat="server" Width="200" MaxLength="14" ValidationGroup="vgLibrettoImpianto" CssClass="txtClass_o" />
						<asp:RequiredFieldValidator ID="rfvtxtNumeroPodPdr" runat="server" ForeColor="Red" ValidationGroup="vgLibrettoImpianto" EnableClientScript="true" 
							ErrorMessage="Numero POD o numero PDR: campo obbligatorio"
							ControlToValidate="txtNumeroPodPdr">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpiantiSearch_btnProcess" TabIndex="1" runat="server" CssClass="buttonClass" Width="370"
                            OnClick="LIM_LibrettiImpiantiSearch_btnProcess_Click" ValidationGroup="vgLibrettoImpianto" Text="RICERCA LIBRETTO IMPIANTO PER ASSUNZIONE INCARICO" />
                        <asp:ValidationSummary ID="vsSaveLibrettoImpianto" ValidationGroup="vgLibrettoImpianto" runat="server" ShowMessageBox="True"
							ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
						</asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowRicerca8">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowIntestazioneResponsabilita" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        LIBRETTO IMPIANTO PER NOMINA TERZO RESPONSABILE
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowIntestazioneResponsabilitaLibretto" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" ShowHeader="false" ShowFooter="false" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpianto"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
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
                                                    <asp:Label ID="lblIndirizzo" runat="server" Text='<%#Eval("Indirizzo") %>' />&nbsp;
                                                    <%#Eval("Civico") %>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblResponsabile" runat="server" Text='<%#Eval("NomeResponsabile") %>' />&nbsp;
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
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowAzienda" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" 
                            Theme="Default" TabIndex="1"
                            EnableCallbackMode="false"
                            CallbackPageSize="30" 
                            AutoPostBack="true" 
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
                            ValueField="IDSoggetto"
                            OnSelectedIndexChanged="ASPxComboBox_SelectedIndexChanged"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox_ButtonClick"
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
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox1" ForeColor="Red" ValidationGroup="vgIncaricoImpianto" Display="Dynamic" runat="server" InitialValue="" ErrorMessage=""
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowNomeLegaleRappresentante" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloNome" AssociatedControlID="lblNomeLegaleRappresentante" Text="Nome legale rappresentante" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblNomeLegaleRappresentante" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCognomeLegaleRappresentante" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCognome" AssociatedControlID="lblCognomeLegaleRappresentante" Text="Cognome legale rappresentante" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label runat="server" ID="lblCognomeLegaleRappresentante" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceFiscale" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceFiscale" AssociatedControlID="lblCodiceFiscale" Text="Codice fiscale legale rappresentante" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label runat="server" ID="lblCodiceFiscale" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowPartitaIva" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPartitaIva" AssociatedControlID="lblPartitaIva" Text="Partita Iva" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label runat="server" ID="lblPartitaIva" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowEmail" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloEmail" AssociatedControlID="lblEmail" Text="Email" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label runat="server" ID="lblEmail" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowEmailPec" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloEmailPec" AssociatedControlID="lblEmailPec" Text="Email Pec" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                       <asp:Label runat="server" ID="lblEmailPec" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDataInizioResponsabilita" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDataInizioResponsabilita" AssociatedControlID="txtDataInizioResponsabile" Text="Data di inizio responsabilità (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataInizioResponsabile" ValidationGroup="vgIncaricoImpianto" Width="90" MaxLength="10" CssClass="txtClass_o" TabIndex="1" />
						<asp:requiredfieldvalidator ID="rfvDataInizioResponsabile" ValidationGroup="vgIncaricoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data inizio Responsabilità: campo obbligatorio"
								ControlToValidate="txtDataInizioResponsabile">&nbsp;*</asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator
							ID="revDataInizioResponsabile" ValidationGroup="vgIncaricoImpianto" ControlToValidate="txtDataInizioResponsabile" Display="Dynamic" ForeColor="Red" ErrorMessage="Data inizio Responsabilità: inserire la data nel formato gg/mm/aaaa"
							runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
							EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDataFineResponsabilita" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDataFineResponsabilita" AssociatedControlID="txtDataFineResponsabile" Text="Data di fine responsabilità (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtDataFineResponsabile" ValidationGroup="vgIncaricoImpianto" Width="90" MaxLength="10" CssClass="txtClass_o" TabIndex="1" />
						<asp:requiredfieldvalidator ID="rfvDataFineResponsabile" ValidationGroup="vgIncaricoImpianto" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data fine Responsabilità: campo obbligatorio"
								ControlToValidate="txtDataFineResponsabile">&nbsp;*</asp:requiredfieldvalidator>
						<asp:RegularExpressionValidator
							ID="revDataFineResponsabile" ValidationGroup="vgIncaricoImpianto" ControlToValidate="txtDataFineResponsabile" Display="Dynamic" ForeColor="Red" ErrorMessage="Data fine Responsabilità: inserire la data nel formato gg/mm/aaaa"
							runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
							EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowTerzoResponsabile" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpiantiSearch_btnAssunzioneTerzoResponsabile" TabIndex="1" ValidationGroup="vgIncaricoImpianto" runat="server" OnClientClick="javascript:return confirm('Confermi di assumere il ruolo di Terzo Responsabile per questo Libretto di Impianto?');" CssClass="buttonClass" Width="370"
                            OnClick="LIM_LibrettiImpiantiSearch_btnAssunzioneTerzoResponsabile_Click" Text="ASSUNZIONE INCARICO TERZO RESPONSABILE" />
                        <asp:ValidationSummary ID="vsIncaricoImpianto" ValidationGroup="vgIncaricoImpianto" runat="server" ShowMessageBox="True"
							ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
						</asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:CustomValidator ID="cvDateResponsabilita" runat="server" EnableClientScript="true" Display="Dynamic"
							OnServerValidate="ControllaDateResponsabilita" 
							CssClass="testoerr"
							ValidationGroup="vgIncaricoImpianto" 
							ErrorMessage="Attenzione: la data di inizio responsabilità non può essere superiore alla data di fine responsabilità" />
                        <br />
                        <asp:CustomValidator ID="cvControllaResponsabilitaGiaInCarico" runat="server" EnableClientScript="true" Display="Dynamic"
							OnServerValidate="ControllaResponsabilitaGiaInCarico" 
							CssClass="testoerr"
							ValidationGroup="vgIncaricoImpianto" 
							ErrorMessage="Attenzione: non è possibile assumere l'incarico perchè sei già il Terzo Responsabile per il medesimo libretto" />
                        <br />
                        <asp:CustomValidator ID="cvControllaResponsabilitaInCaricoAdAltri" runat="server" EnableClientScript="true" Display="Dynamic"
							OnServerValidate="ControllaResponsabilitaInCaricoAdAltri" 
							CssClass="testoerr"
							ValidationGroup="vgIncaricoImpianto" 
							ErrorMessage="Attenzione: non è possibile assumere l'incarico perchè presente già un Terzo Responsabile incaricato per il medesimo libretto!" />
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