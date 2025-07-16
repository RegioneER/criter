<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VasiEspansione.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_VasiEspansione" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoVasoEspansione" 
    OnBeforePerformDataSelect="grdPrincipale_BeforePerformDataSelect" 
    OnRowDeleting="grdPrincipale_RowDeleting" 
    OnRowInserting="grdPrincipale_RowInserting" 
    OnRowUpdating="grdPrincipale_RowUpdating" 
    OnCommandButtonInitialize="grdPrincipale_CommandButtonInitialize" 
    OnCustomButtonCallback="grdPrincipale_CustomButtonCallback" 
    OnCustomButtonInitialize="grdPrincipale_CustomButtonInitialize"
    OnCellEditorInitialize="grdPrincipale_CellEditorInitialize" 
    OnDetailRowGetButtonVisibility="DetailGrid_DetailRowGetButtonVisibility"
    OnHtmlRowCreated="grdPrincipale_HtmlRowCreated"
    OnRowUpdated="grdPrincipale_RowUpdated"
    OnStartRowEditing="grdPrincipale_StartRowEditing" 
    OnDataBound="DetailGrid_DataBound"
    OnRowValidating="grdPrincipale_RowValidating" >
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
                <b>Capacità (l)</b>: <%# Eval("CapacitaLt") %><br />
                <%# Convert.ToBoolean(Eval("fChiuso")) ? "<b>Chiuso</b>" : "<b>Aperto</b>" %> <%# Eval("PressionePrecaricaBar", " - <b>Pressione precarica (bar)</b>: {0}") %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Vaso espansione" VisibleIndex="4" Visible="false">
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
        <dx:GridViewDataSpinEditColumn FieldName="CapacitaLt" VisibleIndex="7" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top"  ColumnSpan="2"  />
            <EditFormSettings Caption="Capacità (l)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la capacità" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataCheckColumn Caption="Aperto/Chiuso" FieldName="fChiuso" VisibleIndex="8" Visible="false">
           <EditFormSettings Visible="True" CaptionLocation="None" />
           <EditItemTemplate>
               <table>
                   <tr>
                       <td>
                           <dx:aspxradiobutton id="btnAperto" runat="server" checked='<%# Convert.ToBoolean(Eval("fChiuso")) == false %>' Text="Aperto" clientinstancename="btnVasoEspansioneAperto">
                               <ClientSideEvents CheckedChanged="
                                                                function (s,e) 
                                                                {
                                                                    if(s.GetChecked()) 
                                                                    {
                                                                        btnVasoEspansioneChiuso.SetChecked(false);
                                                                        pnlVasoEspansionePressionePrecarica.SetVisible(false);
                                                                    }
                                                                }" />
                           </dx:aspxradiobutton></td>
                       <td>/</td>
                       <td>
                           <dx:aspxradiobutton id="btnChiuso" runat="server" Value='<%# Bind("fChiuso") %>' Text="Chiuso" clientinstancename="btnVasoEspansioneChiuso">
                               <ClientSideEvents CheckedChanged="
                                                                function (s,e) 
                                                                {
                                                                    if(s.GetChecked()) 
                                                                    {
                                                                        btnVasoEspansioneAperto.SetChecked(false);
                                                                        pnlVasoEspansionePressionePrecarica.SetVisible(true);
                                                                    }
                                                                }" />
                           </dx:aspxradiobutton>
                       </td>
                   </tr>
               </table>
            </EditItemTemplate>
        </dx:GridViewDataCheckColumn>
        <dx:GridViewDataTextColumn FieldName="PressionePrecaricaBar" VisibleIndex="9" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlPressionePrecarica" ClientInstanceName="pnlVasoEspansionePressionePrecarica" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" Text="Pressione precarica (bar)" Font-Bold="true"></dx:ASPxLabel><br />
                            <dx:ASPxSpinEdit ID="spePressionePrecaricaBar" ClientInstanceName="spePressionePrecaricaBar" runat="server" Width="100px" MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" 
                                OnInit="spePressionePrecaricaBar_Init"
                                Value='<%# Bind("PressionePrecaricaBar") %>'>
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
            SOSTITUZIONI DEL COMPONENTE
            <dx:ASPxGridView ID="grdSostituzioni" runat="server" DataSourceID="dsGridPrincipaleSostituzioni" Width="100%" EnableRowsCache="False"
                KeyFieldName="IDLibrettoImpiantoVasoEspansione" 
                OnBeforePerformDataSelect="grdSostituzioni_BeforePerformDataSelect" >
                <Columns>
                    <dx:GridViewDataTextColumn>
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <b><%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %></b><br />
                            <b>Data installazione</b>: <%# Eval("DataInstallazione", "{0:d}") %>&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("DataDismissione", "<b>Data dismissione</b>: {0:d}") %><br />
                            <b>Capacità (l)</b>: <%# Eval("CapacitaLt") %><br />
                            <%# Convert.ToBoolean(Eval("fChiuso")) ? "<b>Chiuso</b>" : "<b>Aperto</b>" %> <%# Eval("PressionePrecaricaBar", " - <b>Pressione precarica (bar)</b>: {0}") %>
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
    EntitySetName="LIM_LibrettiImpiantiVasiEspansione"
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
    EntitySetName="LIM_LibrettiImpiantiVasiEspansione"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
