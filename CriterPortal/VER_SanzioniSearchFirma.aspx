<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_SanzioniSearchFirma.aspx.cs" Inherits="VER_SanzioniSearchFirma" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoFirmaOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblFirmaOk" Font-Bold="true" Text="Sanzioni firmate con successo" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" ID="tblAccertamenti" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        NOTIFICHE SANZIONI DA FIRMARE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row0">
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="cmbIspettore" Text="Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbIspettore" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="false"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="cmbIspettore_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="cmbIspettore_OnItemRequestedByValue"
                            OnButtonClick="cmbIspettore_ButtonClick"
                            TextFormatString="{0}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Ispettore" Width="400" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" ForeColor="Green" />                   
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row1">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row2">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceAccertamento" AssociatedControlID="txtCodiceTargatura" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceAccertamento" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceIspezione" AssociatedControlID="txtCodiceIspezione" Text="Codice ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceIspezione" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row3">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row4">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA SANZIONI DA FIRMARE" OnClick="btnRicerca_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                        <asp:Button runat="server" ID="btnConfirmFirma" Visible="false" Text="FIRMA SANZIONI SELEZIONATE" OnClick="btnConfirmFirma_Click"
                            OnClientClick="javascript:return(confirm('Confermi di firmare le sanzioni selezionate?'));"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowFirma" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        Per completare la registrazione delle sanzioni sul Sistema Criter seguire i seguenti punti:<br />
                        <ul>
                            <li>1) scaricare, cliccando
                                <asp:HyperLink ID="imgExportZip" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica le sanzioni in formato .zip" Target="_blank" />, le sanzioni in formato .zip;
                            </li>
                            <li>2) salvare il file .zip sul proprio computer, scompattare i file e provvedere a firmare digitalmente gli accertamenti mediante il software in dotazione con il dispositivo di firma digitale;</li>
                            <li>3) caricare le sanzioni firmati digitalmente in formato .p7m selezionando il pulsante Scegli files</li>
                            <li>4) cliccare sul pulsante FIRMA SANZIONI</li>
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
                        <asp:Button runat="server" ID="btnFirmaAnnulla" CausesValidation="false" Text="ANNULLA FIRMA SANZIONI" OnClick="btnFirmaAnnulla_Click"
                            CssClass="buttonClass" Width="350px" />&nbsp;
                        <asp:Button runat="server" ID="btnFirma" ValidationGroup="vgAccertamentiUploadFile" Text="FIRMA SANZIONI" OnClick="btnFirma_Click"
                            OnClientClick="javascript:return(confirm('Confermi di firmare i documenti delle Sanzioni selezionati?'));"
                            CssClass="buttonClass" Width="350px" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row5">
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
                                <asp:BoundColumn DataField="TipoAccertamento" HeaderText="Tipo Sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceSanzione" HeaderText="Cod.sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceAccertamento" HeaderText="Cod.accertamento" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" HeaderText="Cod.ispezione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="Ispettore" HeaderText="Ispettore" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" HeaderText="Cod.targatura" Visible="true" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="Generatore">
                                    <ItemTemplate>
                                        <%#Eval("Prefisso") %>&nbsp;<%#Eval("CodiceProgressivo") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dettaglio" AlternateText="Visualizza dettaglio" 
                                            ImageUrl="~/images/Buttons/viewSmall.png" TabIndex="1"
                                             />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderText="Firma">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutte le sanzioni per la firma" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GuidAccertamento" Visible="false" ReadOnly="True" />
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