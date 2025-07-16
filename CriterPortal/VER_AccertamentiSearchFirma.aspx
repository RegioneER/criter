<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_AccertamentiSearchFirma.aspx.cs" Inherits="VER_AccertamentiSearchFirma" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoFirmaOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblFirmaOk" Font-Bold="true" Text="Accertamenti firmati con successo" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" ID="tblAccertamenti" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA ACCERTAMENTI DA FIRMARE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row1">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
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

                <asp:TableRow runat="server" ID="row2">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row3">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceAccertamento" AssociatedControlID="txtCodiceTargatura" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceAccertamento" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row4">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataRilevazione" Text="Data accertamento (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
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
               
                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA ACCERTAMENTI" OnClick="btnRicerca_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                        <asp:Button runat="server" ID="btnConfirmFirma" Visible="false" Text="FIRMA ACCERTAMENTI SELEZIONATI" OnClick="btnConfirmFirma_Click"
                            OnClientClick="javascript:return(confirm('Confermi di firmare gli accertamenti selezionati?'));"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowFirma" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        Per completare la registrazione degli accertamenti sul Sistema Criter seguire i seguenti punti:<br />
                        <ul>
                            <li>1) scaricare, cliccando
                                <asp:HyperLink ID="imgExportZip" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica gli accertamenti in formato .zip" Target="_blank" />, gli accertamenti in formato .zip;
                            </li>
                            <li>2) salvare il file .zip sul proprio computer, scompattare i file e provvedere a firmare digitalmente gli accertamenti mediante il software in dotazione con il dispositivo di firma digitale;</li>
                            <li>3) caricare gli accertamenti firmati digitalmente in formato .p7m selezionando il pulsante Scegli files</li>
                            <li>4) cliccare sul pulsante FIRMA ACCERTAMENTI</li>
                        </ul>
                        <br />
                        <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" AllowMultiple="true" TabIndex="1" runat="server" Width="350px" />
                        <%--<asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server" ValidationGroup="vgAccertamentiUploadFile"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" 
                            ErrorMessage="Rapporto di controllo firmato digitalmente .p7m: campo necessario"
                            ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server" ValidationGroup="vgAccertamentiUploadFile"
                            ErrorMessage="Rapporto di controllo firmato digitalmente .p7m non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" 
                            ControlToValidate="UploadFileP7m"></asp:RegularExpressionValidator>--%>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowButtonFirma" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnFirmaAnnulla" CausesValidation="false" Text="ANNULLA FIRMA ACCERTAMENTI" OnClick="btnFirmaAnnulla_Click"
                            CssClass="buttonClass" Width="350px" />&nbsp;
                        <asp:Button runat="server" ID="btnFirma" ValidationGroup="vgAccertamentiUploadFile" Text="FIRMA ACCERTAMENTI" OnClick="btnFirma_Click"
                            OnClientClick="javascript:return(confirm('Confermi di firmare i documenti degli Accertamenti selezionati?'));"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row8">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" AllowSorting="True" AllowPaging="True"
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
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Accertamenti da firmare">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoAccertamenti" Width="700" runat="server">
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Azienda:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="230">
                                                    <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("NomeAzienda") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Indirizzo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" Width="230">
                                                    <asp:Label ID="lblSoggettoIndirizzo" runat="server" Text='<%#Eval("IndirizzoAzienda") %>' />
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
                                                    <asp:Label ID="lblCodiceAccertamento" runat="server" Text='<%# Eval("CodiceAccertamento") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Stato&nbsp;accertamento:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                    <asp:Label ID="lblStatoAccertamento" runat="server" Text='<%# Eval("StatoAccertamento") %>' />
                                                </asp:TableCell>
                                                 <asp:TableCell Width="100">
                                                        <b>Data&nbsp;accertamento:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblDataAccertamento" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataRilevazione")) %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                    <b>Accertatore:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                    <asp:Label ID="lblAccertatore" runat="server" Text='<%# Eval("Accertatore") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Coordinatore:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                    <asp:Label ID="lblCoordinatore" runat="server" Text='<%# Eval("Coordinatore") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                    <b>Agente Accertatore:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                    <asp:Label ID="lblAgenteAccertatore" runat="server" Text='<%# Eval("AgenteAccertatore") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="130">
                                                  
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="6">
                                                    <asp:DataGrid ID="dgDocumenti" CssClass="Grid" Width="100%" GridLines="None" HorizontalAlign="Center"
                                                        CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="5" AllowSorting="False" AllowPaging="False"
                                                        AutoGenerateColumns="False" runat="server" DataKeyField="IDAccertamentoDocumento"
                                                        OnItemDataBound="dgDocumenti_ItemDataBound">
                                                        <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridItem" />
                                                        <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="IDAccertamentoDocumento" Visible="false" ReadOnly="True" />
                                                            <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                                            <asp:BoundColumn DataField="IDProceduraAccertamento" Visible="false" ReadOnly="True" />
                                                            <asp:BoundColumn DataField="CodiceAccertamento" Visible="false" ReadOnly="True" />
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista documenti">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTipoProceduraAccertamento" runat="server" Text='<%# string.Format("{0} {1}", "Procedura ", Eval("ProceduraAccertamento")) %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf documento" AlternateText="Visualizza pdf documento" 
                                                                        ImageUrl="~/images/Buttons/pdf.png"
                                                                        CommandName="Pdf" TabIndex="1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza Accertamento" AlternateText="Visualizza Accertamento" ImageUrl="~/images/Buttons/View.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamento") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="Firma">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutti gli accertamenti per la firma" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GuidAccertamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteAgenteAccertatore" Visible="false" ReadOnly="True" />
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
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>