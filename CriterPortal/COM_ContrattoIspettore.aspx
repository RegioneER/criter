<%@ Page Title="Contratto Ispettore" Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="COM_ContrattoIspettore.aspx.cs" Inherits="COM_ContrattoIspettore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblNuovoModificaContrattoIspettore" runat="server" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <asp:Label ID="lblTitoloContrattoIspettore" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="cmbIspettore" Text="Ispettore (*)" />
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
                        <asp:RequiredFieldValidator ID="rfvIspettore" runat="server" ControlToValidate="cmbIspettore" ValidationGroup="vgContratto"
                            ErrorMessage="Ispettore: Campo obbligatorio" InitialValue="">&nbsp;*</asp:RequiredFieldValidator>
                         <asp:CustomValidator ID="cvControllaContrattoPresente" runat="server" ValidationGroup="vgContratto" EnableClientScript="true" OnServerValidate="ControllaContrattoPresente"
                            ErrorMessage="Attenzione: è già presente a sistema un contratto per l'ispettore selezionato<br/>" Display="Dynamic" SetFocusOnError="true" ControlToValidate="cmbIspettore">&nbsp;*</asp:CustomValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoContratto" AssociatedControlID="cmbIspettore" Text="Stato Contratto (*)" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoContratto" runat="server" RepeatColumns="1" RepeatLayout="Table" />
                        <asp:RequiredFieldValidator ID="rfvStatoContratto" runat="server" ControlToValidate="rblStatoContratto" ValidationGroup="vgContratto" Display="Dynamic"
                            ErrorMessage="Stato Contratto: Campo obbligatorio" InitialValue="" EnableClientScript="true">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataInizioContratto" Text="Data inizio contratto (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="100" TabIndex="1" runat="server" MaxLength="10" ID="txtDataInizioContratto" ValidationGroup="vgContratto" />
                        <asp:RequiredFieldValidator
                            ID="rfvDataInizioContratto" ForeColor="Red" runat="server" ValidationGroup="vgContratto" EnableClientScript="true" ErrorMessage="Data inizio contratto: campo obbligatorio"
                            ControlToValidate="txtDataInizioContratto">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revDataInizioContratto" ControlToValidate="txtDataInizioContratto" ForeColor="Red" ErrorMessage="Data inizio contratto: campo non valido" ValidationGroup="vgContratto"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataFineContratto" Text="Data fine contratto (gg/mm/aaaa) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass_o" Width="100" TabIndex="1" MaxLength="10" runat="server" ID="txtDataFineContratto" />
                        <asp:RequiredFieldValidator
                            ID="rfvDataFineContratto" ForeColor="Red" runat="server" ValidationGroup="vgContratto" EnableClientScript="true" ErrorMessage="Data fine contratto: campo obbligatorio"
                            ControlToValidate="txtDataFineContratto">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revDataFineContratto" ControlToValidate="txtDataFineContratto" ForeColor="Red" ErrorMessage="Data fine contratto: campo non valido"
                            runat="server" ValidationGroup="vgContratto" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label ID="lblTitoloNumeroIspezioni" Text="Numero ispezione massime (*)" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtNumeroIspezioniMax" Width="100" runat="server" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvNumeroIspezioniMax" ForeColor="Red" runat="server" EnableClientScript="true"
                            ErrorMessage="Numero ispezione massime: campo obbligatorio" ValidationGroup="vgContratto"
                            ControlToValidate="txtNumeroIspezioniMax">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloFirma" Text="Firma" AssociatedControlID="" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Image runat="server" ID="imgFirma" ImageAlign="AbsMiddle" />
                        <br />
                        <asp:FileUpload ID="updFirma" runat="server" Width="250px" />&nbsp;
				        <asp:Button ID="btnUploadFirma" runat="server" Text="Carica firma" ValidationGroup="vgContratto" CssClass="buttonSmallClass" OnClick="btnUploadFirma_Click" />&nbsp;
                        <asp:Button ID="btnDeleteFirma" runat="server" Text="Cancella firma" OnClientClick="javascript:return confirm('Confermi la cancellazione della firma?')" CausesValidation="false" CssClass="buttonSmallClass" OnClick="btnDeleteFirma_Click" />
                        <asp:RegularExpressionValidator ID="revUploadFirma" runat="server" ValidationGroup="vgContratto"
                                ErrorMessage="Accettate solo immagini con formato .png o .jpg"
                                EnableClientScript="true" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                ValidationExpression="^.+\.((jpg)|(JPG)|(jpeg)|(JPEG)|(png)|(PNG))$"
                                ControlToValidate="updFirma"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow ID="rowAttivo" CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblAttivo" Text="Attivo ( SI / NO )" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:CheckBox ID="cbAttivo" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDocumenti" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">                       
                         <h2> DOCUMENTI CONTRATTO ISPETTORE </h2>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowFileManager" Visible="false">
                    <asp:TableCell ColumnSpan="2">
                        <dx:ASPxFileManager ID="fileManagerDocumenti" runat="server" Height="500" Width="896">
                            <Settings AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.docx,.png,.xls,.xlsx,.doc,.pdf,.msg,.p7m" />
                            <SettingsEditing AllowCopy="false" AllowCreate="false" AllowDelete="true" AllowDownload="true" AllowMove="false" AllowRename="true" />
                            <SettingsFolders Visible="false" />
                            <SettingsFileList ShowFolders="false" View="Details" ShowParentFolder="false" />
                            <SettingsBreadcrumbs Visible="false" ShowParentFolderButton="false" Position="Top" />
                            <SettingsUpload UseAdvancedUploadMode="true" Enabled="true">
                                <AdvancedModeSettings EnableMultiSelect="true" />
                            </SettingsUpload>
                            <Styles FolderContainer-Width="25%" UploadPanel-Height="60" ToolbarItem-Width="1" UploadPanel-Font-Size="10" />
                            <SettingsToolbar ShowPath="false" />
                        </dx:ASPxFileManager>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="2">
                        <br />
                        <asp:Button ID="btnAnnulla" TabIndex="1" runat="server" CssClass="buttonClass" Width="200"
                             OnClick="btnAnnulla_Click" Text="ANNULLA" />&nbsp; 
                        <asp:Button ID="btnSalvaDati" TabIndex="1" runat="server" CssClass="buttonClass" Width="200"
                            OnClick="btnSalvaDati_Click" Text="SALVA DATI CONTRATTO" CausesValidation="True" ValidationGroup="vgContratto" />
                        <asp:ValidationSummary ID="vgContratto" ValidationGroup="vgContratto" runat="server" CssClass="testoerr" ShowMessageBox="True"
                            ShowSummary="false" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadFirma" />
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