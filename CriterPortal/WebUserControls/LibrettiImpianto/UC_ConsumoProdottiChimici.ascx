<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_ConsumoProdottiChimici.ascx.cs" Inherits="UC_ConsumoProdottiChimici" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettiImpiantiConsumoProdottiChimici" 
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
                            <td colspan="6">
                                <b>Unità di misura:</b>&nbsp;<%# Eval("SYS_UnitaMisura.UnitaMisura") %>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Esercizio</b></td>
                            <td><b>Circuito impianto termico</b></td>
                            <td><b>Circuito Acs</b></td>
                            <td><b>Altri circuiti ausiliari</b></td>
                            <td><b>Nome prodotto</b></td>
                            <td><b>Quantità consumata</b></td>
                        </tr>
                        <tr>
                            <td><%# Eval("DataEsercizioStart") %>&nbsp;/&nbsp;<%# Eval("DataEsercizioEnd") %></td>
                            <td><%# Convert.ToBoolean(Eval("fCircuitoImpiantoTermico"))?"SI":"NO" %></td>
                            <td><%# Convert.ToBoolean(Eval("fCircuitoAcs"))?"SI":"NO" %></td>
                            <td><%# Convert.ToBoolean(Eval("fAltriCircuiti"))?"SI":"NO" %></td>
                            <td><%# Eval("NomeProdotto") %></td>
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
        <dx:GridViewDataColumn VisibleIndex="3" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="" />
            <EditItemTemplate>
                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Value='<%# Bind("fCircuitoImpiantoTermico") %>' Text="Circuito impianto termico"></dx:ASPxCheckBox>
                <dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server" Value='<%# Bind("fCircuitoAcs") %>' Text="Circuito Acs"></dx:ASPxCheckBox>
                <dx:ASPxCheckBox ID="ASPxCheckBox3" runat="server" Value='<%# Bind("fAltriCircuiti") %>' Text="Altri circuiti ausiliari"></dx:ASPxCheckBox>
            </EditItemTemplate>
        </dx:GridViewDataColumn>
        <dx:GridViewDataTextColumn FieldName="NomeProdotto" VisibleIndex="4" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il nome del prodotto" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn FieldName="Consumo" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Consumo totale" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire valore nel campo consumo" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataComboBoxColumn Caption="UnitaMisura" FieldName="IDUnitaMisura" VisibleIndex="6" Visible="false">
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
    EntitySetName="LIM_LibrettiImpiantiConsumoProdottiChimici"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto"
    OrderBy="it.IDLibrettiImpiantiConsumoProdottiChimici">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsUnitaMisura" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_UnitaMisura" 
    Select="it.[IDUnitaMisura], it.[UnitaMisura]"
    Where="it.fAttivo=true AND it.[IDUnitaMisura] IN {1,2,3}">
</ef:EntityDataSource>