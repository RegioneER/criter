<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_ConsumoEnergiaElettrica.ascx.cs" Inherits="UC_ConsumoEnergiaElettrica" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettiImpiantiConsumoEnergiaElettrica" 
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
                            <td><b>Esercizio</b></td>
                            <td><b>Lettura iniziale (kWh)</b></td>
                            <td><b>Lettura finale (kWh)</b></td>
                            <td><b>Consumo totale (kWh)</b></td>
                        </tr>
                        <tr>
                            <td><%# Eval("DataEsercizioStart") %>&nbsp;/&nbsp;<%# Eval("DataEsercizioEnd") %></td>
                            <td><%# Eval("LetturaIniziale") %></td>
                            <td><%# Eval("LetturaFinale") %></td>
                            <td><%# Eval("ConsumoTotale") %></td>
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
        <dx:GridViewDataSpinEditColumn FieldName="ConsumoTotale" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Consumo totale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo consumo totale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
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
    EntitySetName="LIM_LibrettiImpiantiConsumoEnergiaElettrica"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto"
    OrderBy="it.IDLibrettiImpiantiConsumoEnergiaElettrica">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>