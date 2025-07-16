<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MacchineFrigorifere.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_MacchineFrigorifere" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdPrincipale" runat="server" DataSourceID="dsGridPrincipale" AutoGenerateColumns="False" Width="100%" EnableRowsCache="False"
    KeyFieldName="IDLibrettoImpiantoMacchinaFrigorifera" 
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
                <b>Fabbricante</b>: <%# Eval("Fabbricante") %> Modello <%# Eval("Modello") %><br />
                <b>Matricola</b>: <%# Eval("Matricola") %><br />
                <b>Sorgente lato esterno</b>: <%# Eval("SYS_SorgenteLatoEsterno.SorgenteLatoEsterno") %><br />
                <b>Fluido frigorigeno</b>: <%# Eval("FiltroFrigorigeno") %><br />
                <b>Fluido lato utenze</b>: <%# Eval("SYS_FluidoLatoUtenze.FluidoLatoUtenze") %><br />
                <b>Tipologia</b>: <%# Eval("SYS_TipologiaMacchineFrigorifere.TipologiaMacchineFrigorifere") %> <%# Eval("Combustibile") %><br />
                <b>circuiti n°</b>: <%# Eval("NumCircuiti") %><br />
                <b>Raffrescamento: EER (o GUE)</b>: <%# Eval("CoefficienteRaffrescamento") %> <b>Potenza frigorifera nominale</b>: <%# Eval("PotenzaFrigoriferaNominaleKw") %> (kW) <b>Potenza assorbita nominale</b>: <%# Eval("PotenzaFrigoriferaAssorbitaNominaleKw") %> (kW)<br />
                <b>Riscaldamento: COP (o h)</b>: <%# Eval("CoefficienteRiscaldamento") %> <b>Potenza termica nominale</b>: <%# Eval("PortataTermicaNominaleKw") %> (kW) <b>Potenza assorbita nominale</b>: <%# Eval("PotenzaTermicaAssorbitaNominaleKw") %> (kW)<br />
                <asp:Label runat="server" ID="lblGeneratoreDismesso" Visible="false" Font-Bold="true" ForeColor="Red"/>
                </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CodiceProgressivo" Caption="Gruppo Frigo/Pompa di calore" VisibleIndex="4" Visible="false">
            <EditFormSettings ColumnSpan="3" Visible="True" CaptionLocation="Top" />
            <EditItemTemplate>
                <%# Eval("Prefisso") %> <%# Eval("CodiceProgressivo","{0:00}") %>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="DataInstallazione" Caption="Data installazione" VisibleIndex="5" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="3" />
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
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
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
        <dx:GridViewDataComboBoxColumn FieldName="IDSorgenteLatoEsterno" Caption="Sorgente lato esterno" VisibleIndex="9" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsSorgenteLatoEsterno" TextField="SorgenteLatoEsterno" ValueField="IDSorgenteLatoEsterno" ValueType="System.Int32" Width="380px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Selezionare la sorgente lato esterno" IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="FiltroFrigorigeno" Caption="Fluido frigorigeno" VisibleIndex="9" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il fluido frigorigeno" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn FieldName="IDFluidoLatoUtenze" Caption="Fluido lato utenze" VisibleIndex="10" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <PropertiesComboBox DataSourceID="dsFluidoLatoUtenze" TextField="FluidoLatoUtenze" ValueField="IDFluidoLatoUtenze" ValueType="System.Int32" Width="380px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Selezionare il fluido lato utenze" IsRequired="True" />
                </ValidationSettings>
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="Tipologia" FieldName="IDTipologiaMacchineFrigorifere" VisibleIndex="17" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" />
            <PropertiesComboBox DataSourceID="dsTipologiaMacchineFrigorifere" TextField="TipologiaMacchineFrigorifere" ValueField="IDTipologiaMacchineFrigorifere" ValueType="System.Int32">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la tipologia" IsRequired="True" />
                </ValidationSettings>
                <ClientSideEvents SelectedIndexChanged="function(s, e) 
                                                        { 
                                                            if((s.GetValue()==2) || (s.GetValue()==4))
                                                                pnlCombustibile.SetVisible(true); 
                                                            else 
                                                                pnlCombustibile.SetVisible(false); 
                                                        }" />
            </PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn FieldName="Combustibile" VisibleIndex="18" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="None" />
            <EditItemTemplate>
                <dx:ASPxPanel ID="pnlCombustibile" ClientInstanceName="pnlCombustibile" runat="server">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" Text="Combustibile" Font-Bold="true"></dx:ASPxLabel><br />
                            <dx:ASPxTextBox ID="txtCombustibile" ClientInstanceName="txtCombustibile" runat="server" Width="380px"
                                OnInit="txtCombustibile_Init"
                                Value='<%# Bind("Combustibile") %>'>
                                <ClientSideEvents />
                            </dx:ASPxTextBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn FieldName="NumCircuiti" VisibleIndex="19" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="3" />
            <EditFormSettings Caption="circuiti n." />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="0" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il numero di circuiti" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="CoefficienteRaffrescamento" VisibleIndex="20" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Raffrescamento: EER (o GUE)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il coefficiente di raffrescamento" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaFrigoriferaNominaleKw" VisibleIndex="20" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Potenza frigorifera nominale (Kw)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza frigorifera nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaFrigoriferaAssorbitaNominaleKw" VisibleIndex="20" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Potenza assorbita nominale (Kw)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza assorbita nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="CoefficienteRiscaldamento" VisibleIndex="30" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Riscaldamento: COP (o h)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il coefficiente di riscaldamento" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PortataTermicaNominaleKw" VisibleIndex="30" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Potenza termica nominale (Kw)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza termica nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataSpinEditColumn FieldName="PotenzaTermicaAssorbitaNominaleKw" VisibleIndex="30" Visible="false">
            <EditFormSettings Visible="True" CaptionLocation="Top" />
            <EditFormSettings Caption="Potenza assorbita nominale (Kw)" />
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999" DecimalPlaces="2" DisplayFormatString="g" Width="100px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la potenza assorbita nominale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>      
    </Columns>
    <ClientSideEvents CustomButtonClick="function(s, e) {if (confirm ('Confermi di Sostituire il componente?')) {e.processOnServer = true;}}" />
    <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
    <SettingsEditing EditFormColumnCount="3"></SettingsEditing>
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
                KeyFieldName="IDLibrettoImpiantoMacchinaFrigorifera" 
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
                            <b>Sorgente lato esterno</b>: <%# Eval("SYS_SorgenteLatoEsterno.SorgenteLatoEsterno") %><br />
                            <b>Fluido frigorigeno</b>: <%# Eval("FiltroFrigorigeno") %><br />
                            <b>Fluido lato utenze</b>: <%# Eval("SYS_FluidoLatoUtenze.FluidoLatoUtenze") %><br />
                            <b>Tipologia</b>: <%# Eval("SYS_TipologiaMacchineFrigorifere.TipologiaMacchineFrigorifere") %> <%# Eval("Combustibile") %><br />
                            <b>circuiti n°</b>: <%# Eval("NumCircuiti") %><br />
                            <b>Raffrescamento: EER (o GUE)</b>: <%# Eval("CoefficienteRaffrescamento") %> <b>Potenza frigorifera nominale</b>: <%# Eval("PotenzaFrigoriferaNominaleKw") %> (kW) <b>Potenza assorbita nominale</b>: <%# Eval("PotenzaFrigoriferaAssorbitaNominaleKw") %> (kW)<br />
                            <b>Riscaldamento: COP (o h)</b>: <%# Eval("CoefficienteRiscaldamento") %> <b>Potenza termica nominale</b>: <%# Eval("PortataTermicaNominaleKw") %> (kW) <b>Potenza assorbita nominale</b>: <%# Eval("PotenzaTermicaAssorbitaNominaleKw") %> (kW)
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
    EntitySetName="LIM_LibrettiImpiantiMacchineFrigorifere"
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
    EntitySetName="LIM_LibrettiImpiantiMacchineFrigorifere"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.CodiceProgressivo=@CodiceProgressivo and it.fAttivo=false"
    OrderBy="it.DataDismissione">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="CodiceProgressivo" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsTipologiaMacchineFrigorifere" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_TipologiaMacchineFrigorifere" 
    Select="it.[IDTipologiaMacchineFrigorifere], it.[TipologiaMacchineFrigorifere]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>

<ef:EntityDataSource ID="dsSorgenteLatoEsterno" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_SorgenteLatoEsterno" 
    Select="it.[IDSorgenteLatoEsterno], it.[SorgenteLatoEsterno]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>

<ef:EntityDataSource ID="dsFluidoLatoUtenze" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_FluidoLatoUtenze" 
    Select="it.[IDFluidoLatoUtenze], it.[FluidoLatoUtenze]"
    Where="it.fAttivo=true">
</ef:EntityDataSource>