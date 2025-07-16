<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_ImportazioneDatiDistributore.aspx.cs" Inherits="COM_ImportazioneDatiDistributore" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        GESTIONE IMPORTAZIONE FILE XML DISTRIBUTORI DI COMBUSTIBILE
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDistributore" AssociatedControlID="cmbAziende" Text="Distributore di combustibile (*)" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAziende" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="false"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Distributore di combustibile" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="300" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox1"
                            ValidationGroup="vgUploadFileXml"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Distributore di combustibile: campo obbligatorio"
                            ControlToValidate="cmbAziende">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAnnoRiferimento" AssociatedControlID="cmbAnni" Text="Anno di riferimento (*)" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbAnni" Theme="Default" TabIndex="1"
                            ValueType="System.Int32" 
                            Width="150px" 
                            DropDownWidth="145px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox2"
                            ValidationGroup="vgUploadFileXml"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Anno di riferimento: campo obbligatorio"
                            ControlToValidate="cmbAnni">&nbsp;*</asp:RequiredFieldValidator>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloFileXml" AssociatedControlID="UploadFileXml" Text="File xml da importare (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:FileUpload ID="UploadFileXml" ToolTip="Scegli file" TabIndex="1" ValidationGroup="vgUploadFileXml" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFileXml" runat="server"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgUploadFileXml"
                            ErrorMessage="File xml da importare: campo necessario"
                            ControlToValidate="UploadFileXml">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileXml" runat="server"
                            ErrorMessage="File xml da importare non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.xml|.Xml|.xMl|.xmL|.XML)$" ValidationGroup="vgUploadFileXml"
                            ControlToValidate="UploadFileXml"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnElaboraFileXml" runat="server" Text="AVVIA ELABORAZIONE FILE XML" Width="250"
                            CausesValidation="True" ValidationGroup="vgUploadFileXml"
                            OnClick="btnElaboraFileXml_Click" CssClass="buttonClass" />
                        <asp:ValidationSummary ID="vgUploadFileXml" ValidationGroup="vgUploadFileXml" runat="server" CssClass="testoerr" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblEsitoImportazione" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Table ID="tblMasterDetails" runat="server" Width="900" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell>
                        <dx:ASPxGridView ID="Grid" runat="server" KeyFieldName="ID" ClientInstanceName="grid" AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="Grid_HtmlRowCreated">
                            <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-CssClass="GridHeader" Row-CssClass="GridItem"/>
                            <SettingsPager AllButton-ShowDefaultText="false" FirstPageButton-ShowDefaultText="false" LastPageButton-ShowDefaultText="false" ShowDefaultImages="false" NextPageButton-ShowDefaultText="false" PrevPageButton-ShowDefaultText="false" Summary-Visible="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="NomeAzienda" Caption="Distributori di combustibile" />
                                <dx:GridViewDataColumn FieldName="NomeFile" />                               
                                <dx:GridViewDataColumn FieldName="NumeroUtenze" Caption="Numero utenze importate">
                                    <CellStyle HorizontalAlign="Center" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="AnnoRiferimento" Caption="Anno di riferimento">
                                    <CellStyle HorizontalAlign="Center" />
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataMemoColumn FieldName="DataOraElaborazione" SortOrder="Descending">
                                    <CellStyle HorizontalAlign="Center" />
                                </dx:GridViewDataMemoColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" Visible="false">
                                    <DataItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" Style="cursor: pointer;" 
                                            ImageUrl="~/images/buttons/deleteSmall.png" CausesValidation="false" 
                                            ToolTip="Cancella file Xml" EnableViewState="false" CommandArgument="<%# Container.KeyValue %>"
                                            OnClientClick="javascript:return confirm('Confermi di cancancellare i dati di questo tracciato Xml?');"/>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnElaboraFileXml" />
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