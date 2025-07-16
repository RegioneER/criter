<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_AccertamentiHelper.aspx.cs" Inherits="VER_AccertamentiHelper" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="gridRaccomandazioni" ClientInstanceName="gridRaccomandazioni" runat="server" 
            KeyFieldName="IDTipologiaRaccomandazione" 
            Width="100%" Styles-Header-BackColor="#ffcc3d" 
            EnableCallBacks="False" 
            EnableCallbackCompression="False">
            <Columns>
                <dx:GridViewDataColumn FieldName="IDTipologiaRaccomandazione" VisibleIndex="1" Settings-AllowAutoFilterTextInputTimer="True" Visible="false" />
                <dx:GridViewDataColumn FieldName="Raccomandazione" Caption="Raccomandazioni" VisibleIndex="2" Settings-AllowAutoFilterTextInputTimer="True" />
            </Columns>
            <SettingsSearchPanel Visible="true" ColumnNames="Raccomandazione" AllowTextInputTimer="True" Delay="500" />
            <SettingsBehavior ProcessSelectionChangedOnServer="True" />
            <SettingsPager EnableAdaptivity="true" />
        </dx:ASPxGridView><br />
        <dx:ASPxGridView ID="gridPrescrizioni" ClientInstanceName="gridRaccomandazioni" runat="server" 
            KeyFieldName="IDTipologiaPrescrizione" 
            Width="100%" Styles-Header-BackColor="#ffcc3d" 
            EnableCallBacks="False" 
            EnableCallbackCompression="False">
            <Columns>
                <dx:GridViewDataColumn FieldName="IDTipologiaPrescrizione" VisibleIndex="1" Settings-AllowAutoFilterTextInputTimer="True" Visible="false" />
                <dx:GridViewDataColumn FieldName="Prescrizione" Caption="Prescrizioni" VisibleIndex="2" Settings-AllowAutoFilterTextInputTimer="True" />
            </Columns>
            <SettingsSearchPanel Visible="true" ColumnNames="Prescrizione" AllowTextInputTimer="True" Delay="500" />
            <SettingsBehavior ProcessSelectionChangedOnServer="True" />
            <SettingsPager EnableAdaptivity="true" />
        </dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
