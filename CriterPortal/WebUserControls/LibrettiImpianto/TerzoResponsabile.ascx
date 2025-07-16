<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TerzoResponsabile.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_TerzoResponsabile" %>
<%@ Register assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxGridView ID="grdTerziResponsabili" runat="server" DataSourceID="dsTerziResponsabili" AutoGenerateColumns="False" Width="100%" EnableRowsCache="false"
    KeyFieldName="IDLibrettoImpiantoResponsabili" 
    ClientInstanceName="grdTerziResponsabili" OnCommandButtonInitialize="grdTerziResponsabili_CommandButtonInitialize"
    OnBeforePerformDataSelect="grdTerziResponsabili_BeforePerformDataSelect" 
    OnRowDeleting="grdTerziResponsabili_RowDeleting" 
    OnRowInserting="grdTerziResponsabili_RowInserting" 
    OnRowUpdating="DetailGrid_RowUpdating" OnInitNewRow="grdTerziResponsabili_InitNewRow"
    OnDataBound="DetailGrid_DataBound">
    <Columns>
        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="20" Width="40px">
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn>
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <b>Ragione sociale</b>: <%# Eval("RagioneSociale") %> <%# Eval("PartitaIva", "P.IVA {0}") %><br />
                <b>Legale Rappresentante</b>: <%# Eval("Cognome") %> <%# Eval("Nome") %><br />
                <b>Iscritto alla CCIAA di</b>: <%# Eval("SYS_Province.Provincia") %> <b>al numero</b> <%# Eval("NumeroCciaa") %><br />
                <b>Assunzione incarico dal</b> <%# Eval("DataInizio", "{0:d}") %> <b>al</b>  <%# Eval("DataFine", "{0:d}") %><br />
                <b>Email</b>: <%# Eval("Email", "E-mail {0}") %><br />
                <b>Email Pec</b>: <%# Eval("EmailPec", "Pec {0}") %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Cognome" Caption="Cognome" Visible="false" VisibleIndex="1">
            <EditFormSettings Visible="True" CaptionLocation="Top" ColumnSpan="2" Caption="Cognome legale rappresentante" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il cognome del legale rappresentante" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Nome" Caption="Nome" Visible="false" VisibleIndex="2">
            <EditFormSettings Visible="True" ColumnSpan="2" CaptionLocation="Top" Caption="Nome legale rappresentante" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire il nome del legale rappresentante" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="RagioneSociale" Caption="RagioneSociale" Visible="false" VisibleIndex="3">
            <EditFormSettings Caption="Ragione sociale" ColumnSpan="2" Visible="True" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la ragione sociale" IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PartitaIva" Caption="PartitaIva" Visible="false" VisibleIndex="4">
            <EditFormSettings Caption="P.IVA" Visible="True" ColumnSpan="2" CaptionLocation="Top" />
            <PropertiesTextEdit Width="280px"></PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Prov CCIAA" FieldName="IDProvinciaCciaa" Visible="false" VisibleIndex="10">
            <PropertiesComboBox DataSourceID="dsProvince" TextField="Provincia" ValueField="IDProvincia" ValueType="System.Int32" Width="380px">
            </PropertiesComboBox>
            <EditFormSettings Caption="Iscritto alla CCIAA di"  Visible="True" ColumnSpan="2" CaptionLocation="Top" />
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataSpinEditColumn Caption="Numero CCIAA" FieldName="NumeroCciaa" Visible="false" VisibleIndex="11">
            <PropertiesSpinEdit MinValue="0" MaxValue="99999999999" Width="150px">
            </PropertiesSpinEdit>
            <EditFormSettings Caption="Al numero"  Visible="True" ColumnSpan="2" CaptionLocation="Top" />
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataDateColumn Caption="Dal" FieldName="DataInizio" Visible="false" VisibleIndex="12">
            <PropertiesDateEdit DisplayFormatString="" Width="140px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la data di inzio responsabilità" IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
            <EditFormSettings Caption="Data inizio assunzione incarico"  Visible="True" ColumnSpan="2" CaptionLocation="Top" />
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="Al" FieldName="DataFine" Visible="false" VisibleIndex="13">
            <PropertiesDateEdit DisplayFormatString="" Width="140px">
                <ValidationSettings CausesValidation="True">
                    <RequiredField ErrorText="Inserire la data di fine responsabilità" IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
            <EditFormSettings Caption="Data fine assunzione incarico" Visible="True" ColumnSpan="2" CaptionLocation="Top" />
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" Visible="false" VisibleIndex="14">
            <EditFormSettings Visible="True" ColumnSpan="2" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RegularExpression ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorText="L'indirizzo Email inserito non è corretto" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="EmailPec" Caption="Pec" Visible="false" VisibleIndex="15">
            <EditFormSettings Visible="True" ColumnSpan="2" CaptionLocation="Top" />
            <PropertiesTextEdit Width="360px">
                <ValidationSettings CausesValidation="True">
                    <RegularExpression ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorText="L'indirizzo Pec inserito non è corretto" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
    </Columns>
    <%--<ClientSideEvents RowClick="function(s,e) { s.StartEditRow(e.visibleIndex); }" />--%>
    <SettingsEditing EditFormColumnCount="4"></SettingsEditing>
    <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="false" />
    <Styles><RowHotTrack Cursor="pointer"></RowHotTrack></Styles>
</dx:ASPxGridView>

<ef:EntityDataSource ID="dsTerziResponsabili" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EnableInsert="True" 
    EnableUpdate="True" 
    EnableDelete="True"
    EntitySetName="LIM_LibrettiImpiantiResponsabili"
    AutoGenerateWhereClause="false"
    Where="it.IDLibrettoImpianto=@IDLibrettoImpianto and it.fAttivo=true">
    <WhereParameters>
        <asp:Parameter Name="IDLibrettoImpianto" Type="Int32" DefaultValue="0" />
    </WhereParameters>
</ef:EntityDataSource>
<ef:EntityDataSource ID="dsProvince" runat="server" 
    ConnectionString="name=CriterDataModel" 
    ContextTypeName="DataLayer.CriterDataModel" 
    DefaultContainerName="CriterDataModel" EnableFlattening="False" 
    EntitySetName="SYS_Province" 
    Select="it.[IDProvincia], it.[SiglaProvincia], it.[Provincia]"
    AutoGenerateWhereClause="false"
    Where="it.fAttivo=true">
</ef:EntityDataSource>
