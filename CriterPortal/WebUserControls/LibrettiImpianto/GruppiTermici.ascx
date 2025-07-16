<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GruppiTermici.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_GruppiTermici" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdGruppiTermici" 
    runat="server"
    DataSourceID="dsGruppiTermici" 
    AutoGenerateColumns="False" 
    Width="100%" 
    EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoGruppoTermico" 
    OnBeforePerformDataSelect="grdGruppiTermici_BeforePerformDataSelect" 
    OnRowDeleting="grdGruppiTermici_RowDeleting" 
    OnRowInserting="grdGruppiTermici_RowInserting" 
    OnRowUpdating="grdGruppiTermici_RowUpdating" 
    OnCommandButtonInitialize="grdGruppiTermici_CommandButtonInitialize" 
    OnCustomButtonCallback="grdGruppiTermici_CustomButtonCallback" 
    OnCustomButtonInitialize="grdGruppiTermici_CustomButtonInitialize"
    OnCellEditorInitialize="grdGruppiTermici_CellEditorInitialize" 
    OnRowUpdated="grdGruppiTermici_RowUpdated"
    OnStartRowEditing="grdGruppiTermici_StartRowEditing" 
    OnDataBound="DetailGrid_DataBound"
    OnRowValidating="grdGruppiTermici_OnRowValidating"
    OnHtmlRowCreated="grdGruppiTermici_HtmlRowCreated"
    >
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="20" Width="40px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="cmdSostituisci" Text="Sostituisci">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>           
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %> <%# Eval("DataDismissione", "Data dismissione {0:d}") %><br />
                <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                <b>Modello</b>: <%# Eval("Modello") %><br />
                <b>Matricola</b>: <%# Eval("Matricola") %><br />
                <b>Combustibile</b>: <%# Eval("SYS_TipologiaCombustibile.TipologiaCombustibile") %> <%# Eval("CombustibileAltro") %><br />
                <b>Fluido termovettore</b>: <%# Eval("SYS_TipologiaFluidoTermoVettore.TipologiaFluidoTermoVettore") %> <%# Eval("FluidoTermovettoreAltro") %><br />
                <b>Potenza termica utile nominale Pn max</b>: <%# Eval("PotenzaTermicaUtileNominaleKw") %> kW<br />
                <b>Rendimento termico utile a Pn max</b>: <%# Eval("RendimentoTermicoUtilePc") %> %<br />
                <b>Tipologia</b>: <%# Eval("SYS_TipologiaGruppiTermici.TipologiaGruppiTermici") %> <%# Eval("AnalisiFumoPrevisteNr", " (Previste {0} analisi fumo)") %><br />
                <asp:Label runat="server" ID="lblGeneratoreDismesso" Visible="false" Font-Bold="true" ForeColor="Red"/>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Gruppo termico" VisibleIndex="4" Visible="false">
            <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top" />
            <EditItemTemplate>
                <%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="DataInstallazione" Caption="Data installazione" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesDateEdit Width="160px">
                <ValidationSettings CausesValidation="True" >
                    <RequiredField ErrorText="Inserire la data di installazione" IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>  
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn FieldName="DataDismissione" Caption="Data dismissione" VisibleIndex="6" Visible="false">
            <EditFormSettings Visible="False" CaptionLocation="Top" />
            <PropertiesDateEdit Width="160px"></PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="Fabbricante" VisibleIndex="7" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il fabbricante" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Modello" VisibleIndex="8" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il modello" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Matricola" VisibleIndex="9" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings ColumnSpan="2" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la matricola" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataComboBoxColumn Caption="Combustibile" FieldName="IDTipologiaCombustibile" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditItemTemplate>
                <dx:ASPxComboBox runat="server" Width="100%" ValueType="System.Int32"   
                    TextField="TipologiaCombustibile" Value='<%# Bind("IDTipologiaCombustibile") %>' DataSourceID="dsTipologiaCombustibile" ValueField="IDTipologiaCombustibile" ID="ddlTipologiaCombustibile"
                    ClientInstanceName="ddlTipologiaCombustibile">
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Inserire la tipologia di combustibile" IsRequired="True" />
                    </ValidationSettings>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                            { 
                                                                if(s.GetValue()==1) 
                                                                    pnlCombustibileAltro.SetVisible(true);  
                                                                else 
                                                                    pnlCombustibileAltro.SetVisible(false);
                                                            }" />
                </dx:ASPxComboBox>
            </EditItemTemplate>
        </dx:GridViewDataComboBoxColumn>
        
        <%--<dx:GridViewDataComboBoxColumn Caption="Combustibile" FieldName="IDTipologiaCombustibile" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsTipologiaCombustibile" TextField="TipologiaCombustibile" ValueField="IDTipologiaCombustibile" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia di combustibile" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if(s.GetValue()==1) 
                                                                pnlCombustibileAltro.SetVisible(true);  
                                                            else 
                                                                pnlCombustibileAltro.SetVisible(false);
                                                        }" />
            </PropertiesComboBox>

            

        </dx:GridViewDataComboBoxColumn>--%>
        <dx:GridViewDataTextColumn FieldName="CombustibileAltro" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlCombustibileAltro" ClientInstanceName="pnlCombustibileAltro" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel ID="lblCombustibileAltro" runat="server" Text="Altro combustibile"></dx:ASPxLabel><br />
                            <dx:ASPxTextBox ID="txtCombustibileAltro" runat="server" 
                                Value='<%# Bind("CombustibileAltro") %>'
                                OnInit="txtCombustibileAltro_Init" Width="360px"
                                ClientInstanceName="txtCombustibileAltro">
                                <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField ErrorText="Inserire il combustibile" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <%--<dx:GridViewDataComboBoxColumn Caption="Fluido termovettore" FieldName="IDTipologiaFluidoTermoVettore" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsTipologiaFluidoTermovettore" TextField="TipologiaFluidoTermovettore" ValueField="IDTipologiaFluidoTermoVettore" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia di fluido termovettore" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if(s.GetValue()==1) 
                                                                pnlFluidoTermovettoreAltro.SetVisible(true); 
                                                            else 
                                                                pnlFluidoTermovettoreAltro.SetVisible(false);
                                                        }" />
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>--%>
        
        <dx:GridViewDataComboBoxColumn Caption="Fluido termovettore" FieldName="IDTipologiaFluidoTermoVettore" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditItemTemplate>
                <dx:ASPxComboBox runat="server" Width="100%" ValueType="System.Int32"   
                        TextField="TipologiaFluidoTermovettore" Value='<%# Bind("IDTipologiaFluidoTermoVettore") %>' DataSourceID="dsTipologiaFluidoTermovettore" ValueField="IDTipologiaFluidoTermoVettore" ID="ddlTipologiaFluidoTermovettore"
                        ClientInstanceName="ddlTipologiaFluidoTermovettore">
                    <ValidationSettings CausesValidation="True">
                        <RequiredField ErrorText="Inserire la tipologia di fluido termovettore" IsRequired="True" />
                    </ValidationSettings>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                            { 
                                                                if(s.GetValue()==1) 
                                                                    pnlFluidoTermovettoreAltro.SetVisible(true); 
                                                                else 
                                                                    pnlFluidoTermovettoreAltro.SetVisible(false);
                                                            }" />
                </dx:ASPxComboBox>
             </EditItemTemplate>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="FluidoTermovettoreAltro" Caption="Fluido Termovettore" VisibleIndex="11" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlFluidoTermovettoreAltro" ClientInstanceName="pnlFluidoTermovettoreAltro" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel ID="lblFluidoTermovettoreAltro" runat="server" Text="Altro fluido termovettore"></dx:ASPxLabel><br />
                            <dx:ASPxTextBox ID="txtFluidoTermovettoreAltro" runat="server" 
                                OnInit="txtFluidoTermovettoreAltro_Init" Width="360px"
                                Value='<%# Bind("FluidoTermovettoreAltro") %>'
                                ClientInstanceName="txtFluidoTermovettoreAltro">
                                <ValidationSettings CausesValidation="true" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField ErrorText="Inserire il fluido termovettore" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Tipologia" FieldName="IDTipologiaGruppiTermici" VisibleIndex="17" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsTipologiaGruppiTermici" TextField="TipologiaGruppiTermici" ValueField="IDTipologiaGruppiTermici" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if(s.GetValue()==2) 
                                                                pnlAnalisiFumoPreviste.SetVisible(true); 
                                                            else 
                                                                pnlAnalisiFumoPreviste.SetVisible(false); 
                                                        }" />
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaTermicaUtileNominaleKw" VisibleIndex="13" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Potenza termica utile nominale Pn max (kW)" />
            <PropertiesSpinEdit MinValue="1" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza termica utile nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="RendimentoTermicoUtilePc" VisibleIndex="16" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Rendimento termico utile a Pn max (%)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="{0}%" NumberFormat="Percent" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il rendimento termico utile" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataTextColumn FieldName="AnalisiFumoPrevisteNr" VisibleIndex="19" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlAnalisiFumoPreviste" ClientInstanceName="pnlAnalisiFumoPreviste" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" Text="Numero analisi fumi previste"></dx:ASPxLabel><br />
                            <dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="spinNrAnalisiFumoPreviste" ClientInstanceName="spinNrAnalisiFumoPreviste" runat="server" 
                                OnInit="spinNrAnalisiFumoPreviste_Init" DisplayFormatString="g0" 
                                NumberType="Integer" DecimalPlaces="0" 
                                Value='<%# Bind("AnalisiFumoPrevisteNr") %>'>
                                <ClientSideEvents />
                            </dx:ASPxSpinEdit>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
    <ClientSideEvents CustomButtonClick="function(s, e) {if (confirm ('Confermi di Sostituire il componente?')) {e.processOnServer = true;}}" />
    <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
    <SettingsEditing EditFormColumnCount="2"></SettingsEditing>
    <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
    <SettingsPager Mode="ShowPager" PageSize="5" />
    <SettingsDetail ShowDetailRow="True" />
    <Styles>
        <RowHotTrack Cursor="pointer"></RowHotTrack>
    </Styles>
    <Templates>
        <DetailRow>
            <dx:ASPxLabel ID="lblGridSostituzioni" runat="server" Text="SOSTITUZIONI DEL COMPONENTE"></dx:ASPxLabel>       
            <dx:ASPxGridView ID="grdSostituzioni" runat="server" DataSourceID="dsGruppiTermiciSostituzioni" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoGruppoTermico" 
                OnBeforePerformDataSelect="grdSostituzioni_BeforePerformDataSelect"
                OnDataBound="grdSostituzioni_DataBound">
                <Columns>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                            <b>Modello</b>: <%# Eval("Modello") %><br />
                            <b>Matricola</b>: <%# Eval("Matricola") %><br />
                            <b>Combustibile</b>: <%# Eval("SYS_TipologiaCombustibile.TipologiaCombustibile") %> <%# Eval("CombustibileAltro") %><br />
                            <b>Fluido termovettore</b>: <%# Eval("SYS_TipologiaFluidoTermoVettore.TipologiaFluidoTermoVettore") %> <%# Eval("FluidoTermovettoreAltro") %><br />
                            <b>Potenza termica termica utile nominale Pn max</b>: <%# Eval("PotenzaTermicaUtileNominaleKw") %> kW<br />
                            <b>Rendimento termico utile a Pn max</b>: <%# Eval("RendimentoTermicoUtilePc") %> %<br />
                            <b>Tipo componente</b>: <%# Eval("SYS_TipologiaGruppiTermici.TipologiaGruppiTermici") %> <%# Eval("AnalisiFumoPrevisteNr", " (Previste {0} analisi fumo)") %><br />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:Table ID="tblTitleBruciatori" Width="100%" runat="server" >
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                        <h3>4.2 BRUCIATORI (se non incorporati nel gruppo termico)</h3>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <dx:ASPxGridView ID="grdBruciatori" runat="server" DataSourceID="dsBruciatori" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoBruciatore" 
                OnBeforePerformDataSelect="grdBruciatori_BeforePerformDataSelect" 
                OnRowDeleting="grdBruciatori_RowDeleting" 
                OnRowInserting="grdBruciatori_RowInserting" 
                OnRowUpdating="grdBruciatori_RowUpdating" 
                OnCommandButtonInitialize="grdBruciatori_CommandButtonInitialize" 
                OnCustomButtonCallback="grdBruciatori_CustomButtonCallback" 
                OnCustomButtonInitialize="grdBruciatori_CustomButtonInitialize"
                OnCellEditorInitialize="grdBruciatori_CellEditorInitialize" 
                OnDetailRowGetButtonVisibility="grdBruciatori_DetailRowGetButtonVisibility"
                OnRowUpdated="grdBruciatori_RowUpdated"
                OnStartRowEditing="grdBruciatori_StartRowEditing"
                OnDataBound="DetailGrid_DataBound"
                OnRowValidating="grdBruciatori_OnRowValidating"
                >
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="17" Width="40px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="cmdSostituisciBruciatore" Text="Sostituisci"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                            <b>Modello</b>: <%# Eval("Modello") %><br />
                            <b>Matricola</b>: <%# Eval("Matricola") %><br />
                            <b>Tipologia</b>: <%# Eval("Tipologia") %><br />
                            <b>Potenza termica termica utile nominale Pn max</b>: <%# Eval("PortataTermicaMaxNominaleKw") %> kW<br />
                            <b>Portata termica nominale min</b>: <%# Eval("PortataTermicaMinNominaleKw") %> kW<br />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Bruciatore" VisibleIndex="1" Visible="false">
                        <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top" />
                        <EditItemTemplate>
                            <%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DataInstallazione" Caption="Data installazione" VisibleIndex="2" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
                        <PropertiesDateEdit Width="160px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="DataDismissione" Caption="Data dismissione" VisibleIndex="3" Visible="false">
                        <EditFormSettings Visible="False" CaptionLocation="Top" />
                        <PropertiesDateEdit Width="160px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Fabbricante" VisibleIndex="4" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire il fabbricante" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Modello" VisibleIndex="5" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire il modello" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Matricola" VisibleIndex="6" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <EditFormSettings ColumnSpan="2" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire la matricola" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Tipologia" Caption="Tipologia" VisibleIndex="7" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="PortataTermicaMaxNominaleKw" VisibleIndex="8" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <EditFormSettings Caption="Portata termica nominale max" />
                        <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire la portata termica nominale max" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="PortataTermicaMinNominaleKw" VisibleIndex="9" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <EditFormSettings Caption="Portata termica nominale min" />
                        <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="{0}%" NumberFormat="Percent" Width="100px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Portata termica nominale min" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                </Columns>
                <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
                <ClientSideEvents CustomButtonClick="function(s, e) {if (confirm ('Confermi di Sostituire il componente?')) {e.processOnServer = true;}}" />
                <SettingsEditing EditFormColumnCount="2"></SettingsEditing>
                <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsDetail ShowDetailRow="True" />
                <Styles>
                    <RowHotTrack Cursor="pointer"></RowHotTrack>
                </Styles>
                <Templates>
                    <DetailRow>
                        SOSTITUZIONI DEL COMPONENTE
                        <dx:ASPxGridView ID="grdBruciatoriSostituzioni" runat="server" DataSourceID="dsBruciatoriSostituzioni" Width="100%" EnableRowsCache="False"
                            KeyFieldName="IDLibrettoImpiantoBruciatore" 
                            OnBeforePerformDataSelect="grdBruciatoriSostituzioni_BeforePerformDataSelect" >
                            <Columns>
                                <dx:GridViewDataTextColumn>
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                                        <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                                        <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                                        <b>Modello</b>: <%# Eval("Modello") %><br />
                                        <b>Matricola</b>: <%# Eval("Matricola") %><br />
                                        <b>Tipologia:</b>: <%# Eval("Tipologia") %><br />
                                        <b>Potenza termica termica utile nominale Pn max</b>: <%# Eval("PortataTermicaMaxNominaleKw") %> kW<br />
                                        <b>Portata termica nominale min</b>: <%# Eval("PortataTermicaMinNominaleKw") %> kW<br />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
            </dx:ASPxGridView>

            <asp:Table ID="tblRecuperatori" Width="100%" runat="server" >
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                        <h3>4.3 RECUPERATORI / CONDENSATORI LATO FUMI (se non incorporati nel gruppo termico)</h3>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <dx:ASPxGridView ID="grdRecuperatori" runat="server" DataSourceID="dsRecuperatori" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoRecuperatore" 
                OnBeforePerformDataSelect="grdRecuperatori_BeforePerformDataSelect" 
                OnRowDeleting="grdRecuperatori_RowDeleting" 
                OnRowInserting="grdRecuperatori_RowInserting" 
                OnRowUpdating="grdRecuperatori_RowUpdating" 
                OnCommandButtonInitialize="grdRecuperatori_CommandButtonInitialize" 
                OnCustomButtonCallback="grdRecuperatori_CustomButtonCallback" 
                OnCustomButtonInitialize="grdRecuperatori_CustomButtonInitialize"
                OnCellEditorInitialize="grdRecuperatori_CellEditorInitialize" 
                OnDetailRowGetButtonVisibility="grdRecuperatori_DetailRowGetButtonVisibility"
                OnRowUpdated="grdRecuperatori_RowUpdated"
                OnStartRowEditing="grdRecuperatori_StartRowEditing"
                OnDataBound="DetailGrid_DataBound"
                OnRowValidating="grdRecuperatori_OnRowValidating"
                >
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="14" Width="40px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="cmdSostituisciRecuperatore" Text="Sostituisci"></dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                            <b>Modello</b>: <%# Eval("Modello") %><br />
                            <b>Matricola</b>: <%# Eval("Matricola") %><br />
                            <b>Portata termica nominale totale</b>: <%# Eval("PortataTermicaNominaleTotaleKw") %> kW<br />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Recuperatore/Condensatore" VisibleIndex="4" Visible="false">
                        <EditFormSettings ColumnSpan="2" Visible="True" CaptionLocation="Top" />
                        <EditItemTemplate>
                            <%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DataInstallazione" Caption="Data installazione" VisibleIndex="5" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
                        <PropertiesDateEdit Width="160px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="DataDismissione" Caption="Data dismissione" VisibleIndex="6" Visible="false">
                        <EditFormSettings Visible="False" CaptionLocation="Top" />
                        <PropertiesDateEdit Width="160px"></PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Fabbricante" VisibleIndex="7" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire il fabbricante" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Modello" VisibleIndex="8" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire il modello" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Matricola" VisibleIndex="9" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <PropertiesTextEdit Width="360px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire la matricola" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="PortataTermicaNominaleTotaleKw" VisibleIndex="13" Visible="false">
                        <EditFormSettings Visible="True" CaptionLocation="Top" />
                        <EditFormSettings Caption="Portata termica nominale totale" />
                        <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                            <ValidationSettings CausesValidation="True">
                                <RequiredField ErrorText="Inserire la portata termica nominale totale" IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                </Columns>
                <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
                <ClientSideEvents CustomButtonClick="function(s, e) {if (confirm ('Confermi di Sostituire il componente?')) {e.processOnServer = true;}}" />
                <SettingsEditing EditFormColumnCount="2"></SettingsEditing>
                <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsDetail ShowDetailRow="True" />
                <Styles>
                    <RowHotTrack Cursor="pointer"></RowHotTrack>
                </Styles>
                <Templates>
                    <DetailRow>
                        SOSTITUZIONI DEL COMPONENTE
                        <dx:ASPxGridView ID="grdRecuperatoriSostituzioni" runat="server" DataSourceID="dsRecuperatoriSostituzioni" Width="100%" EnableRowsCache="False"
                            KeyFieldName="IDLibrettoImpiantoRecuperatore" 
                            OnBeforePerformDataSelect="grdRecuperatoriSostituzioni_BeforePerformDataSelect" >
                            <Columns>
                                <dx:GridViewDataTextColumn>
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                                        <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                                        <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                                        <b>Modello</b>: <%# Eval("Modello") %><br />
                                        <b>Matricola</b>: <%# Eval("Matricola") %><br />
                                        <b>Portata termica nominale totale</b> <%# Eval("PortataTermicaNominaleTotaleKw") %> kW<br />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>

