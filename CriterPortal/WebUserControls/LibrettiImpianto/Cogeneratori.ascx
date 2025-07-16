<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cogeneratori.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_Cogeneratori" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoCogeneratore" 
    OnBeforePerformDataSelect="grdPrincipale_BeforePerformDataSelect" 
    OnRowDeleting="grdPrincipale_RowDeleting" 
    OnRowInserting="grdPrincipale_RowInserting" 
    OnRowUpdating="grdPrincipale_RowUpdating" 
    OnCommandButtonInitialize="grdPrincipale_CommandButtonInitialize" 
    OnCustomButtonCallback="grdPrincipale_CustomButtonCallback" 
    OnCustomButtonInitialize="grdPrincipale_CustomButtonInitialize"
    OnCellEditorInitialize="grdPrincipale_CellEditorInitialize" 
    OnDetailRowGetButtonVisibility="DetailGrid_DetailRowGetButtonVisibility"
    OnRowUpdated="grdPrincipale_RowUpdated"
    OnStartRowEditing="grdPrincipale_StartRowEditing"
    OnDataBound="DetailGrid_DataBound"
    OnRowValidating="grdPrincipale_RowValidating"
    OnHtmlRowCreated="grdPrincipale_HtmlRowCreated"
    >
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="40" Width="40px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="cmdSostituisci" Text="Sostituisci"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %> <%# Eval("DataDismissione", "Data dismissione {0:d}") %><br />
                <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                <b>Modello </b>: <%# Eval("Modello") %><br />
                <b>Matricola</b>: <%# Eval("Matricola") %><br />
                <b>Tipologia</b>: <%# Eval("SYS_TipologiaCogeneratore.TipologiaCogeneratore") %><br />
                <b>Combustibile</b>: <%# Eval("SYS_TipologiaCombustibile.TipologiaCombustibile") %> <%# Eval("CombustibileAltro") %><br />
                <b>Potenza termica nominale</b>: <%# Eval("PotenzaTermicaNominaleKw") %> kW<br />
                <b>Potenza elettrica nominale</b>: <%# Eval("PotenzaElettricaNominaleKw") %> kW<br />
                <br />
                <table>
                    <tr>
                        <td><b>Dati di targa</b></td>
                        <td><b>min</b></td>
                        <td><b>/</b></td>
                        <td><b>max</b></td>
                        <td>&nbsp;</td>
                        <td><b>min</b></td>
                        <td><b>/</b></td>
                        <td><b>max</b></td>
                    </tr>
                    <tr>
                        <td><b>Temperatura acqua in uscita (°C)</b></td>
                        <td><%# Eval("TemperaturaAcquaUscitaGradiMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("TemperaturaAcquaUscitaGradiMax") %></td>
                        <td><b>Temperatura fumi a valle dello scambiatore (°C)</b></td>
                        <td><%# Eval("TemperaturaFumiValleMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("TemperaturaFumiValleMax") %></td>
                    </tr>
                    <tr>
                        <td><b>Temperatura acqua in ingresso (°C)</b></td>
                        <td><%# Eval("TemperaturaAcquaIngressoGradiMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("TemperaturaAcquaIngressoGradiMax") %></td>
                        <td><b>Temperatura fumi a monte dello scambiatore (°C)</b></td>
                        <td><%# Eval("TemperaturaFumiMonteMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("TemperaturaFumiMonteMax") %></td>
                    </tr>
                    <tr>
                        <td><b>Temperatura acqua motore (solo m.c.i.)(°C)</b></td>
                        <td><%# Eval("TemperaturaAcquaMotoreMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("TemperaturaAcquaMotoreMax") %></td>
                        <td><b>Emissioni di monossido di carbonio CO (mg/Nm3 riportati al 5% di O2 nei fumi)</b></td>
                        <td><%# Eval("EmissioniCOMin") %></td>
                        <td><b>/</b></td>
                        <td><%# Eval("EmissioniCOMax") %></td>
                    </tr>
                </table>
                 <asp:Label runat="server" ID="lblGeneratoreDismesso" Visible="false" Font-Bold="true" ForeColor="Red"/>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Cogeneratore/Trigeneratore" VisibleIndex="4" Visible="false">
            <EditFormSettings ColumnSpan="3" Visible="True" CaptionLocation="Top" />
            <EditItemTemplate>
                <%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="DataInstallazione" Caption="Data installazione" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesDateEdit Width="160px">
                <ValidationSettings CausesValidation="True">
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
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la matricola" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Tipologia" FieldName="IDTipologiaCogeneratore" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsTipologiaCogeneratore" TextField="TipologiaCogeneratore" ValueField="IDTipologiaCogeneratore" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Combustibile" FieldName="IDTipologiaCombustibile" VisibleIndex="11" Visible="false">
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
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="CombustibileAltro" VisibleIndex="12" Visible="false">
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
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaTermicaNominaleKw" VisibleIndex="13" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <EditFormSettings Caption="Potenza termica nominale al massimo recupero (kW)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza termica nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaElettricaNominaleKw" VisibleIndex="14" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <EditFormSettings Caption="Potenza elettrica nominale ai morsetti del generatore (kW)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza elettrica nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataColumn VisibleIndex="15">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <EditItemTemplate>
                <table>
                    <tr>
                        <td>Dati di targa</td>
                        <td>min</td>
                        <td>/</td>
                        <td>max</td>
                        <td>&nbsp;</td>
                        <td>min</td>
                        <td>/</td>
                        <td>max</td>
                    </tr>
                    <tr>
                        <td>Temperatura acqua in uscita (°C)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit0" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaUscitaGradiMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit1" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaUscitaGradiMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>Temperatura fumi a valle dello scambiatore (°C)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit2" runat="server" Width="70px" Value='<%# Bind("TemperaturaFumiValleMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit3" runat="server" Width="70px" Value='<%# Bind("TemperaturaFumiValleMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                    </tr>
                    <tr>
                        <td>Temperatura acqua in ingresso (°C)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit4" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaIngressoGradiMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit5" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaIngressoGradiMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>Temperatura fumi a monte dello scambiatore (°C)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit6" runat="server" Width="70px" Value='<%# Bind("TemperaturaFumiMonteMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit7" runat="server" Width="70px" Value='<%# Bind("TemperaturaFumiMonteMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                    </tr>
                    <tr>
                        <td>Temperatura acqua motore (solo m.c.i.)(°C)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit8" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaMotoreMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit9" runat="server" Width="70px" Value='<%# Bind("TemperaturaAcquaMotoreMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>Emissioni di monossido di carbonio CO (mg/Nm3 riportati al 5% di O2 nei fumi)</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit10" runat="server" Width="70px" Value='<%# Bind("EmissioniCOMin") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                        <td>/</td>
                        <td><dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="ASPxSpinEdit11" runat="server" Width="70px" Value='<%# Bind("EmissioniCOMax") %>' NumberType="Float" DecimalPlaces="2"></dx:ASPxSpinEdit></td>
                    </tr>
                </table>
            </EditItemTemplate>
        </dx:GridViewDataColumn>
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
            SOSTITUZIONI DEL COMPONENTE
            <dx:ASPxGridView ID="grdSostituzioni" runat="server" DataSourceID="dsGridPrincipaleSostituzioni" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoCogeneratore" 
                OnBeforePerformDataSelect="grdSostituzioni_BeforePerformDataSelect" >
                <Columns>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                            <b>Modello</b>: <%# Eval("Modello") %><br />
                            <b>Matricola</b>: <%# Eval("Matricola") %><br />
                            <b>Tipologia</b>: <%# Eval("SYS_TipologiaCogeneratore.TipologiaCogeneratore") %><br />
                            <b>Combustibile</b>: <%# Eval("SYS_TipologiaCombustibile.TipologiaCombustibile") %> <%# Eval("CombustibileAltro") %><br />
                            <b>Potenza termica nominale</b>: <%# Eval("PotenzaTermicaNominaleKw") %> kW<br />
                            <b>Potenza elettrica nominale</b>: <%# Eval("PotenzaElettricaNominaleKw") %> kW<br />
                            <br />
                            <table>
                                <tr>
                                    <td><b>Dati di targa</b></td>
                                    <td><b>min</b></td>
                                    <td><b>/</b></td>
                                    <td><b>max</b></td>
                                    <td>&nbsp;</td>
                                    <td><b>min</b></td>
                                    <td><b>/</b></td>
                                    <td><b>max</b></td>
                                </tr>
                                <tr>
                                    <td><b>Temperatura acqua in uscita (°C)</b></td>
                                    <td><%# Eval("TemperaturaAcquaUscitaGradiMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("TemperaturaAcquaUscitaGradiMax") %></td>
                                    <td><b>Temperatura fumi a valle dello scambiatore (°C)</b></td>
                                    <td><%# Eval("TemperaturaFumiValleMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("TemperaturaFumiValleMax") %></td>
                                </tr>
                                <tr>
                                    <td><b>Temperatura acqua in ingresso (°C)</b></td>
                                    <td><%# Eval("TemperaturaAcquaIngressoGradiMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("TemperaturaAcquaIngressoGradiMax") %></td>
                                    <td><b>Temperatura fumi a monte dello scambiatore (°C)</b></td>
                                    <td><%# Eval("TemperaturaFumiMonteMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("TemperaturaFumiMonteMax") %></td>
                                </tr>
                                <tr>
                                    <td><b>Temperatura acqua motore (solo m.c.i.)(°C)</b></td>
                                    <td><%# Eval("TemperaturaAcquaMotoreMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("TemperaturaAcquaMotoreMax") %></td>
                                    <td><b>Emissioni di monossido di carbonio CO (mg/Nm3 riportati al 5% di O2 nei fumi)</b></td>
                                    <td><%# Eval("EmissioniCOMin") %></td>
                                    <td><b>/</b></td>
                                    <td><%# Eval("EmissioniCOMax") %></td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>

<ef:EntityDataSource ID="dsGridPrincipale" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiCogeneratori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.fAttivo=true"
    OrderBy="it.CodiceProgressivo">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsGridPrincipaleSostituzioni" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="false"
    EnableDelete="True" 
    EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiCogeneratori"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>

<ef:EntityDataSource ID="dsTipologiaCogeneratore" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaCogeneratore" 
    Select="it.[IDTipologiaCogeneratore], it.[TipologiaCogeneratore]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaCombustibile" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaCombustibile" 
    Select="it.[IDTipologiaCombustibile], it.[TipologiaCombustibile]"
    Where="it.fAttivo=true AND it.[IDTipologiaCombustibile] IN {1,2,3,4,10,11,12,13,14}">
</ef:EntityDataSource>