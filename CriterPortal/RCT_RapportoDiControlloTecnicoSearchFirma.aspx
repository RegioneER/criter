<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_RapportoDiControlloTecnicoSearchFirma.aspx.cs" Inherits="RCT_RapportoDiControlloTecnicoSearchFirma" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentDisplay" ID="Content">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoFirmaOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblFirmaOk" Font-Bold="true" Text="Rapporti di controllo firmati con successo" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" ID="tblRapportiControllo" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA RAPPORTI DI CONTROLLO TECNICO DA FIRMARE
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento">
                        La registrazione obbligatoria dei Rapporti di controllo di efficienza energetica nel sistema CRITER della Regione Emilia-Romagna avviene in forma di dichiarazione sostitutiva di atto notorio ai sensi dell'articolo 47 del T.U. delle disposizioni legislative e regolamentari in materia di documentazione amministrativa, di cui al decreto del Presidente della Repubblica 28 dicembre 2000, n. 445.
                        In ogni caso il soggetto si impegna a garantire la totale conformità dei Rapporti di controllo tecnico di efficienza energetica registrati nel sistema CRITER con gli originali consegnati ai clienti, assumendo le relative responsabilità di legge.
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row1">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
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
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row2">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPersona" AssociatedControlID="ASPxComboBox2" Text="Operatore/Addetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Theme="Default" TabIndex="1"
                            TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32"
                            Width="350px" DropDownWidth="300px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row3">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row4">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="RCT_RapportoDiControllo_lblTipologieRapportiDiControllo" AssociatedControlID="rblTipologieRapportoDiControllo" Text="Tipologie rapporti di controllo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieRapportoDiControllo" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row5">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="RCT_RapportoDiControllo_lblTipologieControllo" AssociatedControlID="rblTipologieControllo" Text="Tipologie di controllo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieControllo" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA RAPPORTI DI CONTROLLO" OnClick="btnRicerca_Click" CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                        <asp:Button runat="server" ID="btnConfirmFirma" Visible="false" Text="FIRMA RAPPORTI DI CONTROLLO SELEZIONATI" OnClick="btnConfirmFirma_Click"
                            OnClientClick="javascript:return(confirm('Confermi di firmare i rapporti selezionati?'));"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowFileTxt" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                        <asp:TextBox ID="txtJsonTxt" runat="server" TabIndex="1" Height="300" Width="100%" ReadOnly="true" TextMode="MultiLine" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowFirma" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        Per completare la registrazione dei rapporti di controllo sul Sistema Criter seguire i seguenti punti:<br />
                        <ul>
                            <li>scaricare, cliccando
                                <asp:HyperLink ID="imgExportJson" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica i rapporti di controllo" Target="_blank" />, i rapporti di controllo in formato .txt;</li>
                            <li>salvare il file .txt sul proprio computer e provvedere a firmarlo digitalmente mediante il software in dotazione con il dispositivo di firma digitale;</li>
                            <li>caricare i rapporti di controllo firmati digitalmente in formato .p7m selezionando il pulsante Scegli file</li>
                            <li>selezionare il pulsante FIRMA E CREA RAPPORTI DI CONTROLLO DEFINITIVI</li>
                        </ul>
                        <br />
                        <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server" ValidationGroup="vgRapportiControlloUploadFile"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" 
                            ErrorMessage="Rapporto di controllo firmato digitalmente .p7m: campo necessario"
                            ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server" ValidationGroup="vgRapportiControlloUploadFile"
                            ErrorMessage="Rapporto di controllo firmato digitalmente .p7m non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" 
                            ControlToValidate="UploadFileP7m"></asp:RegularExpressionValidator>
                        <asp:Label runat="server" ForeColor="Green" ID="lblCheckP7m" Font-Bold="true" />
                        <asp:Label runat="server" ID="lblNameGlobalFile" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowSpid" Visible="false">
                    <asp:TableCell ColumnSpan="2" Width="900" CssClass="riempimento">
                        Per confermare la registrazione dei rapporti di controllo tramite sistema SPID fare click sul pulsante FIRMA E CREA RAPPORTI DI CONTROLLO DEFINITIVI<br />
                        <br />
                        <asp:Image runat="server" ID="imgSpid" ImageUrl="~/images/spid-logo-b-lb.png" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowButtonFirma" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnFirma" ValidationGroup="vgRapportiControlloUploadFile" Text="FIRMA E CREA RAPPORTI DI CONTROLLO DEFINITIVO" OnClick="btnFirma_Click"
                            OnClientClick="javascript:return(confirm('La registrazione obbligatoria del Rapporto di Controllo nel sistema Criter della Regione Emilia-Romagna avviene mediante apposizione di firma digitale sul documento che viene formato nel rispetto delle regole tecniche di cui all’articolo 71 del D.Lgs. 7 marzo 2005, n. 82-CAD, che garantiscono l’identificabilità dell’autore e l’integrità del documento stesso. La trasmissione del Rapporto di Controllo alla Regione Emilia-Romagna avviene in forma di dichiarazione sostitutiva di atto notorio ai sensi dell’articolo 47 del T.U. delle disposizioni legislative e regolamentari in materia di documentazione amministrativa, di cui al decreto del Presidente della Repubblica 28 dicembre 2000, n. 445.\n\nPrima di procedere assicurarsi di inserire nel proprio PC il dispositivo per la firma digitale.\n\nConfermando tale operazione dopo aver firmato digitalmente il rapporto di controllo non sarà più possibile effettuare modifiche e reso definitivo. Confermi?'));"
                            CssClass="buttonClass" Width="350px" />&nbsp;
                        <asp:Button runat="server" ID="btnFirmaAnnulla" CausesValidation="false" Text="ANNULLA FIRMA RAPPORTI DI CONTROLLO" OnClick="btnFirmaAnnulla_Click"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row8">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDRapportoControlloTecnico"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDRapportoControlloTecnico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoManutentore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoAzienda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoRapportoDiControllo" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Rapporti di Controllo Tecnico">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoRapporti" Width="700" runat="server">
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Azienda:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="230">
                                                    <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("SoggettoAzienda") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Operatore/Addetto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="230">
                                                    <asp:Label ID="lblSoggettoManutentore" runat="server" Text='<%#Eval("SoggettoManutentore") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="5">
                                                    <asp:Label ID="lblNomeResponsabile" runat="server" Text='<%#Eval("NomeResponsabile") %>' />&nbsp;
                                                    <asp:Label ID="lbCognomeResponsabile" runat="server" Text='<%#Eval("CognomeResponsabile") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Codice targatura:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                    <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
                                                    <%# Eval("NumeroRevisione") is System.DBNull  ? "" : " - Rev " + Eval("NumeroRevisione")  %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    <b>Generatore:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblGeneratorePrefisso" runat="server" Text='<%#Eval("Prefisso") %>' />
                                                    <asp:Label ID="lblGeneratoreCodiceProgressivo" runat="server" Text='<%#Eval("CodiceProgressivo") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Data&nbsp;controllo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblDataControllo" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataControllo")) %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Stato&nbsp;rapporto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                    <asp:Label ID="lblStatoLibrettoImpianto" runat="server" Text='<%# Eval("StatoRapportoDiControllo") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    <b>Tipologia&nbsp;rapporto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblTipologiaRapporto" runat="server" Text='<%# Eval("DescrizioneRCT") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Tipo&nbsp;controllo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblTipoRapportoControllo" runat="server" Text='<%# Eval("TipologiaControllo") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="700">
                                                <asp:TableCell ColumnSpan="6">
                                                    <asp:TextBox ID="txtJsonRapporto" runat="server" Text='<%#Eval("JsonFormat").ToString().Replace("<", " ").Replace(">", " ") %>' TabIndex="1" Height="100" Width="100%" Rows="100" ReadOnly="true" TextMode="MultiLine" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dati rapporto di controllo" AlternateText="Visualizza dati rapporto di controllo" ImageUrl="~/images/Buttons/view.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") +","+ DataBinder.Eval(Container.DataItem,"IDTipologiaRCT") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoAzienda") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoManutentore") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf rapporto di controllo" AlternateText="Visualizza pdf rapporto di controllo" ImageUrl="~/images/Buttons/pdf.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="Pdf" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="Firma">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutti i rapporti per la firma" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IDTipologiaRCT" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataControllo" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFirma" />
        </Triggers>
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