<ef:EntityDataSource ID="dsGruppiTermici" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiGruppiTermici"
    OnInserted="dsGruppiTermici_Inserted"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.fAttivo=true"
    OrderBy="it.CodiceProgressivo">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsGruppiTermiciSostituzioni" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiGruppiTermici"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaGruppiTermici" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaGruppiTermici" 
    Select="it.[IDTipologiaGruppiTermici], it.[TipologiaGruppiTermici]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaCombustibile" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaCombustibile" 
    Select="it.[IDTipologiaCombustibile], it.[TipologiaCombustibile]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaFluidoTermoVettore" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaFluidoTermoVettore" 
    Select="it.[IDTipologiaFluidoTermoVettore], it.[TipologiaFluidoTermoVettore]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsBruciatori" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiBruciatori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpiantoGruppoTermico=@IDLibrettoImpiantoGruppoTermico and it.fAttivo=true"
    OrderBy="it.CodiceProgressivo">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpiantoGruppoTermico" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsBruciatoriSostituzioni" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiBruciatori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpiantoGruppoTermico=@IDLibrettoImpiantoGruppoTermico and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpiantoGruppoTermico" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>

<ef:EntityDataSource ID="dsRecuperatori" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiRecuperatori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpiantoGruppoTermico=@IDLibrettoImpiantoGruppoTermico and it.fAttivo=true"
    OrderBy="it.CodiceProgressivo">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpiantoGruppoTermico" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsRecuperatoriSostituzioni" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiRecuperatori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpiantoGruppoTermico=@IDLibrettoImpiantoGruppoTermico and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpiantoGruppoTermico" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>