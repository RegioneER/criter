<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImpiantiVMC.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_ImpiantiVMC" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoImpiantiVMC" 
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
    OnRowValidating="grdPrincipale_RowValidating">
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="30" Width="40px">
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
                <b>Modello</b>: <%# Eval("Modello") %><br />
                <b>Tipo componente</b>: <%# Eval("SYS_TipologiaImpiantiVMC.TipologiaImpianto") %> <%# Eval("TipologiaImpiantoAltro") %><br />
                <b>Massima portata aria (mc/h)</b>: <%# Eval("PortataAriaMaxMch") %><br />
                <b>Rendimento di recupero / COP</b>: <%# Eval("RendimentoRecuperoCop") %><br />
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Impianto" VisibleIndex="4" Visible="false">
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
        <dx:GridViewDataComboBoxColumn Caption="Tipologia" FieldName="IDTipologiaImpiantoVMC" VisibleIndex="17" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesComboBox DataSourceID="dsTipologiaImpiantiVMC" TextField="TipologiaImpianto" ValueField="IDTipologiaImpiantiVMC" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if(s.GetValue()==4) 
                                                                pnlTipologiaImpianto.SetVisible(true); 
                                                            else 
                                                                pnlTipologiaImpianto.SetVisible(false); 
                                                        }" />
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="TipologiaImpiantoAltro" VisibleIndex="18" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None"  ColumnSpan="2"/>
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlTipologiaImpianto" ClientInstanceName="pnlTipologiaImpianto" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" Text="Descrizione tipologia" Font-Bold="true"></dx:ASPxLabel><br />
                            <dx:ASPxTextBox ID="txtTipologiaImpianto" ClientInstanceName="txtTipologiaImpianto" runat="server" Width="380px"
                                OnInit="txtTipologiaImpianto_Init"
                                Value='<%# Bind("TipologiaImpiantoAltro") %>'>
                                <ClientSideEvents />
                            </dx:ASPxTextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PortataAriaMaxMch" VisibleIndex="19" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Massima portata aria (mc/h)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la massima portata d'aria" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="RendimentoRecuperoCop" VisibleIndex="20" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Rendimento di recupero / COP" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il rendimento di recupero" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
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
                KeyFieldName="IDLibrettoImpiantoImpiantiVMC" 
                OnBeforePerformDataSelect="grdSostituzioni_BeforePerformDataSelect" >
                <Columns>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Fabbricante</b>: <%# Eval("Fabbricante") %><br />
                            <b>Modello</b>: <%# Eval("Modello") %><br />
                            <b>Tipo componente</b>: <%# Eval("SYS_TipologiaImpiantiVMC.TipologiaImpianto") %> <%# Eval("TipologiaImpiantoAltro") %><br />
                            <b>Massima portata aria (mc/h)</b>: <%# Eval("PortataAriaMaxMch") %><br />
                            <b>Rendimento di recupero / COP</b>: <%# Eval("RendimentoRecuperoCop") %><br />
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
    EntitySetName="LIM_LibrettiImpiantiImpiantiVMC"
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
    EntitySetName="LIM_LibrettiImpiantiImpiantiVMC"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaImpiantiVMC" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaImpiantiVMC" 
    Select="it.[IDTipologiaImpiantiVMC], it.[TipologiaImpianto]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>