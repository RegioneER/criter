<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ANAG_AnagraficaAccreditamento.aspx.cs" Inherits="ANAG_AnagraficaAccreditamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblAccreditamentoIspettore" runat="server" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2><asp:Label runat="server" ID="lblTitoloTipoSoggetto" /></h2>
                        <asp:Label runat="server" ID="lblIDStatoAccreditamento" Visible="false" />
                        <asp:Label runat="server" ID="lblfAttivo" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label ID="lblTitoloSoggetto" AssociatedControlID="lnkSoggetto" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell Width="600">
                        <asp:HyperLink runat="server" ID="lnkSoggetto" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloEmail" AssociatedControlID="lblEmail" Text="Email" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblEmail" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloEmailPec" AssociatedControlID="lblEmailPec" Text="Email Pec" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblEmailPec" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <h3>Gestione stato di Accreditamento</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloStatoAccreditamento" AssociatedControlID="rblStatoAccreditamento" Text="Stato di accreditamento" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <dx:ASPxRadioButtonList ID="rblStatoAccreditamento" AutoPostBack="true" RepeatColumns="2"  
                            ClientInstanceName="rblStatoAccreditamento" Border-BorderStyle="None" RepeatDirection="Horizontal" 
                            OnSelectedIndexChanged="rblStatoAccreditamento_SelectedIndexChanged" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowDataAccreditamento" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloDataAccreditamento" AssociatedControlID="txtDataAccreditamento" Text="Data accreditamento (gg/mm/aaaa)" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblDataAccreditamento" />
                        <asp:TextBox runat="server" ID="txtDataAccreditamento" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataAccreditamento" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Data di accreditamento: campo obbligatorio"
                            ControlToValidate="txtDataAccreditamento">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtDataAccreditamento" ValidationGroup="vgAccreditamento" ControlToValidate="txtDataAccreditamento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di accreditamento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowDataRinnovo" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloDataRinnovo" AssociatedControlID="lblDataRinnovo" Text="Data rinnovo (gg/mm/aaaa)" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" ID="lblDataRinnovo" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowDataAnnullamento" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloDataAnnullamento" AssociatedControlID="txtDataAnnullamento" Text="Data annullamento (gg/mm/aaaa)" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtDataAnnullamento" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataAnnullamento" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Data di annullamento: campo obbligatorio"
                            ControlToValidate="txtDataAnnullamento">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtDataAnnullamento" ValidationGroup="vgAccreditamento" ControlToValidate="txtDataAnnullamento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di annullamento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowMotivoAnnullamento" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloMotivoAnnullamento" AssociatedControlID="txtMotivoAnnullamento" Text="Motivo annullamento" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtMotivoAnnullamento" TextMode="MultiLine" Width="580" Height="200" Rows="4" TabIndex="1" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtMotivoAnnullamento" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Motivo annullamento: campo obbligatorio"
                            ControlToValidate="txtMotivoAnnullamento">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowDataSospensione" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloDataSospensione" Text="Data sospensione (gg/mm/aaaa)" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        da:<asp:TextBox runat="server" ID="txtDataSospensioneDa" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataSospensioneDa" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Data di sospensione da: campo obbligatorio"
                            ControlToValidate="txtDataSospensioneDa">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtDataSospensioneDa" ValidationGroup="vgAccreditamento" ControlToValidate="txtDataSospensioneDa" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di sospensione da: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        a:<asp:TextBox runat="server" ID="txtDataSospensioneA" Width="80" TabIndex="1" MaxLength="10" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataSospensioneA" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Data di sospensione a: campo obbligatorio"
                            ControlToValidate="txtDataSospensioneA">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtDataSospensioneA" ValidationGroup="vgAccreditamento" ControlToValidate="txtDataSospensioneA" Display="Dynamic" ForeColor="Red" ErrorMessage="Data di sospensione a: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowMotivoSospensione" Visible="false">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblTitoloMotivoSospensione" AssociatedControlID="txtMotivoSospensione" Text="Motivo sospensione" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtMotivoSospensione" TextMode="MultiLine" Width="580" Height="200" Rows="4" TabIndex="1" ValidationGroup="vgAccreditamento" CssClass="txtClass_o" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtMotivoSospensione" ForeColor="Red" runat="server" ValidationGroup="vgAccreditamento" EnableClientScript="true" ErrorMessage="Motivo sospensione: campo obbligatorio"
                            ControlToValidate="txtMotivoSospensione">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowAttivo" CssClass="riempimento">
                    <asp:TableCell CssClass="riempimento2">
                        <asp:Label ID="lblAttivo" Text="Attivo su ispezione ( SI / NO )" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell>
                       <asp:ImageButton runat="server" ID="imgFlagAttivo" BorderStyle="None" OnClick="imgFlagAttivo_Click" AlternateText="Attiva/Disattiva il soggetto" ToolTip="Attiva/Disattiva il soggetto" OnClientClick="javascript:return confirm('Confermi il cambio di stato attivo/non attivo');" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowAttivazioneUtenzaIspettore" runat="server" CssClass="riempimento" Visible="false">
					<asp:TableCell Width="225" CssClass="riempimento2">
						<asp:Label runat="server" ID="lblAttivazioneUtenzaIspettore" AssociatedControlID="imgAttivaUtenzaIspettore" Text="Attivazione credenziali di accesso al Criter" />
					</asp:TableCell>
					<asp:TableCell>
                        <asp:ImageButton runat="server" ID="imgAttivaUtenzaIspettore" OnClick="imgAttivaUtenzaIspettore_Click" BorderStyle="None" 
                            ImageUrl="~/images/buttons/access.png" AlternateText="Attiva utenza ispettore" ToolTip="Attiva utenza ispettore" 
                            OnClientClick="javascript:return confirm('Confermi di attivare l\'utenza all\'ispettore?');" TabIndex="1" />
					</asp:TableCell>
				</asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <h3>Storico cambio stati accreditamento</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                         <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" 
                            PageSize="100" AllowSorting="false" AllowPaging="false"
                            AutoGenerateColumns="False" OnItemDataBound="DataGrid_ItemDataBound" runat="server" DataKeyField="IDAccreditamentoStato"
                            >
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDAccreditamentoStato" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggetto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoAccreditamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteUltimaModifica" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataAnnullamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="MotivazioneAnnullamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataSospensioneDa" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataSospensioneA" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="MotivazioneSospensione" Visible="false" ReadOnly="True" />
                                
                                <asp:BoundColumn DataField="Data" ItemStyle-Width="190px" HeaderText="Data" ReadOnly="True" />
                                <asp:BoundColumn DataField="Utente" HeaderText="Utente" ItemStyle-Width="300px" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoAccreditamento" HeaderText="Stato accreditamento" ItemStyle-Width="200px" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSospensioneAnnullamento" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTitoloDocumenti" runat="server">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">                       
                         <h3>Documenti accreditamento Ispettore</h3>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow CssClass="riempimento" runat="server" ID="rowDocumenti">
                    <asp:TableCell ColumnSpan="2">
                        <dx:ASPxFileManager ID="fileManagerDocumenti" runat="server" Height="500" >
                            <Settings AllowedFileExtensions=".jpg,.jpeg,.gif,.rtf,.txt,.docx,.png,.xls,.xlsx,.doc,.pdf,.msg,.p7m" />
                            <SettingsEditing AllowCopy="false" AllowCreate="false" AllowDelete="true" AllowDownload="true" AllowMove="false" AllowRename="true" />
                            <SettingsFolders Visible="false" />
                            <SettingsFileList ShowFolders="false" View="Details" ShowParentFolder="false" />
                            <SettingsBreadcrumbs Visible="false" ShowParentFolderButton="false" Position="Top" />
                            <SettingsUpload UseAdvancedUploadMode="true" Enabled="true" >
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
                        <asp:Button ID="btnSalvaDati" TabIndex="1" runat="server" CssClass="buttonClass" Width="300"
                            OnClick="btnSalvaDati_Click" Text="SALVA DATI ACCREDITAMENTO" CausesValidation="True" ValidationGroup="vgAccreditamento" />
                        <asp:ValidationSummary ID="vgAccreditamento" ValidationGroup="vgAccreditamento" runat="server" CssClass="testoerr" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
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