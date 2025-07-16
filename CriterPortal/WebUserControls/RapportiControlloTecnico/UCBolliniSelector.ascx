<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCBolliniSelector.ascx.cs" Inherits="RCT_UC_UCBolliniSelector" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<dx:ASPxGridView ID="gridSelezioneManuale" ClientInstanceName="grid" runat="server" OnSelectionChanged="gridSelezioneManuale_SelectionChanged" 
    KeyFieldName="IDBollinoCalorePulito" 
    Width="100%" 
    Border-BorderStyle="None" 
    BorderBottom-BorderStyle="None" 
    BorderLeft-BorderStyle="None" 
    BorderRight-BorderStyle="None" 
    BorderTop-BorderStyle="None" 
    EnableCallBacks="False" EnableCallbackCompression="False">
    <Columns>
        <dx:GridViewDataColumn FieldName="IDBollinoCalorePulito" VisibleIndex="1" Settings-AllowAutoFilterTextInputTimer="True" Visible="false" />
        <dx:GridViewDataColumn FieldName="CodiceBollino" VisibleIndex="2" Settings-AllowAutoFilterTextInputTimer="True" />
        <dx:GridViewDataTextColumn FieldName="CostoBollino" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <PropertiesTextEdit DisplayFormatString="{0:N2} €" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Caption=""></dx:GridViewCommandColumn>
    </Columns>
    <SettingsSearchPanel Visible="true" ColumnNames="CodiceBollino" AllowTextInputTimer="True" Delay="500" />
    <SettingsBehavior ProcessSelectionChangedOnServer="True" />
    <SettingsPager EnableAdaptivity="true" />
</dx:ASPxGridView>
<asp:Label runat="server" ID="lblImportoTotaleSelezionati" Font-Bold="true" ForeColor="Green" />
<asp:Label runat="server" ID="lblIDSoggetto" Visible="false" />
<asp:Label runat="server" ID="lblIDSoggettoDerived" Visible="false" />