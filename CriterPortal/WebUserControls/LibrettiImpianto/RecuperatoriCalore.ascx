<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecuperatoriCalore.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_RecuperatoriCalore" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoRecuperatoreCalore" 
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
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="20" Width="40px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="cmdSostituisci" Text="Sostituisci"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %> <%# Eval("DataDismissione", "Data dismissione {0:d}") %><br />
                <b>Tipologia</b>: <%# Eval("Tipologia") %><br />
                <b>Modalità installazione</b>: <%# Eval("SYS_ModalitaInstallazioneRecuperatoriCalore.ModalitaInstallazione") %><br />
                <b>Portata ventilatore di mandata</b>: <%# Eval("PortataVentilatoreMandataLts") %> l/s<br />
                <b>Potenza ventilatore di mandata</b>: <%# Eval("PotenzaVentilatoreMandataKw") %> kW<br />
                <b>Portata ventilatore di ripresa</b>: <%# Eval("PortataVentilatoreRipresaLts") %> l/s<br />
                <b>Potenza ventilatore di ripresa</b>: <%# Eval("PotenzaVentilatoreRipresaKw") %> kW
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Recuperatore" VisibleIndex="4" Visible="false">
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
        <dx:GridViewDataTextColumn FieldName="Tipologia" VisibleIndex="7" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Modalità installazione" FieldName="IDModalitaInstallazione" VisibleIndex="8" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsModalitaInstallazioneRecuperatoriCalore" TextField="ModalitaInstallazione" ValueField="IDModalitaInstallazioneRecuperatoriCalore" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Selezionare la modalità di installazione" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if(s.GetValue()==2) 
                                                                pnlVentilatore.SetVisible(true); 
                                                            else 
                                                                pnlVentilatore.SetVisible(false); 
                                                        }" />
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn Name="Ventilatore" VisibleIndex="18" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlVentilatore" ClientInstanceName="pnlVentilatore" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Portata ventilatore di mandata (l/s)" Font-Bold="true"></dx:ASPxLabel><br />
                                        <dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="spePortataVentilatoreMandata" runat="server" 
                                            Value='<%# Bind("PortataVentilatoreMandataLts") %>'
                                            OnInit="EditorVentilatore_Init"
                                            DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Inserire la portata del ventilatore di mandata" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Potenza ventilatore di mandata (kw)" Font-Bold="true"></dx:ASPxLabel><br />
                                        <dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="spePotenzaVentilatoreMandata" runat="server" 
                                            Value='<%# Bind("PotenzaVentilatoreMandataKw") %>'
                                            OnInit="EditorVentilatore_Init"
                                            DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Inserire la potenza del ventilatore di mandata" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Portata ventilatore di ripresa (l/s)" Font-Bold="true"></dx:ASPxLabel><br />
                                        <dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="spePortataVentilatoreRipresa" runat="server" 
                                            Value='<%# Bind("PortataVentilatoreRipresaLts") %>'
                                            OnInit="EditorVentilatore_Init"
                                            DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Inserire la portata del ventilatore di ripresa" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Potenza ventilatore di ripresa (kw)" Font-Bold="true"></dx:ASPxLabel><br />
                                        <dx:ASPxSpinEdit MinValue="0" MaxValue="99999999" ID="spePotenzaVentilatoreRipresa" runat="server" 
                                            Value='<%# Bind("PotenzaVentilatoreRipresaKw") %>'
                                            OnInit="EditorVentilatore_Init"
                                            DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                                            <ValidationSettings CausesValidation="True">
                                                <RequiredField ErrorText="Inserire la potenza del ventilatore di ripresa" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                            </table>
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
            SOSTITUZIONI DEL COMPONENTE
            <dx:ASPxGridView ID="grdSostituzioni" runat="server" DataSourceID="dsGridPrincipaleSostituzioni" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoRecuperatoreCalore" 
                OnBeforePerformDataSelect="grdSostituzioni_BeforePerformDataSelect" >
                <Columns>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Tipologia</b>: <%# Eval("Tipologia") %><br />
                            <b>Modalità installazione</b>: <%# Eval("SYS_ModalitaInstallazioneRecuperatoriCalore.ModalitaInstallazione") %><br />
                            <b>Portata ventilatore di mandata</b>: <%# Eval("PortataVentilatoreMandataLts") %> l/s<br />
                            <b>Potenza ventilatore di mandata</b>: <%# Eval("PotenzaVentilatoreMandataKw") %> kW<br />
                            <b>Portata ventilatore di ripresa</b>: <%# Eval("PortataVentilatoreRipresaLts") %> l/s<br />
                            <b>Potenza ventilatore di ripresa</b>: <%# Eval("PotenzaVentilatoreRipresaKw") %> kW
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
    EntitySetName="LIM_LibrettiImpiantiRecuperatoriCalore"
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
    EntitySetName="LIM_LibrettiImpiantiRecuperatoriCalore"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsModalitaInstallazioneRecuperatoriCalore" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_ModalitaInstallazioneRecuperatoriCalore" 
    Select="it.[IDModalitaInstallazioneRecuperatoriCalore], it.[ModalitaInstallazione]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>