<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_ConsumoCombustibile.ascx.cs" Inherits="UC_ConsumoCombustibile" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettiImpiantiConsumoCombustibile" 
    OnBeforePerformDataSelect="grdPrincipale_BeforePerformDataSelect" 
    OnRowDeleting="grdPrincipale_RowDeleting" 
    OnRowInserting="grdPrincipale_RowInserting" 
    OnRowUpdating="grdPrincipale_RowUpdating" 
    OnCommandButtonInitialize="grdPrincipale_CommandButtonInitialize" 
    OnCellEditorInitialize="grdPrincipale_CellEditorInitialize" 
    OnDetailRowGetButtonVisibility="DetailGrid_DetailRowGetButtonVisibility"
    OnRowUpdated="grdPrincipale_RowUpdated"
    OnStartRowEditing="grdPrincipale_StartRowEditing" 
    OnDataBound="DetailGrid_DataBound" 
    OnRowValidating="grdPrincipale_RowValidating">
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="7" Width="40px">
            
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <table width="100%" border="1" bordercolor="#dadcdb">
                    <tbody>
                        <tr>
                            <td colspan="5">
                                <b>Tipo combustile:</b>&nbsp;<%# Eval("SYS_TipologiaCombustibile.TipologiaCombustibile") %> <%# Eval("CombustibileAltro") %><br />
                                <b>Unità di misura:</b>&nbsp;<%# Eval("SYS_UnitaMisura.UnitaMisura") %>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Esercizio</b></td>
                            <td><b>Acquisti</b></td>
                            <td><b>Scorta o lettura iniziale</b></td>
                            <td><b>Scorta o lettura finale</b></td>
                            <td><b>Consumo</b></td>
                        </tr>
                        <tr>
                            <td><%# Eval("DataEsercizioStart") %>&nbsp;/&nbsp;<%# Eval("DataEsercizioEnd") %></td>
                            <td><%# Eval("Acquisti") %></td>
                            <td><%# Eval("LetturaIniziale") %></td>
                            <td><%# Eval("LetturaFinale") %></td>
                            <td><%# Eval("Consumo") %></td>
                        </tr>
                    </tbody>
                </table>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn FieldName="DataEsercizioStart" VisibleIndex="1" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Anno di esercizio iniziale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Anno di esercizio iniziale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="DataEsercizioEnd" VisibleIndex="2" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Anno di esercizio finale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Anno di esercizio finale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="Acquisti" VisibleIndex="3" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Acquisti" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo acquisti" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="LetturaIniziale" VisibleIndex="3" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Lettura iniziale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo lettura iniziale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="LetturaFinale" VisibleIndex="4" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Lettura finale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo lettura finale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="Consumo" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Consumo" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo consumo" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="Combustibile" FieldName="IDTipologiaCombustibile" VisibleIndex="6" Visible="false">
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
        <dx:GridViewDataTextColumn FieldName="CombustibileAltro" VisibleIndex="7" Visible="false">
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
        <dx:GridViewDataComboBoxColumn Caption="UnitaMisura" FieldName="IDUnitaMisura" VisibleIndex="8" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsUnitaMisura" TextField="UnitaMisura" ValueField="IDUnitaMisura" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire l'unità di misura" IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
    </Columns>
    <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
    <SettingsEditing EditFormColumnCount="2"></SettingsEditing>
    <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
    <SettingsPager Mode="ShowPager" PageSize="5" />
    <SettingsDetail ShowDetailRow="True" />
    <Styles>
        <RowHotTrack Cursor="pointer"></RowHotTrack>
    </Styles>
</dx:ASPxGridView>
<ef:EntityDataSource ID="dsGridPrincipale" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EnableDelete="True" EnableInsert="True" EnableUpdate="True" 
    EntitySetName="LIM_LibrettiImpiantiConsumoCombustibile"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto"
    OrderBy="it.IDLibrettiImpiantiConsumoCombustibile">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaCombustibile" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaCombustibile" 
    Select="it.[IDTipologiaCombustibile], it.[TipologiaCombustibile]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsUnitaMisura" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_UnitaMisura" 
    Select="it.[IDUnitaMisura], it.[UnitaMisura]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>